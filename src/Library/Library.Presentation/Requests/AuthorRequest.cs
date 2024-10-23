namespace Library.Presentation.Requests;

public record class AuthorRequest(
    string Name,
    string Surname,
    DateTime BirthDate,
    string Country);