using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddNewRulesMedicalCareMedicalReferral : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MedicalReferrals_MedicalCareId",
                table: "MedicalReferrals");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalReferrals_MedicalCareId",
                table: "MedicalReferrals",
                column: "MedicalCareId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MedicalReferrals_MedicalCareId",
                table: "MedicalReferrals");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalReferrals_MedicalCareId",
                table: "MedicalReferrals",
                column: "MedicalCareId",
                unique: true);
        }
    }
}
