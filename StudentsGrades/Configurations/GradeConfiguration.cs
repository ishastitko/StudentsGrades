using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentsGrades.Models;

namespace StudentsGrades.Configurations
{
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.HasKey(g => g.GradeId);

            builder
                .HasOne(g => g.Student)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.StudentId);

            builder
                .HasOne(g => g.Subject)
                .WithMany(j => j.Grades)
                .HasForeignKey(g => g.SubjectId);
        }
    }
}
