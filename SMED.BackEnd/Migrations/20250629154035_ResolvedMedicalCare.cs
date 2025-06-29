using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class ResolvedMedicalCare : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalProcedures_MedicalCares_CareId",
                table: "MedicalProcedures");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalProcedures_MedicalCares_CareId",
                table: "MedicalProcedures",
                column: "CareId",
                principalTable: "MedicalCares",
                principalColumn: "CareId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalProcedures_MedicalCares_CareId",
                table: "MedicalProcedures");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalProcedures_MedicalCares_CareId",
                table: "MedicalProcedures",
                column: "CareId",
                principalTable: "MedicalCares",
                principalColumn: "CareId");
        }
    }
}
