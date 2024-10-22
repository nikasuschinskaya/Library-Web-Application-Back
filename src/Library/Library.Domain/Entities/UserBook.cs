using Library.Domain.Entities.Base;
using Library.Domain.Enums;

namespace Library.Domain.Entities;

public class UserBook : BaseEntity
{
    public Guid UserId { get; set; }
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
