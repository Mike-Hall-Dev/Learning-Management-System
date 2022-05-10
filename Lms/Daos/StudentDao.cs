using Dapper;
using Lms.Dtos.Request;
using Lms.Models;
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

        public async Task<IEnumerable<Student>> GetStudentsWithOptionalParams(StudentRequestForParams studentParams)
        {

            var query = "SELECT TOP (25) * FROM Student";
            var conditions = new List<string>();

            if (studentParams.FirstName != null)
            {
                conditions.Add("FirstName=@FirstName");
            }

            if (studentParams.LastName != null)
            {
                conditions.Add("LastName=@LastName");
            }

            if (conditions.Any())
            {
                query += " WHERE " + string.Join(" AND ", conditions);
            }

            var parameters = new
            {
                FirstName = studentParams.FirstName,
                LastName = studentParams.LastName
            };

            using (var connection = _context.CreateConnection())
            {
                var students = await connection.QueryAsync<Student>(query, parameters);

                if (students.Count() == 0)
                {
                    return null;
                }

                return students.ToList();
            }
        }

        public async Task<Student> GetStudentById(Guid id)
        {
            var query = $"SELECT * FROM Student WHERE Id=@Id";

            var parameters = new
            {
                Id = id
            };

            using (var connection = _context.CreateConnection())
            {
                var student = await connection.QueryFirstOrDefaultAsync<Student>(query, parameters);

                return student;
            }
        }
        public async Task<IEnumerable<Course>> GetEnrollmentsById(Guid id, bool isActive, bool hasQueryParam)
        {
            var query = $"SELECT Course.Id, Course.[Name], Course.[Subject], Course.TeacherId FROM Course JOIN Enrollment ON Enrollment.CourseId=Course.Id JOIN Student on Student.Id=Enrollment.StudentId WHERE StudentId=@Id ";

            var parameters = new
            {
                Id = id
            };

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
                var result = await connection.QueryAsync<Course>(query, parameters);

                if (result.Count() == 0)
                {
                    return null;
                }
                return result;
            }
        }

        public async Task<Student> CreateStudent(StudentRequestDto newStudent)
        {
            var query = $"INSERT INTO Student(FirstName, MiddleInitial, LastName, Email)" +
                $" OUTPUT INSERTED.Id VALUES(@FirstName, @MiddleInitial, @LastName, @Email)";

            var parameters = new
            {
                FirstName = newStudent.FirstName,
                MiddleInitial = newStudent.MiddleInitial,
                LastName = newStudent.LastName,
                Email = newStudent.Email
            };

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<Guid>(query, parameters);

                var createdStudent = new Student
                {
                    Id = id,
                    FirstName = newStudent.FirstName,
                    MiddleInitial = newStudent.MiddleInitial,
                    LastName = newStudent.LastName,
                    Email = newStudent.Email
                };

                return createdStudent;
            }
        }

        public async Task DeleteStudentById(Guid id)
        {
            var query = $"DELETE FROM Student WHERE Id=@Id";

            var parameters = new
            {
                Id = id
            };

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task UpdateStudentById(Guid id, StudentRequestDto updateRequest)
        {
            var query = $"UPDATE Student SET FirstName=@FirstName, MiddleInitial=@MiddleInitial, LastName=@LastName, Email=@Email WHERE Id=@Id";

            var parameters = new
            {
                FirstName = updateRequest.FirstName,
                MiddleInitial = updateRequest.MiddleInitial,
                LastName = updateRequest.LastName,
                Email = updateRequest.Email,
                Id = id
            };

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
