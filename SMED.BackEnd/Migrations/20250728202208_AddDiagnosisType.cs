using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddDiagnosisType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiagnosticType",
                table: "Diagnosis");

            migrationBuilder.AddColumn<int>(
                name: "DiagnosticTypeId",
                table: "Diagnosis",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DiagnosticTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosticTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosis_DiagnosticTypeId",
                table: "Diagnosis",
                column: "DiagnosticTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnosis_DiagnosticTypes_DiagnosticTypeId",
                table: "Diagnosis",
                column: "DiagnosticTypeId",
                principalTable: "DiagnosticTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diagnosis_DiagnosticTypes_DiagnosticTypeId",
                table: "Diagnosis");

            migrationBuilder.DropTable(
                name: "DiagnosticTypes");

            migrationBuilder.DropIndex(
                name: "IX_Diagnosis_DiagnosticTypeId",
                table: "Diagnosis");

            migrationBuilder.DropColumn(
                name: "DiagnosticTypeId",
                table: "Diagnosis");

            migrationBuilder.AddColumn<string>(
                name: "DiagnosticType",
                table: "Diagnosis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
