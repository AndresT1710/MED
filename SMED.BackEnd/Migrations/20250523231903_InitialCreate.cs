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
                    LaborActivityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonLaborActivities", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_PersonLaborActivities_LaborActivities_LaborActivityId",
                        column: x => x.LaborActivityId,
                        principalTable: "LaborActivities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PersonLaborActivities_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonLateralityTypes",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    LateralityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonLateralityTypes", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_PersonLateralityTypes_LateralityTypes_LateralityId",
                        column: x => x.LateralityId,
                        principalTable: "LateralityTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PersonLateralityTypes_Persons_PersonId",
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
                    Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
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
                name: "FamilyHistoryDetails",
                columns: table => new
                {
                    FamilyHistoryDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicalRecordNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DiseaseId = table.Column<int>(type: "int", nullable: true),
                    ClinicalHistoryId = table.Column<int>(type: "int", nullable: false)
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
                        principalColumn: "DiseaseId");
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
                    DiseaseId = table.Column<int>(type: "int", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_GynecologicalHistories_Diseases_DiseaseId",
                        column: x => x.DiseaseId,
                        principalTable: "Diseases",
                        principalColumn: "DiseaseId");
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
                    DiseaseId = table.Column<int>(type: "int", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_ObstetricHistory_Disease",
                        column: x => x.DiseaseId,
                        principalTable: "Diseases",
                        principalColumn: "DiseaseId");
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
                name: "SurgeryHistories",
                columns: table => new
                {
                    SurgeryHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryNumber = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime", nullable: true),
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
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Diseases_DiseaseTypeId",
                table: "Diseases",
                column: "DiseaseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyContacts_PatientId",
                table: "EmergencyContacts",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyHistoryDetails_ClinicalHistoryId",
                table: "FamilyHistoryDetails",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyHistoryDetails_DiseaseId",
                table: "FamilyHistoryDetails",
                column: "DiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodIntoleranceHistories_ClinicalHistoryId",
                table: "FoodIntoleranceHistories",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodIntoleranceHistories_FoodId",
                table: "FoodIntoleranceHistories",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_GynecologicalHistories_ClinicalHistoryId",
                table: "GynecologicalHistories",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GynecologicalHistories_DiseaseId",
                table: "GynecologicalHistories",
                column: "DiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthProfessionals_HealthProfessionalTypeId",
                table: "HealthProfessionals",
                column: "HealthProfessionalTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalVisits_PatientId",
                table: "MedicalVisits",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_ObstetricHistories_ClinicalHistoryId",
                table: "ObstetricHistories",
                column: "ClinicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ObstetricHistories_DiseaseId",
                table: "ObstetricHistories",
                column: "DiseaseId");

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
                name: "IX_PersonLateralityTypes_LateralityId",
                table: "PersonLateralityTypes",
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
                name: "IX_Progresses_MedicalVisitId",
                table: "Progresses",
                column: "MedicalVisitId");

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
                name: "IX_Users_PersonId",
                table: "Users",
                column: "PersonId",
                unique: true,
                filter: "[PersonId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllergyHistories");

            migrationBuilder.DropTable(
                name: "EmergencyContacts");

            migrationBuilder.DropTable(
                name: "FamilyHistoryDetails");

            migrationBuilder.DropTable(
                name: "FoodIntoleranceHistories");

            migrationBuilder.DropTable(
                name: "GynecologicalHistories");

            migrationBuilder.DropTable(
                name: "HealthProfessionals");

            migrationBuilder.DropTable(
                name: "ObstetricHistories");

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
                name: "PersonLateralityTypes");

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
                name: "Progresses");

            migrationBuilder.DropTable(
                name: "SurgeryHistories");

            migrationBuilder.DropTable(
                name: "ToxicHabitHistories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Allergies");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "HealthProfessionalTypes");

            migrationBuilder.DropTable(
                name: "Relationships");

            migrationBuilder.DropTable(
                name: "Diseases");

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
                name: "MedicalVisits");

            migrationBuilder.DropTable(
                name: "Surgeries");

            migrationBuilder.DropTable(
                name: "ClinicalHistories");

            migrationBuilder.DropTable(
                name: "ToxicHabits");

            migrationBuilder.DropTable(
                name: "DiseaseTypes");

            migrationBuilder.DropTable(
                name: "Pronvince");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Genders");
        }
    }
}
