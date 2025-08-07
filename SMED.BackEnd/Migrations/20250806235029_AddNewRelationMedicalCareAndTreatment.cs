using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddNewRelationMedicalCareAndTreatment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalDiagnosisTreatment");

            migrationBuilder.AddColumn<int>(
                name: "MedicalCareId",
                table: "Treatments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_MedicalCareId",
                table: "Treatments",
                column: "MedicalCareId");

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_MedicalCares_MedicalCareId",
                table: "Treatments",
                column: "MedicalCareId",
                principalTable: "MedicalCares",
                principalColumn: "CareId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_MedicalCares_MedicalCareId",
                table: "Treatments");

            migrationBuilder.DropIndex(
                name: "IX_Treatments_MedicalCareId",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "MedicalCareId",
                table: "Treatments");

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
        }
    }
}
