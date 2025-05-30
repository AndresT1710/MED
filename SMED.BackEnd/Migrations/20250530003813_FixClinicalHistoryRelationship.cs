using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class FixClinicalHistoryRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ClinicalHistories_PatientId",
                table: "ClinicalHistories");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalHistories_PatientId",
                table: "ClinicalHistories",
                column: "PatientId",
                unique: true,
                filter: "[PatientId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ClinicalHistories_PatientId",
                table: "ClinicalHistories");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalHistories_PatientId",
                table: "ClinicalHistories",
                column: "PatientId");
        }
    }
}
