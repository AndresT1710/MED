using SMED.Shared.DTOs;

namespace SMED.BackEnd.Repositories.Interface
{
    public interface IClinicalHistoryRepository : IRepository<ClinicalHistoryDTO, int>
    {
        Task<List<ClinicalHistoryDTO>> SearchAsync(string searchTerm, bool b);
        Task<bool> PatientHasClinicalHistoryAsync(int personId);
        Task<ClinicalHistoryDTO?> GetByPatientIdAsync(int personId);
    }
}
