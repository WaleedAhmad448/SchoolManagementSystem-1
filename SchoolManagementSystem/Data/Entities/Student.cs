using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Data.Entities
{
    public class Student : IEntity
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Display(Name = "Enrollment Date")]
        public DateTime? EnrollmentDate { get; set; }
        
        //Data formatada
        public string FormattedEnrollmentDate => EnrollmentDate?.ToString("dd/MM/yyyy");


        [Display(Name = "Status")]
        public StudentStatus Status { get; set; } = StudentStatus.Pending;

        public int? SchoolClassId { get; set; } 
        public SchoolClass SchoolClass { get; set; }

        [Display(Name = "Image")]
        public Guid ImageId { get; set; }

        public string ImageFullPath => ImageId == Guid.Empty
           ? $"https://schoolstorageaccount.blob.core.windows.net/images/noimage.png"
            : $"https://schoolstorageaccount.blob.core.windows.net/students/{ImageId}";
    }

    // Enum para padronizar o status do estudante
    public enum StudentStatus
    {
        Pending,
        Active,
        Inactive
    }
}
