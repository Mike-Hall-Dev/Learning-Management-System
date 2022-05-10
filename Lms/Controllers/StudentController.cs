using Lms.Daos;
using Lms.Dtos.Request;
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
        /// Gets students with optional query params. Returns max of 25 students. 
        /// </summary>
        [HttpGet]
        [Route("students")]
        public async Task<IActionResult> GetStudentsWithParams([FromQuery] StudentRequestForParams studentParams)
        {
            try
            {
                var students = await _studentDao.GetStudentsWithOptionalParams(studentParams);

                if (students == null)
                {
                    return StatusCode(204);
                }

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
        [Route("students/{id}", Name = "GetStudentById")]
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
        /// Get Enrollments by student Id. Returns both active and inactive enrollments if query param not passed.
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
                var createdStudent = await _studentDao.CreateStudent(newStudent);
                var createdStudentDto = createdStudent.ConvertToDto();

                return CreatedAtRoute(nameof(GetStudentById), new { id = createdStudentDto.Id }, createdStudentDto);
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
