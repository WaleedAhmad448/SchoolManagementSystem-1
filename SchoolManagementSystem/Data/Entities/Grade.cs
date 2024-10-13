using SchoolManagementSystem.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Grade : IEntity
{
    public int Id { get; set; }

    public int? StudentId { get; set; } 
    public Student? Student { get; set; }

    public int? SubjectId { get; set; }
    public Subject? Subject { get; set; }

    [Range(0, 100)]
    [Column(TypeName = "decimal(5, 2)")]
    public decimal Value { get; set; }

    [MaxLength(20)]
    public string Status { get; set; }

    [Display(Name = "Date Recorded")]
    public DateTime DateRecorded { get; set; }
}
