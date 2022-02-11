using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lms.Models
{
    public class Student
    {
        [Key]
        [Required]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Course> Courses { get; set; }

        //[NotMapped]
        //public virtual List<Course> ActiveCourseIds { get; set; }

        //[NotMapped]
        //public virtual List<Course> InactiveCourseIds { get; set; }

    }
}
