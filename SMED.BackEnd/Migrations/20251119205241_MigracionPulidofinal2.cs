using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class MigracionPulidofinal2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalReferrals_Locations_LocationId",
                table: "MedicalReferrals");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "MedicalReferrals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalReferrals_Locations_LocationId",
                table: "MedicalReferrals",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalReferrals_Locations_LocationId",
                table: "MedicalReferrals");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "MedicalReferrals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalReferrals_Locations_LocationId",
                table: "MedicalReferrals",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
