using Library.Domain.Entities.Base;

namespace Library.Domain.Entities;

public class RefreshToken : BaseEntity
{
    public string Token { get; set; }

    public Guid UserId { get; set; }
    public virtual User User { get; set; }

    public DateTime ExpiryDate { get; set; }
    public bool IsRevoked { get; set; }
}
