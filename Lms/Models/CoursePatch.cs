using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lms.Models
{
    public class CoursePatch
    {
        [Display(Name = "name")]
        public string Name { get; set; }

        public int TeacherId { get; set; }

        [Required]
        public bool Active { get; set; }

        //public DateTime starttime = new DateTime();
    }
}
