using System;
using System.ComponentModel.DataAnnotations;

namespace Lms.Models
{
    public class CourseResponseForEnrollmentsDto
    {
        [Key]
        [Required]
        public Guid CourseId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Subject { get; set; }

        public Guid? TeacherId { get; set; }

    }
}
