using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsGrades.Data;
using StudentsGrades.Models;
using StudentsGrades.Services;

namespace StudentsGrades.Controllers{
    
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsGradesController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsGradesController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        /*[HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _studentService.
        }*/

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(Guid id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) 
                return NotFound();

            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(string firstName, string lastName)
        {
            var student = await _studentService.CreateStudentAsync(firstName, lastName);

            return CreatedAtAction(nameof(GetStudentById),
                new {id = student.StudentId}, student);
        }

        
    }
}
