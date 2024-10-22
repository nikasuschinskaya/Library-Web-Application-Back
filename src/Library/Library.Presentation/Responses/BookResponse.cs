using Library.Domain.Entities;

namespace Library.Presentation.Responses;

public record class BookResponse(
    string Name,
    string ISBN,
    string Genre,
    string Description,
    string? ImageURL,
    string BookStockStatus,
    IReadOnlyCollection<Author> Authors);
