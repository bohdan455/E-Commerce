using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixOrderpartIssue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderParts_OrderPartId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderPartId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "OrderParts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderParts_OrderId",
                table: "OrderParts",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderParts_Orders_OrderId",
                table: "OrderParts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderParts_Orders_OrderId",
                table: "OrderParts");

            migrationBuilder.DropIndex(
                name: "IX_OrderParts_OrderId",
                table: "OrderParts");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "OrderParts");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderPartId",
                table: "Orders",
                column: "OrderPartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderParts_OrderPartId",
                table: "Orders",
                column: "OrderPartId",
                principalTable: "OrderParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
