using System.Text.Json.Serialization;

namespace StudentsGrades.Models
{
    /*
     * Each subject has unique ID and subject name
     * A subject can have many grades and students that have these grades
     */
    public class Subject
    {
        public Guid SubjectId { get; } = Guid.NewGuid();
        public string SubjectName { get; set; } = string.Empty;

        /*
         * JsonIgnore helps not sending unnecessary data to frontend
         * In this case we don't need a list of all Students and Grades of a Subject
         * It can lead to infinite cycle
         */
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
