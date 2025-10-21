using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesNutricion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CareId",
                table: "FoodPlan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FoodPlan_CareId",
                table: "FoodPlan",
                column: "CareId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodPlan_MedicalCares_CareId",
                table: "FoodPlan",
                column: "CareId",
                principalTable: "MedicalCares",
                principalColumn: "CareId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodPlan_MedicalCares_CareId",
                table: "FoodPlan");

            migrationBuilder.DropIndex(
                name: "IX_FoodPlan_CareId",
                table: "FoodPlan");

            migrationBuilder.DropColumn(
                name: "CareId",
                table: "FoodPlan");
        }
    }
}
