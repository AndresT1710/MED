// PatientRepository.cs
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class PatientRepository : IRepository<PatientDTO, int>, IClinicalHistoryPatientRepository
    {
        private readonly SGISContext _context;

        public PatientRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<PatientDTO>> GetAllAsync()
        {
            var patients = await _context.Patients
                .Include(p => p.PersonNavigation)
                    .ThenInclude(p => p.PersonDocument)
                        .ThenInclude(d => d.DocumentTypeNavigation)
                .Include(p => p.PersonNavigation)
                    .ThenInclude(p => p.ClinicalHistory)
                .ToListAsync();

            return patients.Select(MapToDTO).ToList();
        }

        public async Task<PatientDTO?> GetByIdAsync(int id)
        {
            var patient = await _context.Patients
                .Include(p => p.PersonNavigation)
                    .ThenInclude(p => p.PersonDocument)
                        .ThenInclude(d => d.DocumentTypeNavigation)
                .Include(p => p.PersonNavigation)
                    .ThenInclude(p => p.ClinicalHistory)
                .FirstOrDefaultAsync(p => p.PersonId == id);

            return patient == null ? null : MapToDTO(patient);
        }

        public async Task<PatientDTO> AddAsync(PatientDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.Patients.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDTO(entity);
        }

        public async Task<PatientDTO?> UpdateAsync(PatientDTO dto)
        {
            var existing = await _context.Patients
                .Include(p => p.PersonNavigation)
                    .ThenInclude(p => p.PersonDocument)
                        .ThenInclude(d => d.DocumentTypeNavigation)
                .FirstOrDefaultAsync(p => p.PersonId == dto.PersonId);

            if (existing == null) return null;

            existing.PersonNavigation.FirstName = dto.Person.FirstName;
            existing.PersonNavigation.MiddleName = dto.Person.MiddleName;
            existing.PersonNavigation.LastName = dto.Person.LastName;
            existing.PersonNavigation.SecondLastName = dto.Person.SecondLastName;
            existing.PersonNavigation.BirthDate = dto.Person.BirthDate;
            existing.PersonNavigation.GenderId = dto.Person.GenderId;
            existing.PersonNavigation.Email = dto.Person.Email;

            if (existing.PersonNavigation.PersonDocument != null && dto.Person.Document != null)
            {
                existing.PersonNavigation.PersonDocument.DocumentTypeId = dto.Person.Document.DocumentTypeId;
                existing.PersonNavigation.PersonDocument.DocumentNumber = dto.Person.Document.DocumentNumber;

                if (existing.PersonNavigation.PersonDocument.DocumentTypeNavigation != null)
                {
                    existing.PersonNavigation.PersonDocument.DocumentTypeNavigation.Name = dto.Person.Document.DocumentTypeName;
                }
            }

            await _context.SaveChangesAsync();
            return MapToDTO(existing);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null) return false;

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return true;
        }

        // Nuevo método especializado corregido
        public async Task<List<PatientDTO>> GetPatientsWithHistoryAsync()
        {
            var patientsWithHistory = await _context.Patients
                .Include(p => p.PersonNavigation)
                    .ThenInclude(p => p.PersonDocument)
                        .ThenInclude(d => d.DocumentTypeNavigation)
                .Include(p => p.ClinicalHistory)  // incluir aquí la navegación directa
                .Where(p => p.ClinicalHistory != null)  // filtrar directamente
                .ToListAsync();

            return patientsWithHistory.Select(MapToDTO).ToList();
        }


        private PatientDTO MapToDTO(Patient entity)
        {
            return new PatientDTO
            {
                PersonId = entity.PersonId,
                Person = entity.PersonNavigation == null ? null : new PersonDTO
                {
                    Id = entity.PersonNavigation.Id,
                    FirstName = entity.PersonNavigation.FirstName,
                    MiddleName = entity.PersonNavigation.MiddleName,
                    LastName = entity.PersonNavigation.LastName,
                    SecondLastName = entity.PersonNavigation.SecondLastName,
                    BirthDate = entity.PersonNavigation.BirthDate,
                    GenderId = entity.PersonNavigation.GenderId,
                    Email = entity.PersonNavigation.Email,
                    Document = entity.PersonNavigation.PersonDocument == null ? null : new PersonDocumentDTO
                    {
                        PersonId = entity.PersonNavigation.PersonDocument.PersonId,
                        DocumentTypeId = entity.PersonNavigation.PersonDocument.DocumentTypeId,
                        DocumentNumber = entity.PersonNavigation.PersonDocument.DocumentNumber,
                        DocumentTypeName = entity.PersonNavigation.PersonDocument.DocumentTypeNavigation?.Name
                    },
                    ClinicalHistory = entity.PersonNavigation.ClinicalHistory == null ? null : new ClinicalHistoryDTO
                    {
                        ClinicalHistoryId = entity.PersonNavigation.ClinicalHistory.ClinicalHistoryId,
                        HistoryNumber = entity.PersonNavigation.ClinicalHistory.HistoryNumber,
                        CreationDate = entity.PersonNavigation.ClinicalHistory.CreationDate,
                        IsActive = entity.PersonNavigation.ClinicalHistory.IsActive,
                        GeneralObservations = entity.PersonNavigation.ClinicalHistory.GeneralObservations
                    }
                }
            };
        }

        private Patient MapToEntity(PatientDTO dto)
        {
            return new Patient
            {
                PersonId = dto.PersonId,
                PersonNavigation = new Person
                {
                    Id = dto.Person.Id,
                    FirstName = dto.Person.FirstName,
                    MiddleName = dto.Person.MiddleName,
                    LastName = dto.Person.LastName,
                    SecondLastName = dto.Person.SecondLastName,
                    BirthDate = dto.Person.BirthDate,
                    GenderId = dto.Person.GenderId,
                    Email = dto.Person.Email,
                    PersonDocument = dto.Person.Document == null ? null : new PersonDocument
                    {
                        PersonId = dto.Person.Document.PersonId,
                        DocumentTypeId = dto.Person.Document.DocumentTypeId,
                        DocumentNumber = dto.Person.Document.DocumentNumber,
                        DocumentTypeNavigation = new DocumentType
                        {
                            Name = dto.Person.Document.DocumentTypeName
                        }
                    }
                }
            };
        }
    }
}
