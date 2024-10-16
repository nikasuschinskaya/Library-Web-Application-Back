using Library.Domain.Entities.Base;

namespace Library.Domain.Entities;

public class Book : NamedEntity
{
    public string IBSN { get; set; }
    public string Genre { get; set; }
    public string Description { get; set; }
    public string? ImagePath { get; set; }
    public virtual List<Author> Authors { get; set; }
    public virtual List<UserBook> UserBooks { get; set; } = [];

    public Book() { }

    public Book(string name, string iSBN, string genre, string description, List<Author> authors)
       : this(Guid.NewGuid(), name, iSBN, genre, description, string.Empty, authors, []) { }

    public Book(Guid id,
                string name,
                string iBSN,
                string genre,
                string description,
                string? imagePath,
                List<Author> authors,
                List<UserBook> userBooks)
    {
        Id = id;
        Name = name;
        IBSN = iBSN;
        Genre = genre;
        Description = description;
        ImagePath = imagePath;
        Authors = authors;
        UserBooks = userBooks;
    }
}
