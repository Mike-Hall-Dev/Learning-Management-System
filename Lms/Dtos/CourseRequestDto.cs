using System;
using System.ComponentModel.DataAnnotations;

namespace Lms.Models
{
    public class CourseRequestDto
    {
        [Required]
        public string Name { get; set; }

        public string Subject { get; set; }

        public Guid? TeacherId { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string Room { get; set; }

    }
}
