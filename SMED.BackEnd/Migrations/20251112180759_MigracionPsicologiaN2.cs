using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class MigracionPsicologiaN2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Differential",
                table: "PsychologicalDiagnoses",
                newName: "DifferentialDiagnosis");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DifferentialDiagnosis",
                table: "PsychologicalDiagnoses",
                newName: "Differential");
        }
    }
}
