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

        public async Task CreateStudent(StudentCreateDto newStudent)
        {
            var query = $"INSERT INTO Student(FirstName, MiddleInitial, LastName, Email) " +
                $"VALUES('{newStudent.FirstName}','{newStudent.MiddleInitial}','{newStudent.LastName}','{newStudent.Email}')";
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

        public async Task<Student> GetStudentById(Guid id)
        {
            var query = $"SELECT * FROM Student WHERE Id = '{id}'";
            using (var connection = _context.CreateConnection())
            {
                var student = await connection.QueryFirstOrDefaultAsync<Student>(query);

                return student;
            }
        }

        public async Task DeleteStudentById(Guid id)
        {
            var query = $"DELETE FROM Student WHERE Id = '{id}'";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }

        public async Task UpdateStudentById(Guid id, Student updateRequest)
        {
            var query = $"UPDATE Student SET " +
                $"FirstName ='{updateRequest.FirstName}'," +
                $"MiddleInitial ='{updateRequest.MiddleInitial}', " +
                $"LastName ='{updateRequest.LastName}'," +
                $"Email ='{updateRequest.Email}' " +
                $"WHERE Id = '{id}'";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }
        
        // Bugged, cuts off the last column selected in the query, student.email gets returned and null
        public async Task<IEnumerable<CourseReadForEnrollmentsDto>> GetEnrollmentsById(Guid id, bool isActive, bool hasQueryParam)
        {
            var query = $"SELECT Course.Id as courseId, Course.[Name], Course.[Subject], Course.TeacherId FROM Course JOIN Enrollment ON Enrollment.CourseId=Course.Id JOIN Student on Student.Id=Enrollment.StudentId WHERE StudentId='{id}'";

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
                var result = await connection.QueryAsync<CourseReadForEnrollmentsDto>(query);

                if (result.Count() == 0)
                {
                    return null;
                }
                return result;
            }
        }

    }
}
