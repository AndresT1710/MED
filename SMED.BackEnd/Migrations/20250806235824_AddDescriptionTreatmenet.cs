using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionTreatmenet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "NonPharmacologicalTreatments");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Treatments",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Treatments");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "NonPharmacologicalTreatments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
