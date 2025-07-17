using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddRulesPhysicalExam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhysicalExams_PathologicalEvidences_PathologicalEvidenceId",
                table: "PhysicalExams");

            migrationBuilder.DropForeignKey(
                name: "FK_PhysicalExams_Regions_RegionId",
                table: "PhysicalExams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Regions",
                table: "Regions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PathologicalEvidences",
                table: "PathologicalEvidences");

            migrationBuilder.RenameTable(
                name: "Regions",
                newName: "Region");

            migrationBuilder.RenameTable(
                name: "PathologicalEvidences",
                newName: "PathologicalEvidence");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Region",
                table: "Region",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PathologicalEvidence",
                table: "PathologicalEvidence",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhysicalExams_PathologicalEvidence_PathologicalEvidenceId",
                table: "PhysicalExams",
                column: "PathologicalEvidenceId",
                principalTable: "PathologicalEvidence",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_PhysicalExams_Region_RegionId",
                table: "PhysicalExams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Region",
                table: "Region");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PathologicalEvidence",
                table: "PathologicalEvidence");

            migrationBuilder.RenameTable(
                name: "Region",
                newName: "Regions");

            migrationBuilder.RenameTable(
                name: "PathologicalEvidence",
                newName: "PathologicalEvidences");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Regions",
                table: "Regions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PathologicalEvidences",
                table: "PathologicalEvidences",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhysicalExams_PathologicalEvidences_PathologicalEvidenceId",
                table: "PhysicalExams",
                column: "PathologicalEvidenceId",
                principalTable: "PathologicalEvidences",
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
    }
}
