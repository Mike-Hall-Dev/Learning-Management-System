﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Lms.Models
{
    public class Student
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        public char MiddleInitial { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Email { get; set; }

    }
}
