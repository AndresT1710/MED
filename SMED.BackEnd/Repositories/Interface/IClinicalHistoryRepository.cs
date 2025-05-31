using SMED.Shared.DTOs;

namespace SMED.BackEnd.Repositories.Interface
{
    public interface IClinicalHistoryRepository : IRepository<ClinicalHistoryDTO, int>
    {
        Task<List<ClinicalHistoryDTO>> SearchAsync(string searchTerm, bool b);
    }
}
