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

        public async Task CreateTeacher(TeacherPost newTeacher)
        {
            var query = $"INSERT INTO Teacher(Name) VALUES('{newTeacher.Name}')";
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

    }
}
