using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class ConfigurarIdAutogenerado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonLateralityTypes_LateralityTypes_LateralityId",
                table: "PersonLateralityTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonLateralityTypes_Persons_PersonId",
                table: "PersonLateralityTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonLateralityTypes",
                table: "PersonLateralityTypes");

            migrationBuilder.RenameTable(
                name: "PersonLateralityTypes",
                newName: "PersonLateralities");

            migrationBuilder.RenameIndex(
                name: "IX_PersonLateralityTypes_LateralityId",
                table: "PersonLateralities",
                newName: "IX_PersonLateralities_LateralityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonLateralities",
                table: "PersonLateralities",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonLateralities_LateralityTypes_LateralityId",
                table: "PersonLateralities",
                column: "LateralityId",
                principalTable: "LateralityTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonLateralities_Persons_PersonId",
                table: "PersonLateralities",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonLateralities_LateralityTypes_LateralityId",
                table: "PersonLateralities");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonLateralities_Persons_PersonId",
                table: "PersonLateralities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonLateralities",
                table: "PersonLateralities");

            migrationBuilder.RenameTable(
                name: "PersonLateralities",
                newName: "PersonLateralityTypes");

            migrationBuilder.RenameIndex(
                name: "IX_PersonLateralities_LateralityId",
                table: "PersonLateralityTypes",
                newName: "IX_PersonLateralityTypes_LateralityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonLateralityTypes",
                table: "PersonLateralityTypes",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonLateralityTypes_LateralityTypes_LateralityId",
                table: "PersonLateralityTypes",
                column: "LateralityId",
                principalTable: "LateralityTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonLateralityTypes_Persons_PersonId",
                table: "PersonLateralityTypes",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
