using System.ComponentModel.DataAnnotations;

namespace Lms.Models
{
    public class TeacherRequestDto
    {
        [Required]
        public string FirstName { get; set; }

        public char MiddleInitial { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Email { get; set; }

    }
}

