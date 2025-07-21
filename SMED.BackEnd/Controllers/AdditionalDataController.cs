using Microsoft.AspNetCore.Mvc;
using SGIS.Models;
using SMED.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdditionalDataController : ControllerBase
    {
        private readonly SGISContext _context;

        public AdditionalDataController(SGISContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<AdditionalDataDTO>>> GetAll()
        {
            var data = await _context.AdditionalData
                .Select(a => new AdditionalDataDTO
                {
                    AdditionalDataId = a.AdditionalDataId,
                    Observacion = a.Observacion,
                    MedicalCareId = a.MedicalCareId
                })
                .ToListAsync();

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdditionalDataDTO>> GetById(int id)
        {
            var data = await _context.AdditionalData
                .Where(a => a.AdditionalDataId == id)
                .Select(a => new AdditionalDataDTO
                {
                    AdditionalDataId = a.AdditionalDataId,
                    Observacion = a.Observacion,
                    MedicalCareId = a.MedicalCareId
                })
                .FirstOrDefaultAsync();

            return data != null ? Ok(data) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<AdditionalDataDTO>> Create(AdditionalDataDTO dto)
        {
            var entity = new AdditionalData
            {
                Observacion = dto.Observacion,
                MedicalCareId = dto.MedicalCareId
            };

            _context.AdditionalData.Add(entity);
            await _context.SaveChangesAsync();

            dto.AdditionalDataId = entity.AdditionalDataId;

            return CreatedAtAction(nameof(GetById), new { id = dto.AdditionalDataId }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AdditionalDataDTO dto)
        {
            if (id != dto.AdditionalDataId)
                return BadRequest();

            var entity = await _context.AdditionalData.FindAsync(id);
            if (entity == null)
                return NotFound();

            entity.Observacion = dto.Observacion;
            entity.MedicalCareId = dto.MedicalCareId;

            await _context.SaveChangesAsync();

            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.AdditionalData.FindAsync(id);
            if (entity == null)
                return NotFound();

            _context.AdditionalData.Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("by-medical-care/{medicalCareId}")]
        public async Task<ActionResult<IEnumerable<AdditionalDataDTO>>> GetByMedicalCare(int medicalCareId)
        {
            var result = await _context.AdditionalData
                .Where(a => a.MedicalCareId == medicalCareId)
                .Select(a => new AdditionalDataDTO
                {
                    AdditionalDataId = a.AdditionalDataId,
                    Observacion = a.Observacion,
                    MedicalCareId = a.MedicalCareId
                })
                .ToListAsync();

            return Ok(result);
        }
    }

}
