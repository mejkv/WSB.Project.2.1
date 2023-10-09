using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagazynEdu.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_DeviceCase_DeviceCaseId",
                table: "Devices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeviceCase",
                table: "DeviceCase");

            migrationBuilder.RenameTable(
                name: "DeviceCase",
                newName: "DeviceCases");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeviceCases",
                table: "DeviceCases",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthorDevice",
                columns: table => new
                {
                    AuthorsId = table.Column<int>(type: "int", nullable: false),
                    DevicesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorDevice", x => new { x.AuthorsId, x.DevicesId });
                    table.ForeignKey(
                        name: "FK_AuthorDevice_Authors_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorDevice_Devices_DevicesId",
                        column: x => x.DevicesId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorDevice_DevicesId",
                table: "AuthorDevice",
                column: "DevicesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_DeviceCases_DeviceCaseId",
                table: "Devices",
                column: "DeviceCaseId",
                principalTable: "DeviceCases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_DeviceCases_DeviceCaseId",
                table: "Devices");

            migrationBuilder.DropTable(
                name: "AuthorDevice");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeviceCases",
                table: "DeviceCases");

            migrationBuilder.RenameTable(
                name: "DeviceCases",
                newName: "DeviceCase");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeviceCase",
                table: "DeviceCase",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_DeviceCase_DeviceCaseId",
                table: "Devices",
                column: "DeviceCaseId",
                principalTable: "DeviceCase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
