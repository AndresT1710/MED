using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddNewEntityFisioterapia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActionFs",
                columns: table => new
                {
                    ActionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionFs", x => x.ActionId);
                });

            migrationBuilder.CreateTable(
                name: "BodyZones",
                columns: table => new
                {
                    BodyZoneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyZones", x => x.BodyZoneId);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    ColorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.ColorId);
                });

            migrationBuilder.CreateTable(
                name: "CurrentIllnesses",
                columns: table => new
                {
                    CurrentIllnessId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EvolutionTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Localization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Intensity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AggravatingFactors = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MitigatingFactors = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NocturnalPain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weakness = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Paresthesias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComplementaryExams = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalCareId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentIllnesses", x => x.CurrentIllnessId);
                    table.ForeignKey(
                        name: "FK_CurrentIllnesses_MedicalCares_MedicalCareId",
                        column: x => x.MedicalCareId,
                        principalTable: "MedicalCares",
                        principalColumn: "CareId");
                });

            migrationBuilder.CreateTable(
                name: "Edemas",
                columns: table => new
                {
                    EdemaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Edemas", x => x.EdemaId);
                });

            migrationBuilder.CreateTable(
                name: "JointConditions",
                columns: table => new
                {
                    JointConditionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JointConditions", x => x.JointConditionId);
                });

            migrationBuilder.CreateTable(
                name: "JointRangeOfMotions",
                columns: table => new
                {
                    JointRangeOfMotionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JointRangeOfMotions", x => x.JointRangeOfMotionId);
                });

            migrationBuilder.CreateTable(
                name: "MedicalEvaluationMembers",
                columns: table => new
                {
                    MedicalEvaluationMembersId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalEvaluationMembers", x => x.MedicalEvaluationMembersId);
                });

            migrationBuilder.CreateTable(
                name: "MedicalEvaluationPositions",
                columns: table => new
                {
                    MedicalEvaluationPositionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalEvaluationPositions", x => x.MedicalEvaluationPositionId);
                });

            migrationBuilder.CreateTable(
                name: "PainMoments",
                columns: table => new
                {
                    PainMomentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PainMoments", x => x.PainMomentId);
                });

            migrationBuilder.CreateTable(
                name: "PhysiotherapyDiagnostics",
                columns: table => new
                {
                    PhysiotherapyDiagnosticId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicalCareId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysiotherapyDiagnostics", x => x.PhysiotherapyDiagnosticId);
                    table.ForeignKey(
                        name: "FK_PhysiotherapyDiagnostics_MedicalCares_MedicalCareId",
                        column: x => x.MedicalCareId,
                        principalTable: "MedicalCares",
                        principalColumn: "CareId");
                });

            migrationBuilder.CreateTable(
                name: "ResultTypes",
                columns: table => new
                {
                    ResultTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultTypes", x => x.ResultTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Scales",
                columns: table => new
                {
                    ScaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scales", x => x.ScaleId);
                });

            migrationBuilder.CreateTable(
                name: "SensitivityLevels",
                columns: table => new
                {
                    SensitivityLevelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensitivityLevels", x => x.SensitivityLevelId);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    SessionsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MedicalCareId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.SessionsId);
                    table.ForeignKey(
                        name: "FK_Sessions_MedicalCares_MedicalCareId",
                        column: x => x.MedicalCareId,
                        principalTable: "MedicalCares",
                        principalColumn: "CareId");
                });

            migrationBuilder.CreateTable(
                name: "Shades",
                columns: table => new
                {
                    ShadeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shades", x => x.ShadeId);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "Strengths",
                columns: table => new
                {
                    StrengthId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Strengths", x => x.StrengthId);
                });

            migrationBuilder.CreateTable(
                name: "Swellings",
                columns: table => new
                {
                    SwellingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Swellings", x => x.SwellingId);
                });

            migrationBuilder.CreateTable(
                name: "Trophisms",
                columns: table => new
                {
                    TrophismId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trophisms", x => x.TrophismId);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfActivities",
                columns: table => new
                {
                    TypeOfActivityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfActivities", x => x.TypeOfActivityId);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfMedicalEvaluations",
                columns: table => new
                {
                    TypeOfMedicalEvaluationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfMedicalEvaluations", x => x.TypeOfMedicalEvaluationId);
                });

            migrationBuilder.CreateTable(
                name: "Views",
                columns: table => new
                {
                    ViewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Views", x => x.ViewId);
                });

            migrationBuilder.CreateTable(
                name: "OsteoarticularEvaluations",
                columns: table => new
                {
                    OsteoarticularEvaluationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalCareId = table.Column<int>(type: "int", nullable: true),
                    JointConditionId = table.Column<int>(type: "int", nullable: true),
                    JointRangeOfMotionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OsteoarticularEvaluations", x => x.OsteoarticularEvaluationId);
                    table.ForeignKey(
                        name: "FK_OsteoarticularEvaluations_JointConditions_JointConditionId",
                        column: x => x.JointConditionId,
                        principalTable: "JointConditions",
                        principalColumn: "JointConditionId");
                    table.ForeignKey(
                        name: "FK_OsteoarticularEvaluations_JointRangeOfMotions_JointRangeOfMotionId",
                        column: x => x.JointRangeOfMotionId,
                        principalTable: "JointRangeOfMotions",
                        principalColumn: "JointRangeOfMotionId");
                    table.ForeignKey(
                        name: "FK_OsteoarticularEvaluations_MedicalCares_MedicalCareId",
                        column: x => x.MedicalCareId,
                        principalTable: "MedicalCares",
                        principalColumn: "CareId");
                });

            migrationBuilder.CreateTable(
                name: "PhysiotherapyTreatments",
                columns: table => new
                {
                    PhysiotherapyTreatmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Recommendations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhysiotherapyDiagnosticId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysiotherapyTreatments", x => x.PhysiotherapyTreatmentId);
                    table.ForeignKey(
                        name: "FK_PhysiotherapyTreatments_PhysiotherapyDiagnostics_PhysiotherapyDiagnosticId",
                        column: x => x.PhysiotherapyDiagnosticId,
                        principalTable: "PhysiotherapyDiagnostics",
                        principalColumn: "PhysiotherapyDiagnosticId");
                });

            migrationBuilder.CreateTable(
                name: "SpecialTests",
                columns: table => new
                {
                    SpecialTestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Test = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResultTypeId = table.Column<int>(type: "int", nullable: false),
                    MedicalCareId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialTests", x => x.SpecialTestId);
                    table.ForeignKey(
                        name: "FK_SpecialTests_MedicalCares_MedicalCareId",
                        column: x => x.MedicalCareId,
                        principalTable: "MedicalCares",
                        principalColumn: "CareId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpecialTests_ResultTypes_ResultTypeId",
                        column: x => x.ResultTypeId,
                        principalTable: "ResultTypes",
                        principalColumn: "ResultTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PainScales",
                columns: table => new
                {
                    PainScaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Observation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionId = table.Column<int>(type: "int", nullable: true),
                    ScaleId = table.Column<int>(type: "int", nullable: true),
                    PainMomentId = table.Column<int>(type: "int", nullable: true),
                    MedicalCareId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PainScales", x => x.PainScaleId);
                    table.ForeignKey(
                        name: "FK_PainScales_ActionFs_ActionId",
                        column: x => x.ActionId,
                        principalTable: "ActionFs",
                        principalColumn: "ActionId");
                    table.ForeignKey(
                        name: "FK_PainScales_MedicalCares_MedicalCareId",
                        column: x => x.MedicalCareId,
                        principalTable: "MedicalCares",
                        principalColumn: "CareId");
                    table.ForeignKey(
                        name: "FK_PainScales_PainMoments_PainMomentId",
                        column: x => x.PainMomentId,
                        principalTable: "PainMoments",
                        principalColumn: "PainMomentId");
                    table.ForeignKey(
                        name: "FK_PainScales_Scales_ScaleId",
                        column: x => x.ScaleId,
                        principalTable: "Scales",
                        principalColumn: "ScaleId");
                });

            migrationBuilder.CreateTable(
                name: "SensitivityEvaluations",
                columns: table => new
                {
                    SensitivityEvaluationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Demandmas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SensitivityLevelId = table.Column<int>(type: "int", nullable: true),
                    BodyZoneId = table.Column<int>(type: "int", nullable: true),
                    MedicalCareId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensitivityEvaluations", x => x.SensitivityEvaluationId);
                    table.ForeignKey(
                        name: "FK_SensitivityEvaluations_BodyZones_BodyZoneId",
                        column: x => x.BodyZoneId,
                        principalTable: "BodyZones",
                        principalColumn: "BodyZoneId");
                    table.ForeignKey(
                        name: "FK_SensitivityEvaluations_MedicalCares_MedicalCareId",
                        column: x => x.MedicalCareId,
                        principalTable: "MedicalCares",
                        principalColumn: "CareId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SensitivityEvaluations_SensitivityLevels_SensitivityLevelId",
                        column: x => x.SensitivityLevelId,
                        principalTable: "SensitivityLevels",
                        principalColumn: "SensitivityLevelId");
                });

            migrationBuilder.CreateTable(
                name: "SkinEvaluations",
                columns: table => new
                {
                    SkinEvaluationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalCareId = table.Column<int>(type: "int", nullable: true),
                    ColorId = table.Column<int>(type: "int", nullable: true),
                    EdemaId = table.Column<int>(type: "int", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: true),
                    SwellingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkinEvaluations", x => x.SkinEvaluationId);
                    table.ForeignKey(
                        name: "FK_SkinEvaluations_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "ColorId");
                    table.ForeignKey(
                        name: "FK_SkinEvaluations_Edemas_EdemaId",
                        column: x => x.EdemaId,
                        principalTable: "Edemas",
                        principalColumn: "EdemaId");
                    table.ForeignKey(
                        name: "FK_SkinEvaluations_MedicalCares_MedicalCareId",
                        column: x => x.MedicalCareId,
                        principalTable: "MedicalCares",
                        principalColumn: "CareId");
                    table.ForeignKey(
                        name: "FK_SkinEvaluations_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "StatusId");
                    table.ForeignKey(
                        name: "FK_SkinEvaluations_Swellings_SwellingId",
                        column: x => x.SwellingId,
                        principalTable: "Swellings",
                        principalColumn: "SwellingId");
                });

            migrationBuilder.CreateTable(
                name: "NeuromuscularEvaluations",
                columns: table => new
                {
                    NeuromuscularEvaluationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalCareId = table.Column<int>(type: "int", nullable: true),
                    ShadeId = table.Column<int>(type: "int", nullable: true),
                    StrengthId = table.Column<int>(type: "int", nullable: true),
                    TrophismId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NeuromuscularEvaluations", x => x.NeuromuscularEvaluationId);
                    table.ForeignKey(
                        name: "FK_NeuromuscularEvaluations_MedicalCares_MedicalCareId",
                        column: x => x.MedicalCareId,
                        principalTable: "MedicalCares",
                        principalColumn: "CareId");
                    table.ForeignKey(
                        name: "FK_NeuromuscularEvaluations_Shades_ShadeId",
                        column: x => x.ShadeId,
                        principalTable: "Shades",
                        principalColumn: "ShadeId");
                    table.ForeignKey(
                        name: "FK_NeuromuscularEvaluations_Strengths_StrengthId",
                        column: x => x.StrengthId,
                        principalTable: "Strengths",
                        principalColumn: "StrengthId");
                    table.ForeignKey(
                        name: "FK_NeuromuscularEvaluations_Trophisms_TrophismId",
                        column: x => x.TrophismId,
                        principalTable: "Trophisms",
                        principalColumn: "TrophismId");
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    ActivityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SessionId = table.Column<int>(type: "int", nullable: true),
                    TypeOfActivityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.ActivityId);
                    table.ForeignKey(
                        name: "FK_Activities_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "SessionsId");
                    table.ForeignKey(
                        name: "FK_Activities_TypeOfActivities_TypeOfActivityId",
                        column: x => x.TypeOfActivityId,
                        principalTable: "TypeOfActivities",
                        principalColumn: "TypeOfActivityId");
                });

            migrationBuilder.CreateTable(
                name: "MedicalEvaluations",
                columns: table => new
                {
                    MedicalEvaluationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalCareId = table.Column<int>(type: "int", nullable: true),
                    TypeOfMedicalEvaluationId = table.Column<int>(type: "int", nullable: true),
                    MedicalEvaluationPositionId = table.Column<int>(type: "int", nullable: true),
                    MedicalEvaluationMembersId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalEvaluations", x => x.MedicalEvaluationId);
                    table.ForeignKey(
                        name: "FK_MedicalEvaluations_MedicalCares_MedicalCareId",
                        column: x => x.MedicalCareId,
                        principalTable: "MedicalCares",
                        principalColumn: "CareId");
                    table.ForeignKey(
                        name: "FK_MedicalEvaluations_MedicalEvaluationMembers_MedicalEvaluationMembersId",
                        column: x => x.MedicalEvaluationMembersId,
                        principalTable: "MedicalEvaluationMembers",
                        principalColumn: "MedicalEvaluationMembersId");
                    table.ForeignKey(
                        name: "FK_MedicalEvaluations_MedicalEvaluationPositions_MedicalEvaluationPositionId",
                        column: x => x.MedicalEvaluationPositionId,
                        principalTable: "MedicalEvaluationPositions",
                        principalColumn: "MedicalEvaluationPositionId");
                    table.ForeignKey(
                        name: "FK_MedicalEvaluations_TypeOfMedicalEvaluations_TypeOfMedicalEvaluationId",
                        column: x => x.TypeOfMedicalEvaluationId,
                        principalTable: "TypeOfMedicalEvaluations",
                        principalColumn: "TypeOfMedicalEvaluationId");
                });

            migrationBuilder.CreateTable(
                name: "PosturalEvaluations",
                columns: table => new
                {
                    PosturalEvaluationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Observation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Grade = table.Column<float>(type: "real", nullable: false),
                    BodyAlignment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicalCareId = table.Column<int>(type: "int", nullable: true),
                    ViewId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PosturalEvaluations", x => x.PosturalEvaluationId);
                    table.ForeignKey(
                        name: "FK_PosturalEvaluations_MedicalCares_MedicalCareId",
                        column: x => x.MedicalCareId,
                        principalTable: "MedicalCares",
                        principalColumn: "CareId");
                    table.ForeignKey(
                        name: "FK_PosturalEvaluations_Views_ViewId",
                        column: x => x.ViewId,
                        principalTable: "Views",
                        principalColumn: "ViewId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_SessionId",
                table: "Activities",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_TypeOfActivityId",
                table: "Activities",
                column: "TypeOfActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentIllnesses_MedicalCareId",
                table: "CurrentIllnesses",
                column: "MedicalCareId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalEvaluations_MedicalCareId",
                table: "MedicalEvaluations",
                column: "MedicalCareId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalEvaluations_MedicalEvaluationMembersId",
                table: "MedicalEvaluations",
                column: "MedicalEvaluationMembersId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalEvaluations_MedicalEvaluationPositionId",
                table: "MedicalEvaluations",
                column: "MedicalEvaluationPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalEvaluations_TypeOfMedicalEvaluationId",
                table: "MedicalEvaluations",
                column: "TypeOfMedicalEvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_NeuromuscularEvaluations_MedicalCareId",
                table: "NeuromuscularEvaluations",
                column: "MedicalCareId");

            migrationBuilder.CreateIndex(
                name: "IX_NeuromuscularEvaluations_ShadeId",
                table: "NeuromuscularEvaluations",
                column: "ShadeId");

            migrationBuilder.CreateIndex(
                name: "IX_NeuromuscularEvaluations_StrengthId",
                table: "NeuromuscularEvaluations",
                column: "StrengthId");

            migrationBuilder.CreateIndex(
                name: "IX_NeuromuscularEvaluations_TrophismId",
                table: "NeuromuscularEvaluations",
                column: "TrophismId");

            migrationBuilder.CreateIndex(
                name: "IX_OsteoarticularEvaluations_JointConditionId",
                table: "OsteoarticularEvaluations",
                column: "JointConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_OsteoarticularEvaluations_JointRangeOfMotionId",
                table: "OsteoarticularEvaluations",
                column: "JointRangeOfMotionId");

            migrationBuilder.CreateIndex(
                name: "IX_OsteoarticularEvaluations_MedicalCareId",
                table: "OsteoarticularEvaluations",
                column: "MedicalCareId");

            migrationBuilder.CreateIndex(
                name: "IX_PainScales_ActionId",
                table: "PainScales",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_PainScales_MedicalCareId",
                table: "PainScales",
                column: "MedicalCareId");

            migrationBuilder.CreateIndex(
                name: "IX_PainScales_PainMomentId",
                table: "PainScales",
                column: "PainMomentId");

            migrationBuilder.CreateIndex(
                name: "IX_PainScales_ScaleId",
                table: "PainScales",
                column: "ScaleId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysiotherapyDiagnostics_MedicalCareId",
                table: "PhysiotherapyDiagnostics",
                column: "MedicalCareId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysiotherapyTreatments_PhysiotherapyDiagnosticId",
                table: "PhysiotherapyTreatments",
                column: "PhysiotherapyDiagnosticId");

            migrationBuilder.CreateIndex(
                name: "IX_PosturalEvaluations_MedicalCareId",
                table: "PosturalEvaluations",
                column: "MedicalCareId");

            migrationBuilder.CreateIndex(
                name: "IX_PosturalEvaluations_ViewId",
                table: "PosturalEvaluations",
                column: "ViewId");

            migrationBuilder.CreateIndex(
                name: "IX_SensitivityEvaluations_BodyZoneId",
                table: "SensitivityEvaluations",
                column: "BodyZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_SensitivityEvaluations_MedicalCareId",
                table: "SensitivityEvaluations",
                column: "MedicalCareId");

            migrationBuilder.CreateIndex(
                name: "IX_SensitivityEvaluations_SensitivityLevelId",
                table: "SensitivityEvaluations",
                column: "SensitivityLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_MedicalCareId",
                table: "Sessions",
                column: "MedicalCareId");

            migrationBuilder.CreateIndex(
                name: "IX_SkinEvaluations_ColorId",
                table: "SkinEvaluations",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_SkinEvaluations_EdemaId",
                table: "SkinEvaluations",
                column: "EdemaId");

            migrationBuilder.CreateIndex(
                name: "IX_SkinEvaluations_MedicalCareId",
                table: "SkinEvaluations",
                column: "MedicalCareId");

            migrationBuilder.CreateIndex(
                name: "IX_SkinEvaluations_StatusId",
                table: "SkinEvaluations",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_SkinEvaluations_SwellingId",
                table: "SkinEvaluations",
                column: "SwellingId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialTests_MedicalCareId",
                table: "SpecialTests",
                column: "MedicalCareId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialTests_ResultTypeId",
                table: "SpecialTests",
                column: "ResultTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "CurrentIllnesses");

            migrationBuilder.DropTable(
                name: "MedicalEvaluations");

            migrationBuilder.DropTable(
                name: "NeuromuscularEvaluations");

            migrationBuilder.DropTable(
                name: "OsteoarticularEvaluations");

            migrationBuilder.DropTable(
                name: "PainScales");

            migrationBuilder.DropTable(
                name: "PhysiotherapyTreatments");

            migrationBuilder.DropTable(
                name: "PosturalEvaluations");

            migrationBuilder.DropTable(
                name: "SensitivityEvaluations");

            migrationBuilder.DropTable(
                name: "SkinEvaluations");

            migrationBuilder.DropTable(
                name: "SpecialTests");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "TypeOfActivities");

            migrationBuilder.DropTable(
                name: "MedicalEvaluationMembers");

            migrationBuilder.DropTable(
                name: "MedicalEvaluationPositions");

            migrationBuilder.DropTable(
                name: "TypeOfMedicalEvaluations");

            migrationBuilder.DropTable(
                name: "Shades");

            migrationBuilder.DropTable(
                name: "Strengths");

            migrationBuilder.DropTable(
                name: "Trophisms");

            migrationBuilder.DropTable(
                name: "JointConditions");

            migrationBuilder.DropTable(
                name: "JointRangeOfMotions");

            migrationBuilder.DropTable(
                name: "ActionFs");

            migrationBuilder.DropTable(
                name: "PainMoments");

            migrationBuilder.DropTable(
                name: "Scales");

            migrationBuilder.DropTable(
                name: "PhysiotherapyDiagnostics");

            migrationBuilder.DropTable(
                name: "Views");

            migrationBuilder.DropTable(
                name: "BodyZones");

            migrationBuilder.DropTable(
                name: "SensitivityLevels");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Edemas");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Swellings");

            migrationBuilder.DropTable(
                name: "ResultTypes");
        }
    }
}
