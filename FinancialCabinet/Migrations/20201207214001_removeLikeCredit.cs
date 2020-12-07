using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancialCabinet.Migrations
{
    public partial class removeLikeCredit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikeCredit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LikeCredit",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "char(36)", nullable: false),
                    CreditId = table.Column<Guid>(type: "char(36)", nullable: false),
                    SingleCreditID = table.Column<Guid>(type: "char(36)", nullable: true),
                    UserID = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeCredit", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LikeCredit_SingleCredit_SingleCreditID",
                        column: x => x.SingleCreditID,
                        principalTable: "SingleCredit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LikeCredit_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LikeCredit_SingleCreditID",
                table: "LikeCredit",
                column: "SingleCreditID");

            migrationBuilder.CreateIndex(
                name: "IX_LikeCredit_UserID",
                table: "LikeCredit",
                column: "UserID");
        }
    }
}
