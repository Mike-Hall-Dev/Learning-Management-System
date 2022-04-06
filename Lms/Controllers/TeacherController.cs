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
        private readonly LmsDao _lmsDao;
        public TeacherController(LmsDao lmsdao)
        {
            _lmsDao = lmsdao;
        }

        [HttpGet]
        [Route("teachers")]
        public async Task<IActionResult> GetAllTeachers()
        {
            try
            {
                var teachers = await _lmsDao.GetAllTeachers();
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
                var teacher = await _lmsDao.GetTeacherById(id);
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

        [HttpDelete]
        [Route("teachers/{id}")]
        public async Task<IActionResult> DeleteTeacherById([FromRoute] int id)
        {
            try
            {
                var teacher = GetTeacherById(id);
                if (teacher == null)
                {
                    return StatusCode(404);
                }

                await _lmsDao.DeleteTeacherById(id);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        [Route("teacher")]
        public async Task<IActionResult> UpdateTeacherById([FromBody] Teacher updateRequest)
        {
            try
            {
                var teacher = await _lmsDao.GetTeacherById(updateRequest.Id);
                if (teacher == null)
                {
                    return StatusCode(404);
                }

                await _lmsDao.UpdateTeacherById(updateRequest);
                return StatusCode(200);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }
    }
}
