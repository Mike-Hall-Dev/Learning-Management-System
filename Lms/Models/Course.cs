using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lms.Models
{
    public class Course
    {
        [Key]
        [Required]
        [Display(Name = "ID")]
        public Guid Id { get; set; }

        [Required]
        [Display(Name="Name")]
        public string Name { get; set; }

        [Display(Name="Subject")]
        public string Subject { get; set; }

        [Display(Name = "Teacher")]
        public Guid TeacherId { get; set; }

        [Display(Name = "Start Time")]
        public DateTime? StartTime { get; set; }

        [Display(Name = "End Time")]
        public DateTime? EndTime { get; set; }

        [Display(Name = "Room")]
        public string Room { get; set; }
    }
}
