using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lms.Models
{
    public class EnrollmentPost
    {
        [Required]
        [ForeignKey("Student")]
        [Display(Name = "StudentId")]
        public int StudentId { get; set; }

        [Required]
        [ForeignKey("Course")]
        [Display(Name = "CourseId")]
        public int CourseId { get; set; }

        [Display(Name = "Active")]
        public bool Active { get; set; }
    }
}
