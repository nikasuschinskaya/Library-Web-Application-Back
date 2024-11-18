using Library.Application.Interfaces.UseCases.Books;
using Library.Domain.Entities;
using Library.Domain.Interfaces;

namespace Library.Application.UseCases.Books;

public class GetBookByIdUseCase : IGetBookByIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetBookByIdUseCase(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Book?> ExecuteAsync(Guid bookId, CancellationToken cancellationToken = default) =>
        await _unitOfWork.Books.GetByIdAsync(bookId, cancellationToken);
}
