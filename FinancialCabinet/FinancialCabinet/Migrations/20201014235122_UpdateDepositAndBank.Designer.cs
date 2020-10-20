﻿// <auto-generated />
using System;
using FinancialCabinet.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FinancialCabinet.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20201014235122_UpdateDepositAndBank")]
    partial class UpdateDepositAndBank
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FinancialCabinet.Entity.Bank", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Address")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("BIK")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("BankAccount")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Information")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("ID");

                    b.ToTable("Bank");
                });

            modelBuilder.Entity("FinancialCabinet.Entity.Credit", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<Guid>("BankId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("GroupCreditId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsGuarantor")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsIncomeCertification")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("MaxSum")
                        .HasColumnType("int");

                    b.Property<int>("MinSum")
                        .HasColumnType("int");

                    b.Property<double>("Percent")
                        .HasColumnType("double");

                    b.Property<int>("Period")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("BankId");

                    b.HasIndex("GroupCreditId");

                    b.ToTable("Credits");
                });

            modelBuilder.Entity("FinancialCabinet.Entity.Deposit", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("BankID")
                        .HasColumnType("char(36)");

                    b.HasKey("ID");

                    b.HasIndex("BankID");

                    b.ToTable("Deposits");
                });

            modelBuilder.Entity("FinancialCabinet.Entity.GroupCredit", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.HasKey("ID");

                    b.ToTable("GroupCredit");
                });

            modelBuilder.Entity("FinancialCabinet.Entity.Individual", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("NumberDocument")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Patronymic")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<double>("Salary")
                        .HasColumnType("double");

                    b.Property<string>("TypeDocument")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid>("UserID")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("Individual");
                });

            modelBuilder.Entity("FinancialCabinet.Entity.LegalEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<double>("CashTurnover")
                        .HasColumnType("double");

                    b.Property<string>("CompanyName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("NumberDocument")
                        .HasColumnType("int");

                    b.Property<int>("Unp")
                        .HasColumnType("int");

                    b.Property<Guid>("UserID")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("LegalEntity");
                });

            modelBuilder.Entity("FinancialCabinet.Entity.LikeDeposit", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("SingleDepositID")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserID")
                        .HasColumnType("char(36)");

                    b.HasKey("ID");

                    b.HasIndex("SingleDepositID");

                    b.HasIndex("UserID");

                    b.ToTable("LikeDeposit");
                });

            modelBuilder.Entity("FinancialCabinet.Entity.Percent", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsInterval")
                        .HasColumnType("tinyint(1)");

                    b.Property<double>("MaxPercent")
                        .HasColumnType("double");

                    b.Property<double?>("MinPercent")
                        .HasColumnType("double");

                    b.Property<Guid>("SingleDepositID")
                        .HasColumnType("char(36)");

                    b.HasKey("ID");

                    b.ToTable("Percent");
                });

            modelBuilder.Entity("FinancialCabinet.Entity.Period", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsInterval")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("MaxPeriod")
                        .HasColumnType("int");

                    b.Property<int>("MaxPeriodType")
                        .HasColumnType("int");

                    b.Property<int?>("MinPeriod")
                        .HasColumnType("int");

                    b.Property<int?>("MinPeriodType")
                        .HasColumnType("int");

                    b.Property<Guid>("SingleDepositID")
                        .HasColumnType("char(36)");

                    b.HasKey("ID");

                    b.ToTable("Period");
                });

            modelBuilder.Entity("FinancialCabinet.Entity.Phone", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("BankID")
                        .HasColumnType("char(36)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("ID");

                    b.HasIndex("BankID");

                    b.ToTable("Phone");
                });

            modelBuilder.Entity("FinancialCabinet.Entity.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("FinancialCabinet.Entity.SingleDeposit", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Currency")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid>("DepositID")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsReplenishable")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsRevocable")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("PercentID")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("PeriodID")
                        .HasColumnType("char(36)");

                    b.Property<int?>("Sum")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("DepositID");

                    b.HasIndex("PercentID")
                        .IsUnique();

                    b.HasIndex("PeriodID")
                        .IsUnique();

                    b.ToTable("SingleDeposit");
                });

            modelBuilder.Entity("FinancialCabinet.Entity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("DateRegistration")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid?>("IndividualID")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("LegalEntityID")
                        .HasColumnType("char(36)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Value")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("FinancialCabinet.Entity.Credit", b =>
                {
                    b.HasOne("FinancialCabinet.Entity.Bank", "Bank")
                        .WithMany()
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinancialCabinet.Entity.GroupCredit", "GroupCredit")
                        .WithMany()
                        .HasForeignKey("GroupCreditId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FinancialCabinet.Entity.Deposit", b =>
                {
                    b.HasOne("FinancialCabinet.Entity.Bank", "Bank")
                        .WithMany("DepositList")
                        .HasForeignKey("BankID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FinancialCabinet.Entity.Individual", b =>
                {
                    b.HasOne("FinancialCabinet.Entity.User", "User")
                        .WithOne("Individual")
                        .HasForeignKey("FinancialCabinet.Entity.Individual", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FinancialCabinet.Entity.LegalEntity", b =>
                {
                    b.HasOne("FinancialCabinet.Entity.User", "User")
                        .WithOne("LegalEntity")
                        .HasForeignKey("FinancialCabinet.Entity.LegalEntity", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FinancialCabinet.Entity.LikeDeposit", b =>
                {
                    b.HasOne("FinancialCabinet.Entity.SingleDeposit", "SingleDeposit")
                        .WithMany("LikeDepositList")
                        .HasForeignKey("SingleDepositID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinancialCabinet.Entity.User", "User")
                        .WithMany("LikeDepositList")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FinancialCabinet.Entity.Phone", b =>
                {
                    b.HasOne("FinancialCabinet.Entity.Bank", "Bank")
                        .WithMany("PhoneList")
                        .HasForeignKey("BankID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FinancialCabinet.Entity.SingleDeposit", b =>
                {
                    b.HasOne("FinancialCabinet.Entity.Deposit", "Deposit")
                        .WithMany("SingleDepositList")
                        .HasForeignKey("DepositID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinancialCabinet.Entity.Percent", "Percent")
                        .WithOne("SingleDeposit")
                        .HasForeignKey("FinancialCabinet.Entity.SingleDeposit", "PercentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinancialCabinet.Entity.Period", "Period")
                        .WithOne("SingleDeposit")
                        .HasForeignKey("FinancialCabinet.Entity.SingleDeposit", "PeriodID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("FinancialCabinet.Entity.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("FinancialCabinet.Entity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("FinancialCabinet.Entity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("FinancialCabinet.Entity.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinancialCabinet.Entity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("FinancialCabinet.Entity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
