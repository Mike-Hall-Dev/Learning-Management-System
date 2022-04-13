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
    public class StudentDao
    {
        private readonly DapperContext _context;

        public StudentDao(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateStudent(StudentPost newStudent)
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
        
        public async Task<IEnumerable<Student>> GetEnrollmentsById(int id, bool isActive, bool hasQueryParam)
        {
            var query = $"SELECT Course.Name, Course.Id, Course.TeacherId, Student.Id as Student FROM Course JOIN Enrollment ON Enrollment.CourseId=Course.Id JOIN Student on Student.Id=Enrollment.StudentId WHERE StudentId='{id}'";

            if (isActive)
            {
                query += "AND Enrollment.Active=1";
            }
            if (!isActive && hasQueryParam)
            {
                query += "AND Enrollment.Active=0";
            }

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<Student>(query);

                if (result.Count() == 0)
                {
                    return null;
                }
                return result;
            }
        }

    }
}
