using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddNewAttributesObH : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ObstetricHistory_Disease",
                table: "ObstetricHistories");

            migrationBuilder.DropIndex(
                name: "IX_ObstetricHistories_DiseaseId",
                table: "ObstetricHistories");

            migrationBuilder.DropColumn(
                name: "DiseaseId",
                table: "ObstetricHistories");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpectedDeliveryDate",
                table: "ObstetricHistories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GestionalAge",
                table: "ObstetricHistories",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpectedDeliveryDate",
                table: "ObstetricHistories");

            migrationBuilder.DropColumn(
                name: "GestionalAge",
                table: "ObstetricHistories");

            migrationBuilder.AddColumn<int>(
                name: "DiseaseId",
                table: "ObstetricHistories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ObstetricHistories_DiseaseId",
                table: "ObstetricHistories",
                column: "DiseaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ObstetricHistory_Disease",
                table: "ObstetricHistories",
                column: "DiseaseId",
                principalTable: "Diseases",
                principalColumn: "DiseaseId");
        }
    }
}
