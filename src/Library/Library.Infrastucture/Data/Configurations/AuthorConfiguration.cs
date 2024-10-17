using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastucture.Data.Configurations;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(25);
        
        builder.Property(x => x.Surname).IsRequired().HasMaxLength(40);
        
        builder.Property(x => x.Country).IsRequired().HasMaxLength(45);

        builder.Property(x => x.BirthDate).IsRequired();
    }
}
