using Lms.Dtos;
using Lms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lms.Extensions
{
    public static class CourseExtensions
    {
        public static CourseResponseDto ConvertToDto(this Course course)
        {
            if (course != null)
            {
                return new CourseResponseDto
                {
                    Id = course.Id,
                    Name = course.Name,
                    Subject = course.Subject,
                    TeacherId = course.TeacherId,
                    StartTime = course.StartTime,
                    EndTime = course.EndTime,
                    Room = course.Room
                };
            }
            return null;
        }

        public static CourseResponseForEnrollmentsDto ConvertToDtoForEnrollments(this Course course)
        {
            if (course != null)
            {
                return new CourseResponseForEnrollmentsDto
                {
                    CourseId = course.Id,
                    Name = course.Name,
                    Subject = course.Subject,
                    TeacherId = course.TeacherId
                };
            }
            return null;
        }

        public static Course ConvertToModel(this CourseRequestDto courseRequestDto)
        {
            if (courseRequestDto != null)
            {
                return new Course
                {
                    Name = courseRequestDto.Name,
                    Subject = courseRequestDto.Subject,
                    TeacherId = courseRequestDto.TeacherId,
                    StartTime = courseRequestDto.StartTime,
                    EndTime = courseRequestDto.EndTime,
                    Room = courseRequestDto.Room
                };
            }
            return null;
        }

        public static List<CourseResponseDto> ConvertToDtoList(this IEnumerable<Course> courseList)
        {
            if (courseList != null)
            {
                var convertedList = new List<CourseResponseDto>();

                foreach (Course course in courseList)
                {
                    var convertedCourse = course.ConvertToDto();
                    convertedList.Add(convertedCourse);
                }

                return convertedList;
            };
            return null;
        }

        public static List<CourseResponseForEnrollmentsDto> ConvertToDtoListForEnrollments(this IEnumerable<Course> courseList)
        {
            if (courseList != null)
            {
                var convertedList = new List<CourseResponseForEnrollmentsDto>();

                foreach (Course course in courseList)
                {
                    var convertedCourse = course.ConvertToDtoForEnrollments();
                    convertedList.Add(convertedCourse);
                }

                return convertedList;
            };
            return null;
        }
    }
}
