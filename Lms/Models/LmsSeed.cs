using Lms.Daos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lms.Models
{
    public class LmsSeed
    {
        public static void InitData(LmsContext context)
        {
            Student student1 = new Student() { Id = 1, Name = "Ricky"};
            Student student2 = new Student() { Id = 2, Name = "Sticky"};

            Teacher teacher1 = new Teacher() { Id = 1, Name = "Stacey" };
            Teacher teacher2 = new Teacher() { Id = 2, Name = "Blacey" };

            Course course1 = new Course() { Id = 1, Name = "Learning 101", TeacherId = 1 };
            Course course2 = new Course() { Id = 2, Name = "Learning 202", TeacherId = 1, Active = true};

            context.Add(student1);
            context.Add(student2);
            context.Add(teacher1);
            context.Add(teacher2);
            context.Add(course1);
            context.Add(course2);
            context.SaveChanges();

        }
    }
}
