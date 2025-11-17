using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class MigracionPsicologiaN5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advances_Sessions_SessionId",
                table: "Advances");

            migrationBuilder.DropIndex(
                name: "IX_Advances_SessionId",
                table: "Advances");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Advances");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SessionId",
                table: "Advances",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Advances_SessionId",
                table: "Advances",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advances_Sessions_SessionId",
                table: "Advances",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "SessionsId");
        }
    }
}
