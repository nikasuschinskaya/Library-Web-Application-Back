using Library.Domain.Entities;

namespace Library.Presentation.Responses;

public record class BookResponse(
    string Name,
    string ISBN,
    Guid GenreId,
    string Description,
    string? ImageURL,
    string BookStockStatus,
    IReadOnlyCollection<Author> Authors);
