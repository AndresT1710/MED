using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddNewRelationsPhysicalExa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhysicalExams_PhysicalExamTypes_PhysicalExamTypeId",
                table: "PhysicalExams");

            migrationBuilder.AlterColumn<int>(
                name: "PhysicalExamTypeId",
                table: "PhysicalExams",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PhysicalExams_PhysicalExamTypes_PhysicalExamTypeId",
                table: "PhysicalExams",
                column: "PhysicalExamTypeId",
                principalTable: "PhysicalExamTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhysicalExams_PhysicalExamTypes_PhysicalExamTypeId",
                table: "PhysicalExams");

            migrationBuilder.AlterColumn<int>(
                name: "PhysicalExamTypeId",
                table: "PhysicalExams",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PhysicalExams_PhysicalExamTypes_PhysicalExamTypeId",
                table: "PhysicalExams",
                column: "PhysicalExamTypeId",
                principalTable: "PhysicalExamTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
