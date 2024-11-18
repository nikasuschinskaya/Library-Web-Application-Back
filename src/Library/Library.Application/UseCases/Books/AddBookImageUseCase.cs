using Library.Application.Interfaces.UseCases.Books;
using Library.Domain.Exceptions;
using Library.Domain.Interfaces;

namespace Library.Application.UseCases.Books;

public class AddBookImageUseCase : IAddBookImageUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public AddBookImageUseCase(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task ExecuteAsync(Guid bookId, string imageURL, CancellationToken cancellationToken = default)
    {
        var book = await _unitOfWork.Books.GetByIdAsync(bookId, cancellationToken)
            ?? throw new EntityNotFoundException(bookId);

        if (string.IsNullOrEmpty(imageURL))
            throw new Exception("Image URL is empty.");

        book.ImageURL = imageURL;

        _unitOfWork.Books.Update(book);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
