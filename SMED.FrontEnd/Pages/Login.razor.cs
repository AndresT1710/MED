using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

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
                if (response.IsSuccessStatusCode)
                {
                    var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
                    if (loginResponse != null && loginResponse.IsAuthenticated)
                    {
                        await JS.InvokeVoidAsync("localStorage.setItem", "authToken", loginResponse.Token);
                        NavigationManager.NavigateTo("/home");
                    }
                    else
                    {
                        ErrorMessage = loginResponse?.Message ?? "Credenciales incorrectas.";
                    }
                }
                else
                {
                    ErrorMessage = "No se pudo conectar con el servidor.";
                }
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

        public class LoginRequestDTO
        {
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        public class LoginResponseDTO
        {
            public bool IsAuthenticated { get; set; }
            public string? Token { get; set; }
            public string? Message { get; set; }
        }
    }
}