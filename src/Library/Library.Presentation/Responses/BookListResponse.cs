using Library.Domain.Entities;

namespace Library.Presentation.Responses;

public record class BookListResponse(
    string Name,
    string ISBN,
    string Genre,
    string? ImageURL,
    string BookStockStatus,
    IReadOnlyCollection<Author> Authors);