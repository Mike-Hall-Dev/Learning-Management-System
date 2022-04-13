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
    public class TeacherController : ControllerBase
    {
        private readonly TeacherDao _teacherDao;
        public TeacherController(TeacherDao teacherDao)
        {
            _teacherDao = teacherDao;
        }

        [HttpGet]
        [Route("teachers")]
        public async Task<IActionResult> GetAllTeachers()
        {
            try
            {
                var teachers = await _teacherDao.GetAllTeachers();
                return Ok(teachers);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("teachers/{id}")]
        public async Task<IActionResult> GetTeacherById([FromRoute] int id)
        {
            try
            {
                var teacher = await _teacherDao.GetTeacherById(id);
                if (teacher == null)
                {
                    return StatusCode(404);
                }
                return Ok(teacher);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Route("teachers")]
        public async Task<IActionResult> CreateNewTeacher([FromBody] TeacherPost newTeacher)
        {
            try
            {
                await _teacherDao.CreateTeacher(newTeacher);
                return Ok(newTeacher);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("teachers/{id}")]
        public async Task<IActionResult> DeleteTeacherById([FromRoute] int id)
        {
            try
            {
                var teacher = await _teacherDao.GetTeacherById(id);
                if (teacher == null)
                {
                    return StatusCode(404);
                }

                await _teacherDao.DeleteTeacherById(id);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        [Route("teachers")]
        public async Task<IActionResult> UpdateTeacherById([FromBody] Teacher updateRequest)
        {
            try
            {
                var teacher = await _teacherDao.GetTeacherById(updateRequest.Id);
                if (teacher == null)
                {
                    return StatusCode(404);
                }

                await _teacherDao.UpdateTeacherById(updateRequest);
                return StatusCode(200);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
