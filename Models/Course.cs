using System.ComponentModel.DataAnnotations;
namespace SRegister.Models
{
    public class Course
    {
    [Key]
        public int CourseId { get; set; }
        [Required]
        public string CourseName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;


    }
}