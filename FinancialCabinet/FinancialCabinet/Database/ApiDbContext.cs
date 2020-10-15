﻿using FinancialCabinet.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FinancialCabinet.Database
{
    public class ApiDbContext : IdentityDbContext<User, Role, Guid>
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
            User.HasOne(e => e.Individual).WithOne(e => e.User).HasForeignKey<Individual>(e => e.UserID);
            User.HasOne(e => e.LegalEntity).WithOne(e => e.User).HasForeignKey<LegalEntity>(e => e.UserID);
            
            EntityTypeBuilder<Individual> Individual = builder.Entity<Individual>();
            Individual.HasKey(e => e.Id);
            Individual.HasOne(e => e.User).WithOne(e => e.Individual);

            EntityTypeBuilder<LegalEntity> LegalEntity = builder.Entity<LegalEntity>();
            LegalEntity.HasKey(e => e.Id);
            LegalEntity.HasOne(e => e.User).WithOne(e => e.LegalEntity);
            
            EntityTypeBuilder<Bank> Bank = builder.Entity<Bank>();
            Bank.HasMany(e => e.DepositList).WithOne(e => e.Bank);
            Bank.HasMany(e => e.PhoneList).WithOne(e => e.Bank);

            EntityTypeBuilder<Deposit> Deposit = builder.Entity<Deposit>();
            Deposit.HasKey(e => e.ID);
            Deposit.HasOne(e => e.Bank).WithMany(e => e.DepositList);
            Deposit.HasMany(e => e.SingleDepositList).WithOne(e => e.Deposit);

            EntityTypeBuilder<SingleDeposit> SingleDeposit = builder.Entity<SingleDeposit>();
            SingleDeposit.HasOne(e => e.Deposit).WithMany(e => e.SingleDepositList);
            SingleDeposit.HasOne(e => e.Period).WithOne(e => e.SingleDeposit).HasForeignKey<SingleDeposit>(e => e.PeriodID);
            SingleDeposit.HasOne(e => e.Percent).WithOne(e => e.SingleDeposit).HasForeignKey<SingleDeposit>(e => e.PercentID);
            SingleDeposit.HasMany(e => e.LikeDepositList).WithOne(e => e.SingleDeposit);

            EntityTypeBuilder<Period> Period = builder.Entity<Period>();
            Period.HasOne(e => e.SingleDeposit).WithOne(e => e.Period);

            EntityTypeBuilder<Percent> Percent = builder.Entity<Percent>();
            Percent.HasOne(e => e.SingleDeposit).WithOne(e => e.Percent);

            EntityTypeBuilder<LikeDeposit> LikeDeposit = builder.Entity<LikeDeposit>();
            LikeDeposit.HasOne(e => e.SingleDeposit).WithMany(e => e.LikeDepositList);

            EntityTypeBuilder<Phone> Phone = builder.Entity<Phone>();
            Phone.HasOne(e => e.Bank).WithMany(e => e.PhoneList);

            EntityTypeBuilder<Credit> Credit = builder.Entity<Credit>();
            Credit.HasKey(e => e.ID);

            base.OnModelCreating(builder);
        }
    }
}
