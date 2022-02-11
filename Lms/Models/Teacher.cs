using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lms.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<Course> CourseIds { get; set; }
    }
}
