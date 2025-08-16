using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentSuccessApi.Data;
using StudentSuccessApi.DTOs;
using StudentSuccessApi.Models;
using StudentSuccessApi.Services;

namespace StudentSuccessApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class StudentsController : ControllerBase
	{
		private readonly AppDbContext _dbContext;
		private readonly StudentSuccessService _successService;

		public StudentsController(AppDbContext dbContext, StudentSuccessService successService)
		{
			_dbContext = dbContext;
			_successService = successService;
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<StudentProfile>> GetById(int id)
		{
			var entity = await _dbContext.StudentProfiles.FindAsync(id);
			if (entity == null) return NotFound();
			return Ok(entity);
		}

		[HttpPost]
		public async Task<ActionResult<StudentProfile>> Create(StudentProfile input)
		{
			input.CreatedAtUtc = DateTime.UtcNow;
			input.UpdatedAtUtc = DateTime.UtcNow;
			_dbContext.StudentProfiles.Add(input);
			await _dbContext.SaveChangesAsync();
			return CreatedAtAction(nameof(GetById), new { id = input.Id }, input);
		}

		[HttpPut("{id:int}")]
		public async Task<IActionResult> Update(int id, StudentProfile input)
		{
			if (id != input.Id) return BadRequest("ID mismatch");

			var existing = await _dbContext.StudentProfiles.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
			if (existing == null) return NotFound();

			input.CreatedAtUtc = existing.CreatedAtUtc;
			input.UpdatedAtUtc = DateTime.UtcNow;

			_dbContext.Entry(input).State = EntityState.Modified;
			await _dbContext.SaveChangesAsync();
			return NoContent();
		}

		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			var entity = await _dbContext.StudentProfiles.FindAsync(id);
			if (entity == null) return NotFound();
			_dbContext.StudentProfiles.Remove(entity);
			await _dbContext.SaveChangesAsync();
			return NoContent();
		}

		[HttpGet("{id:int}/success")]
		public async Task<ActionResult<StudentSuccessSummary>> GetSuccess(int id)
		{
			var entity = await _dbContext.StudentProfiles.FindAsync(id);
			if (entity == null) return NotFound();
			var summary = _successService.Calculate(entity);
			return Ok(summary);
		}
	}
}