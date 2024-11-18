using Library.Domain.Entities.Base;
using System.Text.Json.Serialization;

namespace Library.Domain.Entities;

public class Genre : NamedEntity
{
    [JsonIgnore]
    public virtual List<Book> Books { get; set; }

    public Genre() { }

    public Genre(string name) : this(Guid.NewGuid(), name, []) { }

    public Genre(Guid id, string name, List<Book> books)
    {
        Id = id;
        Name = name;
        Books = books;
    }
}
