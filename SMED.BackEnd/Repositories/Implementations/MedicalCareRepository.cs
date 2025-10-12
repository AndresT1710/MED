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
            // Primero obtenemos solo los IDs y datos básicos
            var medicalCaresData = await _context.MedicalCares
                .AsNoTracking()
                .Where(m => m.LocationId == 1) // LocationId = 1 es "Enfermería"
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

                // 3) Obtener todas las colecciones por separado
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

                // 4) Obtener MedicalServices con consultas separadas (CORREGIDO)
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

                // 5) Obtener MedicalProcedures con consultas separadas
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

                // Obtener los IDs necesarios para las relaciones
                var procedureIds = medicalProceduresData.Select(mp => mp.SpecificProcedureId).Distinct().ToList();
                var healthProfessionalIds = medicalProceduresData.Select(mp => mp.HealthProfessionalId).Distinct().ToList();
                var treatingPhysicianIds = medicalProceduresData.Where(mp => mp.TreatingPhysicianId.HasValue).Select(mp => mp.TreatingPhysicianId.Value).Distinct().ToList();
                var locationIds = medicalProceduresData.Where(mp => mp.LocationId.HasValue).Select(mp => mp.LocationId.Value).Distinct().ToList();

                // Cargar datos relacionados
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

                // Construir el DTO
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

                // 6) Crear DTO final
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
                    CurrentIllnesses = currentIllnesses,
                    PhysiotherapyDiagnostics = physiotherapyDiagnostics,
                    OsteoarticularEvaluations = osteoarticularEvaluations,
                    NeuromuscularEvaluations = neuromuscularEvaluations,
                    SensitivityEvaluations = sensitivityEvaluations,
                    SkinEvaluations = skinEvaluations,
                    SpecialTests = specialTests,
                    PainScales = painScales,
                    Sessions = sessions,
                    VitalSigns = vitalSigns,
                    ReasonForConsultation = reasonForConsultation,
                    MedicalServices = medicalServices,
                    MedicalProcedures = medicalProcedures
                };

                Console.WriteLine($"[DEBUG] DTO creado exitosamente para CareId: {id}");
                Console.WriteLine($"[DEBUG] Patient: {dto.NamePatient}, Professional: {dto.NameHealthProfessional}");
                Console.WriteLine($"[DEBUG] VitalSigns: {dto.VitalSigns != null}, ReasonForConsultation: {dto.ReasonForConsultation != null}");
                Console.WriteLine($"[DEBUG] MedicalServices: {dto.MedicalServices.Count}, MedicalProcedures: {dto.MedicalProcedures.Count}");

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
            var allCares = await _context.MedicalCares
                .AsNoTracking()
                .Include(m => m.LocationNavigation)
                .Include(m => m.PlaceOfAttentionNavigation)
                .Include(m => m.Patient)
                    .ThenInclude(p => p.PersonNavigation)
                .Include(m => m.HealthProfessional)
                    .ThenInclude(h => h.PersonNavigation)
                .ToListAsync();

            Console.WriteLine($"[DEBUG] Total MedicalCares en BD: {allCares.Count}");

            foreach (var care in allCares)
            {
                var locationName = care.LocationNavigation?.Name ?? "NULL";
                Console.WriteLine($"[DEBUG] CareId: {care.CareId}, LocationId: {care.LocationId}, LocationName: {locationName}");
            }

            var medicalCares = allCares
                .Where(m => m.LocationNavigation != null &&
                           (m.LocationNavigation.Name.ToLower() == "fisioterapia" ||
                            m.LocationNavigation.Name.ToLower().Contains("fisio")))
                .OrderByDescending(m => m.CareDate)
                .ToList();

            Console.WriteLine($"[DEBUG] MedicalCares de fisioterapia filtrados: {medicalCares.Count}");

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
