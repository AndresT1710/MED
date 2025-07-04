﻿using Microsoft.AspNetCore.Mvc;
using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;

namespace SMED.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SportsActivitiesHistoryController : ControllerBase
    {
        private readonly IRepository<SportsActivitiesHistoryDTO, int> _repository;

        public SportsActivitiesHistoryController(IRepository<SportsActivitiesHistoryDTO, int> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<SportsActivitiesHistoryDTO>>> GetAll() =>
            Ok(await _repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<SportsActivitiesHistoryDTO>> GetById(int id)
        {
            var dto = await _repository.GetByIdAsync(id);
            return dto != null ? Ok(dto) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<SportsActivitiesHistoryDTO>> Create(SportsActivitiesHistoryDTO dto)
        {
            var created = await _repository.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.SportActivityHistoryId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SportsActivitiesHistoryDTO dto)
        {
            if (id != dto.SportActivityHistoryId) return BadRequest("ID mismatch.");
            var updated = await _repository.UpdateAsync(dto);
            return updated != null ? Ok(updated) : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
        [HttpGet("by-clinical-history/{clinicalHistoryId}")]
        public async Task<ActionResult<List<SportsActivitiesHistoryDTO>>> GetByClinicalHistoryId(int clinicalHistoryId)
        {
            var allRecords = await _repository.GetAllAsync();
            var filtered = allRecords?.Where(x => x.ClinicalHistoryId == clinicalHistoryId).ToList() ?? new List<SportsActivitiesHistoryDTO>();
            return Ok(filtered);
        }
    }
}
