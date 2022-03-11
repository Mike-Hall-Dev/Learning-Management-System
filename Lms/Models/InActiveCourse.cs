using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lms.Models
{
    public class InactiveCourse
    {
        [Key]
        [Display(Name = "CourseId")]
        public int CourseId { get; set; }

        [Display(Name = "StudentId")]
        public int StudentId { get; set; }
    }
}
