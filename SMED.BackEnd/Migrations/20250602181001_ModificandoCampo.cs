using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class ModificandoCampo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "appearanceDate",
                table: "FamilyHistoryDetails");

            migrationBuilder.AddColumn<int>(
                name: "appearanceAge",
                table: "FamilyHistoryDetails",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "appearanceAge",
                table: "FamilyHistoryDetails");

            migrationBuilder.AddColumn<DateTime>(
                name: "appearanceDate",
                table: "FamilyHistoryDetails",
                type: "datetime2",
                nullable: true);
        }
    }
}
