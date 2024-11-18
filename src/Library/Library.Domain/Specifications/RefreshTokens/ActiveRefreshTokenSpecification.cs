using Ardalis.Specification;
using Library.Domain.Entities;

namespace Library.Domain.Specifications.RefreshTokens;

public class ActiveRefreshTokenSpecification : Specification<RefreshToken>
{
    public ActiveRefreshTokenSpecification(Guid userId)
    {
        Query.Where(rt => rt.UserId == userId && rt.ExpiryDate > DateTime.UtcNow && !rt.IsRevoked);
    }
}