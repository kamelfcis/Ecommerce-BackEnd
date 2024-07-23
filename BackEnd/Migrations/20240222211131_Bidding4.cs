using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Migrations
{
    public partial class Bidding4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BiddingTransaction_BidItems_BidItemId",
                table: "BiddingTransaction");

            migrationBuilder.AlterColumn<int>(
                name: "BidItemId",
                table: "BiddingTransaction",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BiddingTransaction_BidItems_BidItemId",
                table: "BiddingTransaction",
                column: "BidItemId",
                principalTable: "BidItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BiddingTransaction_BidItems_BidItemId",
                table: "BiddingTransaction");

            migrationBuilder.AlterColumn<int>(
                name: "BidItemId",
                table: "BiddingTransaction",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_BiddingTransaction_BidItems_BidItemId",
                table: "BiddingTransaction",
                column: "BidItemId",
                principalTable: "BidItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
