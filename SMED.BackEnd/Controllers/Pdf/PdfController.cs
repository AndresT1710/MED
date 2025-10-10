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

        public PdfController(PdfService pdfService, PersonRepository personRepository)
        {
            _pdfService = pdfService;
            _personRepository = personRepository;
        }

        [HttpGet("person/{id}")]
        public async Task<IActionResult> GetPersonPdf(int id)
        {
            try
            {
                // Obtener datos REALES de la persona
                var persona = await _personRepository.GetByIdAsync(id);

                if (persona == null)
                {
                    return NotFound($"Persona con ID {id} no encontrada");
                }

                // Generar PDF profesional
                var pdfBytes = await _pdfService.GeneratePersonPdfAsync(persona);

                var fileName = $"Ficha_Paciente_{ObtenerNombreArchivo(persona)}_{DateTime.Now:yyyyMMddHHmm}.pdf";
                return File(pdfBytes, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error generando PDF: {ex.Message}");
            }
        }

        private string ObtenerNombreArchivo(PersonDTO persona)
        {
            var nombre = $"{persona.FirstName}_{persona.LastName}";
            // Limpiar caracteres especiales para el nombre del archivo
            return System.Text.RegularExpressions.Regex.Replace(nombre, @"[^a-zA-Z0-9_-]", "");
        }
    }
}