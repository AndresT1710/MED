using SMED.Shared.DTOs;
using System.Security.Claims; // Asegúrate de que esta línea esté presente

namespace SMED.FrontEnd.Services
{
    public interface IAuthorizationService
    {
        Task<UserSessionInfo?> GetCurrentUserAsync();
        Task<bool> HasAccessToModuleAsync(string moduleKey);
        Task<List<string>> GetUserModulesAsync();
        Task SetUserSessionAsync(UserSessionInfo userInfo);
        Task ClearSessionAsync();
        Task<List<string>> GetUserRolesAsync();
        Task<bool> IsAdminAsync();
        UserSessionInfo? ParseJwtToken(string token);
    }
}
