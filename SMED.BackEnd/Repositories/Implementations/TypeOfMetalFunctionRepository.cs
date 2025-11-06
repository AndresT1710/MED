using Microsoft.EntityFrameworkCore;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using SGIS.Models;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class TypesOfMentalFunctionsRepository : IRepository<TypesOfMentalFunctionsDTO, int>
    {
        private readonly SGISContext _context;

        public TypesOfMentalFunctionsRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<TypesOfMentalFunctionsDTO>> GetAllAsync()
        {
            var entities = await _context.TypesOfMentalFunctions.ToListAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<TypesOfMentalFunctionsDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.TypesOfMentalFunctions.FindAsync(id);
            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<TypesOfMentalFunctionsDTO> AddAsync(TypesOfMentalFunctionsDTO dto)
        {
            var entity = MapToEntity(dto);
            _context.TypesOfMentalFunctions.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<TypesOfMentalFunctionsDTO?> UpdateAsync(TypesOfMentalFunctionsDTO dto)
        {
            var entity = await _context.TypesOfMentalFunctions.FindAsync(dto.TypeOfMentalFunctionId);
            if (entity == null) return null;

            entity.Name = dto.Name ?? string.Empty;

            await _context.SaveChangesAsync();
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.TypesOfMentalFunctions.FindAsync(id);
            if (entity == null) return false;

            // Verificar si hay funciones mentales asociadas
            var hasRelatedMentalFunctions = await _context.MentalFunctions
                .AnyAsync(mf => mf.TypeOfMentalFunctionId == id);

            if (hasRelatedMentalFunctions)
            {
                throw new InvalidOperationException("No se puede eliminar el tipo de función mental porque tiene funciones mentales asociadas.");
            }

            _context.TypesOfMentalFunctions.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<TypesOfMentalFunctionsDTO>> GetByNameAsync(string name)
        {
            var entities = await _context.TypesOfMentalFunctions
                .Where(t => t.Name.Contains(name))
                .ToListAsync();

            return entities.Select(MapToDto).ToList();
        }

        // =========================
        // 🔹 Mapeos
        // =========================
        private TypesOfMentalFunctionsDTO MapToDto(TypesOfMentalFunctions entity)
        {
            return new TypesOfMentalFunctionsDTO
            {
                TypeOfMentalFunctionId = entity.TypeOfMentalFunctionId,
                Name = entity.Name
            };
        }

        private TypesOfMentalFunctions MapToEntity(TypesOfMentalFunctionsDTO dto)
        {
            return new TypesOfMentalFunctions
            {
                TypeOfMentalFunctionId = dto.TypeOfMentalFunctionId,
                Name = dto.Name ?? string.Empty
            };
        }
    }
}