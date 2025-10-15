using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentTypeController : ControllerBase
    {
        private readonly IRepository<DocumentTypeDTO, int> _repository;

        public DocumentTypeController(IRepository<DocumentTypeDTO, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<DocumentTypeDTO>>> GetAll()
        {
            var documentTypes = await _repository.GetAllAsync();
            return Ok(documentTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentTypeDTO>> GetById(int id)
        {
            var documentType = await _repository.GetByIdAsync(id);
            if (documentType == null) return NotFound();
            return Ok(documentType);
        }

        [HttpPost]
        public async Task<ActionResult<DocumentTypeDTO>> Create(DocumentTypeDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DocumentTypeDTO>> Update(int id, DocumentTypeDTO dto)
        {
            if (id != dto.Id) return BadRequest();
            var updated = await _repository.UpdateAsync(dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _repository.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}