using StudentsGrades.Models;

namespace StudentsGrades.Services
{
    public interface ISubjectService
    {
        Task<Subject> CreateSubjectAsync(string subjectName);
        Task<Subject?> GetSubjectByIdAsync(Guid subjectId);
    }
}
