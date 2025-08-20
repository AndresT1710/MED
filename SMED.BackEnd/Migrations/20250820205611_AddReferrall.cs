using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddReferrall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalReferrals_Services_ServiceId",
                table: "MedicalReferrals");

            migrationBuilder.DropIndex(
                name: "IX_MedicalReferrals_ServiceId",
                table: "MedicalReferrals");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "MedicalReferrals");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "MedicalReferrals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalReferrals_ServiceId",
                table: "MedicalReferrals",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalReferrals_Services_ServiceId",
                table: "MedicalReferrals",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
