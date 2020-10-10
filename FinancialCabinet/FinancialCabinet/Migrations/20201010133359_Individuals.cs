using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancialCabinet.Migrations
{
    public partial class Individuals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Individual_AspNetUsers_UserID",
                table: "Individual");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Individual",
                table: "Individual");

            migrationBuilder.RenameTable(
                name: "Individual",
                newName: "Individuals");

            migrationBuilder.RenameIndex(
                name: "IX_Individual_UserID",
                table: "Individuals",
                newName: "IX_Individuals_UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Individuals",
                table: "Individuals",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Individuals_AspNetUsers_UserID",
                table: "Individuals",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Individuals_AspNetUsers_UserID",
                table: "Individuals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Individuals",
                table: "Individuals");

            migrationBuilder.RenameTable(
                name: "Individuals",
                newName: "Individual");

            migrationBuilder.RenameIndex(
                name: "IX_Individuals_UserID",
                table: "Individual",
                newName: "IX_Individual_UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Individual",
                table: "Individual",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Individual_AspNetUsers_UserID",
                table: "Individual",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
