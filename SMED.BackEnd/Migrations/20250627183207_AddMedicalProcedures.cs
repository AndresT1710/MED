using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddMedicalProcedures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Procedures_MedicalCares_MedicalCareId",
                table: "Procedures");

            migrationBuilder.DropIndex(
                name: "IX_Procedures_MedicalCareId",
                table: "Procedures");

            migrationBuilder.DropColumn(
                name: "MedicalCareId",
                table: "Procedures");

            migrationBuilder.CreateTable(
                name: "MedicalProcedures",
                columns: table => new
                {
                    ProcedureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcedureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SpecificProcedureId = table.Column<int>(type: "int", nullable: false),
                    CareId = table.Column<int>(type: "int", nullable: true),
                    HealthProfessionalId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    TreatingPhysicianId = table.Column<int>(type: "int", nullable: true),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalProcedures", x => x.ProcedureId);
                    table.ForeignKey(
                        name: "FK_MedicalProcedures_HealthProfessionals_HealthProfessionalId",
                        column: x => x.HealthProfessionalId,
                        principalTable: "HealthProfessionals",
                        principalColumn: "HealthProfessionalId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalProcedures_HealthProfessionals_TreatingPhysicianId",
                        column: x => x.TreatingPhysicianId,
                        principalTable: "HealthProfessionals",
                        principalColumn: "HealthProfessionalId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalProcedures_MedicalCares_CareId",
                        column: x => x.CareId,
                        principalTable: "MedicalCares",
                        principalColumn: "CareId");
                    table.ForeignKey(
                        name: "FK_MedicalProcedures_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalProcedures_Procedures_SpecificProcedureId",
                        column: x => x.SpecificProcedureId,
                        principalTable: "Procedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalProcedures_CareId",
                table: "MedicalProcedures",
                column: "CareId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalProcedures_HealthProfessionalId",
                table: "MedicalProcedures",
                column: "HealthProfessionalId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalProcedures_PatientId",
                table: "MedicalProcedures",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalProcedures_SpecificProcedureId",
                table: "MedicalProcedures",
                column: "SpecificProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalProcedures_TreatingPhysicianId",
                table: "MedicalProcedures",
                column: "TreatingPhysicianId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalProcedures");

            migrationBuilder.AddColumn<int>(
                name: "MedicalCareId",
                table: "Procedures",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Procedures_MedicalCareId",
                table: "Procedures",
                column: "MedicalCareId");

            migrationBuilder.AddForeignKey(
                name: "FK_Procedures_MedicalCares_MedicalCareId",
                table: "Procedures",
                column: "MedicalCareId",
                principalTable: "MedicalCares",
                principalColumn: "CareId");
        }
    }
}
