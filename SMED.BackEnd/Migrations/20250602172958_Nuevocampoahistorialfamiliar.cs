using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class Nuevocampoahistorialfamiliar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SurgeryDate",
                table: "SurgeryHistories",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RelationshipId",
                table: "FamilyHistoryDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FamilyHistoryDetails_RelationshipId",
                table: "FamilyHistoryDetails",
                column: "RelationshipId");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyHistoryDetails_Relationships_RelationshipId",
                table: "FamilyHistoryDetails",
                column: "RelationshipId",
                principalTable: "Relationships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamilyHistoryDetails_Relationships_RelationshipId",
                table: "FamilyHistoryDetails");

            migrationBuilder.DropIndex(
                name: "IX_FamilyHistoryDetails_RelationshipId",
                table: "FamilyHistoryDetails");

            migrationBuilder.DropColumn(
                name: "RelationshipId",
                table: "FamilyHistoryDetails");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SurgeryDate",
                table: "SurgeryHistories",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);
        }
    }
}
