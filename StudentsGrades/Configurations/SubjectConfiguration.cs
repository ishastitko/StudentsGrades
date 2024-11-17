using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentsGrades.Models;

namespace StudentsGrades.Configurations
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasKey(a => a.SubjectId);

            builder
                .HasMany(j => j.Students)
                .WithMany(s => s.Subjects)
                .UsingEntity(t => t.ToTable("StudentsSubject"));

            builder
                .HasMany(j => j.Grades)
                .WithOne(g => g.Subject);
        }
    }
}
