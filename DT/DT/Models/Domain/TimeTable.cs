using System.ComponentModel.DataAnnotations;

namespace DT.Models
{
    public class TimeTable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int WorkingDays { get; set; }

        [Required]
        public int SubjectsPerDay { get; set; }

        public int TotalHours => WorkingDays * SubjectsPerDay;

        public List<Subject> Subjects { get; set; }
    }
}
