using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class ProblemSolution : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

           


            migrationBuilder.DropColumn(
                name: "Extremities",
                table: "PhysicalExams");

            migrationBuilder.RenameColumn(
                name: "PhysicalExamDetailId",
                table: "PhysicalExams",
                newName: "PhysicalExamTypeId");

            migrationBuilder.AddColumn<string>(
                name: "Observations",
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
                name: "RegionId",
                table: "PhysicalExams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PathologicalEvidence",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PathologicalEvidence", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.Id);
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
                name: "FK_PhysicalExams_PathologicalEvidence_PathologicalEvidenceId",
                table: "PhysicalExams",
                column: "PathologicalEvidenceId",
                principalTable: "PathologicalEvidence",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhysicalExams_PhysicalExamTypes_PhysicalExamTypeId",
                table: "PhysicalExams",
                column: "PhysicalExamTypeId",
                principalTable: "PhysicalExamTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhysicalExams_Region_RegionId",
                table: "PhysicalExams",
                column: "RegionId",
                principalTable: "Region",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhysicalExams_PathologicalEvidence_PathologicalEvidenceId",
                table: "PhysicalExams");

            migrationBuilder.DropForeignKey(
                name: "FK_PhysicalExams_PhysicalExamTypes_PhysicalExamTypeId",
                table: "PhysicalExams");

            migrationBuilder.DropForeignKey(
                name: "FK_PhysicalExams_Region_RegionId",
                table: "PhysicalExams");

            migrationBuilder.DropTable(
                name: "PathologicalEvidence");

            migrationBuilder.DropTable(
                name: "Region");

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
                name: "Observations",
                table: "PhysicalExams");

            migrationBuilder.DropColumn(
                name: "PathologicalEvidenceId",
                table: "PhysicalExams");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "PhysicalExams");

            migrationBuilder.RenameColumn(
                name: "PhysicalExamTypeId",
                table: "PhysicalExams",
                newName: "PhysicalExamDetailId");

            migrationBuilder.AddColumn<string>(
                name: "Extremities",
                table: "PhysicalExams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
