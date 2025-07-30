using SMED.Shared.DTOs;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

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
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetAllUsersAsync: {ex.Message}");
                return new List<UserDTO>();
            }
        }

        public async Task<UserDTO?> GetUserByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<UserDTO>($"api/User/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetUserByIdAsync (ID: {id}): {ex.Message}");
                return null;
            }
        }

        // Nuevo método para obtener usuario por PersonId
        public async Task<UserDTO?> GetUserByPersonIdAsync(int personId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/User/by-person/{personId}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<UserDTO>();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error en GetUserByPersonIdAsync (PersonId: {personId}): {response.StatusCode} - {errorContent}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción en GetUserByPersonIdAsync (PersonId: {personId}): {ex.Message}");
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
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error al crear usuario: {response.StatusCode} - {errorContent}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción en CreateUserAsync: {ex.Message}");
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
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error al actualizar usuario: {response.StatusCode} - {errorContent}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción en UpdateUserAsync: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/User/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al eliminar usuario: {response.StatusCode} - {errorContent}");
                }
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción en DeleteUserAsync: {ex.Message}");
                return false;
            }
        }
    }
}
