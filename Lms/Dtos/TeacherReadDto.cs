using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lms.Dtos
{
    public class TeacherReadDto
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        public char MiddleInitial { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
