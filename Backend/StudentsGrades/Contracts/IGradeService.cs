using StudentsGrades.Models;

namespace StudentsGrades.Services
{
    public interface IGradeService
    {
        bool DataValidation(int gradeGot, string firstName, string lastName, string subjectName);
        Task<Grade> CreateGradeAsync(int gradeGot, string firstName, string lastName, string subjectName);
        Task<Grade?> GetGradeByIdAsync(Guid gradeId);
        Task<IEnumerable<Grade>> GetAllGradesAsync();
        // Should assign a new date when updating a grade
        Task UpdateGradeAsync(Guid gradeId, int grade);
        Task DeleteGradeAsync(Guid gradeId);
    }
}
