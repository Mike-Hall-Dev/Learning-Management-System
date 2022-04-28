using Dapper;
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

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            var query = "SELECT * FROM Course";
            using (var connection = _context.CreateConnection())
            {
                var courses = await connection.QueryAsync<Course>(query);

                return courses.ToList();
            }
        }

        public async Task<Course> GetCourseById(Guid id)
        {
            var query = $"SELECT * FROM Course WHERE Id = '{id}'";
            using (var connection = _context.CreateConnection())
            {
                var courses = await connection.QueryFirstOrDefaultAsync<Course>(query);

                return courses;
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

        public async Task DeleteCourseById(Guid id)
        {
            var query = $"DELETE FROM Course WHERE Id = '{id}'";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }

        public async Task UpdateCourseById(Guid id, Course updateRequest)
        {
            var query = $"UPDATE Course SET " +
                $"Name ='{updateRequest.Name}'," +
                $"Subject = '{updateRequest.Subject}', " +
                $"TeacherId = '{updateRequest.TeacherId}', " +
                $"StartTime ='{updateRequest.StartTime}', " +
                $"EndTime ='{updateRequest.EndTime}', " +
                $"Room ='{updateRequest.Room}'" +
                $"WHERE Id = '{id}'";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }

        public async Task<IEnumerable<Student>> GetActiveClassRosterById(Guid id)
        {
            var query = $"SELECT  Student.Id, Student.FirstName, Student.MiddleInitial, Student.LastName, Student.Email FROM Course JOIN Enrollment ON Enrollment.CourseId=Course.Id JOIN Student ON Student.Id=Enrollment.StudentId WHERE Course.Id='{id}' AND Active=1";
            using (var connection = _context.CreateConnection())
            {
                var roster = await connection.QueryAsync<Student>(query);

                if (roster.Count() == 0)
                {
                    return null;
                }
                return roster.ToList();
            }
        }
    }
}
