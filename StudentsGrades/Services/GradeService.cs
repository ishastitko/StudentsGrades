using Microsoft.EntityFrameworkCore;
using StudentsGrades.Data;
using StudentsGrades.Models;

namespace StudentsGrades.Services
{
    public class GradeService : IGradeService
    {
        private readonly StudentsGradesDbContext _context;

        public GradeService(StudentsGradesDbContext context)
        {
            _context = context;
        }

        public async Task<Grade> CreateGradeAsync(int gradeGot, string firstName, string lastName, string subjectName)
        {
            var student = new Student(firstName, lastName);
            _context.Students.Add(student);

            var subject = new Subject(subjectName);
            _context.Subjects.Add(subject);

            var grade = new Grade(gradeGot, student.StudentId, subject.SubjectId);
            grade.Student = student;
            grade.Subject = subject;

            _context.Grades.Add(grade);

            await _context.SaveChangesAsync();

            return grade;
        }

        public async Task<Grade?> GetGradeByIdAsync(Guid gradeId)
        {
            return await _context.Grades
                .FirstOrDefaultAsync(s => s.GradeId == gradeId);
        }
    }
}
