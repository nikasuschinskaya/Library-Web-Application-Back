using Library.Application.Interfaces;
using Library.Application.Services.Base;
using Library.Domain.Entities;
using Library.Domain.Exceptions;

namespace Library.Application.Services;

public class BookService : IBookService
{
    private readonly IUnitOfWork _unitOfWork;

    public BookService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task AddBookAsync(Book book, CancellationToken cancellationToken = default)
    {
        _unitOfWork.Repository<Book>().Create(book);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public Task AddBookImageAsync(string imageURL, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteBookAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var book = await GetBookByIdAsync(id, cancellationToken) ?? throw new EntityNotFoundException();

        _unitOfWork.Repository<Book>().Delete(book);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync(CancellationToken cancellationToken = default) => 
        await _unitOfWork.Repository<Book>().GetAll(cancellationToken);

    public async Task<Book?> GetBookByIdAsync(Guid id, CancellationToken cancellationToken = default) => 
        await _unitOfWork.Repository<Book>().GetByIdAsync(id, cancellationToken);

    public async Task<Book?> GetBookByISBNAsync(string iSBN, CancellationToken cancellationToken = default)
    {
        var books = await GetAllBooksAsync(cancellationToken);

        return books.FirstOrDefault(book => book.IBSN == iSBN) ?? throw new EntityNotFoundException();
    }

    public Task GiveBookToUserAsync(Guid bookId, Guid userId, CancellationToken cancellationToken = default)
    {

        throw new NotImplementedException();
    }

    public async Task UpdateBookAsync(Book book, CancellationToken cancellationToken = default)
    {
        _unitOfWork.Repository<Book>().Update(book);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }
}
