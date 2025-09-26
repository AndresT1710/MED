using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class Nuevasparaestimulacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DevelopmentRecords",
                columns: table => new
                {
                    DevelopmentRecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryNumber = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false),
                    DevelopmentMilestone = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    AgeRange = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Observations = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevelopmentRecords", x => x.DevelopmentRecordId);
                    table.ForeignKey(
                        name: "FK_DevelopmentRecord_ClinicalHistory",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NeurologicalExamTypes",
                columns: table => new
                {
                    NeurologicalExamTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NeurologicalExamTypes", x => x.NeurologicalExamTypeId);
                });

            migrationBuilder.CreateTable(
                name: "NeuropsychologicalHistories",
                columns: table => new
                {
                    NeuropsychologicalHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryNumber = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NeuropsychologicalHistories", x => x.NeuropsychologicalHistoryId);
                    table.ForeignKey(
                        name: "FK_NeuropsychologicalHistory_ClinicalHistory",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerinatalHistories",
                columns: table => new
                {
                    PerinatalHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryNumber = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerinatalHistories", x => x.PerinatalHistoryId);
                    table.ForeignKey(
                        name: "FK_PerinatalHistory_ClinicalHistory",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostnatalHistories",
                columns: table => new
                {
                    PostNatalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryNumber = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostnatalHistories", x => x.PostNatalId);
                    table.ForeignKey(
                        name: "FK_PostnatalHistory_ClinicalHistory",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrenatalHistories",
                columns: table => new
                {
                    PrenatalHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryNumber = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrenatalHistories", x => x.PrenatalHistoryId);
                    table.ForeignKey(
                        name: "FK_PrenatalHistory_ClinicalHistory",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NeurologicalExams",
                columns: table => new
                {
                    NeurologicalExamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryNumber = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    LinkPdf = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    ExamDate = table.Column<DateTime>(type: "date", nullable: true),
                    Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    NeurologicalExamTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NeurologicalExams", x => x.NeurologicalExamId);
                    table.ForeignKey(
                        name: "FK_NeurologicalExam_ClinicalHistory",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NeurologicalExam_NeurologicalExamType",
                        column: x => x.NeurologicalExamTypeId,
                        principalTable: "NeurologicalExamTypes",
                        principalColumn: "NeurologicalExamTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DevelopmentRecords_ClinicalHistoryId",
                table: "DevelopmentRecords",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_NeurologicalExams_ClinicalHistoryId",
                table: "NeurologicalExams",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_NeurologicalExams_NeurologicalExamTypeId",
                table: "NeurologicalExams",
                column: "NeurologicalExamTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NeuropsychologicalHistories_ClinicalHistoryId",
                table: "NeuropsychologicalHistories",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PerinatalHistories_ClinicalHistoryId",
                table: "PerinatalHistories",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PostnatalHistories_ClinicalHistoryId",
                table: "PostnatalHistories",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PrenatalHistories_ClinicalHistoryId",
                table: "PrenatalHistories",
                column: "ClinicalHistoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DevelopmentRecords");

            migrationBuilder.DropTable(
                name: "NeurologicalExams");

            migrationBuilder.DropTable(
                name: "NeuropsychologicalHistories");

            migrationBuilder.DropTable(
                name: "PerinatalHistories");

            migrationBuilder.DropTable(
                name: "PostnatalHistories");

            migrationBuilder.DropTable(
                name: "PrenatalHistories");

            migrationBuilder.DropTable(
                name: "NeurologicalExamTypes");
        }
    }
}
