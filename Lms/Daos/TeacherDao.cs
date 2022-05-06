using Dapper;
using Lms.Dtos.Request;
using Lms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lms.Daos
{
    public class TeacherDao
    {
        private readonly DapperContext _context;

        public TeacherDao(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Teacher>> GetStudentsWithOptionalParams(TeacherRequestForParams teacherParams)
        {

            var query = "SELECT TOP (25) * FROM Teacher";
            var conditions = new List<string>();

            if (teacherParams.FirstName != null)
            {
                conditions.Add("FirstName=@FirstName");
            }

            if (teacherParams.LastName != null)
            {
                conditions.Add("LastName=@LastName");
            }

            if (conditions.Any())
            {
                query += " WHERE " + string.Join(" AND ", conditions);
            }

            var parameters = new
            {
                FirstName = teacherParams.FirstName,
                LastName = teacherParams.LastName
            };

            using (var connection = _context.CreateConnection())
            {
                var teachers = await connection.QueryAsync<Teacher>(query, parameters);

                if (teachers.Count() == 0)
                {
                    return null;
                }

                return teachers.ToList();
            }
        }

        public async Task<Teacher> GetTeacherById(Guid id)
        {
            var query = $"SELECT * FROM Teacher WHERE Id = '{id}'";
            using (var connection = _context.CreateConnection())
            {
                var teacher = await connection.QueryFirstOrDefaultAsync<Teacher>(query);

                return teacher;
            }
        }
        public async Task CreateTeacher(TeacherRequestDto newTeacher)
        {
            var query = $"INSERT INTO Teacher(FirstName, MiddleInitial, LastName, Email) OUTPUT INSERTED.Id VALUES('{newTeacher.FirstName}','{newTeacher.MiddleInitial}','{newTeacher.LastName}','{newTeacher.Email}')";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }

        public async Task DeleteTeacherById(Guid id)
        {
            var query = $"DELETE FROM Teacher WHERE Id = '{id}'";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }

        public async Task UpdateTeacherById(Guid id, TeacherRequestDto updateRequest)
        {
            var query = $"UPDATE Teacher SET " +
                $"FirstName ='{updateRequest.FirstName}'," +
                $"MiddleInitial ='{updateRequest.MiddleInitial}', " +
                $"LastName ='{updateRequest.LastName}', " +
                $"Email ='{updateRequest.Email}' " +
                $"WHERE Id = '{id}'";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }
    }
}
