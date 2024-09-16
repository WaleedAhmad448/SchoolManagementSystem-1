using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class StudentViewModel : Student
    {
        // Este campo serve apenas para o upload de imagem, não vai para a base de dados
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
