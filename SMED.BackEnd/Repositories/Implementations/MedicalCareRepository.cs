using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class MedicalCareRepository : IRepository<MedicalCareDTO, int>
    {
        private readonly SGISContext _context;

        public MedicalCareRepository(SGISContext context) => _context = context;

        public async Task<List<MedicalCareDTO>> GetAllAsync()
        {
            var medicalCaresData = await _context.MedicalCares
                .AsNoTracking()
                .OrderByDescending(m => m.CareDate)
                .Select(m => new
                {
                    m.CareId,
                    m.LocationId,
                    m.PlaceOfAttentionId,
                    m.PatientId,
                    m.HealthProfessionalId,
                    m.CareDate
                })
                .ToListAsync();

            Console.WriteLine($"[BACKEND DEBUG] Total registros obtenidos: {medicalCaresData.Count}");

            var locationIds = medicalCaresData.Select(m => m.LocationId).Distinct().ToList();
            var placeOfAttentionIds = medicalCaresData.Select(m => m.PlaceOfAttentionId).Distinct().ToList();
            var patientIds = medicalCaresData.Select(m => m.PatientId).Distinct().ToList();
            var healthProfessionalIds = medicalCaresData.Select(m => m.HealthProfessionalId).Distinct().ToList();

            var locations = await _context.Locations
                .AsNoTracking()
                .Where(l => locationIds.Contains(l.Id))
                .ToDictionaryAsync(l => l.Id, l => l.Name);

            var placeOfAttentions = await _context.PlaceOfAttentions
                .AsNoTracking()
                .Where(p => placeOfAttentionIds.Contains(p.Id))
                .ToDictionaryAsync(p => p.Id, p => p.Name);

            Console.WriteLine($"[BACKEND DEBUG] PlaceOfAttentions cargados desde BD:");
            foreach (var place in placeOfAttentions)
            {
                Console.WriteLine($"[BACKEND DEBUG] Id: {place.Key}, Name: {place.Value}");
            }

            var patients = await _context.Patients
                .AsNoTracking()
                .Where(p => patientIds.Contains(p.PersonId))
                .ToListAsync();

            var patientPersonIds = patients.Select(p => p.PersonId).ToList();
            var patientPersons = await _context.Persons
                .AsNoTracking()
                .Where(p => patientPersonIds.Contains(p.Id))
                .ToListAsync();

            var patientNames = patients
                .Join(patientPersons,
                    patient => patient.PersonId,
                    person => person.Id,
                    (patient, person) => new
                    {
                        PersonId = patient.PersonId,
                        FullName = string.Join(" ", new[] {
                            person.FirstName,
                            person.MiddleName,
                            person.LastName,
                            person.SecondLastName
                        }.Where(n => !string.IsNullOrWhiteSpace(n)))
                    })
                .ToDictionary(p => p.PersonId, p => p.FullName);

            var healthProfessionals = await _context.HealthProfessionals
                .AsNoTracking()
                .Where(hp => healthProfessionalIds.Contains(hp.HealthProfessionalId))
                .ToListAsync();

            var hpPersonIds = healthProfessionals.Select(hp => hp.HealthProfessionalId).ToList();
            var hpPersons = await _context.Persons
                .AsNoTracking()
                .Where(p => hpPersonIds.Contains(p.Id))
                .ToListAsync();

            var healthProfessionalNames = healthProfessionals
                .Join(hpPersons,
                    hp => hp.HealthProfessionalId,
                    person => person.Id,
                    (hp, person) => new
                    {
                        HealthProfessionalId = hp.HealthProfessionalId,
                        FullName = string.Join(" ", new[] {
                            person.FirstName,
                            person.MiddleName,
                            person.LastName,
                            person.SecondLastName
                        }.Where(n => !string.IsNullOrWhiteSpace(n)))
                    })
                .ToDictionary(hp => hp.HealthProfessionalId, hp => hp.FullName);

            var result = medicalCaresData.Select(m =>
            {
                var placeName = placeOfAttentions.ContainsKey(m.PlaceOfAttentionId)
                    ? placeOfAttentions[m.PlaceOfAttentionId]
                    : "Sin ubicación";

                Console.WriteLine($"[BACKEND DEBUG] CareId: {m.CareId}, " +
                                $"PlaceOfAttentionId: {m.PlaceOfAttentionId}, " +
                                $"PlaceName from Dictionary: {placeName}");

                return new MedicalCareDTO
                {
                    CareId = m.CareId,
                    LocationId = m.LocationId,
                    PlaceOfAttentionId = m.PlaceOfAttentionId,
                    Area = locations.ContainsKey(m.LocationId) ? locations[m.LocationId] : "Sin área",
                    NamePlace = placeName,
                    NamePatient = patientNames.ContainsKey(m.PatientId) ? patientNames[m.PatientId] : string.Empty,
                    PatientId = m.PatientId,
                    HealthProfessionalId = m.HealthProfessionalId,
                    NameHealthProfessional = healthProfessionalNames.ContainsKey(m.HealthProfessionalId)
                        ? healthProfessionalNames[m.HealthProfessionalId]
                        : string.Empty,
                    CareDate = m.CareDate
                };
            }).ToList();

            return result;
        }

        public async Task<List<MedicalCareDTO>> GetNursingCareAsync()
        {
            try
            {
                // Primero obtenemos el LocationId para enfermería por nombre
                var nursingLocationIds = await _context.Locations
                    .AsNoTracking()
                    .Where(l => l.Name.ToLower().Contains("enfermería") ||
                               l.Name.ToLower().Contains("enfermeria") ||
                               l.Name.ToLower().Contains("nursing"))
                    .Select(l => l.Id)
                    .ToListAsync();

                Console.WriteLine($"[BACKEND DEBUG GetNursing] LocationIds encontrados: {string.Join(", ", nursingLocationIds)}");

                if (!nursingLocationIds.Any())
                {
                    Console.WriteLine("[BACKEND DEBUG GetNursing] No se encontró ubicación de enfermería");
                    return new List<MedicalCareDTO>();
                }


                // Primero obtenemos solo los IDs y datos básicos
                var medicalCaresData = await _context.MedicalCares
                .AsNoTracking()
                .Where(m => nursingLocationIds.Contains(m.LocationId)) // LocationId = 1 es "Enfermería"
                .OrderByDescending(m => m.CareDate)
                .Select(m => new
                {
                    m.CareId,
                    m.LocationId,
                    m.PlaceOfAttentionId,
                    m.PatientId,
                    m.HealthProfessionalId,
                    m.CareDate
                })
                .ToListAsync();

                Console.WriteLine($"[BACKEND DEBUG GetNursing] Total registros obtenidos: {medicalCaresData.Count}");

                if (!medicalCaresData.Any())
                {
                    Console.WriteLine("[BACKEND DEBUG GetNursing] No se encontraron registros de Enfermería");
                    return new List<MedicalCareDTO>();
                }

                // Obtener los IDs únicos para las consultas
                var locationIds = medicalCaresData.Select(m => m.LocationId).Distinct().ToList();
                var placeOfAttentionIds = medicalCaresData.Select(m => m.PlaceOfAttentionId).Distinct().ToList();
                var patientIds = medicalCaresData.Select(m => m.PatientId).Distinct().ToList();
                var healthProfessionalIds = medicalCaresData.Select(m => m.HealthProfessionalId).Distinct().ToList();

                // Cargar Locations en un diccionario
                var locations = await _context.Locations
                    .AsNoTracking()
                    .Where(l => locationIds.Contains(l.Id))
                    .ToDictionaryAsync(l => l.Id, l => l.Name);

                // Cargar PlaceOfAttentions en un diccionario
                var placeOfAttentions = await _context.PlaceOfAttentions
                    .AsNoTracking()
                    .Where(p => placeOfAttentionIds.Contains(p.Id))
                    .ToDictionaryAsync(p => p.Id, p => p.Name);

                Console.WriteLine($"[BACKEND DEBUG GetNursing] PlaceOfAttentions cargados desde BD:");
                foreach (var place in placeOfAttentions)
                {
                    Console.WriteLine($"[BACKEND DEBUG GetNursing] Id: {place.Key}, Name: {place.Value}");
                }

                // Cargar Patients
                var patients = await _context.Patients
                    .AsNoTracking()
                    .Where(p => patientIds.Contains(p.PersonId))
                    .ToListAsync();

                var patientPersonIds = patients.Select(p => p.PersonId).ToList();
                var patientPersons = await _context.Persons
                    .AsNoTracking()
                    .Where(p => patientPersonIds.Contains(p.Id))
                    .ToListAsync();

                var patientNames = patients
                    .Join(patientPersons,
                        patient => patient.PersonId,
                        person => person.Id,
                        (patient, person) => new
                        {
                            PersonId = patient.PersonId,
                            FullName = string.Join(" ", new[] {
                            person.FirstName,
                            person.MiddleName,
                            person.LastName,
                            person.SecondLastName
                            }.Where(n => !string.IsNullOrWhiteSpace(n)))
                        })
                    .ToDictionary(p => p.PersonId, p => p.FullName);

                // Cargar HealthProfessionals
                var healthProfessionals = await _context.HealthProfessionals
                    .AsNoTracking()
                    .Where(hp => healthProfessionalIds.Contains(hp.HealthProfessionalId))
                    .ToListAsync();

                var hpPersonIds = healthProfessionals.Select(hp => hp.HealthProfessionalId).ToList();
                var hpPersons = await _context.Persons
                    .AsNoTracking()
                    .Where(p => hpPersonIds.Contains(p.Id))
                    .ToListAsync();

                var healthProfessionalNames = healthProfessionals
                    .Join(hpPersons,
                        hp => hp.HealthProfessionalId,
                        person => person.Id,
                        (hp, person) => new
                        {
                            HealthProfessionalId = hp.HealthProfessionalId,
                            FullName = string.Join(" ", new[] {
                            person.FirstName,
                            person.MiddleName,
                            person.LastName,
                            person.SecondLastName
                            }.Where(n => !string.IsNullOrWhiteSpace(n)))
                        })
                    .ToDictionary(hp => hp.HealthProfessionalId, hp => hp.FullName);

                // Construir el resultado final
                var result = medicalCaresData.Select(m =>
                {
                    var placeName = placeOfAttentions.ContainsKey(m.PlaceOfAttentionId)
                        ? placeOfAttentions[m.PlaceOfAttentionId]
                        : "Sin ubicación";

                    Console.WriteLine($"[BACKEND DEBUG GetNursing] CareId: {m.CareId}, " +
                                    $"PlaceOfAttentionId: {m.PlaceOfAttentionId}, " +
                                    $"PlaceName from Dictionary: {placeName}");

                    return new MedicalCareDTO
                    {
                        CareId = m.CareId,
                        LocationId = m.LocationId,
                        PlaceOfAttentionId = m.PlaceOfAttentionId,
                        Area = locations.ContainsKey(m.LocationId) ? locations[m.LocationId] : "Sin área",
                        NamePlace = placeName,
                        NamePatient = patientNames.ContainsKey(m.PatientId) ? patientNames[m.PatientId] : string.Empty,
                        PatientId = m.PatientId,
                        HealthProfessionalId = m.HealthProfessionalId,
                        NameHealthProfessional = healthProfessionalNames.ContainsKey(m.HealthProfessionalId)
                            ? healthProfessionalNames[m.HealthProfessionalId]
                            : string.Empty,
                        CareDate = m.CareDate
                    };
                }).ToList();

                Console.WriteLine($"[BACKEND DEBUG GetNursing] Total DTOs creados: {result.Count}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[BACKEND ERROR GetNursing] Error: {ex.Message}");
                return new List<MedicalCareDTO>();
            }
        }
        public async Task<List<MedicalCareDTO>> GetByAreaAndDateAsync(string area, DateTime? date = null)
        {
            var query = _context.MedicalCares
                .AsNoTracking()
                .Include(m => m.LocationNavigation)
                .Include(m => m.PlaceOfAttentionNavigation)
                .Include(m => m.Patient)
                    .ThenInclude(p => p.PersonNavigation)
                .Include(m => m.HealthProfessional)
                    .ThenInclude(h => h.PersonNavigation)
                .Where(m => m.LocationNavigation != null &&
                           m.LocationNavigation.Name.ToLower() == area.ToLower());

            if (date.HasValue)
            {
                query = query.Where(m => m.CareDate.Date == date.Value.Date);
            }

            var medicalCares = await query
                .OrderByDescending(m => m.CareDate)
                .ToListAsync();

            return medicalCares.Select(m => new MedicalCareDTO
            {
                CareId = m.CareId,
                LocationId = m.LocationId,
                PlaceOfAttentionId = m.PlaceOfAttentionId,
                Area = m.LocationNavigation?.Name,
                NamePlace = m.PlaceOfAttentionNavigation?.Name,
                NamePatient = m.Patient?.PersonNavigation != null
                    ? string.Join(" ", new[] {
                        m.Patient.PersonNavigation.FirstName,
                        m.Patient.PersonNavigation.MiddleName,
                        m.Patient.PersonNavigation.LastName,
                        m.Patient.PersonNavigation.SecondLastName
                    }.Where(n => !string.IsNullOrWhiteSpace(n)))
                    : string.Empty,
                PatientId = m.PatientId,
                HealthProfessionalId = m.HealthProfessionalId,
                NameHealthProfessional = m.HealthProfessional?.PersonNavigation != null
                    ? string.Join(" ", new[] {
                        m.HealthProfessional.PersonNavigation.FirstName,
                        m.HealthProfessional.PersonNavigation.MiddleName,
                        m.HealthProfessional.PersonNavigation.LastName,
                        m.HealthProfessional.PersonNavigation.SecondLastName
                    }.Where(n => !string.IsNullOrWhiteSpace(n)))
                    : string.Empty,
                CareDate = m.CareDate
            }).ToList();
        }

        public async Task<List<MedicalCareDTO>> GetByPlaceOfAttentionAndDateAsync(int? placeOfAttentionId = null, DateTime? date = null)
        {
            var query = _context.MedicalCares
                .AsNoTracking()
                .Include(m => m.LocationNavigation)
                .Include(m => m.PlaceOfAttentionNavigation)
                .Include(m => m.Patient)
                    .ThenInclude(p => p.PersonNavigation)
                .Include(m => m.HealthProfessional)
                    .ThenInclude(h => h.PersonNavigation)
                .Where(m => m.LocationNavigation != null &&
                           (m.LocationNavigation.Name.ToLower() == "enfermería" ||
                            m.LocationNavigation.Name.ToLower() == "enfermeria"));

            if (placeOfAttentionId.HasValue)
            {
                query = query.Where(m => m.PlaceOfAttentionId == placeOfAttentionId.Value);
            }

            if (date.HasValue)
            {
                query = query.Where(m => m.CareDate.Date == date.Value.Date);
            }

            var medicalCares = await query
                .OrderByDescending(m => m.CareDate)
                .ToListAsync();

            return medicalCares.Select(m => new MedicalCareDTO
            {
                CareId = m.CareId,
                LocationId = m.LocationId,
                PlaceOfAttentionId = m.PlaceOfAttentionId,
                Area = m.LocationNavigation?.Name,
                NamePlace = m.PlaceOfAttentionNavigation?.Name,
                NamePatient = m.Patient?.PersonNavigation != null
                    ? string.Join(" ", new[] {
                        m.Patient.PersonNavigation.FirstName,
                        m.Patient.PersonNavigation.MiddleName,
                        m.Patient.PersonNavigation.LastName,
                        m.Patient.PersonNavigation.SecondLastName
                    }.Where(n => !string.IsNullOrWhiteSpace(n)))
                    : string.Empty,
                PatientId = m.PatientId,
                HealthProfessionalId = m.HealthProfessionalId,
                NameHealthProfessional = m.HealthProfessional?.PersonNavigation != null
                    ? string.Join(" ", new[] {
                        m.HealthProfessional.PersonNavigation.FirstName,
                        m.HealthProfessional.PersonNavigation.MiddleName,
                        m.HealthProfessional.PersonNavigation.LastName,
                        m.HealthProfessional.PersonNavigation.SecondLastName
                    }.Where(n => !string.IsNullOrWhiteSpace(n)))
                    : string.Empty,
                CareDate = m.CareDate
            }).ToList();
        }

        public async Task<MedicalCareDTO?> GetByIdAsync(int id)
        {
            try
            {
                Console.WriteLine($"[DEBUG] Buscando MedicalCare ID: {id}");

                var medicalCareQuery = await _context.MedicalCares
                    .AsNoTracking()
                    .Where(m => m.CareId == id)
                    .Select(m => new
                    {
                        m.CareId,
                        m.LocationId,
                        m.PlaceOfAttentionId,
                        m.PatientId,
                        m.HealthProfessionalId,
                        m.CareDate
                    })
                    .FirstOrDefaultAsync();

                if (medicalCareQuery == null)
                {
                    Console.WriteLine($"[DEBUG] No se encontró MedicalCare con ID: {id}");
                    return null;
                }

                // 2) Obtener los nombres por separado
                var locationName = await _context.Locations
                    .AsNoTracking()
                    .Where(l => l.Id == medicalCareQuery.LocationId)
                    .Select(l => l.Name)
                    .FirstOrDefaultAsync();

                var placeOfAttentionName = await _context.PlaceOfAttentions
                    .AsNoTracking()
                    .Where(p => p.Id == medicalCareQuery.PlaceOfAttentionId)
                    .Select(p => p.Name)
                    .FirstOrDefaultAsync();

                Console.WriteLine($"[BACKEND DEBUG GetById] CareId: {id}, " +
                                $"PlaceOfAttentionId: {medicalCareQuery.PlaceOfAttentionId}, " +
                                $"PlaceOfAttentionName: {placeOfAttentionName}");

                var patientName = await _context.Patients
                    .Where(p => p.PersonId == medicalCareQuery.PatientId)
                    .Join(_context.Persons,
                        patient => patient.PersonId,
                        person => person.Id,
                        (patient, person) => new
                        {
                            person.FirstName,
                            person.MiddleName,
                            person.LastName,
                            person.SecondLastName
                        })
                    .Select(p => (p.FirstName ?? "") + " " +
                                 (p.MiddleName ?? "") + " " +
                                 (p.LastName ?? "") + " " +
                                 (p.SecondLastName ?? ""))
                    .FirstOrDefaultAsync();

                var professionalName = await _context.HealthProfessionals
                    .Where(hp => hp.HealthProfessionalId == medicalCareQuery.HealthProfessionalId)
                    .Join(_context.Persons,
                        hp => hp.HealthProfessionalId,
                        person => person.Id,
                        (hp, person) => new
                        {
                            person.FirstName,
                            person.MiddleName,
                            person.LastName,
                            person.SecondLastName
                        })
                    .Select(p => (p.FirstName ?? "") + " " +
                                 (p.MiddleName ?? "") + " " +
                                 (p.LastName ?? "") + " " +
                                 (p.SecondLastName ?? ""))
                    .FirstOrDefaultAsync();

                // 3) Obtener todas las colecciones por separado - FISIOTERAPIA
                var currentIllnesses = await _context.CurrentIllnesses
                    .Where(ci => ci.MedicalCareId == id)
                    .Select(ci => new CurrentIllnessDTO
                    {
                        CurrentIllnessId = ci.CurrentIllnessId,
                        EvolutionTime = ci.EvolutionTime,
                        Localization = ci.Localization,
                        Intensity = ci.Intensity,
                        AggravatingFactors = ci.AggravatingFactors,
                        MitigatingFactors = ci.MitigatingFactors,
                        NocturnalPain = ci.NocturnalPain,
                        Weakness = ci.Weakness,
                        Paresthesias = ci.Paresthesias,
                        ComplementaryExams = ci.ComplementaryExams,
                        MedicalCareId = ci.MedicalCareId
                    })
                    .ToListAsync();

                var physiotherapyDiagnostics = await _context.PhysiotherapyDiagnostics
                    .Where(pd => pd.MedicalCareId == id)
                    .Select(pd => new PhysiotherapyDiagnosticDTO
                    {
                        PhysiotherapyDiagnosticId = pd.PhysiotherapyDiagnosticId,
                        Description = pd.Description,
                        MedicalCareId = pd.MedicalCareId
                    })
                    .ToListAsync();

                var osteoarticularEvaluations = await _context.OsteoarticularEvaluations
                    .Where(oe => oe.MedicalCareId == id)
                    .Select(oe => new OsteoarticularEvaluationDTO
                    {
                        OsteoarticularEvaluationId = oe.OsteoarticularEvaluationId,
                        JointConditionId = oe.JointConditionId,
                        JointRangeOfMotionId = oe.JointRangeOfMotionId,
                        MedicalCareId = oe.MedicalCareId
                    })
                    .ToListAsync();

                var neuromuscularEvaluations = await _context.NeuromuscularEvaluations
                    .Where(ne => ne.MedicalCareId == id)
                    .Select(ne => new NeuromuscularEvaluationDTO
                    {
                        NeuromuscularEvaluationId = ne.NeuromuscularEvaluationId,
                        ShadeId = ne.ShadeId,
                        StrengthId = ne.StrengthId,
                        TrophismId = ne.TrophismId,
                        MedicalCareId = ne.MedicalCareId
                    })
                    .ToListAsync();

                var sensitivityEvaluations = await _context.SensitivityEvaluations
                    .Where(se => se.MedicalCareId == id)
                    .Select(se => new SensitivityEvaluationDTO
                    {
                        SensitivityEvaluationId = se.SensitivityEvaluationId,
                        Demandmas = se.Demandmas,
                        SensitivityLevelId = se.SensitivityLevelId,
                        BodyZoneId = se.BodyZoneId,
                        MedicalCareId = se.MedicalCareId
                    })
                    .ToListAsync();

                var skinEvaluations = await _context.SkinEvaluations
                    .Where(se => se.MedicalCareId == id)
                    .Select(se => new SkinEvaluationDTO
                    {
                        SkinEvaluationId = se.SkinEvaluationId,
                        ColorId = se.ColorId,
                        EdemaId = se.EdemaId,
                        StatusId = se.StatusId,
                        SwellingId = se.SwellingId,
                        MedicalCareId = se.MedicalCareId
                    })
                    .ToListAsync();

                var specialTests = await _context.SpecialTests
                    .Where(st => st.MedicalCareId == id)
                    .Select(st => new SpecialTestDTO
                    {
                        SpecialTestId = st.SpecialTestId,
                        Test = st.Test,
                        Observations = st.Observations,
                        ResultTypeId = st.ResultTypeId,
                        MedicalCareId = st.MedicalCareId
                    })
                    .ToListAsync();

                var painScales = await _context.PainScales
                    .Where(ps => ps.MedicalCareId == id)
                    .Select(ps => new PainScaleDTO
                    {
                        PainScaleId = ps.PainScaleId,
                        Observation = ps.Observation,
                        MedicalCareId = ps.MedicalCareId,
                        ActionId = ps.ActionId,
                        ActionName = ps.Action != null ? ps.Action.Name : null,
                        ScaleId = ps.ScaleId,
                        ScaleValue = ps.Scale != null ? ps.Scale.Value : null,
                        ScaleDescription = ps.Scale != null ? ps.Scale.Description : null,
                        PainMomentId = ps.PainMomentId,
                        PainMomentName = ps.PainMoment != null ? ps.PainMoment.Name : null
                    })
                    .ToListAsync();

                var sessions = await _context.Sessions
                    .Where(s => s.MedicalCareId == id)
                    .Select(s => new SessionsDTO
                    {
                        SessionsId = s.SessionsId,
                        Description = s.Description,
                        Date = s.Date,
                        MedicalCareId = s.MedicalCareId
                    })
                    .ToListAsync();

                // <ADD> 3.1) Obtener ComplementaryExams
                var complementaryExams = await _context.ComplementaryExams
                    .Where(ce => ce.MedicalCareId == id)
                    .Select(ce => new ComplementaryExamsDTO
                    {
                        ComplementaryExamsId = ce.ComplementaryExamsId,
                        Exam = ce.Exam,
                        ExamDate = ce.ExamDate,
                        Descriptions = ce.Descriptions,
                        PdfLink = ce.PdfLink,
                        MedicalCareId = ce.MedicalCareId
                    })
                    .ToListAsync();

                Console.WriteLine($"[DEBUG] ComplementaryExams cargados: {complementaryExams.Count}");

                // 3.2) Obtener PosturalEvaluations
                var posturalEvaluationsData = await _context.PosturalEvaluations
                    .Where(pe => pe.MedicalCareId == id)
                    .Select(pe => new
                    {
                        pe.PosturalEvaluationId,
                        pe.Observation,
                        pe.Grade,
                        pe.BodyAlignment,
                        pe.MedicalCareId,
                        pe.ViewId
                    })
                    .ToListAsync();

                var viewIds = posturalEvaluationsData
                    .Where(pe => pe.ViewId.HasValue)
                    .Select(pe => pe.ViewId.Value)
                    .Distinct()
                    .ToList();

                var views = await _context.Views
                    .Where(v => viewIds.Contains(v.ViewId))
                    .ToDictionaryAsync(v => v.ViewId, v => v.Name);

                var posturalEvaluations = posturalEvaluationsData.Select(pe => new PosturalEvaluationDTO
                {
                    PosturalEvaluationId = pe.PosturalEvaluationId,
                    Observation = pe.Observation,
                    Grade = pe.Grade,
                    BodyAlignment = pe.BodyAlignment,
                    MedicalCareId = pe.MedicalCareId,
                    ViewId = pe.ViewId,
                    ViewName = pe.ViewId.HasValue && views.ContainsKey(pe.ViewId.Value)
                        ? views[pe.ViewId.Value] : null
                }).ToList();

                Console.WriteLine($"[DEBUG] PosturalEvaluations cargadas: {posturalEvaluations.Count}");

                // 3.3) Obtener MedicalEvaluations
                var medicalEvaluationsData = await _context.MedicalEvaluations
                    .Where(me => me.MedicalCareId == id)
                    .Select(me => new
                    {
                        me.MedicalEvaluationId,
                        me.Description,
                        me.MedicalCareId,
                        me.TypeOfMedicalEvaluationId,
                        me.MedicalEvaluationPositionId,
                        me.MedicalEvaluationMembersId
                    })
                    .ToListAsync();

                var typeOfMedicalEvaluationIds = medicalEvaluationsData
                    .Where(me => me.TypeOfMedicalEvaluationId.HasValue)
                    .Select(me => me.TypeOfMedicalEvaluationId.Value)
                    .Distinct()
                    .ToList();

                var medicalEvaluationPositionIds = medicalEvaluationsData
                    .Where(me => me.MedicalEvaluationPositionId.HasValue)
                    .Select(me => me.MedicalEvaluationPositionId.Value)
                    .Distinct()
                    .ToList();

                var medicalEvaluationMembersIds = medicalEvaluationsData
                    .Where(me => me.MedicalEvaluationMembersId.HasValue)
                    .Select(me => me.MedicalEvaluationMembersId.Value)
                    .Distinct()
                    .ToList();

                var typeOfMedicalEvaluations = await _context.TypeOfMedicalEvaluations
                    .Where(t => typeOfMedicalEvaluationIds.Contains(t.TypeOfMedicalEvaluationId))
                    .ToDictionaryAsync(t => t.TypeOfMedicalEvaluationId, t => t.Name);

                var medicalEvaluationPositions = await _context.MedicalEvaluationPositions
                    .Where(p => medicalEvaluationPositionIds.Contains(p.MedicalEvaluationPositionId))
                    .ToDictionaryAsync(p => p.MedicalEvaluationPositionId, p => p.Name);

                var medicalEvaluationMembers = await _context.MedicalEvaluationMembers
                    .Where(m => medicalEvaluationMembersIds.Contains(m.MedicalEvaluationMembersId))
                    .ToDictionaryAsync(m => m.MedicalEvaluationMembersId, m => m.Name);

                var medicalEvaluations = medicalEvaluationsData.Select(me => new MedicalEvaluationDTO
                {
                    MedicalEvaluationId = me.MedicalEvaluationId,
                    Description = me.Description,
                    MedicalCareId = me.MedicalCareId,
                    TypeOfMedicalEvaluationId = me.TypeOfMedicalEvaluationId,
                    TypeOfMedicalEvaluationName = me.TypeOfMedicalEvaluationId.HasValue &&
                                                typeOfMedicalEvaluations.ContainsKey(me.TypeOfMedicalEvaluationId.Value)
                        ? typeOfMedicalEvaluations[me.TypeOfMedicalEvaluationId.Value] : null,
                    MedicalEvaluationPositionId = me.MedicalEvaluationPositionId,
                    MedicalEvaluationPositionName = me.MedicalEvaluationPositionId.HasValue &&
                                                  medicalEvaluationPositions.ContainsKey(me.MedicalEvaluationPositionId.Value)
                        ? medicalEvaluationPositions[me.MedicalEvaluationPositionId.Value] : null,
                    MedicalEvaluationMembersId = me.MedicalEvaluationMembersId,
                    MedicalEvaluationMembersName = me.MedicalEvaluationMembersId.HasValue &&
                                                 medicalEvaluationMembers.ContainsKey(me.MedicalEvaluationMembersId.Value)
                        ? medicalEvaluationMembers[me.MedicalEvaluationMembersId.Value] : null
                }).ToList();

                Console.WriteLine($"[DEBUG] MedicalEvaluations cargadas: {medicalEvaluations.Count}");

                // 4) Obtener datos COMUNES (VitalSigns, ReasonForConsultation)
                var vitalSigns = await _context.VitalSigns
                    .Where(vs => vs.MedicalCareId == id)
                    .Select(vs => new VitalSignsDTO
                    {
                        Id = vs.Id,
                        Weight = vs.Weight,
                        Height = vs.Height,
                        Icm = vs.Icm,
                        BloodPressure = vs.BloodPressure,
                        Temperature = vs.Temperature,
                        MeanArterialPressure = vs.MeanArterialPressure,
                        HeartRate = vs.HeartRate,
                        RespiratoryRate = vs.RespiratoryRate,
                        OxygenSaturation = vs.OxygenSaturation,
                        BloodGlucose = vs.BloodGlucose,
                        Hemoglobin = vs.Hemoglobin,
                        AbdominalCircumference = vs.AbdominalCircumference,
                        MedicalCareId = vs.MedicalCareId
                    })
                    .FirstOrDefaultAsync();

                var reasonForConsultation = await _context.ReasonForConsultations
                    .Where(rc => rc.MedicalCareId == id)
                    .Select(rc => new ReasonForConsultationDTO
                    {
                        Id = rc.Id,
                        Description = rc.Description,
                        MedicalCareId = rc.MedicalCareId
                    })
                    .FirstOrDefaultAsync();

                // 5) Obtener MedicalServices con consultas separadas
                var medicalServicesData = await _context.MedicalServices
                    .Where(ms => ms.CareId == id)
                    .Select(ms => new
                    {
                        ms.ServiceId,
                        ms.ServiceDate,
                        ms.ServiceType,
                        ms.Diagnosis,
                        ms.Observations,
                        ms.Recommendations,
                        ms.HealthProfessionalId,
                        ms.CareId
                    })
                    .ToListAsync();

                var medicalServiceHealthProfessionalIds = medicalServicesData
                    .Select(ms => ms.HealthProfessionalId)
                    .Distinct()
                    .ToList();

                var medicalServiceHealthProfessionals = await _context.HealthProfessionals
                    .Where(hp => medicalServiceHealthProfessionalIds.Contains(hp.HealthProfessionalId))
                    .Join(_context.Persons,
                        hp => hp.HealthProfessionalId,
                        person => person.Id,
                        (hp, person) => new
                        {
                            hp.HealthProfessionalId,
                            Person = person
                        })
                    .ToDictionaryAsync(x => x.HealthProfessionalId, x => x.Person);

                var medicalServices = medicalServicesData.Select(ms =>
                {
                    var person = medicalServiceHealthProfessionals.ContainsKey(ms.HealthProfessionalId)
                        ? medicalServiceHealthProfessionals[ms.HealthProfessionalId]
                        : null;

                    return new MedicalServiceDTO
                    {
                        ServiceId = ms.ServiceId,
                        ServiceDate = ms.ServiceDate,
                        ServiceType = ms.ServiceType,
                        Diagnosis = ms.Diagnosis,
                        Observations = ms.Observations,
                        Recommendations = ms.Recommendations,
                        HealthProfessionalId = ms.HealthProfessionalId,
                        HealthProfessionalName = person != null
                            ? string.Join(" ", new[] {
                person.FirstName,
                person.MiddleName,
                person.LastName,
                person.SecondLastName
                            }.Where(n => !string.IsNullOrWhiteSpace(n)))
                            : null,
                        CareId = ms.CareId
                    };
                }).ToList();

                // 6) Obtener MedicalProcedures con consultas separadas
                var medicalProceduresData = await _context.MedicalProcedures
                    .Where(mp => mp.CareId == id)
                    .Select(mp => new
                    {
                        mp.ProcedureId,
                        mp.ProcedureDate,
                        mp.SpecificProcedureId,
                        mp.HealthProfessionalId,
                        mp.TreatingPhysicianId,
                        mp.LocationId,
                        mp.Status,
                        mp.Observations,
                        mp.CareId
                    })
                    .ToListAsync();

                var procedureIds = medicalProceduresData.Select(mp => mp.SpecificProcedureId).Distinct().ToList();
                var healthProfessionalIds = medicalProceduresData.Select(mp => mp.HealthProfessionalId).Distinct().ToList();
                var treatingPhysicianIds = medicalProceduresData.Where(mp => mp.TreatingPhysicianId.HasValue).Select(mp => mp.TreatingPhysicianId.Value).Distinct().ToList();
                var locationIds = medicalProceduresData.Where(mp => mp.LocationId.HasValue).Select(mp => mp.LocationId.Value).Distinct().ToList();

                var procedures = await _context.Procedures
                    .Where(p => procedureIds.Contains(p.Id))
                    .Include(p => p.TypeOfProcedure)
                    .ToDictionaryAsync(p => p.Id);

                var healthProfessionals = await _context.HealthProfessionals
                    .Where(hp => healthProfessionalIds.Contains(hp.HealthProfessionalId))
                    .Include(hp => hp.PersonNavigation)
                    .ToDictionaryAsync(hp => hp.HealthProfessionalId);

                var treatingPhysicians = await _context.HealthProfessionals
                    .Where(tp => treatingPhysicianIds.Contains(tp.HealthProfessionalId))
                    .Include(tp => tp.PersonNavigation)
                    .ToDictionaryAsync(tp => tp.HealthProfessionalId);

                var locations = await _context.Locations
                    .Where(l => locationIds.Contains(l.Id))
                    .ToDictionaryAsync(l => l.Id);

                var medicalProcedures = medicalProceduresData.Select(mp =>
                {
                    var procedure = procedures.ContainsKey(mp.SpecificProcedureId) ? procedures[mp.SpecificProcedureId] : null;
                    var healthProfessional = healthProfessionals.ContainsKey(mp.HealthProfessionalId) ? healthProfessionals[mp.HealthProfessionalId] : null;
                    var treatingPhysician = mp.TreatingPhysicianId.HasValue && treatingPhysicians.ContainsKey(mp.TreatingPhysicianId.Value)
                        ? treatingPhysicians[mp.TreatingPhysicianId.Value] : null;
                    var location = mp.LocationId.HasValue && locations.ContainsKey(mp.LocationId.Value)
                        ? locations[mp.LocationId.Value] : null;

                    return new MedicalProcedureDTO
                    {
                        ProcedureId = mp.ProcedureId,
                        ProcedureDate = mp.ProcedureDate,
                        TypeOfProcedureId = procedure?.TypeOfProcedureId ?? 0,
                        TypeOfProcedureName = procedure?.TypeOfProcedure?.Name,
                        SpecificProcedureId = mp.SpecificProcedureId,
                        SpecificProcedureName = procedure?.Description,
                        HealthProfessionalId = mp.HealthProfessionalId,
                        HealthProfessionalName = healthProfessional?.PersonNavigation != null ?
                            string.Join(" ", new[] {
                healthProfessional.PersonNavigation.FirstName,
                healthProfessional.PersonNavigation.MiddleName,
                healthProfessional.PersonNavigation.LastName,
                healthProfessional.PersonNavigation.SecondLastName
                            }.Where(n => !string.IsNullOrWhiteSpace(n))) : null,
                        TreatingPhysicianId = mp.TreatingPhysicianId,
                        TreatingPhysicianName = treatingPhysician?.PersonNavigation != null ?
                            string.Join(" ", new[] {
                treatingPhysician.PersonNavigation.FirstName,
                treatingPhysician.PersonNavigation.MiddleName,
                treatingPhysician.PersonNavigation.LastName,
                treatingPhysician.PersonNavigation.SecondLastName
                            }.Where(n => !string.IsNullOrWhiteSpace(n))) : null,
                        LocationId = mp.LocationId,
                        LocationName = location?.Name,
                        Status = mp.Status,
                        Observations = mp.Observations,
                        CareId = mp.CareId
                    };
                }).ToList();

                // 7) Obtener ReviewSystemDevices (Aparatos y Sistemas) - MEDICINA GENERAL
                var reviewSystemDevices = await _context.ReviewSystemDevices
                    .Where(rsd => rsd.MedicalCareId == id)
                    .Select(rsd => new ReviewSystemDevicesDTO
                    {
                        Id = rsd.Id,
                        State = rsd.State,
                        Observations = rsd.Observations,
                        SystemsDevicesId = rsd.SystemsDevicesId,
                        SystemName = rsd.SystemsDevices != null ? rsd.SystemsDevices.Name : null,
                        MedicalCareId = rsd.MedicalCareId
                    })
                    .ToListAsync();

                Console.WriteLine($"[DEBUG] ReviewSystemDevices cargados: {reviewSystemDevices.Count}");

                // <CHANGE> 8) Obtener PhysicalExams con nombres de región y evidencia patológica
                var physicalExamsData = await _context.PhysicalExams
                    .Where(pe => pe.MedicalCareId == id)
                    .Select(pe => new
                    {
                        pe.PhysicalExamId, // <CHANGE> Corregido: usar PhysicalExamId en lugar de Id
                        pe.RegionId,
                        pe.PathologicalEvidenceId,
                        pe.Observation,
                        pe.MedicalCareId
                    })
                    .ToListAsync();

                var regionIds = physicalExamsData.Select(pe => pe.RegionId).Distinct().ToList();
                var pathologicalEvidenceIds = physicalExamsData.Select(pe => pe.PathologicalEvidenceId).Distinct().ToList();

                var regions = await _context.Regions
                    .Where(r => regionIds.Contains(r.Id))
                    .ToDictionaryAsync(r => r.Id, r => r.Name);

                var pathologicalEvidences = await _context.PathologicalEvidences
                    .Where(pe => pathologicalEvidenceIds.Contains(pe.Id))
                    .ToDictionaryAsync(pe => pe.Id, pe => pe.Name);

                var physicalExams = physicalExamsData.Select(pe => new PhysicalExamDTO
                {
                    PhysicalExamId = pe.PhysicalExamId, // <CHANGE> Corregido: usar PhysicalExamId
                    RegionId = pe.RegionId,
                    RegionName = regions.ContainsKey(pe.RegionId) ? regions[pe.RegionId] : null,
                    PathologicalEvidenceId = pe.PathologicalEvidenceId,
                    PathologicalEvidenceName = pathologicalEvidences.ContainsKey(pe.PathologicalEvidenceId)
                        ? pathologicalEvidences[pe.PathologicalEvidenceId] : null,
                    Observation = pe.Observation,
                    MedicalCareId = pe.MedicalCareId
                }).ToList();

                Console.WriteLine($"[DEBUG] PhysicalExams cargados: {physicalExams.Count}");

                // <CHANGE> 9) Obtener AdditionalData
                var additionalData = await _context.AdditionalData
                    .Where(ad => ad.MedicalCareId == id)
                    .Select(ad => new AdditionalDataDTO
                    {
                        AdditionalDataId = ad.AdditionalDataId,
                        Observacion = ad.Observacion,
                        MedicalCareId = ad.MedicalCareId
                    })
                    .FirstOrDefaultAsync();

                Console.WriteLine($"[DEBUG] AdditionalData: {additionalData != null}");

                // <CHANGE> 10) Obtener Diagnoses (MedicalDiagnosis)
                var diagnosesData = await _context.Diagnosis
                    .Where(d => d.MedicalCareId == id)
                    .Select(d => new
                    {
                        d.Id,
                        d.Cie10,
                        d.Denomination,
                        d.DiagnosticTypeId,
                        d.Recurrence,
                        d.DiagnosisMotivation,
                        d.MedicalCareId
                    })
                    .ToListAsync();

                var diagnosticTypeIds = diagnosesData.Select(d => d.DiagnosticTypeId).Distinct().ToList();
                var diagnosticTypes = await _context.DiagnosticTypes
                    .Where(dt => diagnosticTypeIds.Contains(dt.Id))
                    .ToDictionaryAsync(dt => dt.Id, dt => dt.Name);

                var diagnoses = diagnosesData.Select(d => new MedicalDiagnosisDTO
                {
                    Id = d.Id,
                    Cie10 = d.Cie10,
                    Denomination = d.Denomination,
                    DiagnosticTypeId = d.DiagnosticTypeId,
                    DiagnosticTypeName = diagnosticTypes.ContainsKey(d.DiagnosticTypeId)
                        ? diagnosticTypes[d.DiagnosticTypeId] : null,
                    Recurrence = d.Recurrence,
                    DiagnosisMotivation = d.DiagnosisMotivation,
                    MedicalCareId = d.MedicalCareId
                }).ToList();

                Console.WriteLine($"[DEBUG] Diagnoses cargados: {diagnoses.Count}");

                // <CHANGE> 11) Obtener Treatments y sus tipos (Pharmacological, NonPharmacological, Indications)
                var diagnosisIds = diagnoses.Select(d => d.Id).ToList();

                var treatmentsData = await _context.Treatments
                    .Where(t => diagnosisIds.Contains(t.MedicalDiagnosisId)) // <CHANGE> Corregido: usar MedicalDiagnosisId
                    .Select(t => new
                    {
                        t.Id,
                        t.MedicalDiagnosisId
                    })
                    .ToListAsync();

                var treatmentIds = treatmentsData.Select(t => t.Id).ToList();

                // Pharmacological Treatments
                var pharmacologicalTreatments = await _context.PharmacologicalTreatments
                    .Where(pt => treatmentIds.Contains(pt.Id))
                    .Join(_context.Medicines,
                        pt => pt.MedicineId,
                        m => m.Id,
                        (pt, m) => new PharmacologicalTreatmentDTO
                        {
                            Id = pt.Id,
                            MedicineId = pt.MedicineId,
                            MedicineName = m.Name,
                            Dose = pt.Dose,
                            Frequency = pt.Frequency,
                            Duration = pt.Duration,
                            ViaAdmission = pt.ViaAdmission
                        })
                    .ToListAsync();

                Console.WriteLine($"[DEBUG] PharmacologicalTreatments cargados: {pharmacologicalTreatments.Count}");

                // Non-Pharmacological Treatments
                var nonPharmacologicalTreatments = await _context.NonPharmacologicalTreatments
                    .Where(npt => treatmentIds.Contains(npt.Id))
                    .Select(npt => new NonPharmacologicalTreatmentDTO
                    {
                        Id = npt.Id,
                        Description = npt.Description
                    })
                    .ToListAsync();

                Console.WriteLine($"[DEBUG] NonPharmacologicalTreatments cargados: {nonPharmacologicalTreatments.Count}");

                // Indications
                var indications = await _context.Indications
                    .Where(i => treatmentIds.Contains(i.TreatmentId))
                    .Select(i => new IndicationsDTO
                    {
                        Id = i.Id,
                        Description = i.Description,
                        TreatmentId = i.TreatmentId
                    })
                    .ToListAsync();

                Console.WriteLine($"[DEBUG] Indications cargadas: {indications.Count}");

                // <CHANGE> 12) Obtener MedicalReferrals (Derivaciones)
                var referrals = await _context.MedicalReferrals
                    .Where(mr => mr.MedicalCareId == id)
                    .Select(mr => new MedicalReferralDTO
                    {
                        Id = mr.Id,
                        DateOfReferral = mr.DateOfReferral,
                        Description = mr.Description,
                        MedicalCareId = mr.MedicalCareId
                    })
                    .ToListAsync();

                Console.WriteLine($"[DEBUG] Referrals cargados: {referrals.Count}");

                // <CHANGE> 13) Obtener Evolutions
                var evolutions = await _context.Evolutions
                    .Where(e => e.MedicalCareId == id)
                    .Select(e => new EvolutionDTO
                    {
                        Id = e.Id,
                        Description = e.Description,
                        Percentage = e.Percentage,
                        MedicalCareId = e.MedicalCareId
                    })
                    .ToListAsync();

                Console.WriteLine($"[DEBUG] Evolutions cargadas: {evolutions.Count}");

                // <CHANGE> 14) Crear DTO final - AHORA con todas las variables ya declaradas
                var dto = new MedicalCareDTO
                {
                    CareId = medicalCareQuery.CareId,
                    LocationId = medicalCareQuery.LocationId,
                    PlaceOfAttentionId = medicalCareQuery.PlaceOfAttentionId,
                    Area = locationName ?? "Sin Area",
                    NamePlace = placeOfAttentionName ?? "Sin ubicación",
                    PatientId = medicalCareQuery.PatientId,
                    NamePatient = patientName?.Trim() ?? "Sin paciente",
                    HealthProfessionalId = medicalCareQuery.HealthProfessionalId,
                    NameHealthProfessional = professionalName?.Trim() ?? "Sin profesional",
                    CareDate = medicalCareQuery.CareDate,

                    // Fisioterapia
                    CurrentIllnesses = currentIllnesses,
                    PhysiotherapyDiagnostics = physiotherapyDiagnostics,
                    OsteoarticularEvaluations = osteoarticularEvaluations,
                    NeuromuscularEvaluations = neuromuscularEvaluations,
                    SensitivityEvaluations = sensitivityEvaluations,
                    SkinEvaluations = skinEvaluations,
                    SpecialTests = specialTests,
                    PainScales = painScales,
                    Sessions = sessions,

                    // Exámenes Complementarios
                    ComplementaryExams = complementaryExams, // <ADD> Agregar ComplementaryExams al DTO

                    // Evaluaciones Posturales
                    PosturalEvaluations = posturalEvaluations,

                    // Evaluaciones Médicas
                    MedicalEvaluations = medicalEvaluations,

                    // Común
                    VitalSigns = vitalSigns,
                    ReasonForConsultation = reasonForConsultation,
                    MedicalServices = medicalServices,
                    MedicalProcedures = medicalProcedures,

                    // Medicina General
                    ReviewSystemDevices = reviewSystemDevices,
                    PhysicalExams = physicalExams,
                    AdditionalData = additionalData,
                    Diagnoses = diagnoses,
                    PharmacologicalTreatments = pharmacologicalTreatments,
                    NonPharmacologicalTreatments = nonPharmacologicalTreatments,
                    Indications = indications,
                    Referrals = referrals,
                    Evolutions = evolutions
                };

                Console.WriteLine($"[DEBUG] DTO creado exitosamente para CareId: {id}");
                Console.WriteLine($"[DEBUG] Patient: {dto.NamePatient}, Professional: {dto.NameHealthProfessional}");
                Console.WriteLine($"[DEBUG] VitalSigns: {dto.VitalSigns != null}, ReasonForConsultation: {dto.ReasonForConsultation != null}");
                Console.WriteLine($"[DEBUG] MedicalServices: {dto.MedicalServices.Count}, MedicalProcedures: {dto.MedicalProcedures.Count}");
                Console.WriteLine($"[DEBUG] ComplementaryExams: {dto.ComplementaryExams?.Count ?? 0}"); // <ADD> Debug para ComplementaryExams
                Console.WriteLine($"[DEBUG] PosturalEvaluations: {dto.PosturalEvaluations?.Count ?? 0}");
                Console.WriteLine($"[DEBUG] MedicalEvaluations: {dto.MedicalEvaluations?.Count ?? 0}");
                Console.WriteLine($"[DEBUG] ReviewSystemDevices: {dto.ReviewSystemDevices?.Count ?? 0}, PhysicalExams: {dto.PhysicalExams?.Count ?? 0}");
                Console.WriteLine($"[DEBUG] Diagnoses: {dto.Diagnoses?.Count ?? 0}, Treatments: Pharma={dto.PharmacologicalTreatments?.Count ?? 0}, NonPharma={dto.NonPharmacologicalTreatments?.Count ?? 0}");
                Console.WriteLine($"[DEBUG] Referrals: {dto.Referrals?.Count ?? 0}, Evolutions: {dto.Evolutions?.Count ?? 0}");

                return dto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Exception al obtener MedicalCare con ID {id}: {ex.Message}");
                Console.WriteLine($"[ERROR] StackTrace: {ex.StackTrace}");
                throw;
            }
        }



        public async Task<MedicalCareDTO> AddAsync(MedicalCareDTO dto)
        {
            var entity = new MedicalCare
            {
                LocationId = dto.LocationId,
                PlaceOfAttentionId = dto.PlaceOfAttentionId,
                PatientId = dto.PatientId,
                HealthProfessionalId = dto.HealthProfessionalId,
                CareDate = dto.CareDate == default ? DateTime.Now : dto.CareDate
            };

            _context.MedicalCares.Add(entity);
            await _context.SaveChangesAsync();

            dto.CareId = entity.CareId;
            return dto;
        }

        public async Task<MedicalCareDTO?> UpdateAsync(MedicalCareDTO dto)
        {
            var entity = await _context.MedicalCares.FindAsync(dto.CareId);
            if (entity == null) return null;

            entity.LocationId = dto.LocationId;
            entity.PlaceOfAttentionId = dto.PlaceOfAttentionId;
            entity.PatientId = dto.PatientId;
            entity.HealthProfessionalId = dto.HealthProfessionalId;
            entity.CareDate = dto.CareDate;

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                Console.WriteLine($"[DEBUG] Iniciando eliminación de MedicalCare ID: {id}");

                var entity = await _context.MedicalCares
                    .Include(m => m.Diagnoses)
                        .ThenInclude(d => d.Treatments)
                            .ThenInclude(t => t.Indications)
                    .Include(m => m.Diagnoses)
                        .ThenInclude(d => d.OrderDiagnosis)
                    .Include(m => m.Diagnoses)
                        .ThenInclude(d => d.Interconsultations)
                    .Include(m => m.VitalSigns)
                    .Include(m => m.Evolutions)
                    .Include(m => m.ReasonsForConsultation)
                    .Include(m => m.ExamResults)
                    .Include(m => m.IdentifiedDiseases)
                    .Include(m => m.PhysicalExams)
                    .Include(m => m.ReviewSystemDevices)
                    .Include(m => m.MedicalReferrals)
                    .Include(m => m.MedicalServices)
                    .Include(m => m.MedicalProcedures)
                    .Include(m => m.AdditionalData)
                    .Include(m => m.MedicalEvaluations)
                    .Include(m => m.CurrentIllnesses)
                    .Include(m => m.PhysiotherapyDiagnostics)
                    .Include(m => m.OsteoarticularEvaluations)
                    .Include(m => m.NeuromuscularEvaluations)
                    .Include(m => m.SensitivityEvaluations)
                    .Include(m => m.SkinEvaluations)
                    .Include(m => m.SpecialTests)
                    .Include(m => m.PainScales)
                    .Include(m => m.Sessions)
                    .FirstOrDefaultAsync(m => m.CareId == id);

                if (entity == null)
                {
                    Console.WriteLine($"[DEBUG] No se encontró MedicalCare con ID: {id}");
                    return false;
                }

                Console.WriteLine($"[DEBUG] Entidad encontrada, eliminando relaciones...");

                // Eliminar diagnósticos y tratamientos
                foreach (var diagnosis in entity.Diagnoses)
                {
                    foreach (var treatment in diagnosis.Treatments)
                    {
                        _context.Indications.RemoveRange(treatment.Indications);

                        var pharmacological = await _context.PharmacologicalTreatments
                            .Where(pt => pt.Id == treatment.Id)
                            .ToListAsync();
                        _context.PharmacologicalTreatments.RemoveRange(pharmacological);

                        var nonPharmacological = await _context.NonPharmacologicalTreatments
                            .Where(npt => npt.Id == treatment.Id)
                            .ToListAsync();
                        _context.NonPharmacologicalTreatments.RemoveRange(nonPharmacological);
                    }

                    _context.Treatments.RemoveRange(diagnosis.Treatments);
                    _context.OrderDiagnosis.RemoveRange(diagnosis.OrderDiagnosis);
                    _context.Interconsultations.RemoveRange(diagnosis.Interconsultations);
                }

                _context.Diagnosis.RemoveRange(entity.Diagnoses);

                // Eliminar otros registros relacionados
                if (entity.VitalSigns != null)
                    _context.VitalSigns.Remove(entity.VitalSigns);

                _context.Evolutions.RemoveRange(entity.Evolutions);
                _context.ReasonForConsultations.RemoveRange(entity.ReasonsForConsultation);
                _context.ExamResults.RemoveRange(entity.ExamResults);
                _context.IdentifiedDiseases.RemoveRange(entity.IdentifiedDiseases);
                _context.PhysicalExams.RemoveRange(entity.PhysicalExams);
                _context.ReviewSystemDevices.RemoveRange(entity.ReviewSystemDevices);

                if (entity.MedicalReferrals != null && entity.MedicalReferrals.Any())
                    _context.MedicalReferrals.RemoveRange(entity.MedicalReferrals);

                _context.MedicalServices.RemoveRange(entity.MedicalServices);
                _context.MedicalProcedures.RemoveRange(entity.MedicalProcedures);

                if (entity.AdditionalData != null)
                    _context.AdditionalData.Remove(entity.AdditionalData);

                _context.MedicalEvaluations.RemoveRange(entity.MedicalEvaluations);

                Console.WriteLine($"[DEBUG] Eliminando relaciones de fisioterapia...");
                _context.CurrentIllnesses.RemoveRange(entity.CurrentIllnesses);
                _context.PhysiotherapyDiagnostics.RemoveRange(entity.PhysiotherapyDiagnostics);
                _context.OsteoarticularEvaluations.RemoveRange(entity.OsteoarticularEvaluations);
                _context.NeuromuscularEvaluations.RemoveRange(entity.NeuromuscularEvaluations);
                _context.SensitivityEvaluations.RemoveRange(entity.SensitivityEvaluations);
                _context.SkinEvaluations.RemoveRange(entity.SkinEvaluations);
                _context.SpecialTests.RemoveRange(entity.SpecialTests);
                _context.PainScales.RemoveRange(entity.PainScales);
                _context.Sessions.RemoveRange(entity.Sessions);

                // Finalmente eliminar la atención médica
                Console.WriteLine($"[DEBUG] Eliminando MedicalCare...");
                _context.MedicalCares.Remove(entity);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                Console.WriteLine($"[DEBUG] MedicalCare ID {id} eliminado exitosamente");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Error al eliminar MedicalCare ID {id}: {ex.Message}");
                Console.WriteLine($"[ERROR] StackTrace: {ex.StackTrace}");
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<MedicalCareDTO>> GetPhysiotherapyCareAsync()
        {
            try
            {
                // Primero obtenemos el LocationId para fisioterapia por nombre
                var physioLocationIds = await _context.Locations
                    .AsNoTracking()
                    .Where(l => l.Name.ToLower().Contains("fisioterapia") ||
                               l.Name.ToLower().Contains("fisio"))
                    .Select(l => l.Id)
                    .ToListAsync();

                Console.WriteLine($"[BACKEND DEBUG GetPhysiotherapy] LocationIds encontrados: {string.Join(", ", physioLocationIds)}");

                if (!physioLocationIds.Any())
                {
                    Console.WriteLine("[BACKEND DEBUG GetPhysiotherapy] No se encontró ubicación de fisioterapia");
                    return new List<MedicalCareDTO>();
                }

                // Obtenemos solo los IDs y datos básicos
                var medicalCaresData = await _context.MedicalCares
                    .AsNoTracking()
                    .Where(m => physioLocationIds.Contains(m.LocationId))
                    .OrderByDescending(m => m.CareDate)
                    .Select(m => new
                    {
                        m.CareId,
                        m.LocationId,
                        m.PlaceOfAttentionId,
                        m.PatientId,
                        m.HealthProfessionalId,
                        m.CareDate
                    })
                    .ToListAsync();

                Console.WriteLine($"[BACKEND DEBUG GetPhysiotherapy] Total registros obtenidos: {medicalCaresData.Count}");

                if (!medicalCaresData.Any())
                {
                    Console.WriteLine("[BACKEND DEBUG GetPhysiotherapy] No se encontraron registros de Fisioterapia");
                    return new List<MedicalCareDTO>();
                }

                // Obtener los IDs únicos para las consultas
                var locationIds = medicalCaresData.Select(m => m.LocationId).Distinct().ToList();
                var placeOfAttentionIds = medicalCaresData.Select(m => m.PlaceOfAttentionId).Distinct().ToList();
                var patientIds = medicalCaresData.Select(m => m.PatientId).Distinct().ToList();
                var healthProfessionalIds = medicalCaresData.Select(m => m.HealthProfessionalId).Distinct().ToList();

                // Cargar Locations en un diccionario
                var locations = await _context.Locations
                    .AsNoTracking()
                    .Where(l => locationIds.Contains(l.Id))
                    .ToDictionaryAsync(l => l.Id, l => l.Name);

                // Cargar PlaceOfAttentions en un diccionario
                var placeOfAttentions = await _context.PlaceOfAttentions
                    .AsNoTracking()
                    .Where(p => placeOfAttentionIds.Contains(p.Id))
                    .ToDictionaryAsync(p => p.Id, p => p.Name);

                Console.WriteLine($"[BACKEND DEBUG GetPhysiotherapy] PlaceOfAttentions cargados desde BD:");
                foreach (var place in placeOfAttentions)
                {
                    Console.WriteLine($"[BACKEND DEBUG GetPhysiotherapy] Id: {place.Key}, Name: {place.Value}");
                }

                // Cargar Patients
                var patients = await _context.Patients
                    .AsNoTracking()
                    .Where(p => patientIds.Contains(p.PersonId))
                    .ToListAsync();

                var patientPersonIds = patients.Select(p => p.PersonId).ToList();
                var patientPersons = await _context.Persons
                    .AsNoTracking()
                    .Where(p => patientPersonIds.Contains(p.Id))
                    .ToListAsync();

                var patientNames = patients
                    .Join(patientPersons,
                        patient => patient.PersonId,
                        person => person.Id,
                        (patient, person) => new
                        {
                            PersonId = patient.PersonId,
                            FullName = string.Join(" ", new[] {
                        person.FirstName,
                        person.MiddleName,
                        person.LastName,
                        person.SecondLastName
                            }.Where(n => !string.IsNullOrWhiteSpace(n)))
                        })
                    .ToDictionary(p => p.PersonId, p => p.FullName);

                // Cargar HealthProfessionals
                var healthProfessionals = await _context.HealthProfessionals
                    .AsNoTracking()
                    .Where(hp => healthProfessionalIds.Contains(hp.HealthProfessionalId))
                    .ToListAsync();

                var hpPersonIds = healthProfessionals.Select(hp => hp.HealthProfessionalId).ToList();
                var hpPersons = await _context.Persons
                    .AsNoTracking()
                    .Where(p => hpPersonIds.Contains(p.Id))
                    .ToListAsync();

                var healthProfessionalNames = healthProfessionals
                    .Join(hpPersons,
                        hp => hp.HealthProfessionalId,
                        person => person.Id,
                        (hp, person) => new
                        {
                            HealthProfessionalId = hp.HealthProfessionalId,
                            FullName = string.Join(" ", new[] {
                        person.FirstName,
                        person.MiddleName,
                        person.LastName,
                        person.SecondLastName
                            }.Where(n => !string.IsNullOrWhiteSpace(n)))
                        })
                    .ToDictionary(hp => hp.HealthProfessionalId, hp => hp.FullName);

                // Construir el resultado final
                var result = medicalCaresData.Select(m =>
                {
                    var placeName = placeOfAttentions.ContainsKey(m.PlaceOfAttentionId)
                        ? placeOfAttentions[m.PlaceOfAttentionId]
                        : "Sin ubicación";

                    Console.WriteLine($"[BACKEND DEBUG GetPhysiotherapy] CareId: {m.CareId}, " +
                                    $"PlaceOfAttentionId: {m.PlaceOfAttentionId}, " +
                                    $"PlaceName from Dictionary: {placeName}");

                    return new MedicalCareDTO
                    {
                        CareId = m.CareId,
                        LocationId = m.LocationId,
                        PlaceOfAttentionId = m.PlaceOfAttentionId,
                        Area = locations.ContainsKey(m.LocationId) ? locations[m.LocationId] : "Sin área",
                        NamePlace = placeName,
                        NamePatient = patientNames.ContainsKey(m.PatientId) ? patientNames[m.PatientId] : string.Empty,
                        PatientId = m.PatientId,
                        HealthProfessionalId = m.HealthProfessionalId,
                        NameHealthProfessional = healthProfessionalNames.ContainsKey(m.HealthProfessionalId)
                            ? healthProfessionalNames[m.HealthProfessionalId]
                            : string.Empty,
                        CareDate = m.CareDate
                    };
                }).ToList();

                Console.WriteLine($"[BACKEND DEBUG GetPhysiotherapy] Total DTOs creados: {result.Count}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[BACKEND ERROR GetPhysiotherapy] Error: {ex.Message}");
                throw;
            }
        }

        public async Task<List<MedicalCareDTO>> GetByPatientDocumentAsync(string documentNumber)
        {
            var medicalCares = await _context.MedicalCares
                .AsNoTracking()
                .Include(m => m.LocationNavigation)
                .Include(m => m.PlaceOfAttentionNavigation)
                .Include(m => m.Patient)
                    .ThenInclude(p => p.PersonNavigation)
                        .ThenInclude(pn => pn.PersonDocument)
                .Include(m => m.HealthProfessional)
                    .ThenInclude(h => h.PersonNavigation)
                .Where(m => m.Patient.PersonNavigation.PersonDocument.DocumentNumber == documentNumber)
                .OrderByDescending(m => m.CareDate)
                .ToListAsync();

            return medicalCares.Select(m => new MedicalCareDTO
            {
                CareId = m.CareId,
                LocationId = m.LocationId,
                PlaceOfAttentionId = m.PlaceOfAttentionId,
                Area = m.LocationNavigation?.Name,
                NamePlace = m.PlaceOfAttentionNavigation?.Name,
                NamePatient = m.Patient?.PersonNavigation != null
                    ? string.Join(" ", new[] {
                        m.Patient.PersonNavigation.FirstName,
                        m.Patient.PersonNavigation.MiddleName,
                        m.Patient.PersonNavigation.LastName,
                        m.Patient.PersonNavigation.SecondLastName
                    }.Where(n => !string.IsNullOrWhiteSpace(n)))
                    : string.Empty,
                PatientId = m.PatientId,
                HealthProfessionalId = m.HealthProfessionalId,
                NameHealthProfessional = m.HealthProfessional?.PersonNavigation != null
                    ? string.Join(" ", new[] {
                        m.HealthProfessional.PersonNavigation.FirstName,
                        m.HealthProfessional.PersonNavigation.MiddleName,
                        m.HealthProfessional.PersonNavigation.LastName,
                        m.HealthProfessional.PersonNavigation.SecondLastName
                    }.Where(n => !string.IsNullOrWhiteSpace(n)))
                    : string.Empty,
                CareDate = m.CareDate
            }).ToList();
        }

        public async Task<List<MedicalCareDTO>> GetEarlyStimulationCareAsync()
        {
            var allCares = await _context.MedicalCares
                .AsNoTracking()
                .Include(m => m.LocationNavigation)
                .Include(m => m.PlaceOfAttentionNavigation)
                .Include(m => m.Patient)
                    .ThenInclude(p => p.PersonNavigation)
                .Include(m => m.HealthProfessional)
                    .ThenInclude(h => h.PersonNavigation)
                .ToListAsync();

            var medicalCares = allCares
                .Where(m => m.LocationNavigation != null &&
                           (m.LocationNavigation.Name.ToLower() == "estimulación temprana" ||
                            m.LocationNavigation.Name.ToLower().Contains("estimulacion") ||
                            m.LocationNavigation.Name.ToLower().Contains("estimulación")))
                .OrderByDescending(m => m.CareDate)
                .ToList();

            return medicalCares.Select(m => new MedicalCareDTO
            {
                CareId = m.CareId,
                LocationId = m.LocationId,
                PlaceOfAttentionId = m.PlaceOfAttentionId,
                Area = m.LocationNavigation?.Name,
                NamePlace = m.PlaceOfAttentionNavigation?.Name,
                NamePatient = m.Patient?.PersonNavigation != null
                    ? string.Join(" ", new[] {
                m.Patient.PersonNavigation.FirstName,
                m.Patient.PersonNavigation.MiddleName,
                m.Patient.PersonNavigation.LastName,
                m.Patient.PersonNavigation.SecondLastName
                    }.Where(n => !string.IsNullOrWhiteSpace(n)))
                    : string.Empty,
                PatientId = m.PatientId,
                HealthProfessionalId = m.HealthProfessionalId,
                NameHealthProfessional = m.HealthProfessional?.PersonNavigation != null
                    ? string.Join(" ", new[] {
                m.HealthProfessional.PersonNavigation.FirstName,
                m.HealthProfessional.PersonNavigation.MiddleName,
                m.HealthProfessional.PersonNavigation.LastName,
                m.HealthProfessional.PersonNavigation.SecondLastName
                    }.Where(n => !string.IsNullOrWhiteSpace(n)))
                    : string.Empty,
                CareDate = m.CareDate
            }).ToList();
        }

        public async Task<List<MedicalCareDTO>> GetGeneralMedicineCareAsync()
        {
            // Primero obtenemos solo los IDs y datos básicos
            var medicalCaresData = await _context.MedicalCares
                .AsNoTracking()
                .Where(m => m.LocationId == 2) // LocationId = 2 es "Medicina General"
                .OrderByDescending(m => m.CareDate)
                .Select(m => new
                {
                    m.CareId,
                    m.LocationId,
                    m.PlaceOfAttentionId,
                    m.PatientId,
                    m.HealthProfessionalId,
                    m.CareDate
                })
                .ToListAsync();

            Console.WriteLine($"[BACKEND DEBUG GetGeneralMedicine] Total registros obtenidos: {medicalCaresData.Count}");

            if (!medicalCaresData.Any())
            {
                Console.WriteLine("[BACKEND DEBUG GetGeneralMedicine] No se encontraron registros de Medicina General");
                return new List<MedicalCareDTO>();
            }

            // Obtener los IDs únicos para las consultas
            var locationIds = medicalCaresData.Select(m => m.LocationId).Distinct().ToList();
            var placeOfAttentionIds = medicalCaresData.Select(m => m.PlaceOfAttentionId).Distinct().ToList();
            var patientIds = medicalCaresData.Select(m => m.PatientId).Distinct().ToList();
            var healthProfessionalIds = medicalCaresData.Select(m => m.HealthProfessionalId).Distinct().ToList();

            // Cargar Locations en un diccionario
            var locations = await _context.Locations
                .AsNoTracking()
                .Where(l => locationIds.Contains(l.Id))
                .ToDictionaryAsync(l => l.Id, l => l.Name);

            // Cargar PlaceOfAttentions en un diccionario
            var placeOfAttentions = await _context.PlaceOfAttentions
                .AsNoTracking()
                .Where(p => placeOfAttentionIds.Contains(p.Id))
                .ToDictionaryAsync(p => p.Id, p => p.Name);

            Console.WriteLine($"[BACKEND DEBUG GetGeneralMedicine] PlaceOfAttentions cargados desde BD:");
            foreach (var place in placeOfAttentions)
            {
                Console.WriteLine($"[BACKEND DEBUG GetGeneralMedicine] Id: {place.Key}, Name: {place.Value}");
            }

            // Cargar Patients
            var patients = await _context.Patients
                .AsNoTracking()
                .Where(p => patientIds.Contains(p.PersonId))
                .ToListAsync();

            var patientPersonIds = patients.Select(p => p.PersonId).ToList();
            var patientPersons = await _context.Persons
                .AsNoTracking()
                .Where(p => patientPersonIds.Contains(p.Id))
                .ToListAsync();

            var patientNames = patients
                .Join(patientPersons,
                    patient => patient.PersonId,
                    person => person.Id,
                    (patient, person) => new
                    {
                        PersonId = patient.PersonId,
                        FullName = string.Join(" ", new[] {
                            person.FirstName,
                            person.MiddleName,
                            person.LastName,
                            person.SecondLastName
                        }.Where(n => !string.IsNullOrWhiteSpace(n)))
                    })
                .ToDictionary(p => p.PersonId, p => p.FullName);

            // Cargar HealthProfessionals
            var healthProfessionals = await _context.HealthProfessionals
                .AsNoTracking()
                .Where(hp => healthProfessionalIds.Contains(hp.HealthProfessionalId))
                .ToListAsync();

            var hpPersonIds = healthProfessionals.Select(hp => hp.HealthProfessionalId).ToList();
            var hpPersons = await _context.Persons
                .AsNoTracking()
                .Where(p => hpPersonIds.Contains(p.Id))
                .ToListAsync();

            var healthProfessionalNames = healthProfessionals
                .Join(hpPersons,
                    hp => hp.HealthProfessionalId,
                    person => person.Id,
                    (hp, person) => new
                    {
                        HealthProfessionalId = hp.HealthProfessionalId,
                        FullName = string.Join(" ", new[] {
                            person.FirstName,
                            person.MiddleName,
                            person.LastName,
                            person.SecondLastName
                        }.Where(n => !string.IsNullOrWhiteSpace(n)))
                    })
                .ToDictionary(hp => hp.HealthProfessionalId, hp => hp.FullName);

            // Construir el resultado final
            var result = medicalCaresData.Select(m =>
            {
                var placeName = placeOfAttentions.ContainsKey(m.PlaceOfAttentionId)
                    ? placeOfAttentions[m.PlaceOfAttentionId]
                    : "Sin ubicación";

                Console.WriteLine($"[BACKEND DEBUG GetGeneralMedicine] CareId: {m.CareId}, " +
                                $"PlaceOfAttentionId: {m.PlaceOfAttentionId}, " +
                                $"PlaceName from Dictionary: {placeName}");

                return new MedicalCareDTO
                {
                    CareId = m.CareId,
                    LocationId = m.LocationId,
                    PlaceOfAttentionId = m.PlaceOfAttentionId,
                    Area = locations.ContainsKey(m.LocationId) ? locations[m.LocationId] : "Sin área",
                    NamePlace = placeName,
                    NamePatient = patientNames.ContainsKey(m.PatientId) ? patientNames[m.PatientId] : string.Empty,
                    PatientId = m.PatientId,
                    HealthProfessionalId = m.HealthProfessionalId,
                    NameHealthProfessional = healthProfessionalNames.ContainsKey(m.HealthProfessionalId)
                        ? healthProfessionalNames[m.HealthProfessionalId]
                        : string.Empty,
                    CareDate = m.CareDate
                };
            }).ToList();

            Console.WriteLine($"[BACKEND DEBUG GetGeneralMedicine] Total DTOs creados: {result.Count}");
            return result;
        }

    }
}
