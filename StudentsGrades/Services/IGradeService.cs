using StudentsGrades.Models;

namespace StudentsGrades.Services
{
    public interface IGradeService
    {
        Task<Grade> CreateGradeAsync(int gradeGot, string firstName, string lastName, string subjectName);
        Task<Grade?> GetGradeByIdAsync(Guid gradeId);
    }
}
