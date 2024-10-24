using Library.Domain.Entities;

namespace Library.Presentation.Responses;

public record class BookListResponse(
    //Guid Id,
    string Name,
    string ISBN,
    //Guid GenreId,
    string Genre,
    string? ImageURL,
    string BookStockStatus,
    IReadOnlyCollection<Author> Authors);