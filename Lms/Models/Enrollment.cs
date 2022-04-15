using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lms.Models
{
    public class Enrollment
    {
        [Key]
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("Student")]
        [Display(Name = "StudentId")]
        public Guid StudentId { get; set; }

        [Required]
        [ForeignKey("Course")]
        [Display(Name = "CourseId")]
        public Guid CourseId { get; set; }

        [Display(Name= "Active")]
        public bool Active { get; set; }

    }
}
