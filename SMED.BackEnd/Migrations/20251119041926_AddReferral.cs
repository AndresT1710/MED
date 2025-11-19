using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddReferral : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdditionalNotes",
                table: "MedicalReferrals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AttendedByProfessionalId",
                table: "MedicalReferrals",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AttendedDate",
                table: "MedicalReferrals",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsUrgent",
                table: "MedicalReferrals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "MedicalReferrals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalReferrals_AttendedByProfessionalId",
                table: "MedicalReferrals",
                column: "AttendedByProfessionalId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalReferrals_HealthProfessionals_AttendedByProfessionalId",
                table: "MedicalReferrals",
                column: "AttendedByProfessionalId",
                principalTable: "HealthProfessionals",
                principalColumn: "HealthProfessionalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalReferrals_HealthProfessionals_AttendedByProfessionalId",
                table: "MedicalReferrals");

            migrationBuilder.DropIndex(
                name: "IX_MedicalReferrals_AttendedByProfessionalId",
                table: "MedicalReferrals");

            migrationBuilder.DropColumn(
                name: "AdditionalNotes",
                table: "MedicalReferrals");

            migrationBuilder.DropColumn(
                name: "AttendedByProfessionalId",
                table: "MedicalReferrals");

            migrationBuilder.DropColumn(
                name: "AttendedDate",
                table: "MedicalReferrals");

            migrationBuilder.DropColumn(
                name: "IsUrgent",
                table: "MedicalReferrals");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "MedicalReferrals");
        }
    }
}
