namespace Library.Presentation.Requests;

public record class AuthorRequest(
    Guid Id,
    string Name,
    string Surname,
    DateTime BirthDate,
    string Country);