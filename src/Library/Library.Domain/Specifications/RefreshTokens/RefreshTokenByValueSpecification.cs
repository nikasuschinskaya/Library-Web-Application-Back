using Ardalis.Specification;
using Library.Domain.Entities;

namespace Library.Domain.Specifications.RefreshTokens;

public class RefreshTokenByValueSpecification : Specification<RefreshToken>
{
    public RefreshTokenByValueSpecification(string token)
    {
        Query.Where(t => t.Token == token);
    }
}
