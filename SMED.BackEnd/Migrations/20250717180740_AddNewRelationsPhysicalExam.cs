using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddNewRelationsPhysicalExam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhysicalExams_PhysicalExamDetails_PhysicalExamDetailId",
                table: "PhysicalExams");

            migrationBuilder.DropTable(
                name: "PhysicalExamDetails");

            migrationBuilder.DropIndex(
                name: "IX_PhysicalExams_PhysicalExamDetailId",
                table: "PhysicalExams");

            migrationBuilder.DropColumn(
                name: "Extremities",
                table: "PhysicalExams");

            migrationBuilder.DropColumn(
                name: "PhysicalExamDetailId",
                table: "PhysicalExams");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PhysicalExams",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PathologicalEvidenceId",
                table: "PhysicalExams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PhysicalExamTypeId",
                table: "PhysicalExams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RegionId",
                table: "PhysicalExams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PathologicalEvidences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PathologicalEvidences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalExams_PathologicalEvidenceId",
                table: "PhysicalExams",
                column: "PathologicalEvidenceId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalExams_PhysicalExamTypeId",
                table: "PhysicalExams",
                column: "PhysicalExamTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalExams_RegionId",
                table: "PhysicalExams",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhysicalExams_PathologicalEvidences_PathologicalEvidenceId",
                table: "PhysicalExams",
                column: "PathologicalEvidenceId",
                principalTable: "PathologicalEvidences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhysicalExams_PhysicalExamTypes_PhysicalExamTypeId",
                table: "PhysicalExams",
                column: "PhysicalExamTypeId",
                principalTable: "PhysicalExamTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhysicalExams_Regions_RegionId",
                table: "PhysicalExams",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhysicalExams_PathologicalEvidences_PathologicalEvidenceId",
                table: "PhysicalExams");

            migrationBuilder.DropForeignKey(
                name: "FK_PhysicalExams_PhysicalExamTypes_PhysicalExamTypeId",
                table: "PhysicalExams");

            migrationBuilder.DropForeignKey(
                name: "FK_PhysicalExams_Regions_RegionId",
                table: "PhysicalExams");

            migrationBuilder.DropTable(
                name: "PathologicalEvidences");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropIndex(
                name: "IX_PhysicalExams_PathologicalEvidenceId",
                table: "PhysicalExams");

            migrationBuilder.DropIndex(
                name: "IX_PhysicalExams_PhysicalExamTypeId",
                table: "PhysicalExams");

            migrationBuilder.DropIndex(
                name: "IX_PhysicalExams_RegionId",
                table: "PhysicalExams");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "PhysicalExams");

            migrationBuilder.DropColumn(
                name: "PathologicalEvidenceId",
                table: "PhysicalExams");

            migrationBuilder.DropColumn(
                name: "PhysicalExamTypeId",
                table: "PhysicalExams");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "PhysicalExams");

            migrationBuilder.AddColumn<string>(
                name: "Extremities",
                table: "PhysicalExams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PhysicalExamDetailId",
                table: "PhysicalExams",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PhysicalExamDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhysicalExamTypeId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalExamDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhysicalExamDetails_PhysicalExamTypes_PhysicalExamTypeId",
                        column: x => x.PhysicalExamTypeId,
                        principalTable: "PhysicalExamTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalExams_PhysicalExamDetailId",
                table: "PhysicalExams",
                column: "PhysicalExamDetailId",
                unique: true,
                filter: "[PhysicalExamDetailId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalExamDetails_PhysicalExamTypeId",
                table: "PhysicalExamDetails",
                column: "PhysicalExamTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhysicalExams_PhysicalExamDetails_PhysicalExamDetailId",
                table: "PhysicalExams",
                column: "PhysicalExamDetailId",
                principalTable: "PhysicalExamDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
