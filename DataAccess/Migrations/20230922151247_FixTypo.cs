using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderPart_Products_ProductId",
                table: "OrderPart");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderPart_OrderPartId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderPart",
                table: "OrderPart");

            migrationBuilder.RenameTable(
                name: "OrderPart",
                newName: "OrderParts");

            migrationBuilder.RenameIndex(
                name: "IX_OrderPart_ProductId",
                table: "OrderParts",
                newName: "IX_OrderParts_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderParts",
                table: "OrderParts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderParts_Products_ProductId",
                table: "OrderParts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderParts_OrderPartId",
                table: "Orders",
                column: "OrderPartId",
                principalTable: "OrderParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderParts_Products_ProductId",
                table: "OrderParts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderParts_OrderPartId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderParts",
                table: "OrderParts");

            migrationBuilder.RenameTable(
                name: "OrderParts",
                newName: "OrderPart");

            migrationBuilder.RenameIndex(
                name: "IX_OrderParts_ProductId",
                table: "OrderPart",
                newName: "IX_OrderPart_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderPart",
                table: "OrderPart",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPart_Products_ProductId",
                table: "OrderPart",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderPart_OrderPartId",
                table: "Orders",
                column: "OrderPartId",
                principalTable: "OrderPart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
