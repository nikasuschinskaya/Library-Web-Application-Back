using Library.Domain.Entities;

namespace Library.Presentation.Requests;

public record class BookRequest(
    string Name,
    string ISBN,
    string Description,
    string Genre,
    int Count,
    List<Author> Authors);