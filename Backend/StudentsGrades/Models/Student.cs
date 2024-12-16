using System.Text.Json.Serialization;

namespace StudentsGrades.Models
{
    public class Student
    {
        public Guid StudentId { get; } = Guid.NewGuid();
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        // JsonIgnore helps not sending unnecessary data to frontend
        // In that case we don't need a list of all Grades and Subjects of a Student
        [JsonIgnore]
        public List<Grade> Grades { get; set; } = [];
        [JsonIgnore]
        public List<StudentSubject> StudentSubjects { get; set; } = [];

        public Student(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
