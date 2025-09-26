using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class Nuevasparaestimulacion03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NeurologicalExam_ClinicalHistory",
                table: "NeurologicalExams");

            migrationBuilder.AlterColumn<int>(
                name: "ClinicalHistoryId",
                table: "NeurologicalExams",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_NeurologicalExam_ClinicalHistory",
                table: "NeurologicalExams",
                column: "ClinicalHistoryId",
                principalTable: "ClinicalHistories",
                principalColumn: "ClinicalHistoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NeurologicalExam_ClinicalHistory",
                table: "NeurologicalExams");

            migrationBuilder.AlterColumn<int>(
                name: "ClinicalHistoryId",
                table: "NeurologicalExams",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NeurologicalExam_ClinicalHistory",
                table: "NeurologicalExams",
                column: "ClinicalHistoryId",
                principalTable: "ClinicalHistories",
                principalColumn: "ClinicalHistoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
