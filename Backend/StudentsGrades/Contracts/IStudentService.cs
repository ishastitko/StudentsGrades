using StudentsGrades.Models;

namespace StudentsGrades.Services
{
    public interface IStudentService
    {
        Task<Student> CreateStudentAsync(string firstName, string lastName);
        Task<Student?> GetStudentByIdAsync(Guid studentId);
        Task<Student?> GetStudentByNameAsync(string firstName, string lastName);
        Task DeleteStudentAsync(Guid studentId, Guid subjectId);

    }
}
