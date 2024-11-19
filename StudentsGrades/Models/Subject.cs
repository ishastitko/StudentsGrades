using System.Text.Json.Serialization;

namespace StudentsGrades.Models
{
    public class Subject
    {
        public Guid SubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;

        // JsonIgnore helps not sending unnecessary data to frontend
        // In that case we don't need a list of all Students and Grades of a Subject
        [JsonIgnore]
        public List<Grade> Grades { get; set; } = [];
        [JsonIgnore]
        public List<Student> Students { get; set; } = [];

        public Subject() { }

        public Subject(string subjectName)
        {
            SubjectId = Guid.NewGuid();
            SubjectName = subjectName;
        }
    }
}
