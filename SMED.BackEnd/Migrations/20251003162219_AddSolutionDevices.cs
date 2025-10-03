using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddSolutionDevices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReviewSystemDevices_SystemsDevicesId_MedicalCareId",
                table: "ReviewSystemDevices");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewSystemDevices_SystemsDevicesId_MedicalCareId",
                table: "ReviewSystemDevices",
                columns: new[] { "SystemsDevicesId", "MedicalCareId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReviewSystemDevices_SystemsDevicesId_MedicalCareId",
                table: "ReviewSystemDevices");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewSystemDevices_SystemsDevicesId_MedicalCareId",
                table: "ReviewSystemDevices",
                columns: new[] { "SystemsDevicesId", "MedicalCareId" },
                unique: true,
                filter: "[MedicalCareId] IS NOT NULL");
        }
    }
}
