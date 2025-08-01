using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class NewRulesTreatment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PharmacologicalTreatments_Medicines_MedicineId",
                table: "PharmacologicalTreatments");

            migrationBuilder.AddForeignKey(
                name: "FK_PharmacologicalTreatment_Medicine",
                table: "PharmacologicalTreatments",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PharmacologicalTreatment_Medicine",
                table: "PharmacologicalTreatments");

            migrationBuilder.AddForeignKey(
                name: "FK_PharmacologicalTreatments_Medicines_MedicineId",
                table: "PharmacologicalTreatments",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
