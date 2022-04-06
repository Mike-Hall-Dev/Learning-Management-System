using Lms.Daos;
using Lms.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lms.Controllers
{
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly LmsDao _lmsDao;

        public StudentController(LmsDao lmsDao)
        {
            _lmsDao = lmsDao;
        }

        [HttpGet]
        [Route("students")]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var students = await _lmsDao.GetAllStudents();
                return Ok(students);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("student/{id}")]
        public async Task<IActionResult> GetStudentById([FromRoute] int id)
        {
            try
            {
                var student = await _lmsDao.GetStudentById(id);
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

        [HttpPost]
        [Route("student")]
        public async Task<IActionResult> CreateNewStudent([FromBody]Student newStudent)
        {
            try
            {
                await _lmsDao.CreateStudent(newStudent);
                return Ok(newStudent);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("student/{id}")]
        public async Task<IActionResult> DeleteStudentById([FromRoute] int id)
        {
            try
            {
                var student = GetStudentById(id);
                if (student == null)
                {
                    return StatusCode(404);
                }

                await _lmsDao.DeleteStudentById(id);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        [Route("student")]
        public async Task<IActionResult> UpdateStudentById([FromBody] Student updateRequest)
        {
            try
            {
                var student = await _lmsDao.GetStudentById(updateRequest.Id);
                if (student == null)
                {
                    return StatusCode(404);
                }

                await _lmsDao.UpdateStudentById(updateRequest);
                return StatusCode(200);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }
    }
}
