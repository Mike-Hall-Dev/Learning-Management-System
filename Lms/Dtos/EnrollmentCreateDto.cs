using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lms.Models
{
    public class EnrollmentCreateDto
    {

        [Required]
        [ForeignKey("Student")]
        public Guid StudentId { get; set; }

        [Required]
        [ForeignKey("Course")]
        public Guid CourseId { get; set; }

        public bool Active { get; set; }
    }
}
