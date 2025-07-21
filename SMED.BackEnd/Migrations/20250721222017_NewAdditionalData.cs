using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class NewAdditionalData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdditionalData",
                columns: table => new
                {
                    AdditionalDataId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Observacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicalCareId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalData", x => x.AdditionalDataId);
                    table.ForeignKey(
                        name: "FK_AdditionalData_MedicalCares_MedicalCareId",
                        column: x => x.MedicalCareId,
                        principalTable: "MedicalCares",
                        principalColumn: "CareId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalData_MedicalCareId",
                table: "AdditionalData",
                column: "MedicalCareId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalData");
        }
    }
}
