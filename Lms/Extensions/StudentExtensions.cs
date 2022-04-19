using Lms.Dtos;
using Lms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lms.Extensions
{
    public static class StudentExtensions
    {
        public static StudentReadDto ConvertToDto(this Student student)
        {
            if (student != null)
            {
                return new StudentReadDto
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    MiddleInitial = student.MiddleInitial,
                    LastName = student.LastName,
                    Email = student.Email
                };
            }

            return null;
        }
    }
}
