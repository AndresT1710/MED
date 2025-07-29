using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using SMED.Shared.DTOs;
using SMED.FrontEnd.Services;
using System.Text.Json; // Asegúrate de tener este using

namespace SMED.FrontEnd.Pages
{
    public partial class Login
    {
        [Inject]
        public IAuthorizationService AuthService { get; set; } = default!;
        [Inject]
        public HttpClient Http { get; set; } = default!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        public IJSRuntime JS { get; set; } = default!;

        private LoginRequestDTO loginModel = new();
        private bool IsLoading = false;
        private string? ErrorMessage;
        private bool ShowPassword = false;
        private string PasswordInputType => ShowPassword ? "text" : "password";
        private string PasswordToggleIcon => ShowPassword ? "fas fa-eye-slash" : "fas fa-eye";

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
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ErrorMessage = $"Error del servidor ({(int)response.StatusCode}): {response.ReasonPhrase}. Detalles: {errorContent}";
                    return;
                }

                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();

                if (loginResponse?.IsAuthenticated == true &&
                    !string.IsNullOrWhiteSpace(loginResponse.Token))
                {
                    // 1. Guardar el token en localStorage
                    await JS.InvokeVoidAsync("localStorage.setItem", "authToken", loginResponse.Token);
                    Console.WriteLine($"[Login.razor.cs] AuthToken saved to localStorage.");

                    // 2. Parsear el token para obtener la sesión del usuario
                    var userSession = AuthService.ParseJwtToken(loginResponse.Token);

                    if (userSession != null)
                    {
                        Console.WriteLine($"[Login.razor.cs] UserSession Name parsed from token: '{userSession.Name}'");

                        // 3. Serializar y guardar directamente en localStorage
                        var userJsonToSave = JsonSerializer.Serialize(userSession);
                        Console.WriteLine($"[Login.razor.cs] JSON to be saved to localStorage: '{userJsonToSave}'");
                        await JS.InvokeVoidAsync("localStorage.setItem", "userSession", userJsonToSave);
                        Console.WriteLine($"[Login.razor.cs] User session JSON saved directly to localStorage.");

                        // 4. Verificar inmediatamente lo que se acaba de guardar
                        var savedUserJson = await JS.InvokeAsync<string>("localStorage.getItem", "userSession");
                        Console.WriteLine($"[Login.razor.cs] Raw userJson from localStorage AFTER saving (direct read): '{savedUserJson}'");
                        var savedUserSession = JsonSerializer.Deserialize<UserSessionInfo>(savedUserJson);
                        Console.WriteLine($"[Login.razor.cs] UserSession Name AFTER saving (direct read): '{savedUserSession?.Name}'");

                        // 5. Actualizar la caché en memoria del AuthService (sin guardar en localStorage de nuevo)
                        await AuthService.SetUserSessionAsync(userSession);
                        Console.WriteLine($"[Login.razor.cs] AuthService in-memory cache updated.");

                        // Pequeño retardo para asegurar que el navegador procese todo antes de la navegación
                        await Task.Delay(100);
                    }
                    else
                    {
                        ErrorMessage = "Error al procesar la información del usuario desde el token.";
                        IsLoading = false;
                        return;
                    }

                    // Redirigir al home
                    NavigationManager.NavigateTo("/home", true);
                }
                else
                {
                    ErrorMessage = loginResponse?.Message ?? "Credenciales incorrectas.";
                }
            }
            catch (HttpRequestException)
            {
                ErrorMessage = "No se pudo conectar con el servidor. Asegúrate de que el backend esté corriendo.";
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
