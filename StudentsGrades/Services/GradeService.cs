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
                .FindAsync(gradeId);
        }

        public async Task<IEnumerable<Grade>> GetAllGradesAsync()
        {
            return await _context.Grades
                .Include(s  => s.Student)
                .Include(j => j.Subject)
                .ToListAsync();
        }

        public async Task UpdateGradeAsync(Guid gradeId, int newGrade)
        {
            var grade = await _context.Grades.FindAsync(gradeId);
            if (grade != null)
            {
                grade.GradeGot = newGrade;
                //existingGrade.DateTime = grade.DateTime;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteGradeAsync(Guid gradeId)
        {
            var grade = await _context.Grades.FindAsync(gradeId);
            if (grade != null)
            {
                _context.Grades.Remove(grade);
                await _context.SaveChangesAsync();
            }
        }


    }
}
