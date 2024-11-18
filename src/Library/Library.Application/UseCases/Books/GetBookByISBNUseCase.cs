using Library.Application.Interfaces.UseCases.Books;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Domain.Specifications.Books;

namespace Library.Application.UseCases.Books;

public class GetBookByISBNUseCase : IGetBookByISBNUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetBookByISBNUseCase(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Book?> ExecuteAsync(string iSBN, CancellationToken cancellationToken = default) =>
        await _unitOfWork.Books.GetBySpecAsync(new BookByISBNSpecification(iSBN), cancellationToken);
}
