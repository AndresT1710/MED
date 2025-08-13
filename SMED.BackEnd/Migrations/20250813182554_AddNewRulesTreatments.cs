using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddNewRulesTreatments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_MedicalCares_MedicalCareId",
                table: "Treatments");

            migrationBuilder.RenameColumn(
                name: "MedicalCareId",
                table: "Treatments",
                newName: "MedicalDiagnosisId");

            migrationBuilder.RenameIndex(
                name: "IX_Treatments_MedicalCareId",
                table: "Treatments",
                newName: "IX_Treatments_MedicalDiagnosisId");

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_MedicalDiagnosis_MedicalDiagnosisId",
                table: "Treatments",
                column: "MedicalDiagnosisId",
                principalTable: "MedicalDiagnosis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_MedicalDiagnosis_MedicalDiagnosisId",
                table: "Treatments");

            migrationBuilder.RenameColumn(
                name: "MedicalDiagnosisId",
                table: "Treatments",
                newName: "MedicalCareId");

            migrationBuilder.RenameIndex(
                name: "IX_Treatments_MedicalDiagnosisId",
                table: "Treatments",
                newName: "IX_Treatments_MedicalCareId");

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_MedicalCares_MedicalCareId",
                table: "Treatments",
                column: "MedicalCareId",
                principalTable: "MedicalCares",
                principalColumn: "CareId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
