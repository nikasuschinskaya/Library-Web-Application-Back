using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastucture.Data.Configurations;

public class UserBookConfiguration : IEntityTypeConfiguration<UserBook>
{
    public void Configure(EntityTypeBuilder<UserBook> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId).IsRequired();

        builder.Property(x => x.BookId).IsRequired();

        builder.Property(x => x.DateTaken).IsRequired();

        builder.Property(x => x.ReturnDate).IsRequired();

        builder.Property(x => x.Status).IsRequired();
    }
}
