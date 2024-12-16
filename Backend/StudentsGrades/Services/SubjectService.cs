using Microsoft.EntityFrameworkCore;
using StudentsGrades.Data;
using StudentsGrades.Models;

namespace StudentsGrades.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly StudentsGradesDbContext _context;

        public SubjectService(StudentsGradesDbContext context)
        {
            _context = context;
        }

        public async Task<Subject> CreateSubjectAsync(string subjectName)
        {
            var subject = new Subject(subjectName);

            _context.Add(subject);
            await _context.SaveChangesAsync();

            return subject;
        }

        public async Task<Subject?> GetSubjectByIdAsync(Guid subjectId)
        {
            return await _context.Subjects
                .Include(s => s.Students)
                .Include(s => s.Grades)
                .FirstOrDefaultAsync(s => s.SubjectId == subjectId);
        }

        public async Task<Subject?> GetSubjectByNameAsync(string subjectName)
        {
            return await _context.Subjects
                .FirstOrDefaultAsync(s => s.SubjectName == subjectName);
        }

        public async Task DeleteSubjectAsync(Guid subjectId)
        {
            var subject = await _context.Subjects.FindAsync(subjectId);

            if (subject != null)
            {
                // Subject will be deleted only if it has no students and grades
                if (subject.Grades.Count == 0 && subject.Students.Count == 0)
                {
                    _context.Subjects.Remove(subject);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
