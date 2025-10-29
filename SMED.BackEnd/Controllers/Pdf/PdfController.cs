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
        private readonly AgentRepository _agentService;
        private readonly EarlyStimulationSessionsRepository _earlyStimulationSessionsService;
        private readonly EarlyStimulationEvolutionTestRepository _earlyStimulationEvolutionTestService;
        private readonly DocumentTypeRepository _documentTypeService;
        private readonly GenderRepository _genderService;
        private readonly MaritalStatusRepository _maritalStatusService;

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
            SessionsRepository sessionsService,
            AgentRepository agentService,
            EarlyStimulationSessionsRepository earlyStimulationSessionsService,
            EarlyStimulationEvolutionTestRepository earlyStimulationEvolutionTestService,
            DocumentTypeRepository documentTypeService,
            GenderRepository genderService,
            MaritalStatusRepository maritalStatusService)
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
            _agentService = agentService;
            _earlyStimulationSessionsService = earlyStimulationSessionsService;
            _earlyStimulationEvolutionTestService = earlyStimulationEvolutionTestService;
            _documentTypeService = documentTypeService;
            _genderService = genderService;
            _maritalStatusService = maritalStatusService;
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

        [HttpGet("nutrition/{id}")]
        public async Task<IActionResult> GetNutritionPdf(int id)
        {
            try
            {
                // Obtener la atención de nutrición completa
                var nutritionCare = await _medicalCareRepository.GetByIdAsync(id);
                if (nutritionCare == null)
                {
                    return NotFound($"Atención de nutrición con ID {id} no encontrada");
                }

                // Verificar que sea una atención de nutrición
                var isNutritionCare = nutritionCare.Area?.ToLower().Contains("nutrición") == true ||
                                     nutritionCare.Area?.ToLower().Contains("nutricion") == true ||
                                     nutritionCare.LocationId == 3; // ID de nutrición según tu BD

                if (!isNutritionCare)
                {
                    return BadRequest($"La atención con ID {id} no pertenece al área de nutrición");
                }

                // Generar PDF específico para nutrición
                var pdfBytes = await _pdfService.GenerateNutritionPdfAsync(nutritionCare);
                var fileName = $"Atencion_Nutricion_{nutritionCare.CareId}_{DateTime.Now:yyyyMMddHHmm}.pdf";
                return File(pdfBytes, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error generando PDF de nutrición: {ex.Message}");
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


        //---------------------------------------------------------------------------------------------------------------------------------------------
        // EARLY STIMULATION PDF Generation
        //---------------------------------------------------------------------------------------------------------------------------------------------
        [HttpGet("early-stimulation/{id}")]
        public async Task<IActionResult> GetEarlyStimulationPdf(int id)
        {
            try
            {
                // Obtener la atención de estimulación temprana completa
                var earlyStimCare = await _medicalCareRepository.GetByIdAsync(id);
                if (earlyStimCare == null)
                {
                    return NotFound($"Atención de estimulación temprana con ID {id} no encontrada");
                }

                // Cargar datos específicos de estimulación temprana
                await LoadEarlyStimulationData(earlyStimCare);

                // Generar PDF específico para estimulación temprana
                var pdfBytes = await _pdfService.GenerateEarlyStimulationPdfAsync(earlyStimCare);
                var fileName = $"Atencion_Estimulacion_Temprana_{earlyStimCare.CareId}_{DateTime.Now:yyyyMMddHHmm}.pdf";
                return File(pdfBytes, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error generando PDF de estimulación temprana: {ex.Message}");
            }
        }

        private async Task LoadEarlyStimulationData(MedicalCareDTO earlyStimCare)
        {
            try
            {
                if (earlyStimCare == null)
                    throw new ArgumentNullException(nameof(earlyStimCare));

                Console.WriteLine($"[CONTROLLER DEBUG] ===== INICIO CARGA DATOS =====");
                Console.WriteLine($"[CONTROLLER DEBUG] CareId: {earlyStimCare.CareId}");
                Console.WriteLine($"[CONTROLLER DEBUG] PatientId: {earlyStimCare.PatientId}");
                Console.WriteLine($"[CONTROLLER DEBUG] Patient from repo: {earlyStimCare.Patient != null}");
                Console.WriteLine($"[CONTROLLER DEBUG] Agent from repo: {earlyStimCare.Patient?.Agent != null}");
                Console.WriteLine($"[CONTROLLER DEBUG] AgentId from repo: {earlyStimCare.Patient?.AgentId}");

                // Cargar motivo de consulta
                if (earlyStimCare.CareId > 0)
                {
                    var reasons = await _reasonForConsultationService.GetByCareIdAsync(earlyStimCare.CareId);
                    earlyStimCare.ReasonForConsultation = reasons?.FirstOrDefault();
                    Console.WriteLine($"[CONTROLLER DEBUG] ReasonForConsultation cargado: {earlyStimCare.ReasonForConsultation != null}");
                }

                // Asegurar que Patient no sea nulo
                earlyStimCare.Patient ??= new PatientDTO();

                // **CORRECCIÓN: Cargar Agent desde el servicio como hace el componente Blazor**
                if (earlyStimCare.Patient?.Agent == null)
                {
                    Console.WriteLine($"[CONTROLLER DEBUG] Cargando Agent desde AgentService...");

                    // Opción 1: Si tienes el AgentId, cargar por ID
                    if (earlyStimCare.Patient.AgentId.HasValue)
                    {
                        Console.WriteLine($"[CONTROLLER DEBUG] Buscando Agent por ID: {earlyStimCare.Patient.AgentId}");
                        var agentById = await _agentService.GetByIdAsync(earlyStimCare.Patient.AgentId.Value);
                        earlyStimCare.Patient.Agent = agentById;
                        Console.WriteLine($"[CONTROLLER DEBUG] Agent cargado por ID: {agentById != null}");
                    }

                    // Opción 2: Cargar todos y tomar el primero (como hace tu componente Blazor)
                    if (earlyStimCare.Patient.Agent == null)
                    {
                        Console.WriteLine($"[CONTROLLER DEBUG] Cargando todos los Agents...");
                        var allAgents = await _agentService.GetAllAsync() ?? new List<AgentDTO>();

                        // Buscar el Agent que corresponda a este Patient
                        var agentForPatient = allAgents.FirstOrDefault(a => a.AgentId == earlyStimCare.Patient.AgentId);
                        if (agentForPatient == null)
                        {
                            // Si no encuentra por ID, tomar el primero (como hace tu componente)
                            agentForPatient = allAgents.FirstOrDefault();
                        }

                        earlyStimCare.Patient.Agent = agentForPatient;
                        Console.WriteLine($"[CONTROLLER DEBUG] Agent cargado desde GetAll: {agentForPatient != null}");
                    }
                }

                // Enriquecer datos del Agent si existe
                if (earlyStimCare.Patient?.Agent != null)
                {
                    Console.WriteLine($"[CONTROLLER DEBUG] Enriqueciendo datos del Agent...");
                    var agentsList = new List<AgentDTO> { earlyStimCare.Patient.Agent };
                    await EnrichAgentsData(agentsList);
                }

                // Cargar sesiones de estimulación temprana
                earlyStimCare.EarlyStimulationSessions = await _earlyStimulationSessionsService.GetByMedicalCareIdAsync(earlyStimCare.CareId)
                    ?? new List<EarlyStimulationSessionsDTO>();

                // Cargar tests de evolución
                earlyStimCare.EarlyStimulationEvolutionTests = await _earlyStimulationEvolutionTestService.GetByMedicalCareIdAsync(earlyStimCare.CareId)
                    ?? new List<EarlyStimulationEvolutionTestDTO>();

                Console.WriteLine($"[CONTROLLER DEBUG] ===== FIN CARGA DATOS =====");
                Console.WriteLine($"[CONTROLLER DEBUG] Agent final: {earlyStimCare.Patient?.Agent != null}");
                Console.WriteLine($"[CONTROLLER DEBUG] AgentId final: {earlyStimCare.Patient?.AgentId}");
                Console.WriteLine($"[CONTROLLER DEBUG] Agent Name: {earlyStimCare.Patient?.Agent?.FirstName} {earlyStimCare.Patient?.Agent?.LastName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cargando datos de estimulación temprana: {ex.Message}");
                throw;
            }
        }

        private async Task EnrichAgentsData(List<AgentDTO> agents)
        {
            var documentTypes = await _documentTypeService.GetAllAsync();
            var genders = await _genderService.GetAllAsync();
            var maritalStatuses = await _maritalStatusService.GetAllAsync();

            foreach (var agent in agents)
            {
                agent.DocumentTypeName = documentTypes?.FirstOrDefault(d => d.Id == agent.DocumentType)?.Name ?? "N/A";
                agent.GenderName = genders?.FirstOrDefault(g => g.Id == agent.GenderId)?.Name ?? "N/A";
                agent.MaritalStatusName = maritalStatuses?.FirstOrDefault(m => m.Id == agent.MaritalStatusId)?.Name ?? "N/A";
            }
        }
    }
}