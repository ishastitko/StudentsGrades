using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentsGrades.Models;

namespace StudentsGrades.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.StudentId);

            builder
                .HasMany(s => s.StudentSubjects)
                .WithOne(ss => ss.Student);

            builder
                .HasMany(s => s.Grades)
                .WithOne(g => g.Student);
        }
    }
}
