using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityPsychological : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Advances",
                columns: table => new
                {
                    AdvanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    Task = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advances", x => x.AdvanceId);
                    table.ForeignKey(
                        name: "FK_Advances_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "SessionsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PsychologicalDiagnoses",
                columns: table => new
                {
                    PsychologicalDiagnosisId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalCareId = table.Column<int>(type: "int", nullable: false),
                    CIE10 = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DiagnosticTypeId = table.Column<int>(type: "int", nullable: false),
                    Denomination = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PsychologicalDiagnoses", x => x.PsychologicalDiagnosisId);
                    table.ForeignKey(
                        name: "FK_PsychologicalDiagnoses_DiagnosticTypes_DiagnosticTypeId",
                        column: x => x.DiagnosticTypeId,
                        principalTable: "DiagnosticTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PsychologicalDiagnoses_MedicalCares_MedicalCareId",
                        column: x => x.MedicalCareId,
                        principalTable: "MedicalCares",
                        principalColumn: "CareId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypesOfMentalFunctions",
                columns: table => new
                {
                    TypeOfMentalFunctionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesOfMentalFunctions", x => x.TypeOfMentalFunctionId);
                });

            migrationBuilder.CreateTable(
                name: "TherapeuticPlans",
                columns: table => new
                {
                    TherapeuticPlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PsychologicalDiagnosisId = table.Column<int>(type: "int", nullable: false),
                    CaseSummary = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    TherapeuticObjective = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    StrategyApproach = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    AssignedTasks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TherapeuticPlans", x => x.TherapeuticPlanId);
                    table.ForeignKey(
                        name: "FK_TherapeuticPlans_PsychologicalDiagnoses_PsychologicalDiagnosisId",
                        column: x => x.PsychologicalDiagnosisId,
                        principalTable: "PsychologicalDiagnoses",
                        principalColumn: "PsychologicalDiagnosisId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MentalFunctions",
                columns: table => new
                {
                    MentalFunctionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MentalFunctionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfMentalFunctionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MentalFunctions", x => x.MentalFunctionId);
                    table.ForeignKey(
                        name: "FK_MentalFunctions_TypesOfMentalFunctions_TypeOfMentalFunctionId",
                        column: x => x.TypeOfMentalFunctionId,
                        principalTable: "TypesOfMentalFunctions",
                        principalColumn: "TypeOfMentalFunctionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MentalFunctionsPsychologies",
                columns: table => new
                {
                    MentalFunctionsPsychologyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalCareId = table.Column<int>(type: "int", nullable: true),
                    MentalFunctionId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MentalFunctionsPsychologies", x => x.MentalFunctionsPsychologyId);
                    table.ForeignKey(
                        name: "FK_MentalFunctionsPsychologies_MedicalCares_MedicalCareId",
                        column: x => x.MedicalCareId,
                        principalTable: "MedicalCares",
                        principalColumn: "CareId");
                    table.ForeignKey(
                        name: "FK_MentalFunctionsPsychologies_MentalFunctions_MentalFunctionId",
                        column: x => x.MentalFunctionId,
                        principalTable: "MentalFunctions",
                        principalColumn: "MentalFunctionId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Advances_SessionId",
                table: "Advances",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_MentalFunctions_TypeOfMentalFunctionId",
                table: "MentalFunctions",
                column: "TypeOfMentalFunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_MentalFunctionsPsychologies_MedicalCareId",
                table: "MentalFunctionsPsychologies",
                column: "MedicalCareId");

            migrationBuilder.CreateIndex(
                name: "IX_MentalFunctionsPsychologies_MentalFunctionId",
                table: "MentalFunctionsPsychologies",
                column: "MentalFunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_PsychologicalDiagnoses_DiagnosticTypeId",
                table: "PsychologicalDiagnoses",
                column: "DiagnosticTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PsychologicalDiagnoses_MedicalCareId",
                table: "PsychologicalDiagnoses",
                column: "MedicalCareId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TherapeuticPlans_PsychologicalDiagnosisId",
                table: "TherapeuticPlans",
                column: "PsychologicalDiagnosisId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advances");

            migrationBuilder.DropTable(
                name: "MentalFunctionsPsychologies");

            migrationBuilder.DropTable(
                name: "TherapeuticPlans");

            migrationBuilder.DropTable(
                name: "MentalFunctions");

            migrationBuilder.DropTable(
                name: "PsychologicalDiagnoses");

            migrationBuilder.DropTable(
                name: "TypesOfMentalFunctions");
        }
    }
}
