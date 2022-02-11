using Lms.Daos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lms.Models
{
    public class TeacherSeed
    {
        public static void InitData(TeacherContext context)
        {
            Teacher teacher1 = new Teacher();
            teacher1.Id = 1;
            teacher1.Name = "Mildreth";
            Teacher teacher2 = new Teacher();
            teacher2.Id = 2;
            teacher2.Name = "Alfred";
            Teacher teacher3 = new Teacher();
            teacher3.Id = 3;
            teacher3.Name = "Joyce";
            Teacher teacher4 = new Teacher();
            teacher4.Id = 4;
            teacher4.Name = "Maurice";
            Teacher teacher5 = new Teacher();
            teacher5.Id = 5;
            teacher5.Name = "Patricia";

            context.Add(teacher1);
            context.Add(teacher2);
            context.Add(teacher3);
            context.Add(teacher4);
            context.Add(teacher5);

            context.SaveChanges();
        }
    }
}
