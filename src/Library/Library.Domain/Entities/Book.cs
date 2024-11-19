using Library.Domain.Entities.Base;
using Library.Domain.Enums;
using System.Text.Json.Serialization;

namespace Library.Domain.Entities;

public class Book : NamedEntity
{
    public string ISBN { get; set; }

    public Guid GenreId { get; set; }
    public virtual Genre Genre { get; set; }

    public string Description { get; set; }
    public string? ImageURL { get; set; }
    public int Count { get; set; }
    public virtual List<Author> Authors { get; set; } = [];

    [JsonIgnore]
    public virtual List<UserBook> UserBooks { get; set; } = [];
    public BookStockStatus BookStockStatus => Count > 0 ? BookStockStatus.InStock : BookStockStatus.NotInStock;

    public Book() { }

    public Book(string name, string iSBN, Genre genre, string description, int count, List<Author> authors, string? imageURL = null)
       : this(Guid.NewGuid(), name, iSBN, genre, description, imageURL, count, authors, []) { }

    public Book(Guid id,
                string name,
                string iSBN,
                Genre genre,
                string description,
                string? imageURL,
                int count,
                List<Author> authors,
                List<UserBook> userBooks)
    {
        Id = id;
        Name = name;
        ISBN = iSBN;
        Genre = genre;
        Description = description;
        ImageURL = imageURL;
        Count = count;
        Authors = authors;
        UserBooks = userBooks;
    }
}
