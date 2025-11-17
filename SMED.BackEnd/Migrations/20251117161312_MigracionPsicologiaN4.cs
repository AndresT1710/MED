using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class MigracionPsicologiaN4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Sessions_SessionId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_SessionId",
                table: "Activities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Activities_SessionId",
                table: "Activities",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Sessions_SessionId",
                table: "Activities",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "SessionsId");
        }
    }
}
