using SchoolManagementSystem.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagementSystem.Models
{
    public class StudentAttendanceViewModel
    {
        public Student Student { get; set; } // Includes the Student entity
        public List<Attendance> Attendances { get; set; } // List of Attendance records

        public int TotalClasses { get; set; } // Nova propriedade para armazenar o total de aulas

        // Property that calculates the total absences for the student
        public int TotalAbsences => Attendances?.Count(a => a.SubjectId != 0) ?? 0; // Total de faltas

        // Property that determines the overall attendance status based on total absences
        public string OverallAttendanceStatus()
        {
            double allowedAbsences = TotalClasses * 0.3; // 30% do total de aulas
            return TotalAbsences > allowedAbsences ? "Failed" : "Passed"; // Se ultrapassar o limite, reprovado
        }
    }

}
