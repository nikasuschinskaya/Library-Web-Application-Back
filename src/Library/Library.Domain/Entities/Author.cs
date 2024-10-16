using Library.Domain.Entities.Base;

namespace Library.Domain.Entities;

public class Author : NamedEntity
{
    public string Surname { get; set; }
    public DateTime BirthDate { get; set; }
    public string Country { get; set; }

    public virtual List<Book> Books { get; set; }

    public Author() { }

    public Author(string name, string surname, DateTime birthDate, string country) : 
        this(Guid.NewGuid(), name, surname, birthDate, country, []) { }

    public Author(Guid id,
                  string name,
                  string surname,
                  DateTime birthDate,
                  string country,
                  List<Book> books)
    {
        Id = id; 
        Name = name;
        Surname = surname;
        BirthDate = birthDate;
        Country = country;
        Books = books;
    }
}
