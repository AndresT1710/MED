using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddMedicalServicess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HealthProfessionalId",
                table: "MedicalServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "MedicalServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalServices_HealthProfessionalId",
                table: "MedicalServices",
                column: "HealthProfessionalId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalServices_PatientId",
                table: "MedicalServices",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalServices_HealthProfessionals_HealthProfessionalId",
                table: "MedicalServices",
                column: "HealthProfessionalId",
                principalTable: "HealthProfessionals",
                principalColumn: "HealthProfessionalId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalServices_Patients_PatientId",
                table: "MedicalServices",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalServices_HealthProfessionals_HealthProfessionalId",
                table: "MedicalServices");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalServices_Patients_PatientId",
                table: "MedicalServices");

            migrationBuilder.DropIndex(
                name: "IX_MedicalServices_HealthProfessionalId",
                table: "MedicalServices");

            migrationBuilder.DropIndex(
                name: "IX_MedicalServices_PatientId",
                table: "MedicalServices");

            migrationBuilder.DropColumn(
                name: "HealthProfessionalId",
                table: "MedicalServices");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "MedicalServices");
        }
    }
}
