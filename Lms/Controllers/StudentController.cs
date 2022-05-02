using Lms.Daos;
using Lms.Extensions;
using Lms.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Lms.Controllers
{
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentDao _studentDao;

        public StudentController(StudentDao studentDao)
        {
            _studentDao = studentDao;
        }

        /// <summary>
        /// Not finished
        /// </summary>
        [HttpGet]
        [Route("students")]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var students = await _studentDao.GetAllStudents();
                return Ok(students.ConvertToDtoList());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Get student by Id
        /// </summary>
        /// <param name="id">ID for a specific student</param>
        [HttpGet]
        [Route("students/{id}")]
        public async Task<IActionResult> GetStudentById([FromRoute] Guid id)
        {
            try
            {
                var student = await _studentDao.GetStudentById(id);

                if (student == null)
                {
                    return StatusCode(404);
                }
                
                return Ok(student);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Get Enrollments by Id
        /// </summary>
        /// <param name="id">ID for a specific student</param>
        /// <param name="isActive">Boolean to fetch either active or inactive enrollments</param>
        [HttpGet]
        [Route("students/{id}/enrollments")]
        public async Task<IActionResult> GetEnrollmentsById([FromRoute] Guid id, [FromQuery] bool isActive)
        {
            try
            {
                bool hasQueryParam = Request.QueryString.HasValue;

                var enrollments = await _studentDao.GetEnrollmentsById(id, isActive, hasQueryParam);

                if (enrollments == null)
                {
                    return StatusCode(404);
                }

                return Ok(enrollments.ConvertToDtoListForEnrollments());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Create a new Student
        /// </summary>
        /// <param name="newStudent">JSON object for creation of a new student</param>
        [HttpPost]
        [Route("students")]
        public async Task<IActionResult> CreateNewStudent([FromBody] StudentRequestDto newStudent)
        {
            try
            {
                await _studentDao.CreateStudent(newStudent);
                return StatusCode(201, newStudent);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Delete a student
        /// </summary>
        /// <param name="id">ID for a specific student</param>
        [HttpDelete]
        [Route("students/{id}")]
        public async Task<IActionResult> DeleteStudentById([FromRoute] Guid id)
        {
            try
            {
                var student = await _studentDao.GetStudentById(id);
                if (student == null)
                {
                    return StatusCode(404);
                }

                await _studentDao.DeleteStudentById(id);
                return StatusCode(204);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Update a student by Id
        /// </summary>
        /// <param name="id">ID for a specific student</param>
        /// <param name="updateRequest">JSON object with updated data for student</param>
        [HttpPut]
        [Route("students/{id}")]
        public async Task<IActionResult> UpdateStudentById([FromRoute] Guid id, [FromBody] StudentRequestDto updateRequest)
        {
            try
            {
                var student = await _studentDao.GetStudentById(id);

                if (student == null)
                {
                    return StatusCode(404);
                }

                await _studentDao.UpdateStudentById(id, updateRequest);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }
    }
}
