using Library.Application.Interfaces;
using Library.Application.Services.Base;
using Library.Domain.Entities;
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

        _unitOfWork.Repository<Book>().Create(book);
        await _unitOfWork.CompleteAsync(cancellationToken);
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
        return await _unitOfWork.Repository<Book>().GetAll().ToListAsync(cancellationToken);
    }

    public async Task<Book?> GetBookByIdAsync(Guid id, CancellationToken cancellationToken = default) => 
        await _unitOfWork.Repository<Book>().GetByIdAsync(id, cancellationToken);

    public async Task<Book?> GetBookByISBNAsync(string iSBN, CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.Repository<Book>().GetAll()
            .FirstOrDefaultAsync(book => book.ISBN == iSBN, cancellationToken);
    }

    public async Task GiveBookToUserAsync(Guid bookId, Guid userId, CancellationToken cancellationToken = default)
    {
        var userBook = new UserBook(userId, bookId);
        _unitOfWork.Repository<UserBook>().Create(userBook);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task UpdateBookAsync(Book book, CancellationToken cancellationToken = default)
    {
        _unitOfWork.Repository<Book>().Update(book);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }
}
