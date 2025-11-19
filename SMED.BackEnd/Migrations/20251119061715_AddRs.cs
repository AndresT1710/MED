using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddRs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Agregar la columna LocationId como nullable inicialmente
            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "MedicalReferrals",
                type: "int",
                nullable: true);

            // 2. Asignar un valor por defecto a los registros existentes
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM Locations)
                BEGIN
                    INSERT INTO Locations (Name) VALUES ('Ubicación Por Defecto')
                END

                DECLARE @DefaultLocationId INT = (SELECT TOP 1 Id FROM Locations ORDER BY Id);
                UPDATE MedicalReferrals SET LocationId = @DefaultLocationId WHERE LocationId IS NULL;
            ");

            // 3. Hacer la columna NOT NULL
            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "MedicalReferrals",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            // 4. Crear el índice
            migrationBuilder.CreateIndex(
                name: "IX_MedicalReferrals_LocationId",
                table: "MedicalReferrals",
                column: "LocationId");

            // 5. Crear la foreign key
            migrationBuilder.AddForeignKey(
                name: "FK_MedicalReferrals_Locations_LocationId",
                table: "MedicalReferrals",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalReferrals_Locations_LocationId",
                table: "MedicalReferrals");

            migrationBuilder.DropIndex(
                name: "IX_MedicalReferrals_LocationId",
                table: "MedicalReferrals");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "MedicalReferrals");
        }
    }
}
