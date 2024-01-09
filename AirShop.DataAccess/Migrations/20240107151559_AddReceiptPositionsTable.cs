using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AirShop.WebApiPostgre.Migrations
{
    /// <inheritdoc />
    public partial class AddReceiptPositionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReceiptPositions",
                columns: table => new
                {
                    ReceiptPositionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReceiptId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptPositions", x => x.ReceiptPositionId);
                    table.ForeignKey(
                        name: "FK_ReceiptPositions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiptPositions_Receipts_ReceiptId",
                        column: x => x.ReceiptId,
                        principalTable: "Receipts",
                        principalColumn: "ReceiptId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptPositions_ProductId",
                table: "ReceiptPositions",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptPositions_ReceiptId",
                table: "ReceiptPositions",
                column: "ReceiptId");

            // Dodanie sekwencji do ReceiptPositions
            migrationBuilder.Sql(@"
                CREATE SEQUENCE receipt_position_id_seq
                START WITH 1
                INCREMENT BY 1
                NO MINVALUE
                NO MAXVALUE
                CACHE 1;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceiptPositions");

            // Usunięcie sekwencji
            migrationBuilder.Sql(@"
                DROP SEQUENCE IF EXISTS receipt_position_id_seq;
            ");
        }

    }
}
