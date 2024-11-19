using Microsoft.EntityFrameworkCore;
using StudentsGrades.Data;
using StudentsGrades.Models;

namespace StudentsGrades.Services
{
    public class GradeService : IGradeService
    {
        private readonly StudentsGradesDbContext _context;
        private readonly IStudentService _studentService;
        private readonly ISubjectService _subjectService;

        public GradeService(StudentsGradesDbContext context, IStudentService studentService, ISubjectService subjectService)
        {
            _context = context;
            _studentService = studentService;
            _subjectService = subjectService;
        }

        public bool DataValidation(int gradeGot, string firstName, string lastName, string subjectName)
        {
            if (gradeGot < 1 || gradeGot > 4)
                return false;

            if (!firstName.All(char.IsLetter))
                return false;

            if (!lastName.All(char.IsLetter))
                return false;

            return true;
        }

        public async Task<Grade> CreateGradeAsync(int gradeGot, string firstName, string lastName, string subjectName)
        {
            // first we check if student or subject already exist in the database
            // we get the data if they do exist
            // or create new instances if the don't
            var student = await _studentService.GetStudentByNameAsync(firstName, lastName);
            var subject = await _subjectService.GetSubjectByNameAsync(subjectName);

            // updating grade if student and subject already exist
            /*if (student != null && subject != null)
            {
                throw new Exception($"The student already has grade for this subject " +
                    $"{firstName} {lastName} {subjectName} {gradeGot}");
            }*/

            if (student == null)
                student = await _studentService.CreateStudentAsync(firstName, lastName);

            if (subject == null)
                subject = await _subjectService.CreateSubjectAsync(subjectName);

            // create grade instance
            var grade = new Grade(gradeGot, student.StudentId, subject.SubjectId);
            grade.Student = student;
            grade.Subject = subject;

            // linking students, subjects and grades
            student.Grades.Add(grade);
            student.Subjects.Add(subject);

            subject.Grades.Add(grade);
            subject.Students.Add(student);

            // saving changes to the database
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
                grade.DateTime = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteGradeAsync(Guid gradeId)
        {
            var grade = await _context.Grades
                .Include(g => g.Student)
                .Include(g => g.Subject)
                .FirstOrDefaultAsync(g => g.GradeId == gradeId);

            if (grade != null)
            {
                _context.Grades.Remove(grade);

                await _context.SaveChangesAsync();
            }
        }


    }
}
