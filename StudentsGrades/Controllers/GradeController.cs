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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGradeById(Guid id)
        {
            var grade = await _gradeService.GetGradeByIdAsync(id);
            if (grade == null)
                return NotFound();

            return Ok(grade);
        }

        [HttpPost]
        public async Task<ActionResult<Grade>> CreateGrade(int gradeGot, string firstName, string lastName, string subjectName)
        {
            var grade = await _gradeService.CreateGradeAsync(gradeGot, firstName, lastName, subjectName);

            return CreatedAtAction(nameof(GetGradeById),
                new { id = grade.GradeId }, grade);
        }

    }
}
