using Microsoft.JSInterop;
using SMED.Shared.DTOs;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;

namespace SMED.FrontEnd.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly HttpClient _httpClient;
        private UserSessionInfo? _currentUser;



        // Mapeo de roles a módulos permitidos
        private readonly Dictionary<string, List<string>> _roleModules = new()
        {
            //["Admin"] = new() { "Personas", "Historia Clínica", "Atención Médica", "Nutrición", "Enfermería", "Psicología", "Estimulación Temprana", "Fisioterapia", "Usuarios", "Reportes" },
            ["Admin"] = new() { "Personas","Usuarios", "Reportes" },
            ["Enfermero"] = new() { "Personas", "Historia Clínica", "Enfermería" },
            ["Médico General"] = new() { "Personas", "Historia Clínica", "Atención Médica" },
            ["Nutricionista"] = new() { "Personas", "Historia Clínica", "Nutrición" },
            ["Psicólogo"] = new() { "Personas", "Historia Clínica", "Psicología" },
            ["Psicólogo Clínico"] = new() { "Personas", "Historia Clínica", "Psicología" },
            ["Fisioterapeuta"] = new() { "Personas", "Historia Clínica", "Fisioterapia" },
            ["Pediatra"] = new() { "Personas", "Historia Clínica", "Estimulación Temprana" }
        };



        // Metodo Historia clinica roles
        public async Task<bool> HasAccessToMedicalHistoryTabAsync(string tabKey)
        {
            var user = await GetCurrentUserAsync();
            if (user == null) return false;
            if (user.IsAdmin) return true;

            // Mapeo de tabs a roles permitidos
            var tabPermissions = new Dictionary<string, List<string>>
            {
                ["Registro"] = new() {"Enfermero", "Médico General", "Nutricionista", "Psicólogo", "Psicólogo Clínico", "Fisioterapeuta", "Pediatra" },
                ["General"] = new() { "Enfermero", "Médico General", "Nutricionista", "Psicólogo", "Psicólogo Clínico", "Fisioterapeuta", "Pediatra" },
                ["Obstétrico"] = new() { "Enfermero", "Médico General", "Nutricionista", "Psicólogo", "Psicólogo Clínico", "Fisioterapeuta", "Pediatra" },
                ["Ginecológico"] = new() {"Enfermero", "Médico General", "Nutricionista", "Psicólogo", "Psicólogo Clínico", "Fisioterapeuta", "Pediatra" },
                ["Nutrición"] = new() { "Nutricionista" },
                ["Psicológico"] = new() {"Psicólogo", "Psicólogo Clínico" },
                ["Fisioterapia"] = new() { "Fisioterapeuta" },
                ["Estimulación temprana"] = new() { "Pediatra" }
            };

            if (!tabPermissions.ContainsKey(tabKey))
                return false;

            var userRoles = await GetUserRolesAsync();
            return userRoles.Any(role => tabPermissions[tabKey].Contains(role));
        }

        public AuthorizationService(IJSRuntime jsRuntime, HttpClient httpClient)
        {
            _jsRuntime = jsRuntime;
            _httpClient = httpClient;
        }

        public async Task<UserSessionInfo?> GetCurrentUserAsync()
        {
            _currentUser = null;
            try
            {
                var userJson = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "userSession");
                Console.WriteLine($"[Frontend AuthorizationService] Raw userJson from localStorage (on GetCurrentUserAsync): '{userJson}'");
                if (!string.IsNullOrWhiteSpace(userJson))
                {
                    _currentUser = JsonSerializer.Deserialize<UserSessionInfo>(userJson);
                    Console.WriteLine($"[Frontend AuthorizationService] Loaded user from localStorage (on GetCurrentUserAsync): '{_currentUser?.Name}'");
                    return _currentUser;
                }
                var authToken = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
                if (!string.IsNullOrWhiteSpace(authToken))
                {
                    _currentUser = ParseJwtToken(authToken);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Frontend AuthorizationService] Error al obtener el usuario actual: {ex.Message}");
                _currentUser = null;
            }
            return _currentUser;
        }

        public UserSessionInfo? ParseJwtToken(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                Console.WriteLine($"[Frontend AuthorizationService] Raw JWT Payload: '{jwtToken.Payload.SerializeToJson()}'");
                var claims = jwtToken.Claims;

                var userIdClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var emailClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

                // Intentar obtener el nombre de varias posibles reclamaciones
                var nameClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                if (string.IsNullOrEmpty(nameClaim))
                {
                    nameClaim = claims.FirstOrDefault(c => c.Type == "name")?.Value;
                }
                if (string.IsNullOrEmpty(nameClaim)) // Nueva verificación para "unique_name"
                {
                    nameClaim = claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;
                }


                var personIdClaim = claims.FirstOrDefault(c => c.Type == "PersonId")?.Value;
                var isAdminClaim = claims.FirstOrDefault(c => c.Type == "IsAdmin")?.Value;
                var healthProfessionalTypeIdClaim = claims.FirstOrDefault(c => c.Type == "HealthProfessionalTypeId")?.Value;
                var professionalTypeNameClaim = claims.FirstOrDefault(c => c.Type == "ProfessionalTypeName")?.Value;
                var registrationNumberClaim = claims.FirstOrDefault(c => c.Type == "RegistrationNumber")?.Value;

                Console.WriteLine($"[Frontend AuthorizationService] Name claim from JWT: '{nameClaim}'");

                int.TryParse(userIdClaim, out int userId);
                int.TryParse(personIdClaim, out int personId);
                bool.TryParse(isAdminClaim, out bool isAdmin);
                int.TryParse(healthProfessionalTypeIdClaim, out int healthProfessionalTypeId);

                var userSession = new UserSessionInfo
                {
                    UserId = personId,
                    Name = nameClaim ?? "Usuario",
                    Email = emailClaim ?? "",
                    PersonId = personId == 0 ? (int?)null : personId,
                    IsAdmin = isAdmin,
                    HealthProfessionalTypeId = healthProfessionalTypeId == 0 ? (int?)null : healthProfessionalTypeId,
                    ProfessionalTypeName = professionalTypeNameClaim,
                    RegistrationNumber = registrationNumberClaim
                };

                if (userSession.IsAdmin)
                {
                    userSession.AllowedModules = _roleModules["Admin"];
                }
                else if (!string.IsNullOrWhiteSpace(userSession.ProfessionalTypeName) && _roleModules.ContainsKey(userSession.ProfessionalTypeName))
                {
                    userSession.AllowedModules = _roleModules[userSession.ProfessionalTypeName];
                }
                else
                {
                    userSession.AllowedModules = new List<string>();
                }
                return userSession;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Frontend AuthorizationService] Error al parsear el token JWT: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> HasAccessToModuleAsync(string moduleKey)
        {
            var user = await GetCurrentUserAsync();
            if (user == null) return false;
            if (user.IsAdmin) return true;
            return user.AllowedModules.Contains(moduleKey);
        }

        public async Task<List<string>> GetUserModulesAsync()
        {
            var user = await GetCurrentUserAsync();
            return user?.AllowedModules ?? new List<string>();
        }

        public async Task SetUserSessionAsync(UserSessionInfo userInfo)
        {
            Console.WriteLine($"[Frontend AuthorizationService] Updating in-memory user session with name: '{userInfo.Name}'");
            _currentUser = userInfo;
        }

        public async Task ClearSessionAsync()
        {
            _currentUser = null;
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "userSession");
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
            Console.WriteLine("[Frontend AuthorizationService] User session cleared from localStorage.");
        }

        public async Task<List<string>> GetUserRolesAsync()
        {
            var user = await GetCurrentUserAsync();
            if (user == null) return new List<string>();
            if (user.IsAdmin)
                return new List<string> { "Admin" };
            if (!string.IsNullOrWhiteSpace(user.ProfessionalTypeName))
                return new List<string> { user.ProfessionalTypeName };
            return new List<string>();
        }

        public async Task<bool> IsAdminAsync()
        {
            var user = await GetCurrentUserAsync();
            return user?.IsAdmin ?? false;
        }

        public List<string> GetModulesForRole(string role)
        {
            return _roleModules.ContainsKey(role) ? _roleModules[role] : new List<string>();
        }
    }
}
