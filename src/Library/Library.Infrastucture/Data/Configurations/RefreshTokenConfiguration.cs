using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastucture.Data.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId).IsRequired();

        builder.Property(x => x.Token).IsRequired();

        builder.Property(x => x.ExpiryDate).IsRequired();

        builder.Property(x => x.IsRevoked).IsRequired();
    }
}
