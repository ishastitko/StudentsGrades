namespace StudentsGrades.Models
{
    /*
     * The most important class of the project
     * Each grade contains information about a grade itself,
     * Date, information about a student and a subject
     */
    public class Grade
    {
        public Guid GradeId { get; } = Guid.NewGuid();
        public int GradeGot {  get; set; }
        public DateTime DateTime { get; set; } = DateTime.UtcNow;

        // Id's for referencing to objects
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }

        public Student? Student { get; set; }
        public Subject? Subject { get; set; }

        public Grade(int gradeGot, Guid studentId, Guid subjectId)
        {
            GradeGot = gradeGot;
            StudentId = studentId;
            SubjectId = subjectId;
        }
    }
}
