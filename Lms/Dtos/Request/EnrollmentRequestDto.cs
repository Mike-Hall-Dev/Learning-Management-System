using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lms.Models
{
    public class EnrollmentRequestDto
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
