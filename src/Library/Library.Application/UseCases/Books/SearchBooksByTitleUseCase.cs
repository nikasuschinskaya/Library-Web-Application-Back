using Library.Application.Interfaces.UseCases.Books;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Domain.Specifications.Books;

namespace Library.Application.UseCases.Books;

public class SearchBooksByTitleUseCase : ISearchBooksByTitleUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public SearchBooksByTitleUseCase(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IEnumerable<Book>> ExecuteAsync(string title, CancellationToken cancellationToken = default)
    {
        var spec = new BooksByTitleSpecification(title);
        var books = await _unitOfWork.Books.ListAsync(spec, cancellationToken);
        return books;
    }
}
