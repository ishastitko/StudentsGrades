using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentsGrades.Models;

namespace StudentsGrades.Configurations
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasKey(j => j.SubjectId);

            builder
                .HasMany(j => j.StudentSubjects)
                .WithOne(ss => ss.Subject);

            builder
                .HasMany(j => j.Grades)
                .WithOne(g => g.Subject);
        }
    }
}
