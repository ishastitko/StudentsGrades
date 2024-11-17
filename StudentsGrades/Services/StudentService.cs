using StudentsGrades.Models;
using StudentsGrades.Data;
using Microsoft.EntityFrameworkCore;

namespace StudentsGrades.Services
{
    public class StudentService : IStudentService
    {
        private readonly StudentsGradesDbContext _context;

        public StudentService(StudentsGradesDbContext context)
        {
            _context = context;
        }

        public async Task<Student> CreateStudentAsync(string firstName, string lastName)
        {
            var student = new Student(firstName, lastName);

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return student;
        }

        public async Task<Student?> GetStudentByIdAsync(Guid studentId)
        {
            return await _context.Students
                .Include(s => s.Subjects)
                .Include(s => s.Grades)
                .FirstOrDefaultAsync(s => s.StudentId == studentId);
        }
    }
}
