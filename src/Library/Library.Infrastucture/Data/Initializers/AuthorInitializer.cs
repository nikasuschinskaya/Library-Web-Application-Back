using Library.Domain.Entities;
using Library.Infrastucture.Data.Initializers.Base;

namespace Library.Infrastucture.Data.Initializers;

public class AuthorInitializer : BaseInitializer<Author>
{
    public AuthorInitializer() : base(
    [
        new Author("Кадзуо", "Исигуро", new DateTime(1954, 11, 8), "Япония"),
        new Author("Лев", "Толстой", new DateTime(1828, 9, 9), "Россия"),
        new Author("Стивен", "Кинг", new DateTime(1947, 9, 21), "США"),
        new Author("Рэй", "Брэдбери", new DateTime(1920, 8, 22), "США"),
        new Author("Михаил", "Булкагов", new DateTime(1891, 3, 15), "Россия")
    ])
    {
    }
}
