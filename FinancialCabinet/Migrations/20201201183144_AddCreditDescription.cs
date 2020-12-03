using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancialCabinet.Migrations
{
    public partial class AddCreditDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreditDescription",
                table: "Credits",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsForBusiness",
                table: "Credits",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditDescription",
                table: "Credits");

            migrationBuilder.DropColumn(
                name: "IsForBusiness",
                table: "Credits");
        }
    }
}
