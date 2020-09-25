using FinancialCabinet.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialCabinet.Database
{
    public class ApiDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        public ApiDbContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            EntityTypeBuilder<User> User = builder.Entity<User>();
            User.HasKey(u => u.ID);
        }
    }
}
