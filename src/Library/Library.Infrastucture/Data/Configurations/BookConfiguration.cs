using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastucture.Data.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

        builder.Property(x => x.ISBN).IsRequired().HasMaxLength(20);

        builder.Property(x => x.Genre).IsRequired().HasMaxLength(25);

        builder.Property(x => x.Description).IsRequired().HasMaxLength(300);
    }
}
