namespace Library.Presentation.Requests;

public record class UserBookRequest(
    Guid UsedId,
    Guid BookId);