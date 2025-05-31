using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations

{
    public class ClinicalHistoryRepository : IClinicalHistoryRepository
    {
        private readonly SGISContext _context;

        public ClinicalHistoryRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<ClinicalHistoryDTO>> GetAllAsync()
        {
            var histories = await _context.ClinicalHistories
                .Include(ch => ch.Patient)
                    .ThenInclude(p => p.PersonNavigation)
                .ToListAsync();

            return histories.Select(MapToDTO).ToList();
        }

        public async Task<ClinicalHistoryDTO?> GetByIdAsync(int id)
        {
            var history = await _context.ClinicalHistories
                .Include(ch => ch.Patient)
                    .ThenInclude(p => p.PersonNavigation)
                .FirstOrDefaultAsync(ch => ch.ClinicalHistoryId == id);

            return history == null ? null : MapToDTO(history);
        }

        public async Task<ClinicalHistoryDTO> AddAsync(ClinicalHistoryDTO dto)
        {
            if (dto.Patient == null || dto.Patient.PersonId == 0)
                throw new Exception("Patient information is required.");

            // Validar que exista el paciente con ese PersonId
            var patientExists = await _context.Patients.AnyAsync(p => p.PersonId == dto.Patient.PersonId);
            if (!patientExists)
                throw new Exception("Patient not found.");

            var clinicalHistory = new ClinicalHistory
            {
                HistoryNumber = dto.HistoryNumber,
                CreationDate = dto.CreationDate ?? DateTime.Now,
                IsActive = dto.IsActive ?? true,
                GeneralObservations = dto.GeneralObservations,
                PatientId = dto.Patient.PersonId
            };

            _context.ClinicalHistories.Add(clinicalHistory);
            await _context.SaveChangesAsync();

            // Mapear entidad guardada a DTO para devolver
            var createdDto = new ClinicalHistoryDTO
            {
                ClinicalHistoryId = clinicalHistory.ClinicalHistoryId,
                HistoryNumber = clinicalHistory.HistoryNumber,
                CreationDate = clinicalHistory.CreationDate,
                IsActive = clinicalHistory.IsActive,
                GeneralObservations = clinicalHistory.GeneralObservations,
                Patient = new PatientDTO
                {
                    PersonId = clinicalHistory.PatientId ?? 0
                    // Si quieres, puedes mapear más propiedades
                }
            };

            return createdDto;
        }



        public async Task<ClinicalHistoryDTO?> UpdateAsync(ClinicalHistoryDTO dto)
        {
            // Buscar la entidad por su ID correcto
            var entity = await _context.ClinicalHistories
                    .Include(ch => ch.Patient)
                    .FirstOrDefaultAsync(ch => ch.ClinicalHistoryId == dto.ClinicalHistoryId);

            if (entity == null)
                return null;

            // Validar que no exista otro registro con el mismo HistoryNumber (excluyendo el actual)
            bool duplicateExists = await _context.ClinicalHistories
                .AnyAsync(ch => ch.HistoryNumber == dto.HistoryNumber && ch.ClinicalHistoryId != dto.ClinicalHistoryId);

            if (duplicateExists)
                throw new InvalidOperationException("HistoryNumber already exists for another record.");

            // Actualizar campos
            entity.HistoryNumber = dto.HistoryNumber;
            entity.CreationDate = dto.CreationDate;
            entity.IsActive = dto.IsActive;
            entity.GeneralObservations = dto.GeneralObservations;
            entity.PatientId = dto.Patient.PersonId;

            await _context.SaveChangesAsync();

            return MapToDTO(entity);
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ClinicalHistories.FindAsync(id);
            if (entity == null)
                return false;

            _context.ClinicalHistories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ClinicalHistoryDTO>> SearchAsync(string searchTerm, bool searchByIdNumber = false)
        {
            var query = _context.ClinicalHistories
                .Include(ch => ch.Patient)
                    .ThenInclude(p => p.PersonNavigation)
                        .ThenInclude(p => p.PersonDocument)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                if (searchByIdNumber)
                {
                    // Búsqueda por número de documento (cédula)
                    query = query.Where(ch =>
                        ch.Patient != null &&
                        ch.Patient.PersonNavigation != null &&
                        ch.Patient.PersonNavigation.PersonDocument != null &&
                        ch.Patient.PersonNavigation.PersonDocument.DocumentNumber.Contains(searchTerm));
                }
                else
                {
                    // Búsqueda por número de historia clínica
                    query = query.Where(ch => ch.HistoryNumber.Contains(searchTerm));
                }
            }

            return query.Select(MapToDTO).ToList();
        }


        private ClinicalHistoryDTO MapToDTO(ClinicalHistory entity)
        {
            return new ClinicalHistoryDTO
            {
                ClinicalHistoryId = entity.ClinicalHistoryId,
                HistoryNumber = entity.HistoryNumber,
                CreationDate = entity.CreationDate,
                IsActive = entity.IsActive,
                GeneralObservations = entity.GeneralObservations,
                Patient = new PatientDTO
                {
                    PersonId = entity.Patient.PersonId,
                    Person = entity.Patient.PersonNavigation != null ? new PersonDTO
                    {
                        Id = entity.Patient.PersonNavigation.Id,
                        FirstName = entity.Patient.PersonNavigation.FirstName,
                        MiddleName = entity.Patient.PersonNavigation.MiddleName,
                        LastName = entity.Patient.PersonNavigation.LastName,
                        SecondLastName = entity.Patient.PersonNavigation.SecondLastName,
                        BirthDate = entity.Patient.PersonNavigation.BirthDate,
                        Email = entity.Patient.PersonNavigation.Email
                        // Agrega otras propiedades según necesites
                    } : null
                }
            };
        }

        private ClinicalHistory MapToEntity(ClinicalHistoryDTO dto)
        {
            return new ClinicalHistory
            {
                HistoryNumber = dto.HistoryNumber,
                CreationDate = dto.CreationDate ?? DateTime.Now,
                IsActive = dto.IsActive ?? true,
                GeneralObservations = dto.GeneralObservations,
                PatientId = dto.Patient.PersonId
            };
        }
    }

}
