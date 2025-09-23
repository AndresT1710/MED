using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddToxicHabits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToxicHabitHistories");

            migrationBuilder.CreateTable(
                name: "ToxicHabitHistory",
                columns: table => new
                {
                    ToxicHabitHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryNumber = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    RecordDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ToxicHabitId = table.Column<int>(type: "int", nullable: true),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToxicHabitHistory", x => x.ToxicHabitHistoryId);
                    table.ForeignKey(
                        name: "FK_ToxicHabitHistory_ClinicalHistory",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ToxicHabitHistory_ToxicHabit",
                        column: x => x.ToxicHabitId,
                        principalTable: "ToxicHabits",
                        principalColumn: "ToxicHabitId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToxicHabitHistory_ClinicalHistoryId",
                table: "ToxicHabitHistory",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ToxicHabitHistory_ToxicHabitId",
                table: "ToxicHabitHistory",
                column: "ToxicHabitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToxicHabitHistory");

            migrationBuilder.CreateTable(
                name: "ToxicHabitHistories",
                columns: table => new
                {
                    ToxicHabitBackgroundId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false),
                    ToxicHabitId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    HistoryNumber = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    RecordDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToxicHabitHistories", x => x.ToxicHabitBackgroundId);
                    table.ForeignKey(
                        name: "FK_ToxicHabitBackground_ClinicalHistory",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ToxicHabitBackground_ToxicHabit",
                        column: x => x.ToxicHabitId,
                        principalTable: "ToxicHabits",
                        principalColumn: "ToxicHabitId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToxicHabitHistories_ClinicalHistoryId",
                table: "ToxicHabitHistories",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ToxicHabitHistories_ToxicHabitId",
                table: "ToxicHabitHistories",
                column: "ToxicHabitId");
        }
    }
}
