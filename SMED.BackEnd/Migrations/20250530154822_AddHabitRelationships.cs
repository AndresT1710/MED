using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddHabitRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GynecologicalHistories_Diseases_DiseaseId",
                table: "GynecologicalHistories");

            migrationBuilder.CreateTable(
                name: "Habits",
                columns: table => new
                {
                    HabitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habits", x => x.HabitId);
                });

            migrationBuilder.CreateTable(
                name: "HabitHistories",
                columns: table => new
                {
                    HabitHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecordDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HabitId = table.Column<int>(type: "int", nullable: false),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitHistories", x => x.HabitHistoryId);
                    table.ForeignKey(
                        name: "FK_HabitHistory_ClinicalHistory",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HabitHistory_Habits",
                        column: x => x.HabitId,
                        principalTable: "Habits",
                        principalColumn: "HabitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HabitHistories_ClinicalHistoryId",
                table: "HabitHistories",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_HabitHistories_HabitId",
                table: "HabitHistories",
                column: "HabitId");

            migrationBuilder.AddForeignKey(
                name: "FK_GynecologicalHistories_Diseases_DiseaseId",
                table: "GynecologicalHistories",
                column: "DiseaseId",
                principalTable: "Diseases",
                principalColumn: "DiseaseId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GynecologicalHistories_Diseases_DiseaseId",
                table: "GynecologicalHistories");

            migrationBuilder.DropTable(
                name: "HabitHistories");

            migrationBuilder.DropTable(
                name: "Habits");

            migrationBuilder.AddForeignKey(
                name: "FK_GynecologicalHistories_Diseases_DiseaseId",
                table: "GynecologicalHistories",
                column: "DiseaseId",
                principalTable: "Diseases",
                principalColumn: "DiseaseId");
        }
    }
}
