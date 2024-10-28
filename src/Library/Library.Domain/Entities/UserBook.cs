using Library.Domain.Entities.Base;
using Library.Domain.Enums;
using System.Text.Json.Serialization;

namespace Library.Domain.Entities;

/// <summary>
/// The book taken by user
/// </summary>
/// <seealso cref="Library.Domain.Entities.Base.BaseEntity" />
public class UserBook : BaseEntity
{
    public Guid UserId { get; set; }

    [JsonIgnore]
    public virtual User User { get; set; }

    public Guid BookId { get; set; }

    public virtual Book Book { get; set; }

    public DateTime DateTaken { get; set; }
    public DateTime ReturnDate { get; set; }

    public UserBookStatus Status { get; set; }

    public UserBook() { }

    public UserBook(Guid userId, Guid bookId)
    {
        UserId = userId;
        BookId = bookId;

        ReturnDate = DateTime.UtcNow.AddDays(10);
        DateTaken = DateTime.UtcNow;
        Status = UserBookStatus.Taken;
    }
}
