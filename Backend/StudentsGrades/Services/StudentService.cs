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
                .Include(s => s.StudentSubjects)
                .Include(s => s.Grades)
                .FirstOrDefaultAsync(s => s.StudentId == studentId);
        }

        public async Task<Student?> GetStudentByNameAsync(string firstName, string lastName)
        {
            return await _context.Students
                .FirstOrDefaultAsync(s => s.FirstName == firstName && s.LastName == lastName);
        }

        public async Task DeleteStudentAsync(Guid studentId, Guid subjectId)
        {
            // Checking if student have any grades
            var studentHasSubjects = await _context.StudentSubjects
                .AnyAsync(ss => ss.StudentId == studentId && ss.SubjectId != subjectId);

            // Delete student if it doesn't have any grades
            if (!studentHasSubjects)
            {
                var student = await GetStudentByIdAsync(studentId);

                if (student != null)
                    _context.Students.Remove(student);

                await _context.SaveChangesAsync();
            }
        }
    }
}
