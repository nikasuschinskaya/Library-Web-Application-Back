using Library.Application.Interfaces.UseCases.Books;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Domain.Specifications.Books;

namespace Library.Application.UseCases.Books;

public class FilterBooksUseCase : IFilterBooksUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public FilterBooksUseCase(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IEnumerable<Book>> ExecuteAsync(string? genre, string? authorName, CancellationToken cancellationToken = default)
    {
        var spec = new BooksByGenreAndAuthorSpecification(genre, authorName);
        var books = await _unitOfWork.Books.ListAsync(spec, cancellationToken);
        return books;
    }
}
