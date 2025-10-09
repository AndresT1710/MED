using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddNewSessions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MedicalDischarge",
                table: "Sessions",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observations",
                table: "Sessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Treatment",
                table: "Sessions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MedicalDischarge",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "Observations",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "Treatment",
                table: "Sessions");
        }
    }
}
