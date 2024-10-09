using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data.Entities;

public class SchoolDbContext : IdentityDbContext<User>
{
    public DbSet<User> Users { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<SchoolClass> SchoolClasses { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Attendance> Attendance { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Alert> Alerts { get; set; }
    public DbSet<TeacherSubject> TeacherSubjects { get; set; }
    public DbSet<TeacherSchoolClass> TeacherSchoolClasses { get; set; } // Entidade de junção

    public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //// Relacionamentos para SchoolClass
        //modelBuilder.Entity<Subject>()
        //    .HasOne(s => s.SchoolClass)
        //    .WithMany(sc => sc.Subjects)
        //    .HasForeignKey(s => s.SchoolClassId)
        //    .OnDelete(DeleteBehavior.Restrict); // Restringe a exclusão

        modelBuilder.Entity<Student>()
            .HasOne(s => s.SchoolClass)
            .WithMany(sc => sc.Students)
            .HasForeignKey(s => s.SchoolClassId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento entre Student e User
        modelBuilder.Entity<Student>()
            .HasOne(s => s.User)
            .WithMany()
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento muitos-para-muitos entre Teacher e Subject
        modelBuilder.Entity<TeacherSubject>()
            .HasKey(ts => new { ts.TeacherId, ts.SubjectId }); // Chave composta

        modelBuilder.Entity<TeacherSubject>()
            .HasOne(ts => ts.Teacher)
            .WithMany(t => t.TeacherSubjects)
            .HasForeignKey(ts => ts.TeacherId)
            .OnDelete(DeleteBehavior.Cascade); // Exclui automaticamente as associações ao deletar um Teacher

        modelBuilder.Entity<TeacherSubject>()
            .HasOne(ts => ts.Subject)
            .WithMany(s => s.TeacherSubjects)
            .HasForeignKey(ts => ts.SubjectId)
            .OnDelete(DeleteBehavior.Restrict); // Restrição na exclusão da disciplina

        // Relacionamento entre Course e Subject usando a tabela de junção
        modelBuilder.Entity<CourseSubject>()
            .HasKey(cs => new { cs.CourseId, cs.SubjectId }); // Chave composta

        modelBuilder.Entity<CourseSubject>()
            .HasOne(cs => cs.Course)
            .WithMany(c => c.CourseSubjects)
            .HasForeignKey(cs => cs.CourseId);

        modelBuilder.Entity<CourseSubject>()
            .HasOne(cs => cs.Subject)
            .WithMany(s => s.CourseSubjects)
            .HasForeignKey(cs => cs.SubjectId);


        // Relacionamento entre Teacher e SchoolClass
        modelBuilder.Entity<TeacherSchoolClass>()
            .HasKey(tsc => new { tsc.TeacherId, tsc.SchoolClassId }); // Chave composta

        modelBuilder.Entity<TeacherSchoolClass>()
            .HasOne(tsc => tsc.Teacher)
            .WithMany(t => t.TeacherSchoolClasses)
            .HasForeignKey(tsc => tsc.TeacherId)
            .OnDelete(DeleteBehavior.Cascade); // Exclui automaticamente as associações ao deletar um Teacher

        modelBuilder.Entity<TeacherSchoolClass>()
            .HasOne(tsc => tsc.SchoolClass)
            .WithMany(sc => sc.TeacherSchoolClasses)
            .HasForeignKey(tsc => tsc.SchoolClassId)
            .OnDelete(DeleteBehavior.Restrict); // Restrição na exclusão da turma
    }
}
