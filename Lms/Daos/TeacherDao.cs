using Dapper;
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

        public async Task CreateTeacher(TeacherCreateDto newTeacher)
        {
            var query = $"INSERT INTO Teacher(FirstName, MiddleIniital, LastName, Email) VALUES('{newTeacher.FirstName}','{newTeacher.MiddleInitial}','{newTeacher.LastName}','{newTeacher.Email}'";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }
        public async Task<IEnumerable<Teacher>> GetAllTeachers()
        {
            var query = "SELECT * FROM Teacher";
            using (var connection = _context.CreateConnection())
            {
                var teachers = await connection.QueryAsync<Teacher>(query);

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

        public async Task DeleteTeacherById(Guid id)
        {
            var query = $"DELETE FROM Teacher WHERE Id = '{id}'";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }

        
        public async Task UpdateTeacherById(Teacher updateRequest)
        {
            var query = $"UPDATE Teacher SET " +
                $"FirstName ='{updateRequest.FirstName}'," +
                $"MiddleInitial ='{updateRequest.MiddleInitial}', " +
                $"LastName ='{updateRequest.LastName}', " +
                $"Email ='{updateRequest.Email}' " +
                $"WHERE Id = '{updateRequest.Id}'";

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }

    }
}
