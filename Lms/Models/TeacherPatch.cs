﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lms.Models
{
    public class TeacherPatch
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        public List<Course> Courses { get; set; }
    }
}

