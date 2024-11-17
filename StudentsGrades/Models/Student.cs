namespace StudentsGrades.Models
{
    public class Student
    {
        public Guid StudentId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        
        public List<Grade> Grades { get; set; } = new List<Grade>();
        public List<Subject> Subjects { get; set; } = new List<Subject>();

        public Student() { }

        public Student(string firstName, string lastName)
        {
            StudentId = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
