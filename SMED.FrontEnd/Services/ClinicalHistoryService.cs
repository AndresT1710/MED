using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using SMED.Shared.DTOs;
public class ClinicalHistoryService
{
    private readonly HttpClient _httpClient;

    public ClinicalHistoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ClinicalHistoryDTO>> GetAllClinicalHistories()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<ClinicalHistoryDTO>>("api/ClinicalHistory")
               ?? Enumerable.Empty<ClinicalHistoryDTO>();
    }

    public async Task<ClinicalHistoryDTO?> GetById(int id)
    {
        return await _httpClient.GetFromJsonAsync<ClinicalHistoryDTO>($"api/ClinicalHistory/{id}");
    }

    public async Task<ClinicalHistoryDTO> Add(ClinicalHistoryCreateDTO dto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/ClinicalHistory", dto);

        if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
        {
            var errorContent = await response.Content.ReadFromJsonAsync<JsonElement>();
            var message = errorContent.GetProperty("message").GetString();
            throw new InvalidOperationException(message);
        }

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ClinicalHistoryDTO>();
    }
    public async Task<ClinicalHistoryDTO> Update(BasicClinicalHistroyDTO dto)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/ClinicalHistory/{dto.ClinicalHistoryId}", dto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ClinicalHistoryDTO>();
    }

    public async Task<bool> Delete(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/ClinicalHistory/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<ClinicalHistoryDTO>> SearchClinicalHistories(string searchTerm, bool byIdNumber = false)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<ClinicalHistoryDTO>>(
                $"api/ClinicalHistory/search?term={Uri.EscapeDataString(searchTerm)}&byIdNumber={byIdNumber}")
                ?? Enumerable.Empty<ClinicalHistoryDTO>();
        }
        catch (HttpRequestException ex) when (ex.Message.Contains("404"))
        {
            return Enumerable.Empty<ClinicalHistoryDTO>();
        }
    }
    public async Task<(bool hasHistory, string message, ClinicalHistoryDTO? existingHistory)> CheckPatientHistoryAsync(int personId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/ClinicalHistory/check-patient/{personId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<JsonElement>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var hasHistory = result.GetProperty("hasHistory").GetBoolean();
                var message = hasHistory ? result.GetProperty("message").GetString() : "";

                ClinicalHistoryDTO? existingHistory = null;
                if (hasHistory && result.TryGetProperty("existingHistory", out var historyElement))
                {
                    var historyJson = historyElement.GetRawText();
                    existingHistory = JsonSerializer.Deserialize<ClinicalHistoryDTO>(historyJson,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }

                return (hasHistory, message ?? "", existingHistory);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return (false, $"Error del servidor: {response.StatusCode}", null);
            }
        }
        catch (Exception ex)
        {
            return (false, $"Error al verificar: {ex.Message}", null);
        }
    }
}
