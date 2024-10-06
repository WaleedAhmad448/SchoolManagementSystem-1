using Microsoft.AspNetCore.Mvc.ModelBinding;
using SchoolManagementSystem.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SchoolManagementSystem.Models
{
    public class TeacherViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pending User is required")]
        [Display(Name = "Pending User")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Academic Degree")]
        public AcademicDegree AcademicDegree { get; set; }

        public DateTime? HireDate { get; set; }

        public string FormattedHireDate => HireDate?.ToString("dd/MM/yyyy");

        // Coleção de IDs das turmas associadas ao professor
        public ICollection<int> SchoolClassIds { get; set; } = new List<int>();

        // Coleção de IDs das disciplinas que o professor leciona
        public ICollection<int> SubjectIds { get; set; } = new List<int>();

        // ID da imagem para a foto de perfil do professor
        public Guid ImageId { get; set; }

        [Display(Name = "Image")]
        public IFormFile? ImageFile { get; set; }

        public string ImageFullPath => ImageId == Guid.Empty
            ? "https://schoolstorageaccount.blob.core.windows.net/images/noimage.png"
            : $"https://schoolstorageaccount.blob.core.windows.net/teachers/{ImageId}";

        // Status do professor
        public TeacherStatus Status { get; set; } = TeacherStatus.Active;

        // Propriedade para armazenar os utilizadores pendentes
        public IEnumerable<User>? PendingUsers { get; set; }

        // Propriedade para armazenar as turmas associadas ao professor
        public IEnumerable<SchoolClass>? SchoolClasses { get; set; }

        // Propriedade para armazenar as disciplinas associadas ao professor
        public IEnumerable<Subject>? Subjects { get; set; }
    }
}
