using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Migrations
{
    public partial class Bidding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BidItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsurancePrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BiddingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BidItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BiddingTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TransactionBidValue = table.Column<double>(type: "float", nullable: false),
                    BidItemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiddingTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiddingTransaction_BidItems_BidItemId",
                        column: x => x.BidItemId,
                        principalTable: "BidItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BiddingTransaction_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BiddingTransaction_BidItemId",
                table: "BiddingTransaction",
                column: "BidItemId");

            migrationBuilder.CreateIndex(
                name: "IX_BiddingTransaction_UserId",
                table: "BiddingTransaction",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BiddingTransaction");

            migrationBuilder.DropTable(
                name: "BidItems");
        }
    }
}
