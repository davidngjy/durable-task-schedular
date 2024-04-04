using Application.Abstractions;
using Domain.BankAccounts;
using Domain.ScheduledBankAccountCreations;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BankAccount>();
        modelBuilder.Entity<ScheduledTransfer>();
        modelBuilder.Entity<ScheduledBankAccountCreation>();
        modelBuilder.Entity<User>();
    }
}
