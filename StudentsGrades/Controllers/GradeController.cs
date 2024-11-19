using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentsGrades.Services;
using StudentsGrades.Models;
using StudentsGrades.Data;

namespace StudentsGrades.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IGradeService _gradeService;

        public GradeController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IEnumerable<Grade>> GetGrades()
        {
            return await _gradeService.GetAllGradesAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGradeById(Guid id)
        {
            var grade = await _gradeService.GetGradeByIdAsync(id);
            if (grade == null)
                return NotFound();

            return Ok(grade);
        }

        [HttpPost]
        [Route("Post")]
        public async Task<ActionResult<Grade>> CreateGrade(int gradeGot, string firstName, string lastName, string subjectName)
        {
            var grade = await _gradeService.CreateGradeAsync(gradeGot, firstName, lastName, subjectName);

            return CreatedAtAction(nameof(GetGradeById),
                new { id = grade.GradeId }, grade);
        }

        [HttpPut]
        [Route("Update")]
        public async Task UpdateGrade(Guid gradeId, int newGrateGot)
        {
            await _gradeService.UpdateGradeAsync(gradeId, newGrateGot);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task DeleteAsync(Guid gradeId)
        {
            await _gradeService.DeleteGradeAsync(gradeId);
        }

    }
}
