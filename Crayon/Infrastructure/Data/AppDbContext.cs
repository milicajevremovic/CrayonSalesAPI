using Crayon.Crayon.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace Crayon.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<PurchasedSoftware> PurchasedSoftwares { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed accounts
            var account1 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Account A"
            };

            var account2 = new Account
            {
                Id = Guid.NewGuid(),
                Name = "Account B"
            };

            modelBuilder.Entity<Account>().HasData(account1, account2);

            // seed purchased softwares for our accounts
            var software1 = new PurchasedSoftware
            {
                Id = Guid.NewGuid(),
                SoftwareName = "Microsoft Office",
                Quantity = 5,
                State = "Active",
                ValidTo = DateTime.UtcNow.AddMonths(10),
                AccountId = account1.Id
            };

            var software2 = new PurchasedSoftware
            {
                Id = Guid.NewGuid(),
                SoftwareName = "Adobe Photoshop",
                Quantity = 2,
                State = "Active",
                ValidTo = DateTime.UtcNow.AddMonths(12),
                AccountId = account1.Id
            };

            var software3 = new PurchasedSoftware
            {
                Id = Guid.NewGuid(),
                SoftwareName = "Visual Studio",
                Quantity = 3,
                State = "Active",
                ValidTo = DateTime.UtcNow.AddMonths(36),
                AccountId = account2.Id
            };

            modelBuilder.Entity<PurchasedSoftware>().HasData(software1, software2, software3);
        }
    }
}
