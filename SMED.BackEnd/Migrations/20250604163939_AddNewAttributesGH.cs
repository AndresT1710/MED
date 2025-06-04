using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddNewAttributesGH : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GynecologicalHistories_Diseases_DiseaseId",
                table: "GynecologicalHistories");

            migrationBuilder.DropIndex(
                name: "IX_GynecologicalHistories_DiseaseId",
                table: "GynecologicalHistories");

            migrationBuilder.DropColumn(
                name: "DiseaseId",
                table: "GynecologicalHistories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiseaseId",
                table: "GynecologicalHistories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GynecologicalHistories_DiseaseId",
                table: "GynecologicalHistories",
                column: "DiseaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_GynecologicalHistories_Diseases_DiseaseId",
                table: "GynecologicalHistories",
                column: "DiseaseId",
                principalTable: "Diseases",
                principalColumn: "DiseaseId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
