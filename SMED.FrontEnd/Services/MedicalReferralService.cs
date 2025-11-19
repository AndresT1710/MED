using SMED.Shared.DTOs;
using System.Net.Http.Json;

namespace SMED.FrontEnd.Services
{
    public class MedicalReferralService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MedicalReferralService> _logger;

        public MedicalReferralService(HttpClient httpClient, ILogger<MedicalReferralService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ServiceResult<List<MedicalReferralDTO>>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/MedicalReferral");
                if (response.IsSuccessStatusCode)
                {
                    var referrals = await response.Content.ReadFromJsonAsync<List<MedicalReferralDTO>>();
                    return new ServiceResult<List<MedicalReferralDTO>>
                    {
                        Success = true,
                        Data = referrals ?? new List<MedicalReferralDTO>()
                    };
                }

                _logger.LogWarning("Error al obtener derivaciones médicas: {StatusCode}", response.StatusCode);
                return new ServiceResult<List<MedicalReferralDTO>>
                {
                    Success = false,
                    Error = $"Error del servidor: {response.StatusCode}",
                    Data = new List<MedicalReferralDTO>()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las derivaciones médicas");
                return new ServiceResult<List<MedicalReferralDTO>>
                {
                    Success = false,
                    Error = ex.Message,
                    Data = new List<MedicalReferralDTO>()
                };
            }
        }

        public async Task<ServiceResult<List<MedicalReferralDTO>>> GetByLocationAsync(int locationId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/MedicalReferral/location/{locationId}");
                if (response.IsSuccessStatusCode)
                {
                    var referrals = await response.Content.ReadFromJsonAsync<List<MedicalReferralDTO>>();
                    return new ServiceResult<List<MedicalReferralDTO>>
                    {
                        Success = true,
                        Data = referrals ?? new List<MedicalReferralDTO>()
                    };
                }

                return new ServiceResult<List<MedicalReferralDTO>>
                {
                    Success = false,
                    Error = $"Error al obtener derivaciones por ubicación: {response.StatusCode}",
                    Data = new List<MedicalReferralDTO>()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener derivaciones por ubicación: {LocationId}", locationId);
                return new ServiceResult<List<MedicalReferralDTO>>
                {
                    Success = false,
                    Error = ex.Message,
                    Data = new List<MedicalReferralDTO>()
                };
            }
        }

        public async Task<ServiceResult<List<MedicalReferralDTO>>> GetPendingReferralsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/MedicalReferral/pending");
                if (response.IsSuccessStatusCode)
                {
                    var referrals = await response.Content.ReadFromJsonAsync<List<MedicalReferralDTO>>();
                    return new ServiceResult<List<MedicalReferralDTO>>
                    {
                        Success = true,
                        Data = referrals ?? new List<MedicalReferralDTO>()
                    };
                }

                return new ServiceResult<List<MedicalReferralDTO>>
                {
                    Success = false,
                    Error = $"Error al obtener derivaciones pendientes: {response.StatusCode}",
                    Data = new List<MedicalReferralDTO>()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener derivaciones pendientes");
                return new ServiceResult<List<MedicalReferralDTO>>
                {
                    Success = false,
                    Error = ex.Message,
                    Data = new List<MedicalReferralDTO>()
                };
            }
        }

        public async Task<ServiceResult<MedicalReferralDTO>> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/MedicalReferral/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var referral = await response.Content.ReadFromJsonAsync<MedicalReferralDTO>();
                    return new ServiceResult<MedicalReferralDTO>
                    {
                        Success = true,
                        Data = referral
                    };
                }

                return new ServiceResult<MedicalReferralDTO>
                {
                    Success = false,
                    Error = $"Derivación médica no encontrada: {response.StatusCode}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener derivación médica por ID: {Id}", id);
                return new ServiceResult<MedicalReferralDTO>
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<ServiceResult<List<MedicalReferralDTO>>> GetByMedicalCareIdAsync(int medicalCareId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/MedicalReferral/medical-care/{medicalCareId}");
                if (response.IsSuccessStatusCode)
                {
                    var referrals = await response.Content.ReadFromJsonAsync<List<MedicalReferralDTO>>();
                    return new ServiceResult<List<MedicalReferralDTO>>
                    {
                        Success = true,
                        Data = referrals ?? new List<MedicalReferralDTO>()
                    };
                }

                return new ServiceResult<List<MedicalReferralDTO>>
                {
                    Success = false,
                    Error = $"Error al obtener derivaciones por atención médica: {response.StatusCode}",
                    Data = new List<MedicalReferralDTO>()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener derivaciones por MedicalCareId: {MedicalCareId}", medicalCareId);
                return new ServiceResult<List<MedicalReferralDTO>>
                {
                    Success = false,
                    Error = ex.Message,
                    Data = new List<MedicalReferralDTO>()
                };
            }
        }


        public async Task<ServiceResult<bool>> UpdateStatusAsync(int id, string status, int? attendedByProfessionalId = null)
        {
            try
            {
                var request = new { Status = status, AttendedByProfessionalId = attendedByProfessionalId };
                var response = await _httpClient.PutAsJsonAsync($"api/MedicalReferral/{id}/status", request);

                if (response.IsSuccessStatusCode)
                {
                    return new ServiceResult<bool>
                    {
                        Success = true,
                        Data = true
                    };
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                return new ServiceResult<bool>
                {
                    Success = false,
                    Error = $"Error al actualizar estado: {response.StatusCode} - {errorContent}",
                    Data = false
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar estado de derivación con ID: {Id}", id);
                return new ServiceResult<bool>
                {
                    Success = false,
                    Error = ex.Message,
                    Data = false
                };
            }
        }
        public async Task<ServiceResult<MedicalReferralDTO>> CreateAsync(MedicalReferralDTO referralDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/MedicalReferral", referralDto);
                if (response.IsSuccessStatusCode)
                {
                    var createdReferral = await response.Content.ReadFromJsonAsync<MedicalReferralDTO>();
                    return new ServiceResult<MedicalReferralDTO>
                    {
                        Success = true,
                        Data = createdReferral
                    };
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                return new ServiceResult<MedicalReferralDTO>
                {
                    Success = false,
                    Error = $"Error al crear derivación médica: {response.StatusCode} - {errorContent}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear derivación médica");
                return new ServiceResult<MedicalReferralDTO>
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<ServiceResult<MedicalReferralDTO>> UpdateAsync(MedicalReferralDTO referralDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/MedicalReferral/{referralDto.Id}", referralDto);
                if (response.IsSuccessStatusCode)
                {
                    var updatedReferral = await response.Content.ReadFromJsonAsync<MedicalReferralDTO>();
                    return new ServiceResult<MedicalReferralDTO>
                    {
                        Success = true,
                        Data = updatedReferral
                    };
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                return new ServiceResult<MedicalReferralDTO>
                {
                    Success = false,
                    Error = $"Error al actualizar derivación médica: {response.StatusCode} - {errorContent}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar derivación médica con ID: {Id}", referralDto.Id);
                return new ServiceResult<MedicalReferralDTO>
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/MedicalReferral/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return new ServiceResult<bool>
                    {
                        Success = true,
                        Data = true
                    };
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                return new ServiceResult<bool>
                {
                    Success = false,
                    Error = $"Error al eliminar derivación médica: {response.StatusCode} - {errorContent}",
                    Data = false
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar derivación médica con ID: {Id}", id);
                return new ServiceResult<bool>
                {
                    Success = false,
                    Error = ex.Message,
                    Data = false
                };
            }
        }


    }

    // Clase auxiliar para manejar respuestas del servicio
    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string Error { get; set; } = string.Empty;
    }
}