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
                .Include(s => s.StudentSubjects)
                .Include(s => s.Grades)
                .FirstOrDefaultAsync(s => s.SubjectId == subjectId);
        }

        public async Task<Subject?> GetSubjectByNameAsync(string subjectName)
        {
            return await _context.Subjects
                .FirstOrDefaultAsync(s => s.SubjectName == subjectName);
        }

        public async Task DeleteSubjectAsync(Guid subjectId, Guid studentId)
        {
            // Checking if subject have any grades
            var subjectHasStudents = await _context.StudentSubjects
                .AnyAsync(ss => ss.SubjectId == subjectId && ss.StudentId != studentId);

            // Delete subject if it doesn't have any grades
            if (!subjectHasStudents)
            {
                var subject = await GetSubjectByIdAsync(subjectId);

                if (subject != null)
                    _context.Subjects.Remove(subject);

                await _context.SaveChangesAsync();
            }
        }
    }
}
