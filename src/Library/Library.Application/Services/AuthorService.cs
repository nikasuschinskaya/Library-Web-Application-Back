using Library.Application.Interfaces;
using Library.Application.Services.Base;
using Library.Domain.Entities;
using Library.Domain.Exceptions;

namespace Library.Application.Services;

public class AuthorService : IAuthorService
{
    private readonly IUnitOfWork _unitOfWork;

    public AuthorService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task AddAuthorAsync(Author author, CancellationToken cancellationToken = default)
    {
        var existingAuthors = await GetAllAuthorsAsync(cancellationToken);

        var existingAuthor = existingAuthors.FirstOrDefault(a =>
            a.Name == author.Name &&
            a.Surname == author.Surname &&
            a.BirthDate == author.BirthDate &&
            a.Country == author.Country);

        if (existingAuthor != null) throw new EntityDublicateException();

        _unitOfWork.Repository<Author>().Create(author); 
        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task DeleteAuthorAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var author = await GetAuthorByIdAsync(id, cancellationToken) ?? throw new EntityNotFoundException();
        _unitOfWork.Repository<Author>().Delete(author);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public Task<IEnumerable<Author>> GetAllAuthorsAsync(CancellationToken cancellationToken = default) => 
        _unitOfWork.Repository<Author>().GetAll(cancellationToken);

    public Task<Author?> GetAuthorByIdAsync(Guid id, CancellationToken cancellationToken = default) => 
        _unitOfWork.Repository<Author>().GetByIdAsync(id, cancellationToken);

    public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(Guid authorId, CancellationToken cancellationToken = default)
    {
        var books = await _unitOfWork.Repository<Book>().GetAll(cancellationToken);

        var booksByAuthor = books.Where(book => book.Authors.Any(author => author.Id == authorId));

        return booksByAuthor;
    }

    public async Task UpdateAuthorAsync(Author author, CancellationToken cancellationToken = default)
    {
        _unitOfWork.Repository<Author>().Update(author);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }
}
