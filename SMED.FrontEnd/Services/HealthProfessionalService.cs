using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class HealthProfessionalService
    {
        private readonly HttpClient _httpClient;

        public HealthProfessionalService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<HealthProfessionalDTO>> GetAllHealthProfessionalsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<HealthProfessionalDTO>>("api/HealthProfessional");
                return response ?? new List<HealthProfessionalDTO>();
            }
            catch (Exception)
            {
                return new List<HealthProfessionalDTO>();
            }
        }

        public async Task<HealthProfessionalDTO?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<HealthProfessionalDTO>($"api/HealthProfessional/{id}");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting health professional by ID {id}: {ex.Message}");
                return null;
            }
        }

        public async Task<List<HealthProfessionalDTO>> SearchHealthProfessionalsAsync(string searchTerm)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<HealthProfessionalDTO>>($"api/HealthProfessional/search?searchTerm={Uri.EscapeDataString(searchTerm)}");
                return response ?? new List<HealthProfessionalDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching health professionals: {ex.Message}");
                return new List<HealthProfessionalDTO>();
            }
        }

        // MÉTODO PARA OBTENER EL PROFESIONAL LOGEADO
        public async Task<HealthProfessionalDTO?> GetCurrentHealthProfessionalAsync(AuthorizationService authService)
        {
            try
            {
                var currentUser = await authService.GetCurrentUserAsync();
                if (currentUser == null)
                {
                    Console.WriteLine("No se pudo obtener el usuario actual");
                    return null;
                }

                Console.WriteLine($"Buscando profesional para PersonId: {currentUser.PersonId}, Nombre: {currentUser.Name}");

                // Obtener todos los profesionales
                var allProfessionals = await GetAllHealthProfessionalsAsync();

                // Buscar por PersonId (la relación más directa)
                if (currentUser.PersonId.HasValue)
                {
                    var professionalByPersonId = allProfessionals.FirstOrDefault(p =>
                        p.HealthProfessionalId == currentUser.PersonId.Value);

                    if (professionalByPersonId != null)
                    {
                        Console.WriteLine($"Profesional encontrado por PersonId: {professionalByPersonId.FullName}");
                        return professionalByPersonId;
                    }
                }

                // Buscar por nombre (fallback)
                if (!string.IsNullOrEmpty(currentUser.Name))
                {
                    var professionalByName = allProfessionals.FirstOrDefault(p =>
                        !string.IsNullOrEmpty(p.FullName) &&
                        p.FullName.Contains(currentUser.Name, StringComparison.OrdinalIgnoreCase));

                    if (professionalByName != null)
                    {
                        Console.WriteLine($"Profesional encontrado por nombre: {professionalByName.FullName}");
                        return professionalByName;
                    }
                }

                // Buscar por tipo de profesional (segundo fallback)
                if (!string.IsNullOrEmpty(currentUser.ProfessionalTypeName))
                {
                    var professionalByType = allProfessionals.FirstOrDefault(p =>
                        !string.IsNullOrEmpty(p.NameTypeProfessional) &&
                        p.NameTypeProfessional.Equals(currentUser.ProfessionalTypeName, StringComparison.OrdinalIgnoreCase));

                    if (professionalByType != null)
                    {
                        Console.WriteLine($"Profesional encontrado por tipo: {professionalByType.FullName}");
                        return professionalByType;
                    }
                }

                Console.WriteLine("No se encontró ningún profesional que coincida con el usuario logueado");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error obteniendo profesional actual: {ex.Message}");
                return null;
            }
        }
    }
}