using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesMeasures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    MeasurementsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalCareId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.MeasurementsId);
                    table.ForeignKey(
                        name: "FK_Measurements_MedicalCares_MedicalCareId",
                        column: x => x.MedicalCareId,
                        principalTable: "MedicalCares",
                        principalColumn: "CareId");
                });

            migrationBuilder.CreateTable(
                name: "BioImpedances",
                columns: table => new
                {
                    BioImpedanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BodyFatPercentage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpperSectionFatPercentage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LowerSectionFatPercentage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisceralFat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MuscleMass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoneWeight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BodyWater = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeasurementsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BioImpedances", x => x.BioImpedanceId);
                    table.ForeignKey(
                        name: "FK_BioImpedances_Measurements_MeasurementsId",
                        column: x => x.MeasurementsId,
                        principalTable: "Measurements",
                        principalColumn: "MeasurementsId");
                });

            migrationBuilder.CreateTable(
                name: "Diameters",
                columns: table => new
                {
                    DiametersId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Humerus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Femur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Wrist = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeasurementsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diameters", x => x.DiametersId);
                    table.ForeignKey(
                        name: "FK_Diameters_Measurements_MeasurementsId",
                        column: x => x.MeasurementsId,
                        principalTable: "Measurements",
                        principalColumn: "MeasurementsId");
                });

            migrationBuilder.CreateTable(
                name: "Perimeters",
                columns: table => new
                {
                    PerimetersId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cephalic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Neck = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelaxedArmHalf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Forearm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Wrist = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeasurementsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perimeters", x => x.PerimetersId);
                    table.ForeignKey(
                        name: "FK_Perimeters_Measurements_MeasurementsId",
                        column: x => x.MeasurementsId,
                        principalTable: "Measurements",
                        principalColumn: "MeasurementsId");
                });

            migrationBuilder.CreateTable(
                name: "SkinFolds",
                columns: table => new
                {
                    SkinFoldsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subscapular = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Triceps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Biceps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IliacCrest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Supraespinal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Abdominal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FrontalThigh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedialCalf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedialAxillary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pectoral = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeasurementsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkinFolds", x => x.SkinFoldsId);
                    table.ForeignKey(
                        name: "FK_SkinFolds_Measurements_MeasurementsId",
                        column: x => x.MeasurementsId,
                        principalTable: "Measurements",
                        principalColumn: "MeasurementsId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BioImpedances_MeasurementsId",
                table: "BioImpedances",
                column: "MeasurementsId",
                unique: true,
                filter: "[MeasurementsId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Diameters_MeasurementsId",
                table: "Diameters",
                column: "MeasurementsId",
                unique: true,
                filter: "[MeasurementsId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_MedicalCareId",
                table: "Measurements",
                column: "MedicalCareId",
                unique: true,
                filter: "[MedicalCareId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Perimeters_MeasurementsId",
                table: "Perimeters",
                column: "MeasurementsId",
                unique: true,
                filter: "[MeasurementsId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SkinFolds_MeasurementsId",
                table: "SkinFolds",
                column: "MeasurementsId",
                unique: true,
                filter: "[MeasurementsId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BioImpedances");

            migrationBuilder.DropTable(
                name: "Diameters");

            migrationBuilder.DropTable(
                name: "Perimeters");

            migrationBuilder.DropTable(
                name: "SkinFolds");

            migrationBuilder.DropTable(
                name: "Measurements");
        }
    }
}
