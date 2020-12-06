using System;
using System.Collections.Generic;
using System.Text;
using FinancialCabinet.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialCabinet.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Individual> Individuals { get; set; }
        public virtual DbSet<Deposit> Deposits { get; set; }
        public virtual DbSet<Credit> Credits { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<LegalEntity> LegalEntity { get; set; }
        public virtual DbSet<GroupDeposit> GroupDeposit { get; set; }
        public virtual DbSet<GroupCredit> GroupCredit { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            EntityTypeBuilder<User> User = builder.Entity<User>();
            User.HasMany(e => e.LikeDepositList).WithOne(e => e.User);
            User.HasMany(e => e.LikeCreditList).WithOne(e => e.User);
            User.HasOne(e => e.Individual).WithOne(e => e.User).HasForeignKey<Individual>(e => e.UserID);
            User.HasOne(e => e.LegalEntity).WithOne(e => e.User).HasForeignKey<LegalEntity>(e => e.UserID);

            EntityTypeBuilder<Individual> Individual = builder.Entity<Individual>();
            Individual.HasKey(e => e.Id);
            Individual.HasOne(e => e.User).WithOne(e => e.Individual);

            EntityTypeBuilder<LegalEntity> LegalEntity = builder.Entity<LegalEntity>();
            LegalEntity.HasKey(e => e.Id);
            LegalEntity.HasOne(e => e.User).WithOne(e => e.LegalEntity);

            EntityTypeBuilder<Bank> Bank = builder.Entity<Bank>();
            Bank.ToTable("Bank");
            Bank.HasMany(e => e.DepositList).WithOne(e => e.Bank);
            Bank.HasMany(e => e.CreditList).WithOne(e => e.Bank);
            Bank.HasMany(e => e.PhoneList).WithOne(e => e.Bank);

            EntityTypeBuilder<Deposit> Deposit = builder.Entity<Deposit>();
            Deposit.ToTable("Deposits");
            Deposit.HasKey(e => e.ID);
            Deposit.HasOne(e => e.Bank).WithMany(e => e.DepositList);
            Deposit.HasMany(e => e.SingleDepositList);

            EntityTypeBuilder<SingleDeposit> SingleDeposit = builder.Entity<SingleDeposit>();
            SingleDeposit.ToTable("SingleDeposit");
            SingleDeposit.HasOne(e => e.Deposit).WithMany(e => e.SingleDepositList);
            SingleDeposit.HasOne(e => e.Period).WithOne(e => e.SingleDeposit).HasForeignKey<SingleDeposit>(e => e.PeriodID).OnDelete(DeleteBehavior.Cascade);
            SingleDeposit.HasOne(e => e.Percent).WithOne(e => e.SingleDeposit).HasForeignKey<SingleDeposit>(e => e.PercentID).OnDelete(DeleteBehavior.Cascade);
            SingleDeposit.HasMany(e => e.LikeDepositList).WithOne(e => e.SingleDeposit);
            SingleDeposit.HasMany(e => e.GroupDepositList).WithOne(e => e.SingleDeposit);

            EntityTypeBuilder<Period> Period = builder.Entity<Period>();
            Period.ToTable("Period");
            Period.HasOne(e => e.SingleDeposit).WithOne(e => e.Period);
            Period.HasOne(e => e.SingleCredit).WithOne(e => e.Period);

            EntityTypeBuilder<Percent> Percent = builder.Entity<Percent>();
            Percent.ToTable("Percent");
            Percent.HasOne(e => e.SingleDeposit).WithOne(e => e.Percent);
            Percent.HasOne(e => e.SingleCredit).WithOne(e => e.Percent);

            EntityTypeBuilder<LikeDeposit> LikeDeposit = builder.Entity<LikeDeposit>();
            LikeDeposit.ToTable("LikeDeposit");
            LikeDeposit.HasOne(e => e.SingleDeposit).WithMany(e => e.LikeDepositList);
            LikeDeposit.HasOne(e => e.User).WithMany(e => e.LikeDepositList);

            EntityTypeBuilder<Phone> Phone = builder.Entity<Phone>();
            Phone.ToTable("Phone");
            Phone.HasOne(e => e.Bank).WithMany(e => e.PhoneList);

            EntityTypeBuilder<Credit> Credit = builder.Entity<Credit>();
            Credit.ToTable("Credits");
            Credit.HasKey(e => e.ID);
            Credit.HasOne(e => e.Bank).WithMany(e => e.CreditList);
            Credit.HasMany(e => e.SingleCreditList).WithOne(e => e.Credit);

            EntityTypeBuilder<SingleCredit> SingleCredit = builder.Entity<SingleCredit>();
            SingleCredit.ToTable("SingleCredit");
            SingleCredit.HasOne(e => e.Credit).WithMany(e => e.SingleCreditList);
            SingleCredit.HasOne(e => e.Period).WithOne(e => e.SingleCredit).HasForeignKey<SingleCredit>(e => e.PeriodID).OnDelete(DeleteBehavior.Cascade);
            SingleCredit.HasOne(e => e.Percent).WithOne(e => e.SingleCredit).HasForeignKey<SingleCredit>(e => e.PercentID).OnDelete(DeleteBehavior.Cascade);
            SingleCredit.HasMany(e => e.LikeCreditList).WithOne(e => e.SingleCredit);
            SingleCredit.HasMany(e => e.GroupCreditList).WithOne(e => e.SingleCredit);

            EntityTypeBuilder<LikeCredit> LikeCredit = builder.Entity<LikeCredit>();
            LikeCredit.ToTable("LikeCredit");
            LikeCredit.HasOne(e => e.SingleCredit).WithMany(e => e.LikeCreditList);
            LikeCredit.HasOne(e => e.User).WithMany(e => e.LikeCreditList);

            base.OnModelCreating(builder);
        }
    }
}
