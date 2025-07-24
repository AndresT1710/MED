using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePhysicalExamReviewSystemDeviceRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReviewSystemDevices_MedicalCareId",
                table: "ReviewSystemDevices");

            migrationBuilder.DropIndex(
                name: "IX_PhysicalExams_MedicalCareId",
                table: "PhysicalExams");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewSystemDevices_MedicalCareId",
                table: "ReviewSystemDevices",
                column: "MedicalCareId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalExams_MedicalCareId",
                table: "PhysicalExams",
                column: "MedicalCareId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReviewSystemDevices_MedicalCareId",
                table: "ReviewSystemDevices");

            migrationBuilder.DropIndex(
                name: "IX_PhysicalExams_MedicalCareId",
                table: "PhysicalExams");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewSystemDevices_MedicalCareId",
                table: "ReviewSystemDevices",
                column: "MedicalCareId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalExams_MedicalCareId",
                table: "PhysicalExams",
                column: "MedicalCareId",
                unique: true);
        }
    }
}
