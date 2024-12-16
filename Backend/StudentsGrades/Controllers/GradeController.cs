using Microsoft.AspNetCore.Mvc;
using StudentsGrades.Services;
using StudentsGrades.Models;

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
            // Returns a BadRequest if data didn't pass DataValidation
            var dataValidation = _gradeService.DataValidation(gradeGot, firstName, lastName, subjectName);
            if (!dataValidation)
                return BadRequest();

            var grade = await _gradeService.CreateGradeAsync(gradeGot, firstName, lastName, subjectName);

            return CreatedAtAction(nameof(GetGradeById),
                new { id = grade.GradeId }, grade);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateGrade(Guid gradeId, int newGrateGot)
        {
            // Returns a BadRequest if data didn't pass DataValidation
            if (newGrateGot < 1 || newGrateGot > 4)
                return BadRequest();

            await _gradeService.UpdateGradeAsync(gradeId, newGrateGot);

            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task DeleteAsync(Guid gradeId)
        {
            await _gradeService.DeleteGradeAsync(gradeId);
        }

    }
}
