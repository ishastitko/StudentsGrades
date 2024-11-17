using Microsoft.EntityFrameworkCore;
using StudentsGrades.Data;
using StudentsGrades.Models;
using System.Linq;

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
    }
}
