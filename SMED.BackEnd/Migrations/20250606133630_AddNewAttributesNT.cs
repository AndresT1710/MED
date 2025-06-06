using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddNewAttributesNT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DietaryHabitsHistory",
                columns: table => new
                {
                    DietaryHabitHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalRecordNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietaryHabitsHistory", x => x.DietaryHabitHistoryId);
                    table.ForeignKey(
                        name: "FK_DietaryHabitsHistory_ClinicalHistories_ClinicalHistoryId",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodConsumptionHistory",
                columns: table => new
                {
                    FoodConsumptionHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Hour = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Place = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodConsumptionHistory", x => x.FoodConsumptionHistoryId);
                    table.ForeignKey(
                        name: "FK_FoodConsumptionHistory_ClinicalHistories_ClinicalHistoryId",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodConsumptionHistory_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "FoodId");
                });

            migrationBuilder.CreateTable(
                name: "LifeStyle",
                columns: table => new
                {
                    LifeStyleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeStyle", x => x.LifeStyleId);
                });

            migrationBuilder.CreateTable(
                name: "SleepHabit",
                columns: table => new
                {
                    SleepHabitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SleepHabit", x => x.SleepHabitId);
                });

            migrationBuilder.CreateTable(
                name: "SportsActivities",
                columns: table => new
                {
                    SportActivityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportsActivities", x => x.SportActivityId);
                });

            migrationBuilder.CreateTable(
                name: "WaterConsumptionHistory",
                columns: table => new
                {
                    WaterConsumptionHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalRecordNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Frequency = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaterConsumptionHistory", x => x.WaterConsumptionHistoryId);
                    table.ForeignKey(
                        name: "FK_WaterConsumptionHistory_ClinicalHistories_ClinicalHistoryId",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LifeStyleHistory",
                columns: table => new
                {
                    LifeStyleHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false),
                    LifeStyleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeStyleHistory", x => x.LifeStyleHistoryId);
                    table.ForeignKey(
                        name: "FK_LifeStyleHistory_ClinicalHistories_ClinicalHistoryId",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LifeStyleHistory_LifeStyle_LifeStyleId",
                        column: x => x.LifeStyleId,
                        principalTable: "LifeStyle",
                        principalColumn: "LifeStyleId");
                });

            migrationBuilder.CreateTable(
                name: "SleepHabitHistory",
                columns: table => new
                {
                    HabitSleepHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecordDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SleepHabitId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SleepHabitHistory", x => x.HabitSleepHistoryId);
                    table.ForeignKey(
                        name: "FK_SleepHabitHistory_ClinicalHistories_ClinicalHistoryId",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SleepHabitHistory_SleepHabit_SleepHabitId",
                        column: x => x.SleepHabitId,
                        principalTable: "SleepHabit",
                        principalColumn: "SleepHabitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SportsActivitiesHistory",
                columns: table => new
                {
                    SportActivityHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false),
                    SportActivityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportsActivitiesHistory", x => x.SportActivityHistoryId);
                    table.ForeignKey(
                        name: "FK_SportsActivitiesHistory_ClinicalHistories_ClinicalHistoryId",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SportsActivitiesHistory_SportsActivities_SportActivityId",
                        column: x => x.SportActivityId,
                        principalTable: "SportsActivities",
                        principalColumn: "SportActivityId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DietaryHabitsHistory_ClinicalHistoryId",
                table: "DietaryHabitsHistory",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodConsumptionHistory_ClinicalHistoryId",
                table: "FoodConsumptionHistory",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodConsumptionHistory_FoodId",
                table: "FoodConsumptionHistory",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_LifeStyleHistory_ClinicalHistoryId",
                table: "LifeStyleHistory",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_LifeStyleHistory_LifeStyleId",
                table: "LifeStyleHistory",
                column: "LifeStyleId");

            migrationBuilder.CreateIndex(
                name: "IX_SleepHabitHistory_ClinicalHistoryId",
                table: "SleepHabitHistory",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SleepHabitHistory_SleepHabitId",
                table: "SleepHabitHistory",
                column: "SleepHabitId");

            migrationBuilder.CreateIndex(
                name: "IX_SportsActivitiesHistory_ClinicalHistoryId",
                table: "SportsActivitiesHistory",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SportsActivitiesHistory_SportActivityId",
                table: "SportsActivitiesHistory",
                column: "SportActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_WaterConsumptionHistory_ClinicalHistoryId",
                table: "WaterConsumptionHistory",
                column: "ClinicalHistoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DietaryHabitsHistory");

            migrationBuilder.DropTable(
                name: "FoodConsumptionHistory");

            migrationBuilder.DropTable(
                name: "LifeStyleHistory");

            migrationBuilder.DropTable(
                name: "SleepHabitHistory");

            migrationBuilder.DropTable(
                name: "SportsActivitiesHistory");

            migrationBuilder.DropTable(
                name: "WaterConsumptionHistory");

            migrationBuilder.DropTable(
                name: "LifeStyle");

            migrationBuilder.DropTable(
                name: "SleepHabit");

            migrationBuilder.DropTable(
                name: "SportsActivities");
        }
    }
}
