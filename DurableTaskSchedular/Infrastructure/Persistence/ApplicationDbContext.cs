using Application.Abstractions;
using Domain.BankAccounts;
using Domain.ScheduledBankAccountCreations;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IUnitOfWork
{
    public DbSet<User> Users => Set<User>();
    public DbSet<BankAccount> BankAccounts => Set<BankAccount>();
    public DbSet<ScheduledBankAccountCreation> ScheduledBankAccountCreations => Set<ScheduledBankAccountCreation>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BankAccount>();
        modelBuilder.Entity<ScheduledTransfer>();
        modelBuilder.Entity<ScheduledBankAccountCreation>();
        modelBuilder.Entity<User>();
    }
}
