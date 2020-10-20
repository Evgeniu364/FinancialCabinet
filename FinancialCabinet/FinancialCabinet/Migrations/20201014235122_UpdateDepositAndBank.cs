using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancialCabinet.Migrations
{
    public partial class UpdateDepositAndBank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LikeDeposit_Deposits_DepositID",
                table: "LikeDeposit");

            migrationBuilder.DropIndex(
                name: "IX_LikeDeposit_DepositID",
                table: "LikeDeposit");

            migrationBuilder.DropColumn(
                name: "DepositID",
                table: "LikeDeposit");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "IsReplenishable",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "IsRevocable",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "MaxSum",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "MinSum",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "Percent",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "Period",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Bank");

            migrationBuilder.AddColumn<Guid>(
                name: "SingleDepositID",
                table: "LikeDeposit",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BankID",
                table: "Deposits",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Percent",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    MinPercent = table.Column<double>(nullable: true),
                    MaxPercent = table.Column<double>(nullable: false),
                    IsInterval = table.Column<bool>(nullable: false),
                    SingleDepositID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Percent", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Period",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    MinPeriod = table.Column<int>(nullable: true),
                    MaxPeriod = table.Column<int>(nullable: false),
                    MinPeriodType = table.Column<int>(nullable: true),
                    MaxPeriodType = table.Column<int>(nullable: false),
                    IsInterval = table.Column<bool>(nullable: false),
                    SingleDepositID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Period", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Phone",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    BankID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phone", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Phone_Bank_BankID",
                        column: x => x.BankID,
                        principalTable: "Bank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SingleDeposit",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Sum = table.Column<int>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    IsReplenishable = table.Column<bool>(nullable: false),
                    IsRevocable = table.Column<bool>(nullable: false),
                    DepositID = table.Column<Guid>(nullable: false),
                    PeriodID = table.Column<Guid>(nullable: false),
                    PercentID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleDeposit", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SingleDeposit_Deposits_DepositID",
                        column: x => x.DepositID,
                        principalTable: "Deposits",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SingleDeposit_Percent_PercentID",
                        column: x => x.PercentID,
                        principalTable: "Percent",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SingleDeposit_Period_PeriodID",
                        column: x => x.PeriodID,
                        principalTable: "Period",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LikeDeposit_SingleDepositID",
                table: "LikeDeposit",
                column: "SingleDepositID");

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_BankID",
                table: "Deposits",
                column: "BankID");

            migrationBuilder.CreateIndex(
                name: "IX_Phone_BankID",
                table: "Phone",
                column: "BankID");

            migrationBuilder.CreateIndex(
                name: "IX_SingleDeposit_DepositID",
                table: "SingleDeposit",
                column: "DepositID");

            migrationBuilder.CreateIndex(
                name: "IX_SingleDeposit_PercentID",
                table: "SingleDeposit",
                column: "PercentID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SingleDeposit_PeriodID",
                table: "SingleDeposit",
                column: "PeriodID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Deposits_Bank_BankID",
                table: "Deposits",
                column: "BankID",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LikeDeposit_SingleDeposit_SingleDepositID",
                table: "LikeDeposit",
                column: "SingleDepositID",
                principalTable: "SingleDeposit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deposits_Bank_BankID",
                table: "Deposits");

            migrationBuilder.DropForeignKey(
                name: "FK_LikeDeposit_SingleDeposit_SingleDepositID",
                table: "LikeDeposit");

            migrationBuilder.DropTable(
                name: "Phone");

            migrationBuilder.DropTable(
                name: "SingleDeposit");

            migrationBuilder.DropTable(
                name: "Percent");

            migrationBuilder.DropTable(
                name: "Period");

            migrationBuilder.DropIndex(
                name: "IX_LikeDeposit_SingleDepositID",
                table: "LikeDeposit");

            migrationBuilder.DropIndex(
                name: "IX_Deposits_BankID",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "SingleDepositID",
                table: "LikeDeposit");

            migrationBuilder.DropColumn(
                name: "BankID",
                table: "Deposits");

            migrationBuilder.AddColumn<Guid>(
                name: "DepositID",
                table: "LikeDeposit",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Deposits",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsReplenishable",
                table: "Deposits",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRevocable",
                table: "Deposits",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MaxSum",
                table: "Deposits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinSum",
                table: "Deposits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Percent",
                table: "Deposits",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Period",
                table: "Deposits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Bank",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LikeDeposit_DepositID",
                table: "LikeDeposit",
                column: "DepositID");

            migrationBuilder.AddForeignKey(
                name: "FK_LikeDeposit_Deposits_DepositID",
                table: "LikeDeposit",
                column: "DepositID",
                principalTable: "Deposits",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
