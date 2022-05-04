using System;
using System.ComponentModel.DataAnnotations;

namespace Lms.Dtos
{
    public class CourseResponseDto
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Subject { get; set; }

        public Guid? TeacherId { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string Room { get; set; }
    }
}
