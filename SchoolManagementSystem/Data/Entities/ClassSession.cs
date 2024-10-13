namespace SchoolManagementSystem.Data.Entities
{
    public class ClassSession : IEntity
    {
        public int Id { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public int SchoolClassId { get; set; }
        public SchoolClass SchoolClass { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

}
