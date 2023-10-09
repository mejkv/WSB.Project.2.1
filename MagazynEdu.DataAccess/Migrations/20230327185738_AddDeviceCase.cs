using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagazynEdu.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddDeviceCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeviceCaseId",
                table: "Devices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DeviceCase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCase", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_DeviceCaseId",
                table: "Devices",
                column: "DeviceCaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_DeviceCase_DeviceCaseId",
                table: "Devices",
                column: "DeviceCaseId",
                principalTable: "DeviceCase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_DeviceCase_DeviceCaseId",
                table: "Devices");

            migrationBuilder.DropTable(
                name: "DeviceCase");

            migrationBuilder.DropIndex(
                name: "IX_Devices_DeviceCaseId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "DeviceCaseId",
                table: "Devices");
        }
    }
}
