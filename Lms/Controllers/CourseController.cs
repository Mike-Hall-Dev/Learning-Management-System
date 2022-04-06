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
    public class CourseController : ControllerBase
    {
        private readonly LmsDao _lmsDao;

        public CourseController(LmsDao lmsDao)
        {
            _lmsDao = lmsDao;
        }

        [HttpGet]
        [Route("courses")]
        public async Task<IActionResult> GetAllCourses()
        {
            try
            {
                var courses = await _lmsDao.GetAllCourses();
                return Ok(courses);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("courses/{id}")]
        public async Task<IActionResult> GetCourseById([FromRoute] int id)
        {
            try
            {
                var course = await _lmsDao.GetCourseById(id);
                if (course == null)
                {
                    return StatusCode(404);
                }
                return Ok(course);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("course/{id}")]
        public async Task<IActionResult> DeleteCourseById([FromRoute] int id)
        {
            try
            {
                var course = GetCourseById(id);
                if (course == null)
                {
                    return StatusCode(404);
                }

                await _lmsDao.DeleteCourseById(id);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        [Route("course")]
        public async Task<IActionResult> UpdateCourseById([FromBody] Course updateRequest)
        {
            try
            {
                var course = await _lmsDao.GetCourseById(updateRequest.Id);
                if (course == null)
                {
                    return StatusCode(404);
                }

                await _lmsDao.UpdateCourseById(updateRequest);
                return StatusCode(200);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }
    }
}
