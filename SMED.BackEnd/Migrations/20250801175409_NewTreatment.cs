using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class NewTreatment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalDiagnosis_Diseases_DiseaseId",
                table: "MedicalDiagnosis");

            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_Medicines_MedicineId",
                table: "Treatments");

            migrationBuilder.DropIndex(
                name: "IX_Treatments_MedicineId",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "Dose",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "MedicineId",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "ViaAdmission",
                table: "Treatments");

            migrationBuilder.CreateTable(
                name: "NonPharmacologicalTreatments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonPharmacologicalTreatments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NonPharmacologicalTreatments_Treatments_Id",
                        column: x => x.Id,
                        principalTable: "Treatments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PharmacologicalTreatments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Dose = table.Column<int>(type: "int", nullable: false),
                    Frequency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ViaAdmission = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PharmacologicalTreatments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PharmacologicalTreatments_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PharmacologicalTreatments_Treatments_Id",
                        column: x => x.Id,
                        principalTable: "Treatments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PharmacologicalTreatments_MedicineId",
                table: "PharmacologicalTreatments",
                column: "MedicineId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalDiagnosis_Diseases_DiseaseId",
                table: "MedicalDiagnosis",
                column: "DiseaseId",
                principalTable: "Diseases",
                principalColumn: "DiseaseId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalDiagnosis_Diseases_DiseaseId",
                table: "MedicalDiagnosis");

            migrationBuilder.DropTable(
                name: "NonPharmacologicalTreatments");

            migrationBuilder.DropTable(
                name: "PharmacologicalTreatments");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Treatments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Treatments",
                type: "nvarchar(34)",
                maxLength: 34,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Dose",
                table: "Treatments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Duration",
                table: "Treatments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Frequency",
                table: "Treatments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MedicineId",
                table: "Treatments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ViaAdmission",
                table: "Treatments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_MedicineId",
                table: "Treatments",
                column: "MedicineId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalDiagnosis_Diseases_DiseaseId",
                table: "MedicalDiagnosis",
                column: "DiseaseId",
                principalTable: "Diseases",
                principalColumn: "DiseaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_Medicines_MedicineId",
                table: "Treatments",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
