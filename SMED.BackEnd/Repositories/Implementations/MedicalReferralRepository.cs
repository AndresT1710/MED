using SGIS.Models;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace SMED.BackEnd.Repositories.Implementations
{
    public class MedicalReferralRepository : IRepository<MedicalReferralDTO, int>
    {
        private readonly SGISContext _context;

        public MedicalReferralRepository(SGISContext context)
        {
            _context = context;
        }

        public async Task<List<MedicalReferralDTO>> GetAllAsync()
        {
            var referrals = await _context.MedicalReferrals.ToListAsync();
            return referrals.Select(MapToDto).ToList();
        }

        public async Task<MedicalReferralDTO?> GetByIdAsync(int id)
        {
            var referral = await _context.MedicalReferrals.FindAsync(id);
            return referral == null ? null : MapToDto(referral);
        }

        public async Task<MedicalReferralDTO> AddAsync(MedicalReferralDTO dto)
        {
            var entity = new MedicalReferral
            {
                DateOfReferral = dto.DateOfReferral,
                Description = dto.Description,
                ServiceId = dto.ServiceId,
                MedicalCareId = dto.MedicalCareId
            };

            _context.MedicalReferrals.Add(entity);
            await _context.SaveChangesAsync();
            dto.Id = entity.Id;
            return dto;
        }

        public async Task<MedicalReferralDTO?> UpdateAsync(MedicalReferralDTO dto)
        {
            var entity = await _context.MedicalReferrals.FindAsync(dto.Id);
            if (entity == null) return null;

            entity.DateOfReferral = dto.DateOfReferral;
            entity.Description = dto.Description;
            entity.ServiceId = dto.ServiceId;
            entity.MedicalCareId = dto.MedicalCareId;
            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.MedicalReferrals.FindAsync(id);
            if (entity == null) return false;

            _context.MedicalReferrals.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        private static MedicalReferralDTO MapToDto(MedicalReferral referral) => new MedicalReferralDTO
        {
            Id = referral.Id,
            DateOfReferral = referral.DateOfReferral,
            Description = referral.Description,
            ServiceId = referral.ServiceId,
            MedicalCareId = referral.MedicalCareId
        };
    }

}
