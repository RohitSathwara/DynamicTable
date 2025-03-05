using System.ComponentModel.DataAnnotations;

namespace DT.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int TotalHours { get; set; } 
    }
}
