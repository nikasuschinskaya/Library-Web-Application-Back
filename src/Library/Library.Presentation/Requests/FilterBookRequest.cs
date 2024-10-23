namespace Library.Presentation.Requests;

public record class FilterBookRequest(
    string? Genre = null, 
    string? AuthorName = null);
