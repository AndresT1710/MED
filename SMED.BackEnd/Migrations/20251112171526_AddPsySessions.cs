using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddPsySessions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advances_Sessions_SessionId",
                table: "Advances");

            migrationBuilder.DropIndex(
                name: "IX_PsychologicalDiagnoses_MedicalCareId",
                table: "PsychologicalDiagnoses");

            migrationBuilder.DropColumn(
                name: "AssignedTasks",
                table: "TherapeuticPlans");

            migrationBuilder.RenameColumn(
                name: "Task",
                table: "Advances",
                newName: "Indications");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Activities",
                newName: "NameActivity");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfSessions",
                table: "TherapeuticPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DiagnosisMotivation",
                table: "PsychologicalDiagnoses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Differential",
                table: "PsychologicalDiagnoses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "SessionId",
                table: "Advances",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PsychologySessionId",
                table: "Advances",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateActivity",
                table: "Activities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PsychologySessionId",
                table: "Activities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PsychologySessions",
                columns: table => new
                {
                    PsychologySessionsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MedicalDischarge = table.Column<bool>(type: "bit", nullable: true),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VoluntaryRegistrationLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SummarySession = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalCareId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PsychologySessions", x => x.PsychologySessionsId);
                    table.ForeignKey(
                        name: "FK_PsychologySessions_MedicalCares_MedicalCareId",
                        column: x => x.MedicalCareId,
                        principalTable: "MedicalCares",
                        principalColumn: "CareId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PsychologicalDiagnoses_MedicalCareId",
                table: "PsychologicalDiagnoses",
                column: "MedicalCareId");

            migrationBuilder.CreateIndex(
                name: "IX_Advances_PsychologySessionId",
                table: "Advances",
                column: "PsychologySessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_PsychologySessionId",
                table: "Activities",
                column: "PsychologySessionId");

            migrationBuilder.CreateIndex(
                name: "IX_PsychologySessions_MedicalCareId",
                table: "PsychologySessions",
                column: "MedicalCareId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_PsychologySessions_PsychologySessionId",
                table: "Activities",
                column: "PsychologySessionId",
                principalTable: "PsychologySessions",
                principalColumn: "PsychologySessionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advances_PsychologySessions_PsychologySessionId",
                table: "Advances",
                column: "PsychologySessionId",
                principalTable: "PsychologySessions",
                principalColumn: "PsychologySessionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advances_Sessions_SessionId",
                table: "Advances",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "SessionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_PsychologySessions_PsychologySessionId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Advances_PsychologySessions_PsychologySessionId",
                table: "Advances");

            migrationBuilder.DropForeignKey(
                name: "FK_Advances_Sessions_SessionId",
                table: "Advances");

            migrationBuilder.DropTable(
                name: "PsychologySessions");

            migrationBuilder.DropIndex(
                name: "IX_PsychologicalDiagnoses_MedicalCareId",
                table: "PsychologicalDiagnoses");

            migrationBuilder.DropIndex(
                name: "IX_Advances_PsychologySessionId",
                table: "Advances");

            migrationBuilder.DropIndex(
                name: "IX_Activities_PsychologySessionId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "NumberOfSessions",
                table: "TherapeuticPlans");

            migrationBuilder.DropColumn(
                name: "DiagnosisMotivation",
                table: "PsychologicalDiagnoses");

            migrationBuilder.DropColumn(
                name: "Differential",
                table: "PsychologicalDiagnoses");

            migrationBuilder.DropColumn(
                name: "PsychologySessionId",
                table: "Advances");

            migrationBuilder.DropColumn(
                name: "DateActivity",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "PsychologySessionId",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "Indications",
                table: "Advances",
                newName: "Task");

            migrationBuilder.RenameColumn(
                name: "NameActivity",
                table: "Activities",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "AssignedTasks",
                table: "TherapeuticPlans",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "SessionId",
                table: "Advances",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PsychologicalDiagnoses_MedicalCareId",
                table: "PsychologicalDiagnoses",
                column: "MedicalCareId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Advances_Sessions_SessionId",
                table: "Advances",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "SessionsId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
