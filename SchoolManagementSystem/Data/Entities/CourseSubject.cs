namespace SchoolManagementSystem.Data.Entities
{
    public class CourseSubject // Join table between Course and Discipline
    {
        public int CourseId { get; set; }
        public Course Course { get; set; } // Navigation

        public int SubjectId { get; set; }
        public Subject Subject { get; set; } // Navigation
    }
}
