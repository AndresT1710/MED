using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class PersonalHistoryService
    {
        private readonly HttpClient _http;

        public PersonalHistoryService(HttpClient http)
        {
            _http = http;
        }

        // Obtener todos los antecedentes personales de una historia clínica específica
        public async Task<List<PersonalHistoryDTO>> GetByClinicalHistoryIdAsync(int clinicalHistoryId)
        {
            var response = await _http.GetFromJsonAsync<List<PersonalHistoryDTO>>($"api/personalhistory/by-history/{clinicalHistoryId}");
            return response ?? new List<PersonalHistoryDTO>();
        }

        // Crear nuevo antecedente personal
        public async Task<PersonalHistoryDTO?> CreateAsync(PersonalHistoryDTO dto)
        {
            var response = await _http.PostAsJsonAsync("api/personalhistory", dto);
            return await response.Content.ReadFromJsonAsync<PersonalHistoryDTO>();
        }

        // Actualizar antecedente personal
        public async Task<PersonalHistoryDTO?> UpdateAsync(PersonalHistoryDTO dto)
        {
            var response = await _http.PutAsJsonAsync($"api/personalhistory/{dto.PersonalHistoryId}", dto);
            return await response.Content.ReadFromJsonAsync<PersonalHistoryDTO>();
        }

        // Eliminar antecedente personal
        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/personalhistory/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
