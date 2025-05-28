using System.Net.Http;
using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class PersonService
    {
        private readonly HttpClient _http;

        public PersonService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<PersonDTO>> GetAllPersons()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "api/Person");
                Console.WriteLine($"Sending request to: {_http.BaseAddress}api/Person");

                var response = await _http.SendAsync(request);

                Console.WriteLine($"Response status: {response.StatusCode}");
                Console.WriteLine($"Request actually sent to: {response.RequestMessage.RequestUri}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error response: {errorContent}");
                    return new List<PersonDTO>();
                }

                return await response.Content.ReadFromJsonAsync<List<PersonDTO>>() ?? new List<PersonDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.ToString()}");
                return new List<PersonDTO>();
            }
        }

        public async Task<bool> CreatePerson(PersonDTO person)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/Person", person);

                Console.WriteLine($"Status: {response.StatusCode}");
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {error}");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
                return false;
            }
        }


    }
}
