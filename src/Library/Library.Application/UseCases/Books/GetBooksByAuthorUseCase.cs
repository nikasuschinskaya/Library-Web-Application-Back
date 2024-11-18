using Library.Application.Interfaces.UseCases.Books;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Domain.Specifications.Books;

namespace Library.Application.UseCases.Books;

public class GetBooksByAuthorUseCase : IGetBooksByAuthorUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetBooksByAuthorUseCase(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IEnumerable<Book>> ExecuteAsync(Guid authorId, CancellationToken cancellationToken = default)
    {
        var spec = new BooksByAuthorSpecification(authorId);
        return await _unitOfWork.Books.ListAsync(spec, cancellationToken);
    }
}
