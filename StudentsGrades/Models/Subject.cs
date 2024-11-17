using System.Text.Json.Serialization;

namespace StudentsGrades.Models
{
    public class Subject
    {
        public Guid SubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;

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
