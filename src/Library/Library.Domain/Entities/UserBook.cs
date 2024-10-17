﻿using Library.Domain.Entities.Base;
using Library.Domain.Enums;

namespace Library.Domain.Entities
{
    public class UserBook : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Guid BookId { get; set; }
        public virtual Book Book { get; set; }

        public DateTime DateTaken { get; set; }
        public DateTime ReturnDate { get; set; }

        public BookStatus Status { get; set; }

        public UserBook() { }

        public UserBook(Guid userId, Guid bookId, DateTime returnDate)
        {
            UserId = userId;
            BookId = bookId;
            ReturnDate = returnDate;

            DateTaken = DateTime.UtcNow;
            Status = BookStatus.Taken;
        }

        public void ReturnBook()
        {
            if (Status != BookStatus.Returned)
            {
                Status = BookStatus.Returned;
            }
        }
    }
}