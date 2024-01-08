using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirShop.WebApiPostgre.Migrations
{
    /// <inheritdoc />
    public partial class AddFkAndPkToTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentPosition_Documents_DocumentId",
                table: "DocumentPosition");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentPosition_Products_ProductId",
                table: "DocumentPosition");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentPosition",
                table: "DocumentPosition");

            migrationBuilder.RenameTable(
                name: "DocumentPosition",
                newName: "DocumentPositions");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentPosition_ProductId",
                table: "DocumentPositions",
                newName: "IX_DocumentPositions_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentPosition_DocumentId",
                table: "DocumentPositions",
                newName: "IX_DocumentPositions_DocumentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentPositions",
                table: "DocumentPositions",
                column: "DocumentPositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentPositions_Documents_DocumentId",
                table: "DocumentPositions",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "DocumentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentPositions_Products_ProductId",
                table: "DocumentPositions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentPositions_Documents_DocumentId",
                table: "DocumentPositions");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentPositions_Products_ProductId",
                table: "DocumentPositions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentPositions",
                table: "DocumentPositions");

            migrationBuilder.RenameTable(
                name: "DocumentPositions",
                newName: "DocumentPosition");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentPositions_ProductId",
                table: "DocumentPosition",
                newName: "IX_DocumentPosition_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_DocumentPositions_DocumentId",
                table: "DocumentPosition",
                newName: "IX_DocumentPosition_DocumentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentPosition",
                table: "DocumentPosition",
                column: "DocumentPositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentPosition_Documents_DocumentId",
                table: "DocumentPosition",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "DocumentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentPosition_Products_ProductId",
                table: "DocumentPosition",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
