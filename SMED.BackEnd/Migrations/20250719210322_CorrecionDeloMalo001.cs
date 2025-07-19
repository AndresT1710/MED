using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class CorrecionDeloMalo001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewSystemDevices_SystemsDevices_SystemsDevicesId",
                table: "ReviewSystemDevices");

            migrationBuilder.DropIndex(
                name: "IX_ReviewSystemDevices_SystemsDevicesId",
                table: "ReviewSystemDevices");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewSystemDevices_SystemsDevicesId_MedicalCareId",
                table: "ReviewSystemDevices",
                columns: new[] { "SystemsDevicesId", "MedicalCareId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewSystemDevices_SystemsDevices_SystemsDevicesId",
                table: "ReviewSystemDevices",
                column: "SystemsDevicesId",
                principalTable: "SystemsDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewSystemDevices_SystemsDevices_SystemsDevicesId",
                table: "ReviewSystemDevices");

            migrationBuilder.DropIndex(
                name: "IX_ReviewSystemDevices_SystemsDevicesId_MedicalCareId",
                table: "ReviewSystemDevices");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewSystemDevices_SystemsDevicesId",
                table: "ReviewSystemDevices",
                column: "SystemsDevicesId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewSystemDevices_SystemsDevices_SystemsDevicesId",
                table: "ReviewSystemDevices",
                column: "SystemsDevicesId",
                principalTable: "SystemsDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
