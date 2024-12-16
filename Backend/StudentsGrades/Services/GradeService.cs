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

        // Register context
        public GradeService(StudentsGradesDbContext context, IStudentService studentService, ISubjectService subjectService)
        {
            _context = context;
            _studentService = studentService;
            _subjectService = subjectService;
        }

        // Grade is in range of 1 to 4, Name contains only latter
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
            /*
             * first we check if student or subject already exist in the database
             * we get the data if they do exist
             * or create new instances if they don't
             */
            var student = await _studentService.GetStudentByNameAsync(firstName, lastName);
            var subject = await _subjectService.GetSubjectByNameAsync(subjectName);

            // updating grade and finishing the method if student and subject already exist
            if (student != null && subject != null)
            {
                var existGrade = await _context.Grades
                    .FirstOrDefaultAsync(g => g.StudentId == student.StudentId && g.SubjectId == subject.SubjectId);

                if (existGrade != null)
                {
                    await UpdateGradeAsync(existGrade.GradeId, gradeGot);
                    return existGrade;
                }
            }

            if (student == null)
                student = await _studentService.CreateStudentAsync(firstName, lastName);

            if (subject == null)
                subject = await _subjectService.CreateSubjectAsync(subjectName);

            // create grade instance
            var grade = new Grade(gradeGot, student.StudentId, subject.SubjectId);
            grade.Student = student;
            grade.Subject = subject;

            var studentSubject = new StudentSubject(student.StudentId, subject.SubjectId);
            await _context.StudentSubjects.AddAsync(studentSubject);

            // linking students, subjects and grades
            student.Grades.Add(grade);
            student.StudentSubjects.Add(studentSubject);

            subject.Grades.Add(grade);
            subject.StudentSubjects.Add(studentSubject);

            // saving changes to the database
            await _context.Grades.AddAsync(grade);

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
                // Delete row in table that connects student and subjects
                var studentSubject = await _context.StudentSubjects
                    .FirstOrDefaultAsync(ss => ss.StudentId == grade.StudentId && ss.SubjectId == grade.SubjectId);

                if (studentSubject != null)
                    _context.StudentSubjects.Remove(studentSubject);

                // Delete grade
                _context.Grades.Remove(grade);

                // Calls method to delte student or subject if they don't have any grades
                await _studentService.DeleteStudentAsync(grade.StudentId, grade.SubjectId);
                await _subjectService.DeleteSubjectAsync(grade.SubjectId, grade.StudentId);

                await _context.SaveChangesAsync();
            }
        }


    }
}
