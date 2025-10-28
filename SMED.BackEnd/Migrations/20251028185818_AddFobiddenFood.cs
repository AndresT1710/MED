using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddFobiddenFood : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodPlans_RecommendedFoods_RecommendedFoodId",
                table: "FoodPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodPlans_Restrictions_RestrictionId",
                table: "FoodPlans");

            migrationBuilder.DropTable(
                name: "RecommendedFoods");

            migrationBuilder.DropTable(
                name: "Restrictions");

            migrationBuilder.DropIndex(
                name: "IX_FoodPlans_RestrictionId",
                table: "FoodPlans");

            migrationBuilder.RenameColumn(
                name: "RestrictionId",
                table: "FoodPlans",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "RecommendedFoodId",
                table: "FoodPlans",
                newName: "FoodId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodPlans_RecommendedFoodId",
                table: "FoodPlans",
                newName: "IX_FoodPlans_FoodId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "FoodPlans",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Frequency",
                table: "FoodPlans",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Indications",
                table: "FoodPlans",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ForbiddenFoods",
                columns: table => new
                {
                    ForbiddenFoodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    CareId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForbiddenFoods", x => x.ForbiddenFoodId);
                    table.ForeignKey(
                        name: "FK_ForbiddenFoods_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "FoodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ForbiddenFoods_MedicalCares_CareId",
                        column: x => x.CareId,
                        principalTable: "MedicalCares",
                        principalColumn: "CareId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ForbiddenFoods_CareId",
                table: "ForbiddenFoods",
                column: "CareId");

            migrationBuilder.CreateIndex(
                name: "IX_ForbiddenFoods_FoodId",
                table: "ForbiddenFoods",
                column: "FoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodPlans_Foods_FoodId",
                table: "FoodPlans",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "FoodId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodPlans_Foods_FoodId",
                table: "FoodPlans");

            migrationBuilder.DropTable(
                name: "ForbiddenFoods");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "FoodPlans");

            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "FoodPlans");

            migrationBuilder.DropColumn(
                name: "Indications",
                table: "FoodPlans");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "FoodPlans",
                newName: "RestrictionId");

            migrationBuilder.RenameColumn(
                name: "FoodId",
                table: "FoodPlans",
                newName: "RecommendedFoodId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodPlans_FoodId",
                table: "FoodPlans",
                newName: "IX_FoodPlans_RecommendedFoodId");

            migrationBuilder.CreateTable(
                name: "RecommendedFoods",
                columns: table => new
                {
                    RecommendedFoodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Frequency = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
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
                name: "Restrictions",
                columns: table => new
                {
                    RestrictionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restrictions", x => x.RestrictionId);
                    table.ForeignKey(
                        name: "FK_Restrictions_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "FoodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodPlans_RestrictionId",
                table: "FoodPlans",
                column: "RestrictionId",
                unique: true,
                filter: "[RestrictionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RecommendedFoods_FoodId",
                table: "RecommendedFoods",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Restrictions_FoodId",
                table: "Restrictions",
                column: "FoodId");

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
        }
    }
}
