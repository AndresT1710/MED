using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddNutricionTabless : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodPlan_MedicalCares_CareId",
                table: "FoodPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodPlan_RecommendedFoods_RecommendedFoodId",
                table: "FoodPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodPlan_Restriction_RestrictionId",
                table: "FoodPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_Restriction_Foods_FoodId",
                table: "Restriction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Restriction",
                table: "Restriction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodPlan",
                table: "FoodPlan");

            migrationBuilder.RenameTable(
                name: "Restriction",
                newName: "Restrictions");

            migrationBuilder.RenameTable(
                name: "FoodPlan",
                newName: "FoodPlans");

            migrationBuilder.RenameIndex(
                name: "IX_Restriction_FoodId",
                table: "Restrictions",
                newName: "IX_Restrictions_FoodId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodPlan_RestrictionId",
                table: "FoodPlans",
                newName: "IX_FoodPlans_RestrictionId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodPlan_RecommendedFoodId",
                table: "FoodPlans",
                newName: "IX_FoodPlans_RecommendedFoodId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodPlan_CareId",
                table: "FoodPlans",
                newName: "IX_FoodPlans_CareId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Restrictions",
                table: "Restrictions",
                column: "RestrictionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodPlans",
                table: "FoodPlans",
                column: "FoodPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodPlans_MedicalCares_CareId",
                table: "FoodPlans",
                column: "CareId",
                principalTable: "MedicalCares",
                principalColumn: "CareId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodPlans_RecommendedFoods_RecommendedFoodId",
                table: "FoodPlans",
                column: "RecommendedFoodId",
                principalTable: "RecommendedFoods",
                principalColumn: "RecommendedFoodId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodPlans_Restrictions_RestrictionId",
                table: "FoodPlans",
                column: "RestrictionId",
                principalTable: "Restrictions",
                principalColumn: "RestrictionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restrictions_Foods_FoodId",
                table: "Restrictions",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "FoodId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodPlans_MedicalCares_CareId",
                table: "FoodPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodPlans_RecommendedFoods_RecommendedFoodId",
                table: "FoodPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodPlans_Restrictions_RestrictionId",
                table: "FoodPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_Restrictions_Foods_FoodId",
                table: "Restrictions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Restrictions",
                table: "Restrictions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodPlans",
                table: "FoodPlans");

            migrationBuilder.RenameTable(
                name: "Restrictions",
                newName: "Restriction");

            migrationBuilder.RenameTable(
                name: "FoodPlans",
                newName: "FoodPlan");

            migrationBuilder.RenameIndex(
                name: "IX_Restrictions_FoodId",
                table: "Restriction",
                newName: "IX_Restriction_FoodId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodPlans_RestrictionId",
                table: "FoodPlan",
                newName: "IX_FoodPlan_RestrictionId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodPlans_RecommendedFoodId",
                table: "FoodPlan",
                newName: "IX_FoodPlan_RecommendedFoodId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodPlans_CareId",
                table: "FoodPlan",
                newName: "IX_FoodPlan_CareId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Restriction",
                table: "Restriction",
                column: "RestrictionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodPlan",
                table: "FoodPlan",
                column: "FoodPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodPlan_MedicalCares_CareId",
                table: "FoodPlan",
                column: "CareId",
                principalTable: "MedicalCares",
                principalColumn: "CareId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodPlan_RecommendedFoods_RecommendedFoodId",
                table: "FoodPlan",
                column: "RecommendedFoodId",
                principalTable: "RecommendedFoods",
                principalColumn: "RecommendedFoodId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodPlan_Restriction_RestrictionId",
                table: "FoodPlan",
                column: "RestrictionId",
                principalTable: "Restriction",
                principalColumn: "RestrictionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restriction_Foods_FoodId",
                table: "Restriction",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "FoodId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
