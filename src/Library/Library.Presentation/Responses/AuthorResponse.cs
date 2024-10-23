namespace Library.Presentation.Responses;

public record class AuthorResponse(
    Guid Id,
    string Name,
    string Surname,
    DateTime BirthDate,
    string Country,
    IReadOnlyCollection<BookResponse> Books);
