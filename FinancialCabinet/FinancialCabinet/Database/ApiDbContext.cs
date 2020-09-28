using FinancialCabinet.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FinancialCabinet.Database
{
    public class ApiDbContext : IdentityDbContext<User>
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Deposit> Deposits { get; set; }

        public virtual DbSet<Credit> Credits { get; set; }

        public ApiDbContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            EntityTypeBuilder<User> User = builder.Entity<User>();
            User.HasMany(e => e.LikeDepositList).WithOne(e => e.User);

            EntityTypeBuilder<Deposit> Deposit = builder.Entity<Deposit>();
            Deposit.HasKey(e => e.ID);
            Deposit.HasMany(e => e.LikeDepositList).WithOne(e => e.Deposit);

            EntityTypeBuilder<Credit> Credit = builder.Entity<Credit>();
            Credit.HasKey(e => e.ID);
            base.OnModelCreating(builder);
        }
    }
}
