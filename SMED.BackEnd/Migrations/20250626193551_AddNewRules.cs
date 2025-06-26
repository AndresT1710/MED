using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMED.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddNewRules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diagnoses_Diseases_DiseaseId",
                table: "Diagnoses");

            migrationBuilder.DropForeignKey(
                name: "FK_Diagnoses_MedicalCares_MedicalCareId",
                table: "Diagnoses");

            migrationBuilder.DropForeignKey(
                name: "FK_DiagnosisTreatment_Diagnoses_DiagnosisId",
                table: "DiagnosisTreatment");

            migrationBuilder.DropForeignKey(
                name: "FK_Interconsultations_Diagnoses_DiagnosisId",
                table: "Interconsultations");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDiagnoses_Diagnoses_DiagnosisId",
                table: "OrderDiagnoses");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDiagnoses_Orders_OrderId",
                table: "OrderDiagnoses");

            migrationBuilder.DropForeignKey(
                name: "FK_VitalSigns_MedicalCares_MedicalCareId",
                table: "VitalSigns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDiagnoses",
                table: "OrderDiagnoses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Diagnoses",
                table: "Diagnoses");

            migrationBuilder.RenameTable(
                name: "OrderDiagnoses",
                newName: "OrderDiagnosis");

            migrationBuilder.RenameTable(
                name: "Diagnoses",
                newName: "Diagnosis");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDiagnoses_OrderId_DiagnosisId",
                table: "OrderDiagnosis",
                newName: "IX_OrderDiagnosis_OrderId_DiagnosisId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDiagnoses_DiagnosisId",
                table: "OrderDiagnosis",
                newName: "IX_OrderDiagnosis_DiagnosisId");

            migrationBuilder.RenameIndex(
                name: "IX_Diagnoses_MedicalCareId",
                table: "Diagnosis",
                newName: "IX_Diagnosis_MedicalCareId");

            migrationBuilder.RenameIndex(
                name: "IX_Diagnoses_DiseaseId",
                table: "Diagnosis",
                newName: "IX_Diagnosis_DiseaseId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Weight",
                table: "VitalSigns",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Temperature",
                table: "VitalSigns",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "RespiratoryRate",
                table: "VitalSigns",
                type: "int",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "OxygenSaturation",
                table: "VitalSigns",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "MeanArterialPressure",
                table: "VitalSigns",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Icm",
                table: "VitalSigns",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Hemoglobin",
                table: "VitalSigns",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "Height",
                table: "VitalSigns",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HeartRate",
                table: "VitalSigns",
                type: "int",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<string>(
                name: "BloodPressure",
                table: "VitalSigns",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "BloodGlucose",
                table: "VitalSigns",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "AbdominalCircumference",
                table: "VitalSigns",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDiagnosis",
                table: "OrderDiagnosis",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Diagnosis",
                table: "Diagnosis",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnosis_Diseases_DiseaseId",
                table: "Diagnosis",
                column: "DiseaseId",
                principalTable: "Diseases",
                principalColumn: "DiseaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnosis_MedicalCares_MedicalCareId",
                table: "Diagnosis",
                column: "MedicalCareId",
                principalTable: "MedicalCares",
                principalColumn: "CareId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosisTreatment_Diagnosis_DiagnosisId",
                table: "DiagnosisTreatment",
                column: "DiagnosisId",
                principalTable: "Diagnosis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Interconsultations_Diagnosis_DiagnosisId",
                table: "Interconsultations",
                column: "DiagnosisId",
                principalTable: "Diagnosis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDiagnosis_Diagnosis_DiagnosisId",
                table: "OrderDiagnosis",
                column: "DiagnosisId",
                principalTable: "Diagnosis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDiagnosis_Orders_OrderId",
                table: "OrderDiagnosis",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VitalSigns_MedicalCares_MedicalCareId",
                table: "VitalSigns",
                column: "MedicalCareId",
                principalTable: "MedicalCares",
                principalColumn: "CareId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diagnosis_Diseases_DiseaseId",
                table: "Diagnosis");

            migrationBuilder.DropForeignKey(
                name: "FK_Diagnosis_MedicalCares_MedicalCareId",
                table: "Diagnosis");

            migrationBuilder.DropForeignKey(
                name: "FK_DiagnosisTreatment_Diagnosis_DiagnosisId",
                table: "DiagnosisTreatment");

            migrationBuilder.DropForeignKey(
                name: "FK_Interconsultations_Diagnosis_DiagnosisId",
                table: "Interconsultations");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDiagnosis_Diagnosis_DiagnosisId",
                table: "OrderDiagnosis");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDiagnosis_Orders_OrderId",
                table: "OrderDiagnosis");

            migrationBuilder.DropForeignKey(
                name: "FK_VitalSigns_MedicalCares_MedicalCareId",
                table: "VitalSigns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDiagnosis",
                table: "OrderDiagnosis");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Diagnosis",
                table: "Diagnosis");

            migrationBuilder.RenameTable(
                name: "OrderDiagnosis",
                newName: "OrderDiagnoses");

            migrationBuilder.RenameTable(
                name: "Diagnosis",
                newName: "Diagnoses");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDiagnosis_OrderId_DiagnosisId",
                table: "OrderDiagnoses",
                newName: "IX_OrderDiagnoses_OrderId_DiagnosisId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDiagnosis_DiagnosisId",
                table: "OrderDiagnoses",
                newName: "IX_OrderDiagnoses_DiagnosisId");

            migrationBuilder.RenameIndex(
                name: "IX_Diagnosis_MedicalCareId",
                table: "Diagnoses",
                newName: "IX_Diagnoses_MedicalCareId");

            migrationBuilder.RenameIndex(
                name: "IX_Diagnosis_DiseaseId",
                table: "Diagnoses",
                newName: "IX_Diagnoses_DiseaseId");

            migrationBuilder.AlterColumn<int>(
                name: "Weight",
                table: "VitalSigns",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Temperature",
                table: "VitalSigns",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "RespiratoryRate",
                table: "VitalSigns",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "OxygenSaturation",
                table: "VitalSigns",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MeanArterialPressure",
                table: "VitalSigns",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Icm",
                table: "VitalSigns",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Hemoglobin",
                table: "VitalSigns",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Height",
                table: "VitalSigns",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "HeartRate",
                table: "VitalSigns",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BloodPressure",
                table: "VitalSigns",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "BloodGlucose",
                table: "VitalSigns",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "AbdominalCircumference",
                table: "VitalSigns",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDiagnoses",
                table: "OrderDiagnoses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Diagnoses",
                table: "Diagnoses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnoses_Diseases_DiseaseId",
                table: "Diagnoses",
                column: "DiseaseId",
                principalTable: "Diseases",
                principalColumn: "DiseaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnoses_MedicalCares_MedicalCareId",
                table: "Diagnoses",
                column: "MedicalCareId",
                principalTable: "MedicalCares",
                principalColumn: "CareId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosisTreatment_Diagnoses_DiagnosisId",
                table: "DiagnosisTreatment",
                column: "DiagnosisId",
                principalTable: "Diagnoses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Interconsultations_Diagnoses_DiagnosisId",
                table: "Interconsultations",
                column: "DiagnosisId",
                principalTable: "Diagnoses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDiagnoses_Diagnoses_DiagnosisId",
                table: "OrderDiagnoses",
                column: "DiagnosisId",
                principalTable: "Diagnoses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDiagnoses_Orders_OrderId",
                table: "OrderDiagnoses",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VitalSigns_MedicalCares_MedicalCareId",
                table: "VitalSigns",
                column: "MedicalCareId",
                principalTable: "MedicalCares",
                principalColumn: "CareId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
