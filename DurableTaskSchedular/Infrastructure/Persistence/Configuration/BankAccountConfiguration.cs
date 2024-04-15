using Domain.BankAccounts;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
{
    public void Configure(EntityTypeBuilder<BankAccount> builder)
    {
        builder
            .ToTable("bank_accounts");

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .ValueGeneratedNever()
            .HasColumnName("id")
            .HasConversion(
                x => x.Value,
                x => new BankAccountId(x)
            );

        builder
            .Property(x => x.UserId)
            .HasColumnName("user_id")
            .HasConversion(
                x => x.Value,
                x => new UserId(x)
            );

        builder
            .Property(x => x.Currency)
            .HasColumnName("currency")
            .HasConversion(
                x => x.Code,
                x => Currency.FromCode(x)
            );

        builder
            .Property(x => x.CreatedDateTime)
            .HasColumnName("created_datetime");

        builder
            .ComplexProperty(
                x => x.Balance,
                b =>
                {
                    b
                        .Property(x => x.Amount)
                        .HasColumnName("balance");
                });

        builder
            .HasMany(x => x.ScheduledTransfers)
            .WithOne()
            .HasForeignKey("BankAccountId")
            .IsRequired();

        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
