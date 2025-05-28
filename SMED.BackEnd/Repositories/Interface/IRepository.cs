using SMED.Shared.DTOs;

namespace SMED.BackEnd.Repositories.Interface
{
    public interface IRepository<TDto, TKey>
    {
        Task<List<TDto>> GetAllAsync();
        Task<TDto?> GetByIdAsync(TKey id);
        Task<TDto> AddAsync(TDto dto);
        Task<TDto?> UpdateAsync(TDto dto);
        Task<bool> DeleteAsync(TKey id);
    }
}
