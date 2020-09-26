﻿// <auto-generated />
using System;
using FinancialCabinet.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FinancialCabinet.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    partial class ApiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8");

            modelBuilder.Entity("FinancialCabinet.Entity.Deposit", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Currency")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsReplenishable")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsRevocable")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaxSum")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MinSum")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Percent")
                        .HasColumnType("REAL");

                    b.Property<int>("Period")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("Deposits");
                });

            modelBuilder.Entity("FinancialCabinet.Entity.LikeDeposit", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DepositID")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserID")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("DepositID");

                    b.HasIndex("UserID");

                    b.ToTable("LikeDeposit");
                });

            modelBuilder.Entity("FinancialCabinet.Entity.User", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FinancialCabinet.Entity.LikeDeposit", b =>
                {
                    b.HasOne("FinancialCabinet.Entity.Deposit", "Deposit")
                        .WithMany("LikeDepositList")
                        .HasForeignKey("DepositID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinancialCabinet.Entity.User", "User")
                        .WithMany("LikeDepositList")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
