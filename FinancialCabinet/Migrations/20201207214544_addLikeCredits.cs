using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancialCabinet.Migrations
{
    public partial class addLikeCredits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LikeCredit",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false),
                    SingleCreditId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeCredit", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LikeCredit_SingleCredit_SingleCreditId",
                        column: x => x.SingleCreditId,
                        principalTable: "SingleCredit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikeCredit_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LikeCredit_SingleCreditId",
                table: "LikeCredit",
                column: "SingleCreditId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeCredit_UserID",
                table: "LikeCredit",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikeCredit");
        }
    }
}
