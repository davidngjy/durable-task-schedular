using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedNever()
            .HasConversion(
                x => x.Value,
                x => new UserId(x)
            );

        builder
            .ComplexProperty(
                x => x.Name,
                p =>
                {
                    p.Property(x => x.FirstName).HasColumnName("first_name");
                    p.Property(x => x.LastName).HasColumnName("last_name");
                });
    }
}
