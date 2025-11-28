using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Services;
using SMED.BackEnd.Repositories.Implementations;
using SMED.Shared.DTOs;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Asegurar que todos los endpoints requieren autenticación
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
        private readonly PsychologicalDiagnosisRepository _psychologicalDiagnosisService;
        private readonly DiagnosticTypeRepository _diagnosticTypeService;
        private readonly TherapeuticPlanRepository _therapeuticPlanService;
        private readonly PsychologySessionsRepository _psychologySessionsService;
        private readonly ActivityRepository _activityService;
        private readonly TypeOfActivityRepository _typeOfActivityService;
        private readonly AdvanceRepository _advanceService;

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
            MaritalStatusRepository maritalStatusService,
            PsychologicalDiagnosisRepository psychologicalDiagnosisService,
            DiagnosticTypeRepository diagnosticTypeService,
            TherapeuticPlanRepository therapeuticPlanService,
            PsychologySessionsRepository psychologySessionsService,
            ActivityRepository activityService,
            TypeOfActivityRepository typeOfActivityService,
            AdvanceRepository advanceService)
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
            _psychologicalDiagnosisService = psychologicalDiagnosisService;
            _diagnosticTypeService = diagnosticTypeService;
            _therapeuticPlanService = therapeuticPlanService;
            _psychologySessionsService = psychologySessionsService;
            _activityService = activityService;
            _typeOfActivityService = typeOfActivityService;
            _advanceService = advanceService;
        }

        #region Métodos Auxiliares

        /// <summary>
        /// Obtiene el rol del usuario desde el token JWT
        /// </summary>
        private string GetCurrentUserRole()
        {
            try
            {
                Console.WriteLine($"[PDF CONTROLLER] 🔐 Verificando permisos...");

                // Verificar si es Admin
                var isAdminClaim = User.FindFirst("IsAdmin")?.Value;
                if (isAdminClaim == "true")
                {
                    Console.WriteLine($"[PDF CONTROLLER] ✅ Usuario Admin - acceso completo");
                    return "Admin";
                }

                // Obtener tipo de profesional
                var professionalTypeName = User.FindFirst("ProfessionalTypeName")?.Value;
                Console.WriteLine($"[PDF CONTROLLER] 👤 ProfessionalTypeName: {professionalTypeName}");

                // Mapear rol
                var role = professionalTypeName?.ToLower() switch
                {
                    "médico general" or "medico general" => "Médico General",
                    "enfermero" or "enfermera" => "Enfermero",
                    "nutricionista" => "Nutricionista",
                    "psicólogo" or "psicologo" or "psicólogo clínico" or "psicologo clinico" => "Psicólogo",
                    "fisioterapeuta" => "Fisioterapeuta",
                    "pediatra" => "Pediatra",
                    _ => "Unknown"
                };

                Console.WriteLine($"[PDF CONTROLLER] 🎭 Rol asignado: {role}");
                return role;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[PDF CONTROLLER] ❌ Error obteniendo rol: {ex.Message}");
                return "Unknown";
            }
        }

        /// <summary>
        /// Limpia el nombre para usar en archivos
        /// </summary>
        private string LimpiarNombreArchivo(string nombre)
        {
            if (string.IsNullOrEmpty(nombre)) return "Documento";
            return System.Text.RegularExpressions.Regex.Replace(nombre, @"[^a-zA-Z0-9_-]", "_");
        }

        /// <summary>
        /// Enriquece los datos de agentes con información adicional
        /// </summary>
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

        #endregion

        #region Endpoints PDF

        /// <summary>
        /// Genera PDF de Ficha de Paciente
        /// </summary>
        [HttpGet("person/{id}")]
        public async Task<IActionResult> GetPersonPdf(int id)
        {
            try
            {
                Console.WriteLine($"[PDF CONTROLLER] 📄 Generando PDF de Persona ID: {id}");

                var persona = await _personRepository.GetByIdAsync(id);
                if (persona == null)
                {
                    return NotFound($"Persona con ID {id} no encontrada");
                }

                var pdfBytes = await _pdfService.GeneratePersonPdfAsync(persona);
                var fileName = $"Ficha_Paciente_{LimpiarNombreArchivo($"{persona.FirstName}_{persona.LastName}")}_{DateTime.Now:yyyyMMddHHmm}.pdf";

                Console.WriteLine($"[PDF CONTROLLER] ✅ PDF generado: {fileName} ({pdfBytes.Length:N0} bytes)");

                return File(pdfBytes, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[PDF CONTROLLER] ❌ Error: {ex.Message}");
                return StatusCode(500, $"Error generando PDF: {ex.Message}");
            }
        }

        /// <summary>
        /// Genera PDF de Historia Clínica (con control de permisos por rol)
        /// </summary>
        [HttpGet("clinicalhistory/{id}")]
        public async Task<IActionResult> GetClinicalHistoryPdf(int id)
        {
            try
            {
                Console.WriteLine($"[PDF CONTROLLER] 📋 Generando PDF de Historia Clínica ID: {id}");

                var clinicalHistory = await _clinicalHistoryRepository.GetByIdAsync(id);
                if (clinicalHistory == null)
                {
                    return NotFound($"Historia clínica con ID {id} no encontrada");
                }

                var userRole = GetCurrentUserRole();
                var pdfBytes = await _pdfService.GenerateClinicalHistoryPdfAsync(clinicalHistory, userRole);
                var fileName = $"Historia_Clinica_{clinicalHistory.HistoryNumber}_{DateTime.Now:yyyyMMddHHmm}.pdf";

                Console.WriteLine($"[PDF CONTROLLER] ✅ PDF generado: {fileName}");

                return File(pdfBytes, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[PDF CONTROLLER] ❌ Error: {ex.Message}");
                return StatusCode(500, $"Error generando PDF de historia clínica: {ex.Message}");
            }
        }

        /// <summary>
        /// Genera PDF de Atención de Enfermería
        /// </summary>
        [HttpGet("nursing/{id}")]
        public async Task<IActionResult> GetNursingPdf(int id)
        {
            try
            {
                Console.WriteLine($"[PDF CONTROLLER] 💉 Generando PDF de Enfermería ID: {id}");

                var nursingCare = await _medicalCareRepository.GetByIdAsync(id);
                if (nursingCare == null)
                {
                    return NotFound($"Atención de enfermería con ID {id} no encontrada");
                }

                var pdfBytes = await _pdfService.GenerateNursingPdfAsync(nursingCare);
                var fileName = $"Atencion_Enfermeria_{nursingCare.CareId}_{DateTime.Now:yyyyMMddHHmm}.pdf";

                Console.WriteLine($"[PDF CONTROLLER] ✅ PDF generado: {fileName}");

                return File(pdfBytes, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[PDF CONTROLLER] ❌ Error: {ex.Message}");
                return StatusCode(500, $"Error generando PDF de enfermería: {ex.Message}");
            }
        }

        /// <summary>
        /// Genera PDF de Atención de Nutrición
        /// </summary>
        [HttpGet("nutrition/{id}")]
        public async Task<IActionResult> GetNutritionPdf(int id)
        {
            try
            {
                Console.WriteLine($"[PDF CONTROLLER] 🥗 Generando PDF de Nutrición ID: {id}");

                var nutritionCare = await _medicalCareRepository.GetByIdAsync(id);
                if (nutritionCare == null)
                {
                    return NotFound($"Atención de nutrición con ID {id} no encontrada");
                }

                // Verificar que sea nutrición
                var isNutritionCare = nutritionCare.Area?.ToLower().Contains("nutrición") == true ||
                                     nutritionCare.Area?.ToLower().Contains("nutricion") == true ||
                                     nutritionCare.LocationId == 3;

                if (!isNutritionCare)
                {
                    return BadRequest($"La atención con ID {id} no pertenece al área de nutrición");
                }

                var pdfBytes = await _pdfService.GenerateNutritionPdfAsync(nutritionCare);
                var fileName = $"Atencion_Nutricion_{nutritionCare.CareId}_{DateTime.Now:yyyyMMddHHmm}.pdf";

                Console.WriteLine($"[PDF CONTROLLER] ✅ PDF generado: {fileName}");

                return File(pdfBytes, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[PDF CONTROLLER] ❌ Error: {ex.Message}");
                return StatusCode(500, $"Error generando PDF de nutrición: {ex.Message}");
            }
        }

        /// <summary>
        /// Genera PDF de Atención Médica General
        /// </summary>
        [HttpGet("medical-care/{id}")]
        public async Task<IActionResult> GetMedicalCarePdf(int id)
        {
            try
            {
                Console.WriteLine($"[PDF CONTROLLER] ⚕️ Generando PDF de Atención Médica ID: {id}");

                var medicalCare = await _medicalCareRepository.GetByIdAsync(id);
                if (medicalCare == null)
                {
                    return NotFound($"Atención médica con ID {id} no encontrada");
                }

                var pdfBytes = await _pdfService.GenerateMedicalCarePdfAsync(medicalCare);
                var fileName = $"Atencion_Medica_{medicalCare.CareId}_{DateTime.Now:yyyyMMddHHmm}.pdf";

                Console.WriteLine($"[PDF CONTROLLER] ✅ PDF generado: {fileName}");

                return File(pdfBytes, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[PDF CONTROLLER] ❌ Error: {ex.Message}");
                return StatusCode(500, $"Error generando PDF de atención médica: {ex.Message}");
            }
        }

        #endregion

        #region Fisioterapia

        /// <summary>
        /// Genera PDF de Atención de Fisioterapia
        /// </summary>
        [HttpGet("physiotherapy/{id}")]
        public async Task<IActionResult> GetPhysiotherapyPdf(int id)
        {
            try
            {
                Console.WriteLine($"[PDF CONTROLLER] 🏃 Generando PDF de Fisioterapia ID: {id}");

                var physioCare = await _medicalCareRepository.GetByIdAsync(id);
                if (physioCare == null)
                {
                    return NotFound($"Atención de fisioterapia con ID {id} no encontrada");
                }

                await LoadPhysiotherapyData(physioCare);

                var pdfBytes = await _pdfService.GeneratePhysiotherapyPdfAsync(physioCare);
                var fileName = $"Atencion_Fisioterapia_{physioCare.CareId}_{DateTime.Now:yyyyMMddHHmm}.pdf";

                Console.WriteLine($"[PDF CONTROLLER] ✅ PDF generado: {fileName}");

                return File(pdfBytes, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[PDF CONTROLLER] ❌ Error: {ex.Message}");
                return StatusCode(500, $"Error generando PDF de fisioterapia: {ex.Message}");
            }
        }

        private async Task LoadPhysiotherapyData(MedicalCareDTO physioCare)
        {
            try
            {
                Console.WriteLine($"[PDF CONTROLLER] 📥 Cargando datos de fisioterapia...");

                var reasons = await _reasonForConsultationService.GetByCareIdAsync(physioCare.CareId);
                physioCare.ReasonForConsultation = reasons?.FirstOrDefault();

                var illnesses = await _currentIllnessService.GetByCareIdAsync(physioCare.CareId);
                physioCare.CurrentIllnesses = illnesses ?? new List<CurrentIllnessDTO>();

                physioCare.PainScales = await _painScaleService.GetByCareIdAsync(physioCare.CareId) ?? new List<PainScaleDTO>();
                physioCare.SkinEvaluations = await _skinEvaluationService.GetByCareIdAsync(physioCare.CareId) ?? new List<SkinEvaluationDTO>();
                physioCare.OsteoarticularEvaluations = await _osteoarticularEvaluationService.GetByCareIdAsync(physioCare.CareId) ?? new List<OsteoarticularEvaluationDTO>();
                physioCare.MedicalEvaluations = await _medicalEvaluationService.GetByCareIdAsync(physioCare.CareId) ?? new List<MedicalEvaluationDTO>();
                physioCare.NeuromuscularEvaluations = await _neuromuscularEvaluationService.GetByCareIdAsync(physioCare.CareId) ?? new List<NeuromuscularEvaluationDTO>();
                physioCare.PosturalEvaluations = await _posturalEvaluationService.GetByCareIdAsync(physioCare.CareId) ?? new List<PosturalEvaluationDTO>();
                physioCare.SpecialTests = await _specialTestService.GetByCareIdAsync(physioCare.CareId) ?? new List<SpecialTestDTO>();
                physioCare.ComplementaryExams = await _complementaryExamsService.GetByCareIdAsync(physioCare.CareId) ?? new List<ComplementaryExamsDTO>();
                physioCare.Sessions = await _sessionsService.GetByCareIdAsync(physioCare.CareId) ?? new List<SessionsDTO>();

                Console.WriteLine($"[PDF CONTROLLER] ✅ Datos de fisioterapia cargados");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[PDF CONTROLLER] ❌ Error cargando datos fisioterapia: {ex.Message}");
                throw;
            }
        }

        #endregion

        #region Estimulación Temprana

        /// <summary>
        /// Genera PDF de Atención de Estimulación Temprana
        /// </summary>
        [HttpGet("early-stimulation/{id}")]
        public async Task<IActionResult> GetEarlyStimulationPdf(int id)
        {
            try
            {
                Console.WriteLine($"[PDF CONTROLLER] 👶 Generando PDF de Estimulación Temprana ID: {id}");

                var earlyStimCare = await _medicalCareRepository.GetByIdAsync(id);
                if (earlyStimCare == null)
                {
                    return NotFound($"Atención de estimulación temprana con ID {id} no encontrada");
                }

                await LoadEarlyStimulationData(earlyStimCare);

                var pdfBytes = await _pdfService.GenerateEarlyStimulationPdfAsync(earlyStimCare);
                var fileName = $"Atencion_Estimulacion_Temprana_{earlyStimCare.CareId}_{DateTime.Now:yyyyMMddHHmm}.pdf";

                Console.WriteLine($"[PDF CONTROLLER] ✅ PDF generado: {fileName}");

                return File(pdfBytes, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[PDF CONTROLLER] ❌ Error: {ex.Message}");
                return StatusCode(500, $"Error generando PDF de estimulación temprana: {ex.Message}");
            }
        }

        private async Task LoadEarlyStimulationData(MedicalCareDTO earlyStimCare)
        {
            try
            {
                if (earlyStimCare == null) throw new ArgumentNullException(nameof(earlyStimCare));

                Console.WriteLine($"[PDF CONTROLLER] 📥 Cargando datos de estimulación temprana...");

                // Motivo de consulta
                if (earlyStimCare.CareId > 0)
                {
                    var reasons = await _reasonForConsultationService.GetByCareIdAsync(earlyStimCare.CareId);
                    earlyStimCare.ReasonForConsultation = reasons?.FirstOrDefault();
                }

                // Inicializar Patient si es nulo
                earlyStimCare.Patient ??= new PatientDTO();

                // Cargar Agent
                if (earlyStimCare.Patient?.Agent == null)
                {
                    Console.WriteLine($"[PDF CONTROLLER] 🔍 Buscando Agent...");

                    if (earlyStimCare.Patient.AgentId.HasValue)
                    {
                        var agentById = await _agentService.GetByIdAsync(earlyStimCare.Patient.AgentId.Value);
                        earlyStimCare.Patient.Agent = agentById;
                    }

                    if (earlyStimCare.Patient.Agent == null)
                    {
                        var allAgents = await _agentService.GetAllAsync() ?? new List<AgentDTO>();
                        earlyStimCare.Patient.Agent = allAgents.FirstOrDefault(a => a.AgentId == earlyStimCare.Patient.AgentId)
                                                   ?? allAgents.FirstOrDefault();
                    }
                }

                // Enriquecer datos del Agent
                if (earlyStimCare.Patient?.Agent != null)
                {
                    var agentsList = new List<AgentDTO> { earlyStimCare.Patient.Agent };
                    await EnrichAgentsData(agentsList);
                }

                // Sesiones y tests
                earlyStimCare.EarlyStimulationSessions = await _earlyStimulationSessionsService.GetByMedicalCareIdAsync(earlyStimCare.CareId)
                                                       ?? new List<EarlyStimulationSessionsDTO>();
                earlyStimCare.EarlyStimulationEvolutionTests = await _earlyStimulationEvolutionTestService.GetByMedicalCareIdAsync(earlyStimCare.CareId)
                                                             ?? new List<EarlyStimulationEvolutionTestDTO>();

                Console.WriteLine($"[PDF CONTROLLER] ✅ Datos de estimulación temprana cargados");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[PDF CONTROLLER] ❌ Error cargando datos estimulación temprana: {ex.Message}");
                throw;
            }
        }

        #endregion

        #region Psicología

        /// <summary>
        /// Genera PDF de Atención de Psicología
        /// </summary>
        [HttpGet("psychology/{id}")]
        public async Task<IActionResult> GetPsychologyPdf(int id)
        {
            try
            {
                Console.WriteLine($"[PDF CONTROLLER] 🧠 Generando PDF de Psicología ID: {id}");

                var psychologyCare = await _medicalCareRepository.GetByIdAsync(id);
                if (psychologyCare == null)
                {
                    return NotFound($"Atención de psicología con ID {id} no encontrada");
                }

                await LoadPsychologyData(psychologyCare);

                var pdfBytes = await _pdfService.GeneratePsychologyPdfAsync(psychologyCare);
                var fileName = $"Atencion_Psicologia_{psychologyCare.CareId}_{DateTime.Now:yyyyMMddHHmm}.pdf";

                Console.WriteLine($"[PDF CONTROLLER] ✅ PDF generado: {fileName}");

                return File(pdfBytes, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[PDF CONTROLLER] ❌ Error: {ex.Message}");
                return StatusCode(500, $"Error generando PDF de psicología: {ex.Message}");
            }
        }

        private async Task LoadPsychologyData(MedicalCareDTO psychologyCare)
        {
            try
            {
                Console.WriteLine($"[PDF CONTROLLER] 📥 Cargando datos de psicología...");

                // Motivo de consulta
                var reasons = await _reasonForConsultationService.GetByCareIdAsync(psychologyCare.CareId);
                psychologyCare.ReasonForConsultation = reasons?.FirstOrDefault();

                // Diagnósticos psicológicos
                var allDiagnoses = await _psychologicalDiagnosisService.GetAllAsync();
                psychologyCare.PsychologicalDiagnoses = allDiagnoses?
                    .Where(d => d.MedicalCareId == psychologyCare.CareId)
                    .ToList() ?? new List<PsychologicalDiagnosisDTO>();

                // Nombres de tipos de diagnóstico
                foreach (var diagnosis in psychologyCare.PsychologicalDiagnoses)
                {
                    if (diagnosis.DiagnosticTypeId > 0)
                    {
                        diagnosis.DiagnosticTypeName = await _diagnosticTypeService.GetDiagnosticTypeNameByIdAsync(diagnosis.DiagnosticTypeId);
                    }
                }

                // Planes terapéuticos
                psychologyCare.TherapeuticPlans = await _therapeuticPlanService.GetByMedicalCareIdAsync(psychologyCare.CareId)
                                                ?? new List<TherapeuticPlanDTO>();

                // Sesiones
                psychologyCare.PsychologySessions = await _psychologySessionsService.GetByMedicalCareIdAsync(psychologyCare.CareId)
                                                  ?? new List<PsychologySessionsDTO>();

                // Actividades
                var allActivities = new List<ActivityDTO>();
                foreach (var session in psychologyCare.PsychologySessions)
                {
                    var sessionActivities = await _activityService.GetByPsychologySessionIdAsync(session.PsychologySessionsId);
                    allActivities.AddRange(sessionActivities);
                }
                psychologyCare.Activities = allActivities;

                // Nombres de tipos de actividad
                var activityTypes = await _typeOfActivityService.GetAllAsync();
                foreach (var activity in psychologyCare.Activities)
                {
                    if (activity.TypeOfActivityId.HasValue)
                    {
                        var activityType = activityTypes.FirstOrDefault(t => t.TypeOfActivityId == activity.TypeOfActivityId);
                        activity.TypeOfActivityName = activityType?.Name ?? "N/A";
                    }
                }

                // Avances
                var allAdvances = new List<AdvanceDTO>();
                foreach (var session in psychologyCare.PsychologySessions)
                {
                    var sessionAdvances = await _advanceService.GetByPsychologySessionIdAsync(session.PsychologySessionsId);
                    allAdvances.AddRange(sessionAdvances);
                }
                psychologyCare.Advances = allAdvances;

                Console.WriteLine($"[PDF CONTROLLER] ✅ Datos de psicología cargados - " +
                    $"Diagnósticos: {psychologyCare.PsychologicalDiagnoses.Count}, " +
                    $"Planes: {psychologyCare.TherapeuticPlans.Count}, " +
                    $"Sesiones: {psychologyCare.PsychologySessions.Count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[PDF CONTROLLER] ❌ Error cargando datos psicología: {ex.Message}");
                throw;
            }
        }

        #endregion
    }
}