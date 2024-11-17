using StudentsGrades.Models;

namespace StudentsGrades.Services
{
    public interface IStudentService
    {
        Task<Student> CreateStudentAsync(string firstName, string lastName);
        Task<Student?> GetStudentByIdAsync(Guid studentId);
        //Task<IEnumerable<Student>> GetAllStudentsAsync();
        //Task UpdateStudentAsync(Student student);
        //Task DeleteStudentAsync(Guid studentId);

    }
}
