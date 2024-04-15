using Domain.BankAccounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class ScheduledTransferConfiguration : IEntityTypeConfiguration<ScheduledTransfer>
{
    public void Configure(EntityTypeBuilder<ScheduledTransfer> builder)
    {
        builder
            .ToTable("scheduled_transfers");

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .ValueGeneratedNever()
            .HasColumnName("id")
            .HasConversion(
                x => x.Value,
                x => new ScheduledTransferId(x)
            );

        builder
            .Property<BankAccountId>("BankAccountId")
            .HasColumnName("bank_account_id")
            .HasConversion(
                x => x.Value,
                x => new BankAccountId(x)
            );

        builder
            .Property(x => x.To)
            .HasColumnName("to_bank_account_id")
            .HasConversion(
                x => x.Value,
                x => new BankAccountId(x)
            );

        builder
            .Property(x => x.Amount)
            .HasColumnName("amount");

        builder
            .Property(x => x.ScheduledDateTime)
            .HasColumnName("scheduled_datetime");
    }
}
