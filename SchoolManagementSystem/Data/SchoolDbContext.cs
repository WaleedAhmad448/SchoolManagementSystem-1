using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data.Entities;
using SchoolManagementSystem.Models;

public class SchoolDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<SchoolClass> SchoolClasses { get; set; }
    public DbSet<Teacher> Teachers { get; set; }

    public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Relacionamento entre SchoolClass e Subject (restrito para não apagar em cascata)
        modelBuilder.Entity<Subject>()
            .HasOne(s => s.SchoolClass)
            .WithMany(sc => sc.Subjects)
            .HasForeignKey(s => s.SchoolClassId)
            .OnDelete(DeleteBehavior.Restrict); // Restringe a exclusão


        // Relacionamento entre Student e SchoolClass (restrito para não apagar em cascata)
        modelBuilder.Entity<Student>()
            .HasOne(s => s.SchoolClass)
            .WithMany(sc => sc.Students)
            .HasForeignKey(s => s.SchoolClassId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento entre Student e User (sem exclusão em cascata)
        modelBuilder.Entity<Student>()
            .HasOne(s => s.User)
            .WithMany() // Nenhuma exclusão em cascata entre Aluno e Utilizador
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento entre Subject e Teacher
        modelBuilder.Entity<Subject>()
            .HasOne(s => s.Teacher)
            .WithMany(t => t.Subjects)
            .HasForeignKey(s => s.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento entre Course e Subject
        modelBuilder.Entity<Subject>()
            .HasOne(s => s.Course)
            .WithMany(c => c.Subjects)
            .HasForeignKey(s => s.CourseId)
            .OnDelete(DeleteBehavior.Restrict);
    }




}