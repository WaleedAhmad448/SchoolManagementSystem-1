namespace SchoolManagementSystem.Data.Entities
{
    public class CourseSubject // Tabela de junção entre Curso e Disciplina
    {
        public int CourseId { get; set; }
        public Course Course { get; set; } // Navegação

        public int SubjectId { get; set; }
        public Subject Subject { get; set; } // Navegação
    }
}
