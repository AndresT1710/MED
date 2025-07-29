using System.Net.Http;
using System.Net.Http.Json;
using SMED.Shared.DTOs;

namespace SMED.FrontEnd.Services
{
    public class PersonService
    {
        private readonly HttpClient _httpClient;

        public PersonService(HttpClient http)
        {
            _httpClient = http;
        }

        public async Task<List<PersonDTO>> GetAllPersons()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "api/Person");
                Console.WriteLine($"Sending request to: {_httpClient.BaseAddress}api/Person");
                var response = await _httpClient.SendAsync(request);
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
                Console.WriteLine($"Exception in GetAllPersons: {ex.ToString()}");
                return new List<PersonDTO>();
            }
        }

        // Método GetPersonById para obtener una persona por su ID
        public async Task<PersonDTO?> GetPersonById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Person/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error getting person by ID {id}: {response.StatusCode}");
                    return null;
                }
                return await response.Content.ReadFromJsonAsync<PersonDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetPersonById (ID: {id}): {ex}");
                return null;
            }
        }

        public async Task<bool> CreatePerson(PersonDTO person)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Person", person);
                Console.WriteLine($"Status CreatePerson: {response.StatusCode}");
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error CreatePerson: {error}");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in CreatePerson: {ex}");
                return false;
            }
        }

        public async Task<bool> UpdatePerson(PersonDTO person)
        {
            try
            {
                if (person == null || person.Id == null)
                {
                    Console.WriteLine("Persona o Id nulo en UpdatePerson");
                    return false;
                }
                var response = await _httpClient.PutAsJsonAsync($"api/Person/{person.Id}", person);
                Console.WriteLine($"Status UpdatePerson: {response.StatusCode}");
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error UpdatePerson: {error}");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in UpdatePerson: {ex}");
                return false;
            }
        }

        public async Task<bool> DeletePerson(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Person/{id}");
                Console.WriteLine($"Status DeletePerson: {response.StatusCode}");
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error DeletePerson: {error}");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in DeletePerson: {ex}");
                return false;
            }
        }

        public async Task<List<PatientDTO>> GetPatientsWithHistoryAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<PatientDTO>>("api/patient/WithClinicalHistory");
                return response ?? new List<PatientDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetPatientsWithHistoryAsync: {ex}");
                return new List<PatientDTO>();
            }
        }

        public async Task<PersonDTO?> GetPersonByDocumentAsync(string documentNumber)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Person/by-document/{documentNumber}");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error getting person by document: {response.StatusCode}");
                    return null;
                }
                return await response.Content.ReadFromJsonAsync<PersonDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetPersonByDocumentAsync: {ex}");
                return null;
            }
        }

        public async Task<List<PersonDTO>> SearchPersonsAsync(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm) || searchTerm.Length < 3)
                    return new List<PersonDTO>();

                var response = await _httpClient.GetAsync($"api/Person/search?term={Uri.EscapeDataString(searchTerm)}");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error searching persons: {response.StatusCode}");
                    return new List<PersonDTO>();
                }
                return await response.Content.ReadFromJsonAsync<List<PersonDTO>>() ?? new List<PersonDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in SearchPersonsAsync: {ex}");
                return new List<PersonDTO>();
            }
        }
    }
}
