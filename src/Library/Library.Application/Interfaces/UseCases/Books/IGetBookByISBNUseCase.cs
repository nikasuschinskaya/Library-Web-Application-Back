using Library.Domain.Entities;

namespace Library.Application.Interfaces.UseCases.Books;

public interface IGetBookByISBNUseCase : IUseCase
{
    Task<Book?> ExecuteAsync(string iSBN, CancellationToken cancellationToken = default);
}