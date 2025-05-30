using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddFamilyHistoryRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamilyHistoryDetails_Diseases_DiseaseId",
                table: "FamilyHistoryDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyHistoryDetails_Diseases_DiseaseId",
                table: "FamilyHistoryDetails",
                column: "DiseaseId",
                principalTable: "Diseases",
                principalColumn: "DiseaseId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamilyHistoryDetails_Diseases_DiseaseId",
                table: "FamilyHistoryDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyHistoryDetails_Diseases_DiseaseId",
                table: "FamilyHistoryDetails",
                column: "DiseaseId",
                principalTable: "Diseases",
                principalColumn: "DiseaseId");
        }
    }
}
