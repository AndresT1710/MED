using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class AgentRepository : IRepository<AgentDTO, int>
    {
        private readonly SGISContext _context;

        public AgentRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<AgentDTO>> GetAllAsync()
        {
            var entities = await _context.Agents
                .Include(a => a.DocumentTypeNavigation)
                .Include(a => a.Gender)
                .Include(a => a.MaritalStatus)
                .Include(a => a.Patients)
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        public async Task<AgentDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.Agents
                .Include(a => a.DocumentTypeNavigation)
                .Include(a => a.Gender)
                .Include(a => a.MaritalStatus)
                .Include(a => a.Patients)
                .FirstOrDefaultAsync(a => a.AgentId == id);

            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<AgentDTO> AddAsync(AgentDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.Agents.Add(entity);
            await _context.SaveChangesAsync();

            await _context.Entry(entity)
                .Reference(a => a.DocumentTypeNavigation).LoadAsync();
            await _context.Entry(entity)
                .Reference(a => a.Gender).LoadAsync();
            await _context.Entry(entity)
                .Reference(a => a.MaritalStatus).LoadAsync();

            return MapToDto(entity);
        }

        public async Task<AgentDTO?> UpdateAsync(AgentDTO dto)
        {
            var entity = await _context.Agents
                .Include(a => a.DocumentTypeNavigation)
                .FirstOrDefaultAsync(a => a.AgentId == dto.AgentId);

            if (entity == null) return null;

            entity.IdentificationNumber = dto.IdentificationNumber;
            entity.FirstName = dto.FirstName;
            entity.MiddleName = dto.MiddleName;
            entity.LastName = dto.LastName;
            entity.SecondLastName = dto.SecondLastName;
            entity.PhoneNumber = dto.PhoneNumber;
            entity.CellphoneNumber = dto.CellphoneNumber;
            entity.DocumentType = dto.DocumentType;
            entity.GenderId = dto.GenderId;
            entity.MaritalStatusId = dto.MaritalStatusId;

            await _context.SaveChangesAsync();

            await _context.Entry(entity)
                .Reference(a => a.DocumentTypeNavigation).LoadAsync();
            await _context.Entry(entity)
                .Reference(a => a.Gender).LoadAsync();
            await _context.Entry(entity)
                .Reference(a => a.MaritalStatus).LoadAsync();

            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Agents.FindAsync(id);
            if (entity == null) return false;

            _context.Agents.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<AgentDTO>> GetByPatientIdAsync(int patientId)
        {
            var agents = await _context.Agents
                .Include(a => a.Patients)
                .Where(a => a.Patients.Any(p => p.PersonId == patientId))
                .Include(a => a.DocumentTypeNavigation)
                .Include(a => a.Gender)
                .Include(a => a.MaritalStatus)
                .ToListAsync();

            return agents.Select(MapToDto).ToList();
        }

        // NUEVO MÉTODO: Asignar agente a paciente
        public async Task<bool> AssignAgentToPatientAsync(int agentId, int patientId)
        {
            try
            {
                var patient = await _context.Patients
                    .FirstOrDefaultAsync(p => p.PersonId == patientId);

                if (patient == null)
                {
                    Console.WriteLine($"❌ Paciente no encontrado: {patientId}");
                    return false;
                }

                var agent = await _context.Agents
                    .FirstOrDefaultAsync(a => a.AgentId == agentId);

                if (agent == null)
                {
                    Console.WriteLine($"❌ Agente no encontrado: {agentId}");
                    return false;
                }

                // Asignar el agente al paciente
                patient.AgentId = agentId;
                await _context.SaveChangesAsync();

                Console.WriteLine($"✅ Agente {agentId} asignado correctamente al paciente {patientId}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error asignando agente {agentId} al paciente {patientId}: {ex.Message}");
                return false;
            }
        }

        private AgentDTO MapToDto(Agent entity) => new AgentDTO
        {
            AgentId = entity.AgentId,
            IdentificationNumber = entity.IdentificationNumber,
            FirstName = entity.FirstName,
            MiddleName = entity.MiddleName,
            LastName = entity.LastName,
            SecondLastName = entity.SecondLastName,
            PhoneNumber = entity.PhoneNumber,
            CellphoneNumber = entity.CellphoneNumber,
            DocumentType = entity.DocumentType,
            DocumentTypeName = entity.DocumentTypeNavigation?.Name,
            GenderId = entity.GenderId,
            GenderName = entity.Gender?.Name,
            MaritalStatusId = entity.MaritalStatusId,
            MaritalStatusName = entity.MaritalStatus?.Name,
            Patients = entity.Patients?.Select(p => new PatientDTO
            {
                PersonId = p.PersonId,
                AgentId = p.AgentId
            }).ToList()
        };

        private Agent MapToEntity(AgentDTO dto) => new Agent
        {
            AgentId = dto.AgentId,
            IdentificationNumber = dto.IdentificationNumber,
            FirstName = dto.FirstName,
            MiddleName = dto.MiddleName,
            LastName = dto.LastName,
            SecondLastName = dto.SecondLastName,
            PhoneNumber = dto.PhoneNumber,
            CellphoneNumber = dto.CellphoneNumber,
            DocumentType = dto.DocumentType,
            GenderId = dto.GenderId,
            MaritalStatusId = dto.MaritalStatusId
        };
    }
}