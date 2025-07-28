﻿using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class DiagnosticTypeRepository : IRepository<DiagnosticTypeDTO, int>
    {
        private readonly SGISContext _context;

        public DiagnosticTypeRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<DiagnosticTypeDTO>> GetAllAsync()
        {
            var diagnosticTypes = await _context.DiagnosticTypes.ToListAsync();
            return diagnosticTypes.Select(MapToDto).ToList();
        }

        public async Task<DiagnosticTypeDTO?> GetByIdAsync(int id)
        {
            var diagnosticType = await _context.DiagnosticTypes.FindAsync(id);
            return diagnosticType == null ? null : MapToDto(diagnosticType);
        }

        public async Task<DiagnosticTypeDTO> AddAsync(DiagnosticTypeDTO dto)
        {
            var entity = new DiagnosticType
            {
                Name = dto.Name
            };

            _context.DiagnosticTypes.Add(entity);
            await _context.SaveChangesAsync();

            dto.Id = entity.Id;
            return dto;
        }

        public async Task<DiagnosticTypeDTO?> UpdateAsync(DiagnosticTypeDTO dto)
        {
            var entity = await _context.DiagnosticTypes.FindAsync(dto.Id);
            if (entity == null) return null;

            entity.Name = dto.Name;
            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.DiagnosticTypes.FindAsync(id);
            if (entity == null) return false;

            _context.DiagnosticTypes.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        private static DiagnosticTypeDTO MapToDto(DiagnosticType diagnosticType) => new DiagnosticTypeDTO
        {
            Id = diagnosticType.Id,
            Name = diagnosticType.Name
        };
    }

}
