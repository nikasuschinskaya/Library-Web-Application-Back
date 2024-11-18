using Library.Domain.Entities;
using Library.Domain.Models;

namespace Library.Application.Interfaces.UseCases.Books;

public interface IGetBooksPagedUseCase : IUseCase
{
    Task<PagedList<Book>> ExecuteAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
}