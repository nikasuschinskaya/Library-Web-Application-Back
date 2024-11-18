using Library.Application.Interfaces.UseCases.Books;
using Library.Domain.Enums;
using Library.Domain.Exceptions;
using Library.Domain.Interfaces;
using Library.Domain.Specifications.Books;

namespace Library.Application.UseCases.Books;

public class ReturnBookUseCase : IReturnBookUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public ReturnBookUseCase(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task ExecuteAsync(Guid bookId, Guid userId, CancellationToken cancellationToken = default)
    {
        var userBookSpec = new ActiveUserBookSpecification(userId, bookId);
        var userBook = await _unitOfWork.UserBooks.GetBySpecAsync(userBookSpec, cancellationToken)
            ?? throw new EntityNotFoundException($"Not found book with {bookId} at user with {userId}.");

        if (userBook.Status != UserBookStatus.Taken)
            throw new Exception("No active borrow record found.");

        userBook.Status = UserBookStatus.Returned;

        var book = await _unitOfWork.Books.GetByIdAsync(bookId, cancellationToken)
            ?? throw new EntityNotFoundException($"Not found book with {bookId}.");

        book.Count++;

        _unitOfWork.Books.Update(book);
        _unitOfWork.UserBooks.Update(userBook);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
