using SMED.Shared.DTOs;
using System.Threading.Tasks;

namespace SMED.BackEnd.Services.Interface
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequest);
    }
}
