using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentsGrades.Models;

namespace StudentsGrades.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(a => a.StudentId);

            builder
                .HasMany(s => s.Subjects)
                .WithMany(j => j.Students)
                .UsingEntity(t => t.ToTable("StudentsSubject"));

            builder
                .HasMany(s => s.Grades)
                .WithOne(g => g.Student);
        }
    }
}
