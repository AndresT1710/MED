using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;

        public ReportsController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        [HttpPost("medical-care")]
        public async Task<IActionResult> GenerateMedicalCareReport([FromBody] ReportRequestDTO request)
        {
            try
            {
                var result = await _reportRepository.GenerateMedicalCareReportAsync(request);
                return File(result.Content, result.ContentType, result.FileName);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error generando reporte: {ex.Message}");
            }
        }

        [HttpPost("patient")]
        public async Task<IActionResult> GeneratePatientReport([FromBody] ReportRequestDTO request)
        {
            try
            {
                var result = await _reportRepository.GeneratePatientReportAsync(request);
                return File(result.Content, result.ContentType, result.FileName);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error generando reporte: {ex.Message}");
            }
        }

        [HttpPost("professional")]
        public async Task<IActionResult> GenerateProfessionalReport([FromBody] ReportRequestDTO request)
        {
            try
            {
                Console.WriteLine($"=== CONTROLLER: GenerateProfessionalReport llamado ===");
                Console.WriteLine($"Request recibido: {System.Text.Json.JsonSerializer.Serialize(request)}");

                var result = await _reportRepository.GenerateProfessionalReportAsync(request);

                Console.WriteLine($"=== CONTROLLER: Reporte generado exitosamente ===");
                Console.WriteLine($"Nombre archivo: {result.FileName}, Tamaño: {result.Content.Length} bytes");

                return File(result.Content, result.ContentType, result.FileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"=== CONTROLLER ERROR ===");
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                // Devuelve el error interno para debugging
                return BadRequest(new
                {
                    error = $"Error generando reporte de profesionales: {ex.Message}",
                    details = ex.StackTrace,
                    innerError = ex.InnerException?.Message
                });
            }
        }

        [HttpPost("statistical")]
        public async Task<IActionResult> GenerateStatisticalReport([FromBody] ReportRequestDTO request)
        {
            try
            {
                var result = await _reportRepository.GenerateStatisticalReportAsync(request);
                return File(result.Content, result.ContentType, result.FileName);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error generando reporte: {ex.Message}");
            }
        }

        // NUEVOS ENDPOINTS
        [HttpPost("professional-detail")]
        public async Task<IActionResult> GenerateProfessionalDetailReport([FromBody] ReportRequestDTO request)
        {
            try
            {
                var result = await _reportRepository.GenerateProfessionalDetailReportAsync(request);
                return File(result.Content, result.ContentType, result.FileName);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error generando reporte detallado de profesional: {ex.Message}");
            }
        }

        [HttpPost("location")]
        public async Task<IActionResult> GenerateLocationReport([FromBody] ReportRequestDTO request)
        {
            try
            {
                var result = await _reportRepository.GenerateLocationReportAsync(request);
                return File(result.Content, result.ContentType, result.FileName);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error generando reporte por ubicación: {ex.Message}");
            }
        }

        [HttpPost("area")]
        public async Task<IActionResult> GenerateAreaReport([FromBody] ReportRequestDTO request)
        {
            try
            {
                var result = await _reportRepository.GenerateAreaReportAsync(request);
                return File(result.Content, result.ContentType, result.FileName);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error generando reporte por área: {ex.Message}");
            }
        }

        [HttpPost("top-patients")]
        public async Task<IActionResult> GenerateTopPatientReport([FromBody] ReportRequestDTO request)
        {
            try
            {
                var result = await _reportRepository.GenerateTopPatientReportAsync(request);
                return File(result.Content, result.ContentType, result.FileName);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error generando reporte de top pacientes: {ex.Message}");
            }
        }

        [HttpGet("statistical-data")]
        public async Task<ActionResult<StatisticalReportDTO>> GetStatisticalData(
    [FromQuery] DateTime? startDate,
    [FromQuery] DateTime? endDate,
    [FromQuery] bool includeAllRecords = true)
        {
            try
            {
                var data = await _reportRepository.GetStatisticalDataAsync(startDate, endDate, includeAllRecords);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error obteniendo datos estadísticos: {ex.Message}");
            }
        }

        [HttpGet("patient-data")]
        public async Task<ActionResult<List<PatientReportDTO>>> GetPatientReportData([FromQuery] ReportRequestDTO request)
        {
            try
            {
                var data = await _reportRepository.GetPatientReportDataAsync(request);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error obteniendo datos de pacientes: {ex.Message}");
            }
        }

        [HttpGet("professional-data")]
        public async Task<ActionResult<List<ProfessionalReportDTO>>> GetProfessionalReportData([FromQuery] ReportRequestDTO request)
        {
            try
            {
                var data = await _reportRepository.GetProfessionalReportDataAsync(request);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error obteniendo datos de profesionales: {ex.Message}");
            }
        }

        // NUEVOS ENDPOINTS DE DATOS
        [HttpGet("professional-detail-data")]
        public async Task<ActionResult<ProfessionalDetailReportDTO>> GetProfessionalDetailData([FromQuery] ReportRequestDTO request)
        {
            try
            {
                var data = await _reportRepository.GetProfessionalDetailDataAsync(request);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error obteniendo datos detallados de profesional: {ex.Message}");
            }
        }

        [HttpGet("location-data")]
        public async Task<ActionResult<LocationReportDTO>> GetLocationReportData([FromQuery] ReportRequestDTO request)
        {
            try
            {
                var data = await _reportRepository.GetLocationReportDataAsync(request);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error obteniendo datos de ubicación: {ex.Message}");
            }
        }

        [HttpGet("area-data")]
        public async Task<ActionResult<AreaReportDTO>> GetAreaReportData([FromQuery] ReportRequestDTO request)
        {
            try
            {
                var data = await _reportRepository.GetAreaReportDataAsync(request);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error obteniendo datos de área: {ex.Message}");
            }
        }

        [HttpGet("top-patients-data")]
        public async Task<ActionResult<List<PatientReportDTO>>> GetTopPatientsData([FromQuery] ReportRequestDTO request)
        {
            try
            {
                var data = await _reportRepository.GetTopPatientsDataAsync(request);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error obteniendo datos de top pacientes: {ex.Message}");
            }
        }


        [HttpPost("debug")]
        public IActionResult DebugReportRequest([FromBody] ReportRequestDTO request)
        {
            try
            {
                var debugInfo = new
                {
                    ReceivedRequest = request,
                    StartDateKind = request.StartDate?.Kind.ToString(),
                    EndDateKind = request.EndDate?.Kind.ToString(),
                    ValidationErrors = new List<string>()
                };

                // Validaciones básicas
                if (request.StartDate.HasValue && request.EndDate.HasValue)
                {
                    if (request.StartDate.Value > request.EndDate.Value)
                    {
                        debugInfo.ValidationErrors.Add("StartDate cannot be greater than EndDate");
                    }
                }

                return Ok(debugInfo);
            }
            catch (Exception ex)
            {
                return BadRequest($"Debug error: {ex.Message}");
            }
        }
    }
}