using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddEarlyStimulationTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EarlyStimulationEvolutionTests",
                columns: table => new
                {
                    TestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Age = table.Column<int>(type: "int", nullable: true),
                    GrossMotorSkills = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FineMotorSkills = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HearingAndLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SocialPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Total = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalCareId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EarlyStimulationEvolutionTests", x => x.TestId);
                    table.ForeignKey(
                        name: "FK_EarlyStimulationEvolutionTests_MedicalCares_MedicalCareId",
                        column: x => x.MedicalCareId,
                        principalTable: "MedicalCares",
                        principalColumn: "CareId");
                });

            migrationBuilder.CreateTable(
                name: "EarlyStimulationSessions",
                columns: table => new
                {
                    SessionsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Treatment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalDischarge = table.Column<bool>(type: "bit", nullable: true),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalCareId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EarlyStimulationSessions", x => x.SessionsId);
                    table.ForeignKey(
                        name: "FK_EarlyStimulationSessions_MedicalCares_MedicalCareId",
                        column: x => x.MedicalCareId,
                        principalTable: "MedicalCares",
                        principalColumn: "CareId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EarlyStimulationEvolutionTests_MedicalCareId",
                table: "EarlyStimulationEvolutionTests",
                column: "MedicalCareId");

            migrationBuilder.CreateIndex(
                name: "IX_EarlyStimulationSessions_MedicalCareId",
                table: "EarlyStimulationSessions",
                column: "MedicalCareId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EarlyStimulationEvolutionTests");

            migrationBuilder.DropTable(
                name: "EarlyStimulationSessions");
        }
    }
}
