using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class Nuevasparapsico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrentProblemHistories",
                columns: table => new
                {
                    CurrentProblemHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryNumber = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false),
                    AppearanceEvolution = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    TriggeringFactors = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    FrequencyIntensitySymptoms = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Impact = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentProblemHistories", x => x.CurrentProblemHistoryId);
                    table.ForeignKey(
                        name: "FK_CurrentProblemHistory_ClinicalHistory",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicationHistories",
                columns: table => new
                {
                    MedicationHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryNumber = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    MedicineId = table.Column<int>(type: "int", nullable: false),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false),
                    ConsumptionDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicationHistories", x => x.MedicationHistoryId);
                    table.ForeignKey(
                        name: "FK_MedicationHistory_ClinicalHistory",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicationHistory_Medicine",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PsychopsychiatricHistories",
                columns: table => new
                {
                    PsychopsychiatricHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryNumber = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Actor = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    HistoryDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    HistoryState = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PsychopsychiatricHistories", x => x.PsychopsychiatricHistoryId);
                    table.ForeignKey(
                        name: "FK_PsychopsychiatricHistory_ClinicalHistory",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PsychosexualHistories",
                columns: table => new
                {
                    PsychosexualHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryNumber = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PsychosexualHistories", x => x.PsychosexualHistoryId);
                    table.ForeignKey(
                        name: "FK_PsychosexualHistory_ClinicalHistory",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkHistories",
                columns: table => new
                {
                    WorkHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryNumber = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false),
                    Experience = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Stability = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    SatisfactionLevel = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkHistories", x => x.WorkHistoryId);
                    table.ForeignKey(
                        name: "FK_WorkHistory_ClinicalHistory",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrentProblemHistories_ClinicalHistoryId",
                table: "CurrentProblemHistories",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationHistories_ClinicalHistoryId",
                table: "MedicationHistories",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationHistories_MedicineId",
                table: "MedicationHistories",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_PsychopsychiatricHistories_ClinicalHistoryId",
                table: "PsychopsychiatricHistories",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PsychosexualHistories_ClinicalHistoryId",
                table: "PsychosexualHistories",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkHistories_ClinicalHistoryId",
                table: "WorkHistories",
                column: "ClinicalHistoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrentProblemHistories");

            migrationBuilder.DropTable(
                name: "MedicationHistories");

            migrationBuilder.DropTable(
                name: "PsychopsychiatricHistories");

            migrationBuilder.DropTable(
                name: "PsychosexualHistories");

            migrationBuilder.DropTable(
                name: "WorkHistories");
        }
    }
}
