using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lms.Models
{
    public class EnrollmentPatch
    {
        [Display(Name = "Active")]
        public bool Active { get; set; }
    }
}
