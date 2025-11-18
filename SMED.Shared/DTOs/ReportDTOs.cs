using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SMED.Shared.DTOs
{
    public class ReportRequestDTO
    {
        public string ReportType { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Format { get; set; } = "PDF";
        public string? Area { get; set; }
        public int? LocationId { get; set; }
        public int? HealthProfessionalId { get; set; }
        public int? PlaceOfAttentionId { get; set; }
        public string? DocumentNumber { get; set; }
        public bool IncludeAllRecords { get; set; } = true; // NUEVO: Por defecto traer todos los registros
    }

    public class ReportResultDTO
    {
        public string FileName { get; set; } = string.Empty;
        public byte[] Content { get; set; } = Array.Empty<byte>();
        public string ContentType { get; set; } = string.Empty;
        public string ReportTitle { get; set; } = string.Empty;
        public DateTime GeneratedAt { get; set; } = DateTime.Now;
    }

    public class StatisticalReportDTO
    {
        public string Period { get; set; } = string.Empty;
        public int TotalConsultations { get; set; }
        public int TotalPatients { get; set; }
        public int TotalProfessionals { get; set; }
        public Dictionary<string, int> ConsultationsByArea { get; set; } = new();
        public Dictionary<string, int> ConsultationsByMonth { get; set; } = new();
        public Dictionary<string, int> TopProfessionals { get; set; } = new();
    }

    public class PatientReportDTO
    {
        public string FullName { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? BloodGroup { get; set; }
        public string? MaritalStatus { get; set; }
        public List<string> MedicalInsurances { get; set; } = new();
        public int TotalConsultations { get; set; }
        public DateTime? LastConsultation { get; set; }
        public List<ConsultationSummaryDTO> Consultations { get; set; } = new();
    }

    public class ConsultationSummaryDTO
    {
        public DateTime CareDate { get; set; }
        public string Area { get; set; } = string.Empty;
        public string Professional { get; set; } = string.Empty;
        public string? Diagnosis { get; set; }
    }

    public class ProfessionalReportDTO
    {
        public string FullName { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
        public string ProfessionalType { get; set; } = string.Empty;
        public string? Email { get; set; }
        public int TotalConsultations { get; set; }
        public Dictionary<string, int> ConsultationsByArea { get; set; } = new();
        public Dictionary<string, int> ConsultationsByMonth { get; set; } = new();
        public List<PatientConsultationDTO> RecentConsultations { get; set; } = new();
    }

    public class PatientConsultationDTO
    {
        public string PatientName { get; set; } = string.Empty;
        public DateTime CareDate { get; set; }
        public string Area { get; set; } = string.Empty;
        public string? Diagnosis { get; set; }
    }

    public class ProfessionalDetailReportDTO
    {
        public string ProfessionalName { get; set; } = string.Empty;
        public int TotalConsultations { get; set; }
        public int UniquePatients { get; set; }
        public Dictionary<string, int> ConsultationsByArea { get; set; } = new();
        public Dictionary<string, int> ConsultationsByLocation { get; set; } = new();
        public List<ProfessionalConsultationDTO> RecentConsultations { get; set; } = new();
    }

    public class ProfessionalConsultationDTO
    {
        public DateTime CareDate { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }

    public class LocationReportDTO
    {
        public string LocationName { get; set; } = string.Empty;
        public int TotalConsultations { get; set; }
        public int UniquePatients { get; set; }
        public int UniqueProfessionals { get; set; }
        public List<DateTime> DaysWithConsultations { get; set; } = new();
        public Dictionary<DateTime, int> DaysWithMostConsultations { get; set; } = new();
        public Dictionary<string, int> ProfessionalsByConsultations { get; set; } = new();
    }

    public class AreaReportDTO
    {
        public string AreaName { get; set; } = string.Empty;
        public int TotalConsultations { get; set; }
        public int UniquePatients { get; set; }
        public int UniqueProfessionals { get; set; }
        public List<int> WeeksWithConsultations { get; set; } = new();
        public Dictionary<string, int> WeeksWithMostConsultations { get; set; } = new();
        public Dictionary<int, int> ConsultationsByDayOfWeek { get; set; } = new();
    }
}