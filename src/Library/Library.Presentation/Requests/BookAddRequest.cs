namespace Library.Presentation.Requests;

public record class BookAddRequest(
    string Name,
    string ISBN,
    string Description,
    string? ImageURL,
    Guid GenreId,
    int Count,
    IEnumerable<Guid> AuthorIds);