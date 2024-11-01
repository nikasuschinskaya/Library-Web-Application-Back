﻿using Library.Application.Extensions;
using Library.Application.Interfaces.Common;
using Library.Application.Interfaces.Services;
using Library.Application.Models;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Services;

public class BookService : IBookService
{
    private readonly IUnitOfWork _unitOfWork;

    public BookService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task AddBookAsync(Book book, CancellationToken cancellationToken = default)
    {
        var existingBook = await _unitOfWork.Repository<Book>().GetAll()
            .AnyAsync(b => b.ISBN == book.ISBN,
            cancellationToken);

        if (existingBook) throw new EntityDublicateException();


        var authors = new List<Author>(book.Authors);
        book.Authors.Clear();
        _unitOfWork.Repository<Book>().Create(book);

        await _unitOfWork.CompleteAsync(cancellationToken);

        foreach (var author in authors)
        {
            var exAuthor = await _unitOfWork.Repository<Author>().GetByIdAsync(author.Id, cancellationToken);
            var exBook = await _unitOfWork.Repository<Book>().GetByIdAsync(book.Id, cancellationToken);
            exAuthor.Books.Add(exBook);
            await _unitOfWork.CompleteAsync();
        }
    }

    public async Task AddBookImageAsync(Guid id, string imageURL, CancellationToken cancellationToken = default)
    {
        var book = await _unitOfWork.Repository<Book>()
            .GetByIdAsync(id, cancellationToken) 
            ?? throw new EntityNotFoundException(id);

        book.ImageURL = imageURL;

        _unitOfWork.Repository<Book>().Update(book);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task DeleteBookAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var book = await GetBookByIdAsync(id, cancellationToken) ?? throw new EntityNotFoundException();

        _unitOfWork.Repository<Book>().Delete(book);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync(CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.Repository<Book>().GetAll()
            .Include(b => b.Authors)
            .ToListAsync(cancellationToken);
    }

    public async Task<PagedList<Book>> GetBooksPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var booksQuery = _unitOfWork.Repository<Book>().GetAll()
             .Include(b => b.Authors);

        return await booksQuery.ToPagedListAsync(pageNumber, pageSize, cancellationToken);
    }

    public async Task<IEnumerable<Book>> SearchBooksByTitleAsync(string title, CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.Repository<Book>()
            .GetAll()
            .Where(book => book.Name.Contains(title))
            .Include(b => b.Authors)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Book>> FilterBooksAsync(string? genre, string? authorName, CancellationToken cancellationToken = default)
    {
        var query = _unitOfWork.Repository<Book>().GetAll()
            .Include(b => b.Genre)
            .Include(b => b.Authors)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(genre))
        {
            string genreLower = genre.ToLower();  
            query = query.Where(book => book.Genre.Name.ToLower().Equals(genreLower));
        }

        if (!string.IsNullOrWhiteSpace(authorName))
        {
            string authorNameLower = authorName.ToLower();  
            query = query.Where(book => book.Authors.Any(a => a.Surname.ToLower().Contains(authorNameLower)));
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<Book?> GetBookByIdAsync(Guid id, CancellationToken cancellationToken = default) => 
        await _unitOfWork.Repository<Book>().GetByIdAsync(id, cancellationToken);

    public async Task<Book?> GetBookByISBNAsync(string iSBN, CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.Repository<Book>().GetAll()
            .FirstOrDefaultAsync(book => book.ISBN == iSBN, cancellationToken);
    }

    public async Task TakeBookAsync(Guid bookId, Guid userId, CancellationToken cancellationToken = default)
    {
        var book = await _unitOfWork.Repository<Book>().GetByIdAsync(bookId, cancellationToken) 
            ?? throw new Exception("Book not found.");

        if (book.Count <= 0)
            throw new Exception("Book is not available.");

        var userBook = new UserBook(userId, bookId);
        _unitOfWork.Repository<UserBook>().Create(userBook);

        book.Count--;

        _unitOfWork.Repository<Book>().Update(book);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task ReturnBookAsync(Guid bookId, Guid userId, CancellationToken cancellationToken = default)
    {
        var userBook = await _unitOfWork.Repository<UserBook>().GetAll()
           .FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BookId == bookId, cancellationToken);

        if (userBook == null || userBook.Status != UserBookStatus.Taken)
            throw new Exception("No active borrow record found.");

        userBook.Status = UserBookStatus.Returned;
        var book = await _unitOfWork.Repository<Book>().GetByIdAsync(bookId, cancellationToken);
        book.Count++;

        _unitOfWork.Repository<Book>().Update(book);
        _unitOfWork.Repository<UserBook>().Update(userBook);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task UpdateBookAsync(Guid id, Book updatedBook, CancellationToken cancellationToken = default)
    {
        var existingBook = await _unitOfWork.Repository<Book>().GetByIdAsync(id, cancellationToken)
            ?? throw new EntityNotFoundException(id);

        existingBook.Name = updatedBook.Name;
        existingBook.ISBN = updatedBook.ISBN;
        existingBook.GenreId = updatedBook.GenreId;
        existingBook.Description = updatedBook.Description;
        existingBook.Count = updatedBook.Count;
        existingBook.Authors = updatedBook.Authors;

        await _unitOfWork.CompleteAsync(cancellationToken);
    }
}
