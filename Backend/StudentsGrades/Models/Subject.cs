using System.Text.Json.Serialization;

namespace StudentsGrades.Models
{
    public class Subject
    {
        public Guid SubjectId { get; } = Guid.NewGuid();
        public string SubjectName { get; set; } = string.Empty;

        // JsonIgnore helps not sending unnecessary data to frontend
        // In that case we don't need a list of all Students and Grades of a Subject
        [JsonIgnore]
        public List<Grade> Grades { get; set; } = [];
        [JsonIgnore]
        public List<StudentSubject> StudentSubjects { get; set; } = [];

        public Subject(string subjectName)
        {
            SubjectName = subjectName;
        }
    }
}
