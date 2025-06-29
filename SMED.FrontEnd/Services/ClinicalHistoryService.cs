using System.Net;
using System.Net.Http.Json;
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
}
