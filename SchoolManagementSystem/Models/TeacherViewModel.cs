using SchoolManagementSystem.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class TeacherViewModel : Teacher
    {
        public string Name => $"{User?.FirstName} {User?.LastName}";

        // Este campo serve apenas para o upload de imagem, não vai para a base de dados
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
