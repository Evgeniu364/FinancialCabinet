using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancialCabinet.Migrations
{
    public partial class AddUserAndDeposit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Deposits",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    MinSum = table.Column<int>(nullable: false),
                    MaxSum = table.Column<int>(nullable: false),
                    Period = table.Column<int>(nullable: false),
                    Percent = table.Column<double>(nullable: false),
                    Currency = table.Column<string>(nullable: true),
                    IsReplenishable = table.Column<bool>(nullable: false),
                    IsRevocable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposits", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LikeDeposit",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false),
                    DepositID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeDeposit", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LikeDeposit_Deposits_DepositID",
                        column: x => x.DepositID,
                        principalTable: "Deposits",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikeDeposit_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LikeDeposit_DepositID",
                table: "LikeDeposit",
                column: "DepositID");

            migrationBuilder.CreateIndex(
                name: "IX_LikeDeposit_UserID",
                table: "LikeDeposit",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikeDeposit");

            migrationBuilder.DropTable(
                name: "Deposits");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
