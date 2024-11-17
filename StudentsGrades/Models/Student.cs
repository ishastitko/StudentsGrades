using System.Text.Json.Serialization;

namespace StudentsGrades.Models
{
    public class Student
    {
        public Guid StudentId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        [JsonIgnore]
        public List<Grade> Grades { get; set; } = [];
        [JsonIgnore]
        public List<Subject> Subjects { get; set; } = [];

        public Student() { }

        public Student(string firstName, string lastName)
        {
            StudentId = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
