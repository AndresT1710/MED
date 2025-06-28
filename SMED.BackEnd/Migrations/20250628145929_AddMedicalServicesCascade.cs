using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddMedicalServicesCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalServices_MedicalCares_CareId",
                table: "MedicalServices");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalServices_MedicalCares_CareId",
                table: "MedicalServices",
                column: "CareId",
                principalTable: "MedicalCares",
                principalColumn: "CareId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalServices_MedicalCares_CareId",
                table: "MedicalServices");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalServices_MedicalCares_CareId",
                table: "MedicalServices",
                column: "CareId",
                principalTable: "MedicalCares",
                principalColumn: "CareId");
        }
    }
}
