using Dapper;
using Lms.Dtos;
using Lms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lms.Daos
{
    public class CourseDao
    {
        private readonly DapperContext _context;

        public CourseDao(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetCoursesWithOptionalParams(CourseRequestForParamsDto courseParams)
        {
            var query = "SELECT TOP (25) * FROM Course";
            var conditions = new List<string>();

            if (courseParams.Name != null)
            {
                conditions.Add("Name=@Name");
            }

            if (courseParams.Subject != null)
            {
                conditions.Add("Subject=@Subject");
            }

            if (conditions.Any())
            {
                query += " WHERE " + string.Join(" AND ", conditions);
            }         

            var parameters = new
            {
                Name = courseParams.Name,
                Subject = courseParams.Subject
            };

            using (var connection = _context.CreateConnection())
            {
                var courses = await connection.QueryAsync<Course>(query, parameters);

                if (courses.Count() == 0)
                {
                    return null;
                }

                return courses.ToList();
            }
        }

        public async Task<Course> GetCourseById(Guid id)
        {
            var query = $"SELECT * FROM Course WHERE Id=@Id";

            var parameters = new
            {
                Id = id
            };

            using (var connection = _context.CreateConnection())
            {
                var courses = await connection.QueryFirstOrDefaultAsync<Course>(query, parameters);

                return courses;
            }
        }
        public async Task<IEnumerable<Student>> GetRosterByCourseId(Guid id)
        {
            var query = $"SELECT  Student.Id, Student.FirstName, Student.MiddleInitial, Student.LastName, Student.Email FROM Course " +
                $"JOIN Enrollment ON Enrollment.CourseId=Course.Id JOIN Student ON Student.Id=Enrollment.StudentId WHERE Course.Id=@Id AND Active=1";
            
            var parameters = new
            {
                Id = id
            };

            using (var connection = _context.CreateConnection())
            {
                var roster = await connection.QueryAsync<Student>(query, parameters);

                if (roster.Count() == 0)
                {
                    return null;
                }

                return roster.ToList();
            }
        }

        public async Task<Course> CreateCourse(CourseRequestDto newCourse)
        {
            var query = "INSERT INTO Course(Name, Subject, TeacherId, StartTime, EndTime, Room)" +
                " OUTPUT INSERTED.Id VALUES(@Name, @Subject, @TeacherId, @StartTime, @EndTime, @Room)";

            var parameters = new
            {
                Name = newCourse.Name,
                Subject = newCourse.Subject,
                TeacherId = newCourse.TeacherId,
                StartTime = newCourse.StartTime,
                EndTime = newCourse.EndTime,
                Room = newCourse.Room
            };

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<Guid>(query, parameters);

                var createdCourse = new Course
                {
                    Id = id,
                    Name = newCourse.Name,
                    Subject = newCourse.Subject,
                    TeacherId = newCourse.TeacherId,
                    StartTime = newCourse.StartTime,
                    EndTime = newCourse.EndTime,
                    Room = newCourse.Room
                };

                return createdCourse;
            }
        }

        public async Task CreateEnrollment(Guid courseId, Guid studentId)
        {
            var query = $"INSERT INTO Enrollment (StudentId, CourseId, Active) VALUES(@StudentId, @CourseId,1)";

            var parameters = new
            {
                CourseId = courseId,
                StudentId = studentId               
            };

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query,parameters);
            }
        }

        public async Task UnenrollByCourseId(Guid courseId, Guid studentId)
        {
            var query = $"UPDATE Enrollment SET Active='0' WHERE CourseId=@CourseId AND StudentId=@StudentId";

            var parameters = new
            {
                CourseId = courseId,
                StudentId = studentId
            };

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<Enrollment> CheckForExistingActiveEnrollment(Guid courseId, Guid studentId)
        {
            var query = $"SELECT * FROM Enrollment WHERE CourseId=@CourseId AND StudentId=@StudentId AND Active = 1";

            var parameters = new
            {
                CourseId = courseId,
                StudentId = studentId
            };

            using (var connection = _context.CreateConnection())
            {
                var enrollment = await connection.QueryFirstOrDefaultAsync<Enrollment>(query, parameters);

                return enrollment;
            }
        }

        public async Task DeleteCourseById(Guid id)
        {
            var query = $"DELETE FROM Course WHERE Id=@Id";

            var parameters = new
            {
                Id = id
            };

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task UpdateCourseById(Guid id, CourseRequestDto updateRequest)
        {

            var query = $"UPDATE Course SET Name=@Name, Subject=@Subject, TeacherId=@TeacherId, StartTime=@StartTime, EndTime=@EndTime, Room=@Room WHERE Id=@Id";

            var parameters = new
            {
                Name = updateRequest.Name,
                Subject = updateRequest.Subject,
                TeacherId = updateRequest.TeacherId,
                StartTime = updateRequest.StartTime,
                EndTime = updateRequest.EndTime,
                Room = updateRequest.Room,
                Id = id
            };

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
