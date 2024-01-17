using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public class Assessment
    {
        public int AssessmentId { get; set; }
        public DateTime Date { get; set; }

        [Range(1, 5, ErrorMessage = "Оценка должна быть в диапазоне от 1 до 5")]
        public int AssessmentNumber { get; set; }
        public int StudentID { get; set; }
        public Student Student { get; set; }
    }
}
