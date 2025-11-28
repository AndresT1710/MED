// Services/AuthorizedHttpHandler.cs en Frontend
using System.Net.Http.Headers;
using Microsoft.JSInterop;

namespace SMED.FrontEnd.Services
{
    public class AuthorizedHttpHandler
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;

        public AuthorizedHttpHandler(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        public async Task<HttpClient> GetAuthorizedClient()
        {
            try
            {
                // Obtener token del localStorage
                var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");

                if (!string.IsNullOrEmpty(token))
                {
                    // Limpiar headers anteriores
                    _httpClient.DefaultRequestHeaders.Remove("Authorization");
                    // Agregar el token
                    _httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token);

                    Console.WriteLine($"[AuthorizedHttpHandler] Token agregado al request");
                }
                else
                {
                    Console.WriteLine($"[AuthorizedHttpHandler] No se encontró token en localStorage");
                }

                return _httpClient;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AuthorizedHttpHandler] Error: {ex.Message}");
                return _httpClient;
            }
        }
    }
}