using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddNewAttributesCL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonLaborActivities_LaborActivities_LaborActivityId",
                table: "PersonLaborActivities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonLaborActivities",
                table: "PersonLaborActivities");

            migrationBuilder.AlterColumn<int>(
                name: "LaborActivityId",
                table: "PersonLaborActivities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonLaborActivities",
                table: "PersonLaborActivities",
                columns: new[] { "PersonId", "LaborActivityId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PersonLaborActivities_LaborActivities_LaborActivityId",
                table: "PersonLaborActivities",
                column: "LaborActivityId",
                principalTable: "LaborActivities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonLaborActivities_LaborActivities_LaborActivityId",
                table: "PersonLaborActivities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonLaborActivities",
                table: "PersonLaborActivities");

            migrationBuilder.AlterColumn<int>(
                name: "LaborActivityId",
                table: "PersonLaborActivities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonLaborActivities",
                table: "PersonLaborActivities",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonLaborActivities_LaborActivities_LaborActivityId",
                table: "PersonLaborActivities",
                column: "LaborActivityId",
                principalTable: "LaborActivities",
                principalColumn: "Id");
        }
    }
}
