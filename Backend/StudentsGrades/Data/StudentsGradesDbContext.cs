using Microsoft.EntityFrameworkCore;
using StudentsGrades.Models;
using StudentsGrades.Configurations;

namespace StudentsGrades.Data
{
    public class StudentsGradesDbContext(DbContextOptions<StudentsGradesDbContext> options) : DbContext(options)
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }

        // Register configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new SubjectConfiguration());
            modelBuilder.ApplyConfiguration(new GradeConfiguration());
            modelBuilder.ApplyConfiguration(new StudentSubjectConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
