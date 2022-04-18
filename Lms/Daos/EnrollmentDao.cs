using Dapper;
using Lms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lms.Daos
{
    public class EnrollmentDao
    {
        private readonly DapperContext _enrollmentDao;

        public EnrollmentDao(DapperContext enrollmentDao)
        {
            _enrollmentDao = enrollmentDao;
        }

        public async Task CreateEnrollment(EnrollmentCreateDto newEnrollment)
        {
            var query = $"INSERT INTO Enrollment (StudentId, CourseId, Active) OUTPUT Inserted.ID VALUES('{newEnrollment.StudentId}','{newEnrollment.CourseId}','{newEnrollment.Active}')";
            using (var connection = _enrollmentDao.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }

        public async Task<Enrollment> GetEnrollmentById(Guid id)
        {
            var query = $"SELECT * FROM Enrollment WHERE Id = '{id}'";
            using (var connection = _enrollmentDao.CreateConnection())
            {
                var enrollment = await connection.QueryFirstOrDefaultAsync<Enrollment>(query);

                return enrollment;
            }
        }

        public async Task UpdateEnrollmentActiveStatus(EnrollmentUpdateStatusDto updateRequest, Guid id)
        {
            var query = $"UPDATE Enrollment SET Active ='{updateRequest.Active}' WHERE Id = '{id}'";

            using (var connection = _enrollmentDao.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }

        public async Task DeleteEnrollmentById(Guid id)
        {
            var query = $"DELETE FROM Enrollment WHERE Id = {id}";
            using (var connection = _enrollmentDao.CreateConnection())
            {
                await connection.ExecuteAsync(query);
            }
        }
    }
}
