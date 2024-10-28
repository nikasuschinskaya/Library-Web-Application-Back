using Library.Domain.Entities;

namespace Library.Presentation.Requests;

public record class BookRequest(
    string Name,
    string ISBN,
    string Description,
    Guid GenreId,
    int Count,
    IEnumerable<AuthorRequest> Authors);