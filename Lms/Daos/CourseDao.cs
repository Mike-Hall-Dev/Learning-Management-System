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

        public async Task CreateCourse(CoursePost newCourse)
        {
            var query = $"INSERT INTO Course(Name, TeacherId) VALUES('{newCourse.Name}', '{newCourse.TeacherId}')";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
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

        public async Task<Course> GetCourseById(int id)
        {
            var query = $"SELECT * FROM Course WHERE Id = {id}";
            using (var connection = _context.CreateConnection())
            {
                var courses = await connection.QueryFirstOrDefaultAsync<Course>(query);

                return courses;
            }
        }


        public async Task DeleteCourseById(int id)
        {
            var query = $"DELETE FROM Course WHERE Id = {id}";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }

        public async Task UpdateCourseById(Course updateRequest)
        {
            var query = $"UPDATE Course SET Name ='{updateRequest.Name}', TeacherId = '{updateRequest.TeacherId}'  WHERE Id = '{updateRequest.Id}'";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }

        //public async Task<IEnumerable<Student>> GetClassRosterById(int id)
        //{
        //    var query = $"SELECT  Student.Id, Student.[Name] FROM Course JOIN Enrollment ON Enrollment.CourseId=Course.Id JOIN Student ON Student.Id=Enrollment.StudentId WHERE Course.Id={id}";
        //    using (var connection = _context.CreateConnection())
        //    {
        //        var roster = await connection.QueryAsync<Student>(query);

        //        return roster.ToList();
        //    }
        //}
    }
}
