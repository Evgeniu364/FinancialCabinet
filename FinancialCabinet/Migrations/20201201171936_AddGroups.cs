using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancialCabinet.Migrations
{
    public partial class AddGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupNumber",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GroupCredit",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    GroupNumber = table.Column<int>(nullable: false),
                    SingleCreditID = table.Column<Guid>(nullable: false),
                    k = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupCredit", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GroupCredit_SingleCredit_SingleCreditID",
                        column: x => x.SingleCreditID,
                        principalTable: "SingleCredit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupDeposit",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    GroupNumber = table.Column<int>(nullable: false),
                    SingleDepositID = table.Column<Guid>(nullable: false),
                    k = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupDeposit", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GroupDeposit_SingleDeposit_SingleDepositID",
                        column: x => x.SingleDepositID,
                        principalTable: "SingleDeposit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupCredit_SingleCreditID",
                table: "GroupCredit",
                column: "SingleCreditID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupDeposit_SingleDepositID",
                table: "GroupDeposit",
                column: "SingleDepositID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupCredit");

            migrationBuilder.DropTable(
                name: "GroupDeposit");

            migrationBuilder.DropColumn(
                name: "GroupNumber",
                table: "AspNetUsers");
        }
    }
}
