using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddNewDiagnosisType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diagnosis_DiagnosticTypes_DiagnosticTypeId",
                table: "Diagnosis");

            migrationBuilder.DropForeignKey(
                name: "FK_Diagnosis_Diseases_DiseaseId",
                table: "Diagnosis");

            migrationBuilder.DropForeignKey(
                name: "FK_Diagnosis_MedicalCares_MedicalCareId",
                table: "Diagnosis");

            migrationBuilder.DropForeignKey(
                name: "FK_DiagnosisTreatment_Diagnosis_DiagnosisId",
                table: "DiagnosisTreatment");

            migrationBuilder.DropForeignKey(
                name: "FK_Interconsultations_Diagnosis_DiagnosisId",
                table: "Interconsultations");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDiagnosis_Diagnosis_DiagnosisId",
                table: "OrderDiagnosis");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Diagnosis",
                table: "Diagnosis");

            migrationBuilder.RenameTable(
                name: "Diagnosis",
                newName: "MedicalDiagnosis");

            migrationBuilder.RenameIndex(
                name: "IX_Diagnosis_MedicalCareId",
                table: "MedicalDiagnosis",
                newName: "IX_MedicalDiagnosis_MedicalCareId");

            migrationBuilder.RenameIndex(
                name: "IX_Diagnosis_DiseaseId",
                table: "MedicalDiagnosis",
                newName: "IX_MedicalDiagnosis_DiseaseId");

            migrationBuilder.RenameIndex(
                name: "IX_Diagnosis_DiagnosticTypeId",
                table: "MedicalDiagnosis",
                newName: "IX_MedicalDiagnosis_DiagnosticTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicalDiagnosis",
                table: "MedicalDiagnosis",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosisTreatment_MedicalDiagnosis_DiagnosisId",
                table: "DiagnosisTreatment",
                column: "DiagnosisId",
                principalTable: "MedicalDiagnosis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Interconsultations_MedicalDiagnosis_DiagnosisId",
                table: "Interconsultations",
                column: "DiagnosisId",
                principalTable: "MedicalDiagnosis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalDiagnosis_DiagnosticTypes_DiagnosticTypeId",
                table: "MedicalDiagnosis",
                column: "DiagnosticTypeId",
                principalTable: "DiagnosticTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalDiagnosis_Diseases_DiseaseId",
                table: "MedicalDiagnosis",
                column: "DiseaseId",
                principalTable: "Diseases",
                principalColumn: "DiseaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalDiagnosis_MedicalCares_MedicalCareId",
                table: "MedicalDiagnosis",
                column: "MedicalCareId",
                principalTable: "MedicalCares",
                principalColumn: "CareId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDiagnosis_MedicalDiagnosis_DiagnosisId",
                table: "OrderDiagnosis",
                column: "DiagnosisId",
                principalTable: "MedicalDiagnosis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiagnosisTreatment_MedicalDiagnosis_DiagnosisId",
                table: "DiagnosisTreatment");

            migrationBuilder.DropForeignKey(
                name: "FK_Interconsultations_MedicalDiagnosis_DiagnosisId",
                table: "Interconsultations");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalDiagnosis_DiagnosticTypes_DiagnosticTypeId",
                table: "MedicalDiagnosis");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalDiagnosis_Diseases_DiseaseId",
                table: "MedicalDiagnosis");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalDiagnosis_MedicalCares_MedicalCareId",
                table: "MedicalDiagnosis");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDiagnosis_MedicalDiagnosis_DiagnosisId",
                table: "OrderDiagnosis");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicalDiagnosis",
                table: "MedicalDiagnosis");

            migrationBuilder.RenameTable(
                name: "MedicalDiagnosis",
                newName: "Diagnosis");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalDiagnosis_MedicalCareId",
                table: "Diagnosis",
                newName: "IX_Diagnosis_MedicalCareId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalDiagnosis_DiseaseId",
                table: "Diagnosis",
                newName: "IX_Diagnosis_DiseaseId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalDiagnosis_DiagnosticTypeId",
                table: "Diagnosis",
                newName: "IX_Diagnosis_DiagnosticTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Diagnosis",
                table: "Diagnosis",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnosis_DiagnosticTypes_DiagnosticTypeId",
                table: "Diagnosis",
                column: "DiagnosticTypeId",
                principalTable: "DiagnosticTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnosis_Diseases_DiseaseId",
                table: "Diagnosis",
                column: "DiseaseId",
                principalTable: "Diseases",
                principalColumn: "DiseaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnosis_MedicalCares_MedicalCareId",
                table: "Diagnosis",
                column: "MedicalCareId",
                principalTable: "MedicalCares",
                principalColumn: "CareId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosisTreatment_Diagnosis_DiagnosisId",
                table: "DiagnosisTreatment",
                column: "DiagnosisId",
                principalTable: "Diagnosis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Interconsultations_Diagnosis_DiagnosisId",
                table: "Interconsultations",
                column: "DiagnosisId",
                principalTable: "Diagnosis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDiagnosis_Diagnosis_DiagnosisId",
                table: "OrderDiagnosis",
                column: "DiagnosisId",
                principalTable: "Diagnosis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
