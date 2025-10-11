using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Services;
using SMED.BackEnd.Repositories.Implementations;
using SMED.Shared.DTOs;
using System.Threading.Tasks;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PdfController : ControllerBase
    {
        private readonly PdfService _pdfService;
        private readonly PersonRepository _personRepository;
        private readonly ClinicalHistoryRepository _clinicalHistoryRepository;

        public PdfController(
            PdfService pdfService,
            PersonRepository personRepository,
            ClinicalHistoryRepository clinicalHistoryRepository)
        {
            _pdfService = pdfService;
            _personRepository = personRepository;
            _clinicalHistoryRepository = clinicalHistoryRepository;
        }

        [HttpGet("person/{id}")]
        public async Task<IActionResult> GetPersonPdf(int id)
        {
            try
            {
                var persona = await _personRepository.GetByIdAsync(id);
                if (persona == null)
                {
                    return NotFound($"Persona con ID {id} no encontrada");
                }

                var pdfBytes = await _pdfService.GeneratePersonPdfAsync(persona);
                var fileName = $"Ficha_Paciente_{ObtenerNombreArchivo(persona)}_{DateTime.Now:yyyyMMddHHmm}.pdf";
                return File(pdfBytes, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error generando PDF: {ex.Message}");
            }
        }

        [HttpGet("clinicalhistory/{id}")]
        public async Task<IActionResult> GetClinicalHistoryPdf(int id)
        {
            try
            {
                // Obtener la historia clínica completa
                var clinicalHistory = await _clinicalHistoryRepository.GetByIdAsync(id);
                if (clinicalHistory == null)
                {
                    return NotFound($"Historia clínica con ID {id} no encontrada");
                }

                // Generar PDF de la historia clínica
                var pdfBytes = await GenerateClinicalHistoryPdfAsync(clinicalHistory);
                var fileName = $"Historia_Clinica_{clinicalHistory.HistoryNumber}_{DateTime.Now:yyyyMMddHHmm}.pdf";
                return File(pdfBytes, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error generando PDF de historia clínica: {ex.Message}");
            }
        }

        private async Task<byte[]> GenerateClinicalHistoryPdfAsync(ClinicalHistoryDTO clinicalHistory)
        {
            // Aquí implementarías la generación del PDF específico para historias clínicas
            // Por ahora, vamos a usar el mismo servicio pero adaptado
            return await _pdfService.GenerateClinicalHistoryPdfAsync(clinicalHistory);
        }

        private string ObtenerNombreArchivo(PersonDTO persona)
        {
            var nombre = $"{persona.FirstName}_{persona.LastName}";
            return System.Text.RegularExpressions.Regex.Replace(nombre, @"[^a-zA-Z0-9_-]", "");
        }
    }
}