using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddNewAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "AllergyHistories");

            migrationBuilder.AddColumn<DateTime>(
                name: "SurgeryDate",
                table: "SurgeryHistories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "appearanceDate",
                table: "FamilyHistoryDetails",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SurgeryDate",
                table: "SurgeryHistories");

            migrationBuilder.DropColumn(
                name: "appearanceDate",
                table: "FamilyHistoryDetails");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AllergyHistories",
                type: "varchar(max)",
                unicode: false,
                nullable: false,
                defaultValue: "");
        }
    }
}
