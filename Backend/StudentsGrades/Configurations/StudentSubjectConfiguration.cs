using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using StudentsGrades.Models;

namespace StudentsGrades.Configurations
{
    public class StudentSubjectConfiguration : IEntityTypeConfiguration<StudentSubject>
    {
        public void Configure(EntityTypeBuilder<StudentSubject> builder)
        {
            builder.HasKey(ss => new { ss.StudentId, ss.SubjectId });

            builder
                .HasOne(ss => ss.Student)
                .WithMany(s => s.StudentSubjects)
                .HasForeignKey(ss => ss.StudentId);

            builder
                .HasOne(ss => ss.Subject)
                .WithMany(j => j.StudentSubjects)
                .HasForeignKey(ss => ss.SubjectId);
        }
    }
}