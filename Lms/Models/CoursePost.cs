using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lms.Models
{
    public class CoursePost
    {
        [Display(Name = "name")]
        [Required]
        public string Name { get; set; }

        public string TeacherId { get; set; }


    }
}
