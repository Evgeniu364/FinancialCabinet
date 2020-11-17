using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancialCabinet.Migrations
{
    public partial class AddNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DepositName",
                table: "Deposits",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreditName",
                table: "Credits",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepositName",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "CreditName",
                table: "Credits");
        }
    }
}
