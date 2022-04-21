using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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

