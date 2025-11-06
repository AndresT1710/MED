using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class TypesOfMentalFunctionsService
    {
        private readonly HttpClient _httpClient;

        public TypesOfMentalFunctionsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TypesOfMentalFunctionsDTO>> GetAllAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<TypesOfMentalFunctionsDTO>>("api/TypesOfMentalFunctions")
                    ?? new List<TypesOfMentalFunctionsDTO>();
            }
            catch (Exception)
            {
                return new List<TypesOfMentalFunctionsDTO>();
            }
        }

        public async Task<TypesOfMentalFunctionsDTO?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<TypesOfMentalFunctionsDTO>($"api/TypesOfMentalFunctions/{id}");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<TypesOfMentalFunctionsDTO?> CreateAsync(TypesOfMentalFunctionsDTO dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/TypesOfMentalFunctions", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<TypesOfMentalFunctionsDTO>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<TypesOfMentalFunctionsDTO?> UpdateAsync(TypesOfMentalFunctionsDTO dto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/TypesOfMentalFunctions/{dto.TypeOfMentalFunctionId}", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<TypesOfMentalFunctionsDTO>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/TypesOfMentalFunctions/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex) when (ex.Message.Contains("400"))
            {
                // Manejar el caso donde hay funciones mentales asociadas
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<TypesOfMentalFunctionsDTO>> GetByNameAsync(string name)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<TypesOfMentalFunctionsDTO>>($"api/TypesOfMentalFunctions/search/{name}")
                    ?? new List<TypesOfMentalFunctionsDTO>();
            }
            catch (Exception)
            {
                return new List<TypesOfMentalFunctionsDTO>();
            }
        }
    }
}