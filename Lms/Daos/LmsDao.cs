using Dapper;
using Lms.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lms.Daos
{
    public class LmsDao
    {
        private readonly DapperContext _context;

        public LmsDao(DapperContext context)
        {
            _context = context;
        }

        //Student Methods Start

        public async Task CreateStudent(Student newStudent)
        {
            var query = $"INSERT INTO Student(Name) VALUES('{newStudent.Name}')";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            var query = "SELECT * FROM Student";
            using (var connection = _context.CreateConnection())
            {
                var students = await connection.QueryAsync<Student>(query);

                return students.ToList();
            }
        }

        public async Task<Student> GetStudentById(int id)
        {
            var query = $"SELECT * FROM Student WHERE Id = {id}";
            using (var connection = _context.CreateConnection())
            {
                var student = await connection.QueryFirstOrDefaultAsync<Student>(query);

                return student;
            }
        }

        public async Task DeleteStudentById(int id)
        {
            var query = $"DELETE FROM Student WHERE Id = {id}";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }

        public async Task UpdateStudentById(Student updateRequest)
        {
            var query = $"UPDATE Student SET Name ='{updateRequest.Name}' WHERE Id = '{updateRequest.Id}'";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }

        //Student Methods End
        //Teacher Methods Start

        public async Task<IEnumerable<Teacher>> GetAllTeachers()
        {
            var query = "SELECT * FROM Teacher";
            using (var connection = _context.CreateConnection())
            {
                var teachers = await connection.QueryAsync<Teacher>(query);

                return teachers.ToList();
            }
        }

        public async Task<Teacher> GetTeacherById(int id)
        {
            var query = $"SELECT * FROM Teacher WHERE Id = {id}";
            using (var connection = _context.CreateConnection())
            {
                var teacher = await connection.QueryFirstOrDefaultAsync<Teacher>(query);

                return teacher;
            }
        }

        public async Task DeleteTeacherById(int id)
        {
            var query = $"DELETE FROM Teacher WHERE Id = {id}";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }

        public async Task UpdateTeacherById(Teacher updateRequest)
        {
            var query = $"UPDATE Teacher SET Name ='{updateRequest.Name}' WHERE Id = '{updateRequest.Id}'";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }

        //Teacher Methods End
        //Course Methods Start

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

    }
}
