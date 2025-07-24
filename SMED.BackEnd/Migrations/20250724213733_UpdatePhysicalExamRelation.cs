using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePhysicalExamRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReviewSystemDevices_SystemsDevicesId_MedicalCareId",
                table: "ReviewSystemDevices");

            migrationBuilder.AlterColumn<int>(
                name: "MedicalCareId",
                table: "ReviewSystemDevices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MedicalCareId",
                table: "PhysicalExams",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewSystemDevices_SystemsDevicesId_MedicalCareId",
                table: "ReviewSystemDevices",
                columns: new[] { "SystemsDevicesId", "MedicalCareId" },
                unique: true,
                filter: "[MedicalCareId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReviewSystemDevices_SystemsDevicesId_MedicalCareId",
                table: "ReviewSystemDevices");

            migrationBuilder.AlterColumn<int>(
                name: "MedicalCareId",
                table: "ReviewSystemDevices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MedicalCareId",
                table: "PhysicalExams",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReviewSystemDevices_SystemsDevicesId_MedicalCareId",
                table: "ReviewSystemDevices",
                columns: new[] { "SystemsDevicesId", "MedicalCareId" },
                unique: true);
        }
    }
}
