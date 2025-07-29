using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<UserDTO>>("api/User");
                return response ?? new List<UserDTO>();
            }
            catch (Exception)
            {
                return new List<UserDTO>();
            }
        }

        public async Task<UserDTO?> GetUserByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<UserDTO>($"api/User/{id}");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<UserDTO?> CreateUserAsync(UserDTO user)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/User", user);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<UserDTO>();
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<UserDTO?> UpdateUserAsync(UserDTO user)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("api/User", user);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<UserDTO>();
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/User/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
