using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeOfServiceId",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CostOfServices",
                columns: table => new
                {
                    CostOfServiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<double>(type: "float", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostOfServices", x => x.CostOfServiceId);
                    table.ForeignKey(
                        name: "FK_CostOfServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfServices", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Services_TypeOfServiceId",
                table: "Services",
                column: "TypeOfServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_CostOfServices_ServiceId",
                table: "CostOfServices",
                column: "ServiceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_TypeOfServices_TypeOfServiceId",
                table: "Services",
                column: "TypeOfServiceId",
                principalTable: "TypeOfServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_TypeOfServices_TypeOfServiceId",
                table: "Services");

            migrationBuilder.DropTable(
                name: "CostOfServices");

            migrationBuilder.DropTable(
                name: "TypeOfServices");

            migrationBuilder.DropIndex(
                name: "IX_Services_TypeOfServiceId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "TypeOfServiceId",
                table: "Services");
        }
    }
}
