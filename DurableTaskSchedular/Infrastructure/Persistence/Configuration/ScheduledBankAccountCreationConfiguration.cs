using Domain.BankAccounts;
using Domain.ScheduledBankAccountCreations;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class ScheduledBankAccountCreationConfiguration : IEntityTypeConfiguration<ScheduledBankAccountCreation>
{
    public void Configure(EntityTypeBuilder<ScheduledBankAccountCreation> builder)
    {
        builder
            .ToTable("scheduled_bank_account_creations");

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .ValueGeneratedNever()
            .HasColumnName("id")
            .HasConversion(
                x => x.Value,
                x => new ScheduledBankAccountCreationId(x)
            );

        builder
            .Property(x => x.Currency)
            .HasColumnName("currency")
            .HasConversion(
                x => x.Code,
                x => Currency.FromCode(x)
            );

        builder
            .Property(x => x.ScheduleDateTime)
            .HasColumnName("scheduled_datetime");

        builder
            .Property(x => x.UserId)
            .HasColumnName("user_id")
            .HasConversion(
                x => x.Value,
                x => new UserId(x)
            );

        builder
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
