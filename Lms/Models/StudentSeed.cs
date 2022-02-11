using Lms.Daos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lms.Models
{
    public static class StudentSeed
    {
        public static void InitData(StudentContext context)
        {
            Student student1 = new Student();
            student1.Id = 1;
            student1.Name = "Phil";
            Student student2 = new Student();
            student2.Id = 2;
            student2.Name = "Tyler";
            Student student3 = new Student();
            student3.Id = 3;
            student3.Name = "Ross";
            Student student4 = new Student();
            student4.Id = 4;
            student4.Name = "Jon";
            Student student5 = new Student();
            student5.Id = 5;
            student5.Name = "Mike";

            context.Add(student1);
            context.Add(student2);
            context.Add(student3);
            context.Add(student4);
            context.Add(student5);

            context.SaveChanges();
        }
    }
}
