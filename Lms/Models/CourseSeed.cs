using Lms.Daos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lms.Models
{
    public class CourseSeed
    {
        public static void InitData(CourseContext context)
        {
            Course course1 = new Course();
            course1.Id = 1;
            course1.Name = "test1";
            course1.Active = true;
            Course course2 = new Course();
            course2.Id = 2;
            course2.Name = "test2";
            course2.Active = false;
            Course course3 = new Course();
            course3.Id = 3;
            course3.Name = "test3";
            course3.Active = true;
            Course course4 = new Course();
            course4.Id = 4;
            course4.Name = "test4";
            course4.Active = false;
            Course course5 = new Course();
            course5.Id = 5;
            course5.Name = "test5";
            course5.Active = true;

            context.Add(course1);
            context.Add(course2);
            context.Add(course3);
            context.Add(course4);
            context.Add(course5);

            context.SaveChanges();
        }
    }
}
