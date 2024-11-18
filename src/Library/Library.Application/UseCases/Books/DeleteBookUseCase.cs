using Library.Application.Interfaces.UseCases.Books;
using Library.Domain.Exceptions;
using Library.Domain.Interfaces;

namespace Library.Application.UseCases.Books;

public class DeleteBookUseCase : IDeleteBookUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBookUseCase(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task ExecuteAsync(Guid bookId, CancellationToken cancellationToken = default)
    {
        var book = await _unitOfWork.Books.GetByIdAsync(bookId, cancellationToken)
            ?? throw new EntityNotFoundException($"Book with ID {bookId} was not found.");

        _unitOfWork.Books.Delete(book);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
