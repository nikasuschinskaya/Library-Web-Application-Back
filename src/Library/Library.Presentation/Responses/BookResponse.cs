using Library.Domain.Entities;

namespace Library.Presentation.Responses;

public record class BookResponse(
    Guid Id,
    string Name,
    string ISBN,
    Guid GenreId,
    string Description,
    string? ImageURL,
    int Count,
    string BookStockStatus,
    IReadOnlyCollection<Author> Authors);
