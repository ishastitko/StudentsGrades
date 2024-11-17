namespace StudentsGrades.Models
{
    public class Subject
    {
        public Guid SubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;

        public List<Grade> Grades { get; set; } = new List<Grade>();
        public List<Student> Students { get; set;} = new List<Student>();

        public Subject() { }

        public Subject(string subjectName)
        {
            SubjectId = Guid.NewGuid();
            SubjectName = subjectName;
        }
    }
}
