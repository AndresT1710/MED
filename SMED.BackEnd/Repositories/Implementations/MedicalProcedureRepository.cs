﻿using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class MedicalProcedureRepository : IRepository<MedicalProcedureDTO, int>
    {
        private readonly SGISContext _context;

        public MedicalProcedureRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<MedicalProcedureDTO>> GetAllAsync()
        {
            var medicalProcedures = await _context.MedicalProcedures
                .Include(mp => mp.Procedure)
                    .ThenInclude(p => p.TypeOfProcedure)
                .Include(mp => mp.HealthProfessional)
                    .ThenInclude(hp => hp.PersonNavigation)
                .Include(mp => mp.TreatingPhysician)
                    .ThenInclude(tp => tp.PersonNavigation)
                .ToListAsync();

            return medicalProcedures.Select(mp => new MedicalProcedureDTO
            {
                ProcedureId = mp.ProcedureId,
                ProcedureDate = mp.ProcedureDate,
                SpecificProcedureId = mp.SpecificProcedureId,
                CareId = mp.CareId,
                HealthProfessionalId = mp.HealthProfessionalId,
                PatientId = mp.PatientId,
                TreatingPhysicianId = mp.TreatingPhysicianId,
                Observations = mp.Observations,
                SpecificProcedureName = mp.Procedure?.Description,
                TypeOfProcedureId = mp.Procedure?.TypeOfProcedureId,
                TypeOfProcedureName = mp.Procedure?.TypeOfProcedure?.Name,
                HealthProfessionalName = mp.HealthProfessional?.PersonNavigation != null
                    ? string.Join(" ", new[] {
                mp.HealthProfessional.PersonNavigation.FirstName,
                mp.HealthProfessional.PersonNavigation.MiddleName,
                mp.HealthProfessional.PersonNavigation.LastName,
                mp.HealthProfessional.PersonNavigation.SecondLastName
                    }.Where(n => !string.IsNullOrWhiteSpace(n))) : null,
                TreatingPhysicianName = mp.TreatingPhysician?.PersonNavigation != null
                    ? string.Join(" ", new[] {
                mp.TreatingPhysician.PersonNavigation.FirstName,
                mp.TreatingPhysician.PersonNavigation.MiddleName,
                mp.TreatingPhysician.PersonNavigation.LastName,
                mp.TreatingPhysician.PersonNavigation.SecondLastName
                    }.Where(n => !string.IsNullOrWhiteSpace(n))) : null
            }).ToList();
        }


        public async Task<MedicalProcedureDTO?> GetByIdAsync(int id)
        {
            var mp = await _context.MedicalProcedures
                .Include(mp => mp.Procedure)
                    .ThenInclude(p => p.TypeOfProcedure)
                .Include(mp => mp.HealthProfessional)
                    .ThenInclude(hp => hp.PersonNavigation)
                .Include(mp => mp.TreatingPhysician)
                    .ThenInclude(tp => tp.PersonNavigation)
                .FirstOrDefaultAsync(mp => mp.ProcedureId == id);

            if (mp == null) return null;

            return new MedicalProcedureDTO
            {
                ProcedureId = mp.ProcedureId,
                ProcedureDate = mp.ProcedureDate,
                SpecificProcedureId = mp.SpecificProcedureId,
                CareId = mp.CareId,
                HealthProfessionalId = mp.HealthProfessionalId,
                PatientId = mp.PatientId,
                TreatingPhysicianId = mp.TreatingPhysicianId,
                Observations = mp.Observations,
                SpecificProcedureName = mp.Procedure?.Description,
                TypeOfProcedureId = mp.Procedure?.TypeOfProcedureId,
                TypeOfProcedureName = mp.Procedure?.TypeOfProcedure?.Name,
                HealthProfessionalName = mp.HealthProfessional?.PersonNavigation != null
                    ? string.Join(" ", new[] {
                mp.HealthProfessional.PersonNavigation.FirstName,
                mp.HealthProfessional.PersonNavigation.MiddleName,
                mp.HealthProfessional.PersonNavigation.LastName,
                mp.HealthProfessional.PersonNavigation.SecondLastName
                    }.Where(n => !string.IsNullOrWhiteSpace(n)))
                    : null,
                TreatingPhysicianName = mp.TreatingPhysician?.PersonNavigation != null
                    ? string.Join(" ", new[] {
                mp.TreatingPhysician.PersonNavigation.FirstName,
                mp.TreatingPhysician.PersonNavigation.MiddleName,
                mp.TreatingPhysician.PersonNavigation.LastName,
                mp.TreatingPhysician.PersonNavigation.SecondLastName
                    }.Where(n => !string.IsNullOrWhiteSpace(n)))
                    : null
            };
        }

        public async Task<MedicalProcedureDTO> AddAsync(MedicalProcedureDTO dto)
        {
            var entity = new MedicalProcedure
            {
                ProcedureDate = dto.ProcedureDate,
                SpecificProcedureId = dto.SpecificProcedureId,
                CareId = dto.CareId,
                HealthProfessionalId = dto.HealthProfessionalId,
                PatientId = dto.PatientId,
                TreatingPhysicianId = dto.TreatingPhysicianId,
                Observations = dto.Observations
            };

            _context.MedicalProcedures.Add(entity);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(entity.ProcedureId) ?? dto;
        }

        public async Task<MedicalProcedureDTO?> UpdateAsync(MedicalProcedureDTO dto)
        {
            var entity = await _context.MedicalProcedures.FindAsync(dto.ProcedureId);
            if (entity == null) return null;

            entity.ProcedureDate = dto.ProcedureDate;
            entity.SpecificProcedureId = dto.SpecificProcedureId;
            entity.CareId = dto.CareId;
            entity.HealthProfessionalId = dto.HealthProfessionalId;
            entity.PatientId = dto.PatientId;
            entity.TreatingPhysicianId = dto.TreatingPhysicianId;
            entity.Observations = dto.Observations;

            await _context.SaveChangesAsync();
            return await GetByIdAsync(entity.ProcedureId);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.MedicalProcedures.FindAsync(id);
            if (entity == null) return false;

            _context.MedicalProcedures.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
