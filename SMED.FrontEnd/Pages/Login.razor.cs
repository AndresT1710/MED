using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Pages
{
    public partial class Login
    {
        private LoginRequestDTO loginModel = new();
        private bool IsLoading = false;
        private string? ErrorMessage;
        private bool ShowPassword = false;

        private string PasswordInputType => ShowPassword ? "text" : "password";
        private string PasswordToggleIcon => ShowPassword ? "oi oi-eye" : "oi oi-eye-slash";

        private void TogglePasswordVisibility()
        {
            ShowPassword = !ShowPassword;
        }

        private async Task HandleLogin()
        {
            ErrorMessage = null;
            IsLoading = true;

            try
            {
                var response = await Http.PostAsJsonAsync("api/auth/login", loginModel);

                if (!response.IsSuccessStatusCode)
                {
                    ErrorMessage = $"Error del servidor ({(int)response.StatusCode}): {response.ReasonPhrase}";
                    return;
                }

                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();

                if (loginResponse?.IsAuthenticated == true &&
                    !string.IsNullOrWhiteSpace(loginResponse.Token))
                {
                    // Guardar token y nombre del usuario en localStorage
                    await JS.InvokeVoidAsync("localStorage.setItem", "authToken", loginResponse.Token);
                    await JS.InvokeVoidAsync("localStorage.setItem", "userName", loginResponse.User?.Name ?? "Usuario");

                    NavigationManager.NavigateTo("/home", true);
                }
                else
                {
                    ErrorMessage = loginResponse?.Message ?? "Credenciales incorrectas.";
                }
            }
            catch (HttpRequestException)
            {
                ErrorMessage = "No se pudo conectar con el servidor. Intenta nuevamente más tarde.";
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error inesperado: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}