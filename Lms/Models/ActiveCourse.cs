using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lms.Models
{
    public class ActiveCourse
    {
        [Key]
        [Display(Name = "CourseId")]
        public int CourseId { get; set; }

        [Display(Name = "StudentId")]
        public int StudentId { get; set; }

        [Display(Name = "SessionStartTime")]
        DateTime SessionStart { get; set; }

        [Display(Name = "SessionEndTime")]
        DateTime SessionEnd { get; set; }
    }
}
