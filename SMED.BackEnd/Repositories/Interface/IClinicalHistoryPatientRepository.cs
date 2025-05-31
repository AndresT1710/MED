using SMED.Shared.DTOs;

namespace SMED.BackEnd.Repositories.Interface
{
    public interface IClinicalHistoryPatientRepository
    {
        Task<List<PatientDTO>> GetPatientsWithHistoryAsync();
    }
}
