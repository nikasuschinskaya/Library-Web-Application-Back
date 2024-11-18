using Ardalis.Specification;
using Library.Domain.Entities;

namespace Library.Domain.Specifications.Authors;

public class AuthorByDetailsSpecification : Specification<Author>
{
    public AuthorByDetailsSpecification(string name, string surname, DateTime birthDate)
    {
        Query.Where(a => a.Name == name && a.Surname == surname && a.BirthDate == birthDate);
    }
}
