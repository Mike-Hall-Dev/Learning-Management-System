using Lms.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lms.Daos
{
        public class CourseContext : DbContext
        {
            public CourseContext(DbContextOptions<CourseContext> options) : base(options) { }

            public DbSet<Course> Courses { get; set; }
        }
}
