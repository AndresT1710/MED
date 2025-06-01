using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Para ToListAsync()
using SGIS.Models;
using SMED.Shared.DTOs;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiseaseController : ControllerBase
    {
        private readonly SGISContext _context;

        public DiseaseController(SGISContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<DiseaseDTO>>> GetAll()
        {
            var diseases = await _context.Diseases
                .Select(d => new DiseaseDTO
                {
                    DiseaseId = d.DiseaseId,
                    Name = d.Name,
                    DiseaseTypeId = d.DiseaseTypeId
                })
                .ToListAsync();

            return Ok(diseases);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DiseaseDTO>> GetById(int id)
        {
            var disease = await _context.Diseases
                .Where(d => d.DiseaseId == id)
                .Select(d => new DiseaseDTO
                {
                    DiseaseId = d.DiseaseId,
                    Name = d.Name,
                    DiseaseTypeId = d.DiseaseTypeId
                })
                .FirstOrDefaultAsync();

            return disease != null ? Ok(disease) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<DiseaseDTO>> Create(DiseaseDTO dto)
        {
            var entity = new Disease
            {
                Name = dto.Name,
                DiseaseTypeId = dto.DiseaseTypeId
            };

            _context.Diseases.Add(entity);
            await _context.SaveChangesAsync();

            dto.DiseaseId = entity.DiseaseId;

            return CreatedAtAction(nameof(GetById), new { id = dto.DiseaseId }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DiseaseDTO dto)
        {
            if (id != dto.DiseaseId)
                return BadRequest();

            var entity = await _context.Diseases.FindAsync(id);
            if (entity == null)
                return NotFound();

            entity.Name = dto.Name;
            entity.DiseaseTypeId = dto.DiseaseTypeId;

            await _context.SaveChangesAsync();

            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.Diseases.FindAsync(id);
            if (entity == null)
                return NotFound();

            _context.Diseases.Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Aquí está la ruta que funcionó para tu compañero
        [HttpGet("disease/{diseaseTypeId}")]
        public async Task<ActionResult<IEnumerable<DiseaseDTO>>> GetDiseasesByType(int diseaseTypeId)
        {
            var diseases = await _context.Diseases
                .Where(d => d.DiseaseTypeId == diseaseTypeId)
                .Select(d => new DiseaseDTO
                {
                    DiseaseId = d.DiseaseId,
                    Name = d.Name,
                    DiseaseTypeId = d.DiseaseTypeId
                })
                .ToListAsync();

            return Ok(diseases);
        }
    }
}
