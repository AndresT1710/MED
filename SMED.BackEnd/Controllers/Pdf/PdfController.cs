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
        private readonly MedicalCareRepository _medicalCareRepository;
        private readonly ReasonForConsultationRepository _reasonForConsultationService;
        private readonly CurrentIllnessRepository _currentIllnessService;
        private readonly PainScaleRepository _painScaleService;
        private readonly SkinEvaluationRepository _skinEvaluationService;
        private readonly OsteoarticularEvaluationRepository _osteoarticularEvaluationService;
        private readonly MedicalEvaluationRepository _medicalEvaluationService;
        private readonly NeuromuscularEvaluationRepository _neuromuscularEvaluationService;
        private readonly PosturalEvaluationRepository _posturalEvaluationService;
        private readonly SpecialTestRepository _specialTestService;
        private readonly ComplementaryExamsRepository _complementaryExamsService;
        private readonly SessionsRepository _sessionsService;

        public PdfController(
            PdfService pdfService,
            PersonRepository personRepository,
            ClinicalHistoryRepository clinicalHistoryRepository,
            MedicalCareRepository medicalCareRepository,
            ReasonForConsultationRepository reasonForConsultationService,
            CurrentIllnessRepository currentIllnessService,
            PainScaleRepository painScaleService,
            SkinEvaluationRepository skinEvaluationService,
            OsteoarticularEvaluationRepository osteoarticularEvaluationService,
            MedicalEvaluationRepository medicalEvaluationService,
            NeuromuscularEvaluationRepository neuromuscularEvaluationService,
            PosturalEvaluationRepository posturalEvaluationService,
            SpecialTestRepository specialTestService,
            ComplementaryExamsRepository complementaryExamsService,
            SessionsRepository sessionsService)
        {
            _pdfService = pdfService;
            _personRepository = personRepository;
            _clinicalHistoryRepository = clinicalHistoryRepository;
            _medicalCareRepository = medicalCareRepository;
            _reasonForConsultationService = reasonForConsultationService;
            _currentIllnessService = currentIllnessService;
            _painScaleService = painScaleService;
            _skinEvaluationService = skinEvaluationService;
            _osteoarticularEvaluationService = osteoarticularEvaluationService;
            _medicalEvaluationService = medicalEvaluationService;
            _neuromuscularEvaluationService = neuromuscularEvaluationService;
            _posturalEvaluationService = posturalEvaluationService;
            _specialTestService = specialTestService;
            _complementaryExamsService = complementaryExamsService;
            _sessionsService = sessionsService;
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

        [HttpGet("nursing/{id}")]
        public async Task<IActionResult> GetNursingPdf(int id)
        {
            try
            {
                // Obtener la atención de enfermería completa
                var nursingCare = await _medicalCareRepository.GetByIdAsync(id);
                if (nursingCare == null)
                {
                    return NotFound($"Atención de enfermería con ID {id} no encontrada");
                }

                // Generar PDF específico para enfermería
                var pdfBytes = await _pdfService.GenerateNursingPdfAsync(nursingCare);
                var fileName = $"Atencion_Enfermeria_{nursingCare.CareId}_{DateTime.Now:yyyyMMddHHmm}.pdf";
                return File(pdfBytes, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error generando PDF de enfermería: {ex.Message}");
            }
        }

        [HttpGet("medical-care/{id}")]
        public async Task<IActionResult> GetMedicalCarePdf(int id)
        {
            try
            {
                // Obtener la atención médica completa con todos sus detalles
                var medicalCare = await _medicalCareRepository.GetByIdAsync(id);
                if (medicalCare == null)
                {
                    return NotFound($"Atención médica con ID {id} no encontrada");
                }

                // Generar PDF específico para atención médica
                var pdfBytes = await _pdfService.GenerateMedicalCarePdfAsync(medicalCare);

                var fileName = $"Atencion_Medica_{medicalCare.CareId}_{DateTime.Now:yyyyMMddHHmm}.pdf";

                return File(pdfBytes, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error generando PDF de atención médica: {ex.Message}");
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------
        // PHYSIOTHERAPY PDF Generation
        //---------------------------------------------------------------------------------------------------------------------------------------------
        [HttpGet("physiotherapy/{id}")]
        public async Task<IActionResult> GetPhysiotherapyPdf(int id)
        {
            try
            {
                // Obtener la atención de fisioterapia completa
                var physioCare = await _medicalCareRepository.GetByIdAsync(id);
                if (physioCare == null)
                {
                    return NotFound($"Atención de fisioterapia con ID {id} no encontrada");
                }

                // Cargar datos específicos de fisioterapia
                await LoadPhysiotherapyData(physioCare);

                // Generar PDF específico para fisioterapia
                var pdfBytes = await _pdfService.GeneratePhysiotherapyPdfAsync(physioCare);
                var fileName = $"Atencion_Fisioterapia_{physioCare.CareId}_{DateTime.Now:yyyyMMddHHmm}.pdf";
                return File(pdfBytes, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error generando PDF de fisioterapia: {ex.Message}");
            }
        }

        private async Task LoadPhysiotherapyData(MedicalCareDTO physioCare)
        {
            try
            {
                // Cargar motivo de consulta
                var reasons = await _reasonForConsultationService.GetByCareIdAsync(physioCare.CareId);
                physioCare.ReasonForConsultation = reasons?.FirstOrDefault();

                // Cargar enfermedad actual
                var illnesses = await _currentIllnessService.GetByCareIdAsync(physioCare.CareId);
                physioCare.CurrentIllnesses = illnesses ?? new List<CurrentIllnessDTO>();

                // Cargar escalas de dolor
                physioCare.PainScales = await _painScaleService.GetByCareIdAsync(physioCare.CareId) ?? new List<PainScaleDTO>();

                // Cargar evaluaciones de piel
                physioCare.SkinEvaluations = await _skinEvaluationService.GetByCareIdAsync(physioCare.CareId) ?? new List<SkinEvaluationDTO>();

                // Cargar evaluaciones osteoarticulares
                physioCare.OsteoarticularEvaluations = await _osteoarticularEvaluationService.GetByCareIdAsync(physioCare.CareId) ?? new List<OsteoarticularEvaluationDTO>();

                // Cargar evaluaciones médicas (articular/muscular)
                physioCare.MedicalEvaluations = await _medicalEvaluationService.GetByCareIdAsync(physioCare.CareId) ?? new List<MedicalEvaluationDTO>();

                // Cargar evaluaciones neuromusculares
                physioCare.NeuromuscularEvaluations = await _neuromuscularEvaluationService.GetByCareIdAsync(physioCare.CareId) ?? new List<NeuromuscularEvaluationDTO>();

                // Cargar evaluaciones posturales
                physioCare.PosturalEvaluations = await _posturalEvaluationService.GetByCareIdAsync(physioCare.CareId) ?? new List<PosturalEvaluationDTO>();

                // Cargar pruebas especiales
                physioCare.SpecialTests = await _specialTestService.GetByCareIdAsync(physioCare.CareId) ?? new List<SpecialTestDTO>();

                // Cargar exámenes complementarios
                physioCare.ComplementaryExams = await _complementaryExamsService.GetByCareIdAsync(physioCare.CareId) ?? new List<ComplementaryExamsDTO>();

                // Cargar sesiones
                physioCare.Sessions = await _sessionsService.GetByCareIdAsync(physioCare.CareId) ?? new List<SessionsDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cargando datos de fisioterapia: {ex.Message}");
            }
        }
    }
}