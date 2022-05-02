using Lms.Dtos;
using Lms.Models;
using System.Collections.Generic;

namespace Lms.Extensions
{
    public static class TeacherExtensions
    {
        public static TeacherResponseDto ConvertToDto(this Teacher teacher)
        {
            if (teacher != null)
            {
                return new TeacherResponseDto
                {
                    Id = teacher.Id,
                    FirstName = teacher.FirstName,
                    MiddleInitial = teacher.MiddleInitial,
                    LastName = teacher.LastName,
                    Email = teacher.Email
                };
            }
            return null;
        }

        public static Teacher ConvertToModel(this TeacherRequestDto teacherRequestDto)
        {
            if (teacherRequestDto != null)
            {
                return new Teacher
                {
                    FirstName = teacherRequestDto.FirstName,
                    MiddleInitial = teacherRequestDto.MiddleInitial,
                    LastName = teacherRequestDto.LastName,
                    Email = teacherRequestDto.Email
                };
            }
            return null;
        }

        public static List<TeacherResponseDto> ConvertToDtoList(this IEnumerable<Teacher> teacherList)
        {
            if (teacherList != null)
            {
                var convertedList = new List<TeacherResponseDto>();
                foreach (Teacher teacher in teacherList)
                {
                    var convertedTeacher = teacher.ConvertToDto();
                    convertedList.Add(convertedTeacher);
                }

                return convertedList;
            };
            return null;
        }
    }
}
