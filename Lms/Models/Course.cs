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
        public int Id { get; set; }

        [Required]
        [Display(Name="name")]
        public string Name { get; set; }

        [Display(Name = "teacher")]
        public string TeacherId { get; set; }

        public bool Active { get; set; }

        //public DateTime starttime = new DateTime();
    }
}
