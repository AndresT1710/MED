using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddRecommendedFoodsFoodPlanRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecommendedFoods",
                columns: table => new
                {
                    RecommendedFoodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Frequency = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecommendedFoods", x => x.RecommendedFoodId);
                    table.ForeignKey(
                        name: "FK_RecommendedFoods_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "FoodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Restriction",
                columns: table => new
                {
                    RestrictionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restriction", x => x.RestrictionId);
                    table.ForeignKey(
                        name: "FK_Restriction_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "FoodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodPlan",
                columns: table => new
                {
                    FoodPlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RestrictionId = table.Column<int>(type: "int", nullable: true),
                    RecommendedFoodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodPlan", x => x.FoodPlanId);
                    table.ForeignKey(
                        name: "FK_FoodPlan_RecommendedFoods_RecommendedFoodId",
                        column: x => x.RecommendedFoodId,
                        principalTable: "RecommendedFoods",
                        principalColumn: "RecommendedFoodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodPlan_Restriction_RestrictionId",
                        column: x => x.RestrictionId,
                        principalTable: "Restriction",
                        principalColumn: "RestrictionId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodPlan_RecommendedFoodId",
                table: "FoodPlan",
                column: "RecommendedFoodId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodPlan_RestrictionId",
                table: "FoodPlan",
                column: "RestrictionId",
                unique: true,
                filter: "[RestrictionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RecommendedFoods_FoodId",
                table: "RecommendedFoods",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Restriction_FoodId",
                table: "Restriction",
                column: "FoodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodPlan");

            migrationBuilder.DropTable(
                name: "RecommendedFoods");

            migrationBuilder.DropTable(
                name: "Restriction");
        }
    }
}
