using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lms.Models
{
    public class CourseCreateDto
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
