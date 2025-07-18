using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Allergies",
                columns: table => new
                {
                    AllergyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergies", x => x.AllergyId);
                });

            migrationBuilder.CreateTable(
                name: "BloodGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiseaseTypes",
                columns: table => new
                {
                    DiseaseTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiseaseTypes", x => x.DiseaseTypeId);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    FoodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.FoodId);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Habits",
                columns: table => new
                {
                    HabitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habits", x => x.HabitId);
                });

            migrationBuilder.CreateTable(
                name: "HealthProfessionalTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthProfessionalTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LaborActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaborActivities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LateralityTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LateralityTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LifeStyle",
                columns: table => new
                {
                    LifeStyleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeStyle", x => x.LifeStyleId);
                });

            migrationBuilder.CreateTable(
                name: "MaritalStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaritalStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalInsurances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalInsurances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    ImageOrders_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PathologicalEvidence",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PathologicalEvidence", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhysicalExamTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalExamTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlaceOfAttentions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceOfAttentions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Professions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pronvince",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pronvince", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Relationships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relationships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Religions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Religions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SleepHabit",
                columns: table => new
                {
                    SleepHabitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SleepHabit", x => x.SleepHabitId);
                });

            migrationBuilder.CreateTable(
                name: "SportsActivities",
                columns: table => new
                {
                    SportActivityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportsActivities", x => x.SportActivityId);
                });

            migrationBuilder.CreateTable(
                name: "Surgeries",
                columns: table => new
                {
                    SurgeryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surgeries", x => x.SurgeryId);
                });

            migrationBuilder.CreateTable(
                name: "SystemsDevices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemsDevices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ToxicHabits",
                columns: table => new
                {
                    ToxicHabitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToxicHabits", x => x.ToxicHabitId);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfProcedures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfProcedures", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "Diseases",
                columns: table => new
                {
                    DiseaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    DiseaseTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diseases", x => x.DiseaseId);
                    table.ForeignKey(
                        name: "FK_Disease_DiseaseType",
                        column: x => x.DiseaseTypeId,
                        principalTable: "DiseaseTypes",
                        principalColumn: "DiseaseTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecommendedFoods",
                columns: table => new
                {
                    RecommendedFoodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Frequency = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecommendedFoods", x => x.RecommendedFoodId);
                    table.ForeignKey(
                        name: "FK_RecommendedFoods_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "FoodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Restriction",
                columns: table => new
                {
                    RestrictionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restriction", x => x.RestrictionId);
                    table.ForeignKey(
                        name: "FK_Restriction_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "FoodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenderId = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Treatments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Recommendations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dose = table.Column<int>(type: "int", nullable: true),
                    Frequency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViaAdmission = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicineId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Treatments_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_Pronvince_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Pronvince",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Procedures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfProcedureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Procedures_TypeOfProcedures_TypeOfProcedureId",
                        column: x => x.TypeOfProcedureId,
                        principalTable: "TypeOfProcedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_TypeOfServices_TypeOfServiceId",
                        column: x => x.TypeOfServiceId,
                        principalTable: "TypeOfServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodPlan",
                columns: table => new
                {
                    FoodPlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RestrictionId = table.Column<int>(type: "int", nullable: true),
                    RecommendedFoodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodPlan", x => x.FoodPlanId);
                    table.ForeignKey(
                        name: "FK_FoodPlan_RecommendedFoods_RecommendedFoodId",
                        column: x => x.RecommendedFoodId,
                        principalTable: "RecommendedFoods",
                        principalColumn: "RecommendedFoodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodPlan_Restriction_RestrictionId",
                        column: x => x.RestrictionId,
                        principalTable: "Restriction",
                        principalColumn: "RestrictionId");
                });

            migrationBuilder.CreateTable(
                name: "HealthProfessionals",
                columns: table => new
                {
                    HealthProfessionalId = table.Column<int>(type: "int", nullable: false),
                    HealthProfessionalTypeId = table.Column<int>(type: "int", nullable: true),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthProfessionals", x => x.HealthProfessionalId);
                    table.ForeignKey(
                        name: "FK_HealthProfessionals_HealthProfessionalTypes_HealthProfessionalTypeId",
                        column: x => x.HealthProfessionalTypeId,
                        principalTable: "HealthProfessionalTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HealthProfessionals_Persons_HealthProfessionalId",
                        column: x => x.HealthProfessionalId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_Patients_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonAddresses",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    MainStreet = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SecondaryStreet1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SecondaryStreet2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    HouseNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Reference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonAddresses", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_PersonAddresses_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonBloodGroups",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    BloodGroupId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonBloodGroups", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_PersonBloodGroups_BloodGroups_BloodGroupId",
                        column: x => x.BloodGroupId,
                        principalTable: "BloodGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PersonBloodGroups_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonDocuments",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: true),
                    DocumentNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonDocuments", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_PersonDocuments_DocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PersonDocuments_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonEducations",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    EducationLevelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonEducations", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_PersonEducations_EducationLevels_EducationLevelId",
                        column: x => x.EducationLevelId,
                        principalTable: "EducationLevels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PersonEducations_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonLaborActivities",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    LaborActivityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonLaborActivities", x => new { x.PersonId, x.LaborActivityId });
                    table.ForeignKey(
                        name: "FK_PersonLaborActivities_LaborActivities_LaborActivityId",
                        column: x => x.LaborActivityId,
                        principalTable: "LaborActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonLaborActivities_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonLateralities",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    LateralityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonLateralities", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_PersonLateralities_LateralityTypes_LateralityId",
                        column: x => x.LateralityId,
                        principalTable: "LateralityTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PersonLateralities_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonMaritalStatuses",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    MaritalStatusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonMaritalStatuses", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_PersonMaritalStatuses_MaritalStatuses_MaritalStatusId",
                        column: x => x.MaritalStatusId,
                        principalTable: "MaritalStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PersonMaritalStatuses_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonMedicalInsurances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: true),
                    MedicalInsuranceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonMedicalInsurances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonMedicalInsurances_MedicalInsurances_MedicalInsuranceId",
                        column: x => x.MedicalInsuranceId,
                        principalTable: "MedicalInsurances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PersonMedicalInsurances_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PersonPhones",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Landline = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonPhones", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_PersonPhones_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonProfessions",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    ProfessionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonProfessions", x => new { x.PersonId, x.ProfessionId });
                    table.ForeignKey(
                        name: "FK_PersonProfessions_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonProfessions_Professions_ProfessionId",
                        column: x => x.ProfessionId,
                        principalTable: "Professions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonReligions",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    ReligionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonReligions", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_PersonReligions_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonReligions_Religions_ReligionId",
                        column: x => x.ReligionId,
                        principalTable: "Religions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Indications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TreatmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Indications_Treatments_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonResidences",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonResidences", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_PersonResidences_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonResidences_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "ClinicalHistories",
                columns: table => new
                {
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryNumber = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    GeneralObservations = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicalHistories", x => x.ClinicalHistoryId);
                    table.ForeignKey(
                        name: "FK_ClinicalHistory_Patient",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PersonId");
                });

            migrationBuilder.CreateTable(
                name: "EmergencyContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmergencyContacts_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalCares",
                columns: table => new
                {
                    CareId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    HealthProfessionalId = table.Column<int>(type: "int", nullable: false),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CareDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalCares", x => x.CareId);
                    table.ForeignKey(
                        name: "FK_MedicalCares_HealthProfessionals_HealthProfessionalId",
                        column: x => x.HealthProfessionalId,
                        principalTable: "HealthProfessionals",
                        principalColumn: "HealthProfessionalId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalCares_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalCares_PlaceOfAttentions_LocationId",
                        column: x => x.LocationId,
                        principalTable: "PlaceOfAttentions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicalVisits",
                columns: table => new
                {
                    MedicalVisitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalVisits", x => x.MedicalVisitId);
                    table.ForeignKey(
                        name: "FK_MedicalVisits_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PersonId");
                });

            migrationBuilder.CreateTable(
                name: "PatientRelationships",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    RelationshipId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientRelationships", x => x.PatientId);
                    table.ForeignKey(
                        name: "FK_PatientRelationships_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientRelationships_Relationships_RelationshipId",
                        column: x => x.RelationshipId,
                        principalTable: "Relationships",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AllergyHistories",
                columns: table => new
                {
                    AllergyHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryNumber = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    AllergyId = table.Column<int>(type: "int", nullable: true),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllergyHistories", x => x.AllergyHistoryId);
                    table.ForeignKey(
                        name: "FK_AllergyHistory_Allergy",
                        column: x => x.AllergyId,
                        principalTable: "Allergies",
                        principalColumn: "AllergyId");
                    table.ForeignKey(
                        name: "FK_AllergyHistory_ClinicalHistory",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DietaryHabitsHistory",
                columns: table => new
                {
                    DietaryHabitHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalRecordNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietaryHabitsHistory", x => x.DietaryHabitHistoryId);
                    table.ForeignKey(
                        name: "FK_DietaryHabitsHistory_ClinicalHistories_ClinicalHistoryId",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FamilyHistoryDetails",
                columns: table => new
                {
                    FamilyHistoryDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalRecordNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    appearanceAge = table.Column<int>(type: "int", nullable: true),
                    DiseaseId = table.Column<int>(type: "int", nullable: true),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false),
                    RelationshipId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyHistoryDetails", x => x.FamilyHistoryDetailId);
                    table.ForeignKey(
                        name: "FK_FamilyHistoryDetails_ClinicalHistories_ClinicalHistoryId",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FamilyHistoryDetails_Diseases_DiseaseId",
                        column: x => x.DiseaseId,
                        principalTable: "Diseases",
                        principalColumn: "DiseaseId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_FamilyHistoryDetails_Relationships_RelationshipId",
                        column: x => x.RelationshipId,
                        principalTable: "Relationships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodConsumptionHistory",
                columns: table => new
                {
                    FoodConsumptionHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Hour = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Place = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodConsumptionHistory", x => x.FoodConsumptionHistoryId);
                    table.ForeignKey(
                        name: "FK_FoodConsumptionHistory_ClinicalHistories_ClinicalHistoryId",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodConsumptionHistory_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "FoodId");
                });

            migrationBuilder.CreateTable(
                name: "FoodIntoleranceHistories",
                columns: table => new
                {
                    FoodIntoleranceHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClinicalHistoryId = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    HistoryNumber = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    FoodId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodIntoleranceHistories", x => x.FoodIntoleranceHistoryId);
                    table.ForeignKey(
                        name: "FK_FoodIntoleranceHistory_ClinicalHistory",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodIntoleranceHistory_Food",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "FoodId");
                });

            migrationBuilder.CreateTable(
                name: "GynecologicalHistories",
                columns: table => new
                {
                    GynecologicalHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalRecordNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GynecologicalDevelopment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Menarche = table.Column<DateOnly>(type: "date", nullable: true),
                    Pubarche = table.Column<DateOnly>(type: "date", nullable: true),
                    MenstrualCycles = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastMenstruation = table.Column<DateOnly>(type: "date", nullable: true),
                    ContraceptiveMethods = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GynecologicalHistories", x => x.GynecologicalHistoryId);
                    table.ForeignKey(
                        name: "FK_GynecologicalHistories_ClinicalHistories_ClinicalHistoryId",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HabitHistories",
                columns: table => new
                {
                    HabitHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecordDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HabitId = table.Column<int>(type: "int", nullable: false),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitHistories", x => x.HabitHistoryId);
                    table.ForeignKey(
                        name: "FK_HabitHistory_ClinicalHistory",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HabitHistory_Habits",
                        column: x => x.HabitId,
                        principalTable: "Habits",
                        principalColumn: "HabitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LifeStyleHistory",
                columns: table => new
                {
                    LifeStyleHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false),
                    LifeStyleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeStyleHistory", x => x.LifeStyleHistoryId);
                    table.ForeignKey(
                        name: "FK_LifeStyleHistory_ClinicalHistories_ClinicalHistoryId",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LifeStyleHistory_LifeStyle_LifeStyleId",
                        column: x => x.LifeStyleId,
                        principalTable: "LifeStyle",
                        principalColumn: "LifeStyleId");
                });

            migrationBuilder.CreateTable(
                name: "ObstetricHistories",
                columns: table => new
                {
                    ObstetricHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryNumber = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false),
                    CurrentPregnancy = table.Column<bool>(type: "bit", nullable: true),
                    PreviousPregnancies = table.Column<bool>(type: "bit", nullable: true),
                    Deliveries = table.Column<bool>(type: "bit", nullable: true),
                    Abortions = table.Column<bool>(type: "bit", nullable: true),
                    CSections = table.Column<bool>(type: "bit", nullable: true),
                    LiveBirths = table.Column<int>(type: "int", nullable: true),
                    Stillbirths = table.Column<int>(type: "int", nullable: true),
                    LivingChildren = table.Column<int>(type: "int", nullable: true),
                    Breastfeeding = table.Column<bool>(type: "bit", nullable: true),
                    GestionalAge = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpectedDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObstetricHistories", x => x.ObstetricHistoryId);
                    table.ForeignKey(
                        name: "FK_ObstetricHistory_ClinicalHistory",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalHistories",
                columns: table => new
                {
                    PersonalHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalRecordNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DiseaseId = table.Column<int>(type: "int", nullable: true),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalHistories", x => x.PersonalHistoryId);
                    table.ForeignKey(
                        name: "FK_PersonalHistories_ClinicalHistories_ClinicalHistoryId",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalHistories_Diseases_DiseaseId",
                        column: x => x.DiseaseId,
                        principalTable: "Diseases",
                        principalColumn: "DiseaseId");
                });

            migrationBuilder.CreateTable(
                name: "SleepHabitHistory",
                columns: table => new
                {
                    HabitSleepHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecordDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SleepHabitId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SleepHabitHistory", x => x.HabitSleepHistoryId);
                    table.ForeignKey(
                        name: "FK_SleepHabitHistory_ClinicalHistories_ClinicalHistoryId",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SleepHabitHistory_SleepHabit_SleepHabitId",
                        column: x => x.SleepHabitId,
                        principalTable: "SleepHabit",
                        principalColumn: "SleepHabitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SportsActivitiesHistory",
                columns: table => new
                {
                    SportActivityHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinutesPerDay = table.Column<int>(type: "int", nullable: false),
                    NumberOfDays = table.Column<int>(type: "int", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false),
                    SportActivityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportsActivitiesHistory", x => x.SportActivityHistoryId);
                    table.ForeignKey(
                        name: "FK_SportsActivitiesHistory_ClinicalHistories_ClinicalHistoryId",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SportsActivitiesHistory_SportsActivities_SportActivityId",
                        column: x => x.SportActivityId,
                        principalTable: "SportsActivities",
                        principalColumn: "SportActivityId");
                });

            migrationBuilder.CreateTable(
                name: "SurgeryHistories",
                columns: table => new
                {
                    SurgeryHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryNumber = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    SurgeryDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false),
                    SurgeryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurgeryHistories", x => x.SurgeryHistoryId);
                    table.ForeignKey(
                        name: "FK_SurgeryHistory_ClinicalHistory",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurgeryHistory_Surgery",
                        column: x => x.SurgeryId,
                        principalTable: "Surgeries",
                        principalColumn: "SurgeryId");
                });

            migrationBuilder.CreateTable(
                name: "ToxicHabitHistories",
                columns: table => new
                {
                    ToxicHabitBackgroundId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryNumber = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    RecordDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ToxicHabitId = table.Column<int>(type: "int", nullable: true),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToxicHabitHistories", x => x.ToxicHabitBackgroundId);
                    table.ForeignKey(
                        name: "FK_ToxicHabitBackground_ClinicalHistory",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ToxicHabitBackground_ToxicHabit",
                        column: x => x.ToxicHabitId,
                        principalTable: "ToxicHabits",
                        principalColumn: "ToxicHabitId");
                });

            migrationBuilder.CreateTable(
                name: "WaterConsumptionHistory",
                columns: table => new
                {
                    WaterConsumptionHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalRecordNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Frequency = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaterConsumptionHistory", x => x.WaterConsumptionHistoryId);
                    table.ForeignKey(
                        name: "FK_WaterConsumptionHistory_ClinicalHistories_ClinicalHistoryId",
                        column: x => x.ClinicalHistoryId,
                        principalTable: "ClinicalHistories",
                        principalColumn: "ClinicalHistoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Diagnosis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cie10 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Denomination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiagnosticType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recurrence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiagnosisMotivation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicalCareId = table.Column<int>(type: "int", nullable: false),
                    DiseaseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnosis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diagnosis_Diseases_DiseaseId",
                        column: x => x.DiseaseId,
                        principalTable: "Diseases",
                        principalColumn: "DiseaseId");
                    table.ForeignKey(
                        name: "FK_Diagnosis_MedicalCares_MedicalCareId",
                        column: x => x.MedicalCareId,
                        principalTable: "MedicalCares",
                        principalColumn: "CareId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Evolutions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Percentage = table.Column<float>(type: "real", nullable: false),
                    MedicalCareId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evolutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evolutions_MedicalCares_MedicalCareId",
                        column: x => x.MedicalCareId,
                        principalTable: "MedicalCares",
                        principalColumn: "CareId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkPdf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExamDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExamTypeId = table.Column<int>(type: "int", nullable: false),
                    MedicalCareId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamResults_ExamTypes_ExamTypeId",
                        column: x => x.ExamTypeId,
                        principalTable: "ExamTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamResults_MedicalCares_MedicalCareId",
                        column: x => x.MedicalCareId,
                        principalTable: "MedicalCares",
                        principalColumn: "CareId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentifiedDiseases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiseaseId = table.Column<int>(type: "int", nullable: false),
                    MedicalCareId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentifiedDiseases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentifiedDiseases_Diseases_DiseaseId",
                        column: x => x.DiseaseId,
                        principalTable: "Diseases",
                        principalColumn: "DiseaseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IdentifiedDiseases_MedicalCares_MedicalCareId",
                        column: x => x.MedicalCareId,
                        principalTable: "MedicalCares",
                        principalColumn: "CareId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalProcedures",
                columns: table => new
                {
                    ProcedureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcedureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SpecificProcedureId = table.Column<int>(type: "int", nullable: false),
                    CareId = table.Column<int>(type: "int", nullable: true),
                    HealthProfessionalId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    TreatingPhysicianId = table.Column<int>(type: "int", nullable: true),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalProcedures", x => x.ProcedureId);
                    table.ForeignKey(
                        name: "FK_MedicalProcedures_HealthProfessionals_HealthProfessionalId",
                        column: x => x.HealthProfessionalId,
                        principalTable: "HealthProfessionals",
                        principalColumn: "HealthProfessionalId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalProcedures_HealthProfessionals_TreatingPhysicianId",
                        column: x => x.TreatingPhysicianId,
                        principalTable: "HealthProfessionals",
                        principalColumn: "HealthProfessionalId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalProcedures_MedicalCares_CareId",
                        column: x => x.CareId,
                        principalTable: "MedicalCares",
                        principalColumn: "CareId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalProcedures_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalProcedures_Procedures_SpecificProcedureId",
                        column: x => x.SpecificProcedureId,
                        principalTable: "Procedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalReferrals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfReferral = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    MedicalCareId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalReferrals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalReferrals_MedicalCares_MedicalCareId",
                        column: x => x.MedicalCareId,
                        principalTable: "MedicalCares",
                        principalColumn: "CareId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalReferrals_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalServices",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CareId = table.Column<int>(type: "int", nullable: true),
                    ServiceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ServiceType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    HealthProfessionalId = table.Column<int>(type: "int", nullable: false),
                    Recommendations = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalServices", x => x.ServiceId);
                    table.ForeignKey(
                        name: "FK_MedicalServices_HealthProfessionals_HealthProfessionalId",
                        column: x => x.HealthProfessionalId,
                        principalTable: "HealthProfessionals",
                        principalColumn: "HealthProfessionalId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalServices_MedicalCares_CareId",
                        column: x => x.CareId,
                        principalTable: "MedicalCares",
                        principalColumn: "CareId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalServices_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhysicalExams",
                columns: table => new
                {
                    PhysicalExamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    PathologicalEvidenceId = table.Column<int>(type: "int", nullable: false),
                    PhysicalExamTypeId = table.Column<int>(type: "int", nullable: true),
                    MedicalCareId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalExams", x => x.PhysicalExamId);
                    table.ForeignKey(
                        name: "FK_PhysicalExams_MedicalCares_MedicalCareId",
                        column: x => x.MedicalCareId,
                        principalTable: "MedicalCares",
                        principalColumn: "CareId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhysicalExams_PathologicalEvidence_PathologicalEvidenceId",
                        column: x => x.PathologicalEvidenceId,
                        principalTable: "PathologicalEvidence",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhysicalExams_PhysicalExamTypes_PhysicalExamTypeId",
                        column: x => x.PhysicalExamTypeId,
                        principalTable: "PhysicalExamTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PhysicalExams_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReasonForConsultations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicalCareId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReasonForConsultations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReasonForConsultations_MedicalCares_MedicalCareId",
                        column: x => x.MedicalCareId,
                        principalTable: "MedicalCares",
                        principalColumn: "CareId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewSystemDevices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemsDevicesId = table.Column<int>(type: "int", nullable: false),
                    MedicalCareId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewSystemDevices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewSystemDevices_MedicalCares_MedicalCareId",
                        column: x => x.MedicalCareId,
                        principalTable: "MedicalCares",
                        principalColumn: "CareId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReviewSystemDevices_SystemsDevices_SystemsDevicesId",
                        column: x => x.SystemsDevicesId,
                        principalTable: "SystemsDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VitalSigns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Icm = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AbdominalCircumference = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BloodPressure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Temperature = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MeanArterialPressure = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HeartRate = table.Column<int>(type: "int", nullable: true),
                    OxygenSaturation = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RespiratoryRate = table.Column<int>(type: "int", nullable: true),
                    BloodGlucose = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Hemoglobin = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MedicalCareId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VitalSigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VitalSigns_MedicalCares_MedicalCareId",
                        column: x => x.MedicalCareId,
                        principalTable: "MedicalCares",
                        principalColumn: "CareId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Progresses",
                columns: table => new
                {
                    ProgressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Observation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgressPercentage = table.Column<double>(type: "float", nullable: false),
                    MedicalVisitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progresses", x => x.ProgressId);
                    table.ForeignKey(
                        name: "FK_Progresses_MedicalVisits_MedicalVisitId",
                        column: x => x.MedicalVisitId,
                        principalTable: "MedicalVisits",
                        principalColumn: "MedicalVisitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiagnosisTreatment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiagnosisId = table.Column<int>(type: "int", nullable: false),
                    TreatmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosisTreatment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiagnosisTreatment_Diagnosis_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "Diagnosis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiagnosisTreatment_Treatments_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Interconsultations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InterconsultationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiagnosisId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interconsultations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interconsultations_Diagnosis_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "Diagnosis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Interconsultations_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDiagnosis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    DiagnosisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDiagnosis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDiagnosis_Diagnosis_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "Diagnosis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDiagnosis_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllergyHistories_AllergyId",
                table: "AllergyHistories",
                column: "AllergyId");

            migrationBuilder.CreateIndex(
                name: "IX_AllergyHistories_ClinicalHistoryId",
                table: "AllergyHistories",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_City_ProvinceId",
                table: "City",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalHistories_HistoryNumber",
                table: "ClinicalHistories",
                column: "HistoryNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalHistories_PatientId",
                table: "ClinicalHistories",
                column: "PatientId",
                unique: true,
                filter: "[PatientId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CostOfServices_ServiceId",
                table: "CostOfServices",
                column: "ServiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosis_DiseaseId",
                table: "Diagnosis",
                column: "DiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosis_MedicalCareId",
                table: "Diagnosis",
                column: "MedicalCareId");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosisTreatment_DiagnosisId",
                table: "DiagnosisTreatment",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosisTreatment_TreatmentId",
                table: "DiagnosisTreatment",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DietaryHabitsHistory_ClinicalHistoryId",
                table: "DietaryHabitsHistory",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Diseases_DiseaseTypeId",
                table: "Diseases",
                column: "DiseaseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyContacts_PatientId",
                table: "EmergencyContacts",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Evolutions_MedicalCareId",
                table: "Evolutions",
                column: "MedicalCareId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamResults_ExamTypeId",
                table: "ExamResults",
                column: "ExamTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamResults_MedicalCareId",
                table: "ExamResults",
                column: "MedicalCareId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyHistoryDetails_ClinicalHistoryId",
                table: "FamilyHistoryDetails",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyHistoryDetails_DiseaseId",
                table: "FamilyHistoryDetails",
                column: "DiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyHistoryDetails_RelationshipId",
                table: "FamilyHistoryDetails",
                column: "RelationshipId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodConsumptionHistory_ClinicalHistoryId",
                table: "FoodConsumptionHistory",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodConsumptionHistory_FoodId",
                table: "FoodConsumptionHistory",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodIntoleranceHistories_ClinicalHistoryId",
                table: "FoodIntoleranceHistories",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodIntoleranceHistories_FoodId",
                table: "FoodIntoleranceHistories",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodPlan_RecommendedFoodId",
                table: "FoodPlan",
                column: "RecommendedFoodId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodPlan_RestrictionId",
                table: "FoodPlan",
                column: "RestrictionId",
                unique: true,
                filter: "[RestrictionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GynecologicalHistories_ClinicalHistoryId",
                table: "GynecologicalHistories",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_HabitHistories_ClinicalHistoryId",
                table: "HabitHistories",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_HabitHistories_HabitId",
                table: "HabitHistories",
                column: "HabitId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthProfessionals_HealthProfessionalTypeId",
                table: "HealthProfessionals",
                column: "HealthProfessionalTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentifiedDiseases_DiseaseId",
                table: "IdentifiedDiseases",
                column: "DiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentifiedDiseases_MedicalCareId",
                table: "IdentifiedDiseases",
                column: "MedicalCareId");

            migrationBuilder.CreateIndex(
                name: "IX_Indications_TreatmentId",
                table: "Indications",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Interconsultations_DiagnosisId",
                table: "Interconsultations",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_Interconsultations_ServiceId",
                table: "Interconsultations",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_LifeStyleHistory_ClinicalHistoryId",
                table: "LifeStyleHistory",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_LifeStyleHistory_LifeStyleId",
                table: "LifeStyleHistory",
                column: "LifeStyleId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalCares_HealthProfessionalId",
                table: "MedicalCares",
                column: "HealthProfessionalId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalCares_LocationId",
                table: "MedicalCares",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalCares_PatientId",
                table: "MedicalCares",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalProcedures_CareId",
                table: "MedicalProcedures",
                column: "CareId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalProcedures_HealthProfessionalId",
                table: "MedicalProcedures",
                column: "HealthProfessionalId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalProcedures_PatientId",
                table: "MedicalProcedures",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalProcedures_SpecificProcedureId",
                table: "MedicalProcedures",
                column: "SpecificProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalProcedures_TreatingPhysicianId",
                table: "MedicalProcedures",
                column: "TreatingPhysicianId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalReferrals_MedicalCareId",
                table: "MedicalReferrals",
                column: "MedicalCareId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalReferrals_ServiceId",
                table: "MedicalReferrals",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalServices_CareId",
                table: "MedicalServices",
                column: "CareId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalServices_HealthProfessionalId",
                table: "MedicalServices",
                column: "HealthProfessionalId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalServices_PatientId",
                table: "MedicalServices",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalVisits_PatientId",
                table: "MedicalVisits",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_ObstetricHistories_ClinicalHistoryId",
                table: "ObstetricHistories",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDiagnosis_DiagnosisId",
                table: "OrderDiagnosis",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDiagnosis_OrderId_DiagnosisId",
                table: "OrderDiagnosis",
                columns: new[] { "OrderId", "DiagnosisId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientRelationships_RelationshipId",
                table: "PatientRelationships",
                column: "RelationshipId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalHistories_ClinicalHistoryId",
                table: "PersonalHistories",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalHistories_DiseaseId",
                table: "PersonalHistories",
                column: "DiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonBloodGroups_BloodGroupId",
                table: "PersonBloodGroups",
                column: "BloodGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonDocuments_DocumentTypeId",
                table: "PersonDocuments",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonEducations_EducationLevelId",
                table: "PersonEducations",
                column: "EducationLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonLaborActivities_LaborActivityId",
                table: "PersonLaborActivities",
                column: "LaborActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonLateralities_LateralityId",
                table: "PersonLateralities",
                column: "LateralityId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonMaritalStatuses_MaritalStatusId",
                table: "PersonMaritalStatuses",
                column: "MaritalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonMedicalInsurances_MedicalInsuranceId",
                table: "PersonMedicalInsurances",
                column: "MedicalInsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonMedicalInsurances_PersonId",
                table: "PersonMedicalInsurances",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonProfessions_ProfessionId",
                table: "PersonProfessions",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonReligions_ReligionId",
                table: "PersonReligions",
                column: "ReligionId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonResidences_CityId",
                table: "PersonResidences",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_GenderId",
                table: "Persons",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalExams_MedicalCareId",
                table: "PhysicalExams",
                column: "MedicalCareId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalExams_PathologicalEvidenceId",
                table: "PhysicalExams",
                column: "PathologicalEvidenceId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalExams_PhysicalExamTypeId",
                table: "PhysicalExams",
                column: "PhysicalExamTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalExams_RegionId",
                table: "PhysicalExams",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Procedures_TypeOfProcedureId",
                table: "Procedures",
                column: "TypeOfProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_Progresses_MedicalVisitId",
                table: "Progresses",
                column: "MedicalVisitId");

            migrationBuilder.CreateIndex(
                name: "IX_ReasonForConsultations_MedicalCareId",
                table: "ReasonForConsultations",
                column: "MedicalCareId");

            migrationBuilder.CreateIndex(
                name: "IX_RecommendedFoods_FoodId",
                table: "RecommendedFoods",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Restriction_FoodId",
                table: "Restriction",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewSystemDevices_MedicalCareId",
                table: "ReviewSystemDevices",
                column: "MedicalCareId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReviewSystemDevices_SystemsDevicesId",
                table: "ReviewSystemDevices",
                column: "SystemsDevicesId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_TypeOfServiceId",
                table: "Services",
                column: "TypeOfServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SleepHabitHistory_ClinicalHistoryId",
                table: "SleepHabitHistory",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SleepHabitHistory_SleepHabitId",
                table: "SleepHabitHistory",
                column: "SleepHabitId");

            migrationBuilder.CreateIndex(
                name: "IX_SportsActivitiesHistory_ClinicalHistoryId",
                table: "SportsActivitiesHistory",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SportsActivitiesHistory_SportActivityId",
                table: "SportsActivitiesHistory",
                column: "SportActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_SurgeryHistories_ClinicalHistoryId",
                table: "SurgeryHistories",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SurgeryHistories_SurgeryId",
                table: "SurgeryHistories",
                column: "SurgeryId");

            migrationBuilder.CreateIndex(
                name: "IX_ToxicHabitHistories_ClinicalHistoryId",
                table: "ToxicHabitHistories",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ToxicHabitHistories_ToxicHabitId",
                table: "ToxicHabitHistories",
                column: "ToxicHabitId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_MedicineId",
                table: "Treatments",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PersonId",
                table: "Users",
                column: "PersonId",
                unique: true,
                filter: "[PersonId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_VitalSigns_MedicalCareId",
                table: "VitalSigns",
                column: "MedicalCareId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WaterConsumptionHistory_ClinicalHistoryId",
                table: "WaterConsumptionHistory",
                column: "ClinicalHistoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllergyHistories");

            migrationBuilder.DropTable(
                name: "CostOfServices");

            migrationBuilder.DropTable(
                name: "DiagnosisTreatment");

            migrationBuilder.DropTable(
                name: "DietaryHabitsHistory");

            migrationBuilder.DropTable(
                name: "EmergencyContacts");

            migrationBuilder.DropTable(
                name: "Evolutions");

            migrationBuilder.DropTable(
                name: "ExamResults");

            migrationBuilder.DropTable(
                name: "FamilyHistoryDetails");

            migrationBuilder.DropTable(
                name: "FoodConsumptionHistory");

            migrationBuilder.DropTable(
                name: "FoodIntoleranceHistories");

            migrationBuilder.DropTable(
                name: "FoodPlan");

            migrationBuilder.DropTable(
                name: "GynecologicalHistories");

            migrationBuilder.DropTable(
                name: "HabitHistories");

            migrationBuilder.DropTable(
                name: "IdentifiedDiseases");

            migrationBuilder.DropTable(
                name: "Indications");

            migrationBuilder.DropTable(
                name: "Interconsultations");

            migrationBuilder.DropTable(
                name: "LifeStyleHistory");

            migrationBuilder.DropTable(
                name: "MedicalProcedures");

            migrationBuilder.DropTable(
                name: "MedicalReferrals");

            migrationBuilder.DropTable(
                name: "MedicalServices");

            migrationBuilder.DropTable(
                name: "ObstetricHistories");

            migrationBuilder.DropTable(
                name: "OrderDiagnosis");

            migrationBuilder.DropTable(
                name: "PatientRelationships");

            migrationBuilder.DropTable(
                name: "PersonAddresses");

            migrationBuilder.DropTable(
                name: "PersonalHistories");

            migrationBuilder.DropTable(
                name: "PersonBloodGroups");

            migrationBuilder.DropTable(
                name: "PersonDocuments");

            migrationBuilder.DropTable(
                name: "PersonEducations");

            migrationBuilder.DropTable(
                name: "PersonLaborActivities");

            migrationBuilder.DropTable(
                name: "PersonLateralities");

            migrationBuilder.DropTable(
                name: "PersonMaritalStatuses");

            migrationBuilder.DropTable(
                name: "PersonMedicalInsurances");

            migrationBuilder.DropTable(
                name: "PersonPhones");

            migrationBuilder.DropTable(
                name: "PersonProfessions");

            migrationBuilder.DropTable(
                name: "PersonReligions");

            migrationBuilder.DropTable(
                name: "PersonResidences");

            migrationBuilder.DropTable(
                name: "PhysicalExams");

            migrationBuilder.DropTable(
                name: "Progresses");

            migrationBuilder.DropTable(
                name: "ReasonForConsultations");

            migrationBuilder.DropTable(
                name: "ReviewSystemDevices");

            migrationBuilder.DropTable(
                name: "SleepHabitHistory");

            migrationBuilder.DropTable(
                name: "SportsActivitiesHistory");

            migrationBuilder.DropTable(
                name: "SurgeryHistories");

            migrationBuilder.DropTable(
                name: "ToxicHabitHistories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "VitalSigns");

            migrationBuilder.DropTable(
                name: "WaterConsumptionHistory");

            migrationBuilder.DropTable(
                name: "Allergies");

            migrationBuilder.DropTable(
                name: "ExamTypes");

            migrationBuilder.DropTable(
                name: "RecommendedFoods");

            migrationBuilder.DropTable(
                name: "Restriction");

            migrationBuilder.DropTable(
                name: "Habits");

            migrationBuilder.DropTable(
                name: "Treatments");

            migrationBuilder.DropTable(
                name: "LifeStyle");

            migrationBuilder.DropTable(
                name: "Procedures");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Diagnosis");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Relationships");

            migrationBuilder.DropTable(
                name: "BloodGroups");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "EducationLevels");

            migrationBuilder.DropTable(
                name: "LaborActivities");

            migrationBuilder.DropTable(
                name: "LateralityTypes");

            migrationBuilder.DropTable(
                name: "MaritalStatuses");

            migrationBuilder.DropTable(
                name: "MedicalInsurances");

            migrationBuilder.DropTable(
                name: "Professions");

            migrationBuilder.DropTable(
                name: "Religions");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "PathologicalEvidence");

            migrationBuilder.DropTable(
                name: "PhysicalExamTypes");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropTable(
                name: "MedicalVisits");

            migrationBuilder.DropTable(
                name: "SystemsDevices");

            migrationBuilder.DropTable(
                name: "SleepHabit");

            migrationBuilder.DropTable(
                name: "SportsActivities");

            migrationBuilder.DropTable(
                name: "Surgeries");

            migrationBuilder.DropTable(
                name: "ToxicHabits");

            migrationBuilder.DropTable(
                name: "ClinicalHistories");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "TypeOfProcedures");

            migrationBuilder.DropTable(
                name: "TypeOfServices");

            migrationBuilder.DropTable(
                name: "Diseases");

            migrationBuilder.DropTable(
                name: "MedicalCares");

            migrationBuilder.DropTable(
                name: "Pronvince");

            migrationBuilder.DropTable(
                name: "DiseaseTypes");

            migrationBuilder.DropTable(
                name: "HealthProfessionals");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "PlaceOfAttentions");

            migrationBuilder.DropTable(
                name: "HealthProfessionalTypes");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Genders");
        }
    }
}
