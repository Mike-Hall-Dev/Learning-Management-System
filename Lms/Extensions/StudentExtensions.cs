using Lms.Dtos;
using Lms.Models;
using System.Collections.Generic;

namespace Lms.Extensions
{
    public static class StudentExtensions
    {
        public static StudentResponseDto ConvertToDto(this Student student)
        {
            if (student != null)
            {
                return new StudentResponseDto
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

        public static Student ConvertToModel(this StudentRequestDto studentRequestDto)
        {
            if (studentRequestDto != null)
            {
                return new Student
                {
                    FirstName = studentRequestDto.FirstName,
                    MiddleInitial = studentRequestDto.MiddleInitial,
                    LastName = studentRequestDto.LastName,
                    Email = studentRequestDto.Email
                };
            }
            return null;
        }

        public static List<StudentResponseDto> ConvertToDtoList(this IEnumerable<Student> studentList)
        {
            if (studentList != null)
            {
                var convertedList = new List<StudentResponseDto>();
                foreach (Student student in studentList)
                {
                    var convertedStudent = student.ConvertToDto();
                    convertedList.Add(convertedStudent);
                }

                return convertedList;
            };
            return null;
        }
    }
}
