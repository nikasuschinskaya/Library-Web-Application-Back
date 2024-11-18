using Library.Application.Interfaces.UseCases.Books;
using Library.Domain.Entities;
using Library.Domain.Exceptions;
using Library.Domain.Interfaces;

namespace Library.Application.UseCases.Books;

public class TakeBookUseCase : ITakeBookUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public TakeBookUseCase(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task ExecuteAsync(Guid bookId, Guid userId, CancellationToken cancellationToken = default)
    {
        var book = await _unitOfWork.Books.GetByIdAsync(bookId, cancellationToken)
            ?? throw new EntityNotFoundException($"Book with {bookId} not found.");

        _ = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken)
           ?? throw new EntityNotFoundException($"User with {userId} not found.");

        var userBook = new UserBook(userId, bookId);
        _unitOfWork.UserBooks.Create(userBook);

        book.Count--;

        _unitOfWork.Books.Update(book);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
