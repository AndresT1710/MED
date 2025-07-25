﻿using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthProfessionalController : ControllerBase
    {
        private readonly IRepository<HealthProfessionalDTO, int> _repository;

        public HealthProfessionalController(IRepository<HealthProfessionalDTO, int> repository) => _repository = repository;

        [HttpGet]
        public async Task<ActionResult<List<HealthProfessionalDTO>>> GetAll() => Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<HealthProfessionalDTO>> GetById(int id) =>
            await _repository.GetByIdAsync(id) is { } dto ? Ok(dto) : NotFound();

        [HttpPost]
        public async Task<ActionResult<HealthProfessionalDTO>> Create(HealthProfessionalDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.HealthProfessionalId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, HealthProfessionalDTO dto)
        {
            if (id != dto.HealthProfessionalId) return BadRequest();
            var updated = await _repository.UpdateAsync(dto);
            return updated != null ? Ok(updated) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
