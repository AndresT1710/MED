using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using SMED.Shared.DTOs;
using SMED.FrontEnd.Services;
using System.Text.Json;
using System.Net;

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
                    // Manejo específico de errores
                    ErrorMessage = await GetFriendlyErrorMessage(response);
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
                        ErrorMessage = "Error al procesar la información del usuario. Por favor, intenta nuevamente.";
                        IsLoading = false;
                        return;
                    }

                    // Redirigir al home
                    NavigationManager.NavigateTo("/home", true);
                }
                else
                {
                    // Mensaje más amigable para credenciales incorrectas
                    ErrorMessage = GetFriendlyMessage(loginResponse?.Message) ?? "Las credenciales ingresadas no son correctas. Por favor, verifica tu email y contraseña.";
                }
            }
            catch (HttpRequestException)
            {
                ErrorMessage = "No se pudo conectar con el servidor. Por favor, verifica tu conexión a internet e intenta nuevamente.";
            }
            catch (TaskCanceledException)
            {
                ErrorMessage = "La solicitud tardó demasiado tiempo. Por favor, intenta nuevamente.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Login.razor.cs] Error inesperado: {ex.Message}");
                ErrorMessage = "Ocurrió un error inesperado. Por favor, intenta nuevamente.";
            }
            finally
            {
                IsLoading = false;
            }
        }

        // Método para obtener mensajes de error amigables desde la respuesta HTTP
        private async Task<string> GetFriendlyErrorMessage(HttpResponseMessage response)
        {
            try
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[Login.razor.cs] Error response content: {errorContent}");

                // Intentar deserializar la respuesta de error
                if (!string.IsNullOrWhiteSpace(errorContent))
                {
                    try
                    {
                        var errorResponse = JsonSerializer.Deserialize<LoginResponseDTO>(errorContent, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        if (!string.IsNullOrWhiteSpace(errorResponse?.Message))
                        {
                            return GetFriendlyMessage(errorResponse.Message);
                        }
                    }
                    catch (JsonException)
                    {
                        // Si no se puede deserializar como LoginResponseDTO, intentar extraer el mensaje directamente
                        if (errorContent.Contains("message"))
                        {
                            try
                            {
                                using var doc = JsonDocument.Parse(errorContent);
                                if (doc.RootElement.TryGetProperty("message", out var messageElement))
                                {
                                    var message = messageElement.GetString();
                                    if (!string.IsNullOrWhiteSpace(message))
                                    {
                                        return GetFriendlyMessage(message);
                                    }
                                }
                            }
                            catch { }
                        }
                    }
                }

                // Mensajes por código de estado HTTP
                return response.StatusCode switch
                {
                    HttpStatusCode.Unauthorized => "Las credenciales ingresadas no son correctas. Por favor, verifica tu email y contraseña.",
                    HttpStatusCode.BadRequest => "Los datos ingresados no son válidos. Por favor, verifica la información.",
                    HttpStatusCode.InternalServerError => "Error interno del servidor. Por favor, intenta más tarde.",
                    HttpStatusCode.ServiceUnavailable => "El servicio no está disponible temporalmente. Por favor, intenta más tarde.",
                    HttpStatusCode.RequestTimeout => "La solicitud tardó demasiado tiempo. Por favor, intenta nuevamente.",
                    _ => "Ocurrió un error al intentar iniciar sesión. Por favor, intenta nuevamente."
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Login.razor.cs] Error parsing error response: {ex.Message}");
                return "Ocurrió un error al intentar iniciar sesión. Por favor, intenta nuevamente.";
            }
        }

        // Método para convertir mensajes técnicos en mensajes amigables
        private string GetFriendlyMessage(string? technicalMessage)
        {
            if (string.IsNullOrWhiteSpace(technicalMessage))
                return "Ocurrió un error inesperado. Por favor, intenta nuevamente.";

            var message = technicalMessage.ToLowerInvariant();

            // Mapeo de mensajes técnicos a mensajes amigables
            return message switch
            {
                var msg when msg.Contains("usuario no encontrado") => "El email ingresado no está registrado en el sistema.",
                var msg when msg.Contains("user not found") => "El email ingresado no está registrado en el sistema.",
                var msg when msg.Contains("contraseña incorrecta") => "La contraseña ingresada es incorrecta.",
                var msg when msg.Contains("password incorrect") => "La contraseña ingresada es incorrecta.",
                var msg when msg.Contains("invalid password") => "La contraseña ingresada es incorrecta.",
                var msg when msg.Contains("credenciales incorrectas") => "Las credenciales ingresadas no son correctas.",
                var msg when msg.Contains("invalid credentials") => "Las credenciales ingresadas no son correctas.",
                var msg when msg.Contains("cuenta bloqueada") => "Tu cuenta ha sido bloqueada. Contacta al administrador.",
                var msg when msg.Contains("account locked") => "Tu cuenta ha sido bloqueada. Contacta al administrador.",
                var msg when msg.Contains("cuenta desactivada") => "Tu cuenta está desactivada. Contacta al administrador.",
                var msg when msg.Contains("account disabled") => "Tu cuenta está desactivada. Contacta al administrador.",
                var msg when msg.Contains("token expirado") => "Tu sesión ha expirado. Por favor, inicia sesión nuevamente.",
                var msg when msg.Contains("token expired") => "Tu sesión ha expirado. Por favor, inicia sesión nuevamente.",
                var msg when msg.Contains("email no válido") => "El formato del email no es válido.",
                var msg when msg.Contains("invalid email") => "El formato del email no es válido.",
                var msg when msg.Contains("campos requeridos") => "Por favor, completa todos los campos requeridos.",
                var msg when msg.Contains("required fields") => "Por favor, completa todos los campos requeridos.",
                _ => "Las credenciales ingresadas no son correctas. Por favor, verifica tu email y contraseña."
            };
        }
    }
}
