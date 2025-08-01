using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class NewRulesTreatments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalDiagnosis_Diseases_DiseaseId",
                table: "MedicalDiagnosis");

            migrationBuilder.DropTable(
                name: "DiagnosisTreatment");

            migrationBuilder.CreateTable(
                name: "MedicalDiagnosisTreatment",
                columns: table => new
                {
                    DiagnosesId = table.Column<int>(type: "int", nullable: false),
                    TreatmentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalDiagnosisTreatment", x => new { x.DiagnosesId, x.TreatmentsId });
                    table.ForeignKey(
                        name: "FK_MedicalDiagnosisTreatment_MedicalDiagnosis_DiagnosesId",
                        column: x => x.DiagnosesId,
                        principalTable: "MedicalDiagnosis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalDiagnosisTreatment_Treatments_TreatmentsId",
                        column: x => x.TreatmentsId,
                        principalTable: "Treatments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalDiagnosisTreatment_TreatmentsId",
                table: "MedicalDiagnosisTreatment",
                column: "TreatmentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalDiagnosis_Diseases_DiseaseId",
                table: "MedicalDiagnosis",
                column: "DiseaseId",
                principalTable: "Diseases",
                principalColumn: "DiseaseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalDiagnosis_Diseases_DiseaseId",
                table: "MedicalDiagnosis");

            migrationBuilder.DropTable(
                name: "MedicalDiagnosisTreatment");

            migrationBuilder.CreateTable(
                name: "DiagnosisTreatment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiagnosisId = table.Column<int>(type: "int", nullable: false),
                    TreatmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosisTreatment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiagnosisTreatment_MedicalDiagnosis_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "MedicalDiagnosis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiagnosisTreatment_Treatments_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosisTreatment_DiagnosisId",
                table: "DiagnosisTreatment",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosisTreatment_TreatmentId",
                table: "DiagnosisTreatment",
                column: "TreatmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalDiagnosis_Diseases_DiseaseId",
                table: "MedicalDiagnosis",
                column: "DiseaseId",
                principalTable: "Diseases",
                principalColumn: "DiseaseId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
