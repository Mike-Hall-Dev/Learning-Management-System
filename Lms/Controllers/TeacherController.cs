using Lms.Daos;
using Lms.Extensions;
using Lms.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Lms.Controllers
{
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly TeacherDao _teacherDao;
        public TeacherController(TeacherDao teacherDao)
        {
            _teacherDao = teacherDao;
        }

        /// <summary>
        /// Not finished
        /// </summary>
        [HttpGet]
        [Route("teachers")]
        public async Task<IActionResult> GetAllTeachers()
        {
            try
            {
                var teachers = await _teacherDao.GetAllTeachers();

                return Ok(teachers.ConvertToDtoList());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Get Teacher by Id
        /// </summary>
        /// <param name="id">ID for a specific teacher</param>
        [HttpGet]
        [Route("teachers/{id}")]
        public async Task<IActionResult> GetTeacherById([FromRoute] Guid id)
        {
            try
            {
                var teacher = await _teacherDao.GetTeacherById(id);
                if (teacher == null)
                {
                    return StatusCode(404);
                }
                return Ok(teacher.ConvertToDto());

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Create a new Teacher
        /// </summary>
        /// <param name="newTeacher">JSON object for creation of a new teacher</param>
        [HttpPost]
        [Route("teachers")]
        public async Task<IActionResult> CreateNewTeacher([FromBody] TeacherRequestDto newTeacher)
        {
            try
            {
                await _teacherDao.CreateTeacher(newTeacher);
                return StatusCode(201, newTeacher);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Delete a Teacher by Id
        /// </summary>
        /// <param name="id">ID for a specific teacher</param>        
        [HttpDelete]
        [Route("teachers/{id}")]
        public async Task<IActionResult> DeleteTeacherById([FromRoute] Guid id)
        {
            try
            {
                var teacher = await _teacherDao.GetTeacherById(id);
                if (teacher == null)
                {
                    return StatusCode(404);
                }

                await _teacherDao.DeleteTeacherById(id);
                return StatusCode(204);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Update a Teacher by Id
        /// </summary>
        /// <param name="id">ID for a specific teacher</param>
        /// <param name="updateRequest">JSON object with updated data for teacher</param>        
        [HttpPut]
        [Route("teachers/{id}")]
        public async Task<IActionResult> UpdateTeacherById([FromRoute] Guid id, [FromBody] TeacherRequestDto updateRequest)
        {
            try
            {
                var teacher = await _teacherDao.GetTeacherById(id);
                if (teacher == null)
                {
                    return StatusCode(404);
                }

                await _teacherDao.UpdateTeacherById(id, updateRequest);
                return StatusCode(200);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
