using Lms.Daos;
using Lms.Models;
using Microsoft.AspNetCore.Authorization;
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
        private readonly CourseDao _courseDao;

        public CourseController(CourseDao courseDao)
        {
            _courseDao = courseDao;
        }

        [HttpGet]
        [Route("courses")]
        public async Task<IActionResult> GetAllCourses()
        {
            try
            {
                var courses = await _courseDao.GetAllCourses();
                return Ok(courses);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("courses/{id}")]
        public async Task<IActionResult> GetCourseById([FromRoute] Guid id)
        {
            try
            {
                var course = await _courseDao.GetCourseById(id);
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

       [HttpGet]
       [Route("courses/{id}/roster")]
        public async Task<IActionResult> GetClassRoster([FromRoute] Guid id)
        {
            try
            {
                var roster = await _courseDao.GetActiveClassRosterById(id);

                if (roster == null)
                {
                    return StatusCode(404);
                }
                return Ok(roster);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Route("courses")]
        public async Task<IActionResult> CreateNewCourse([FromBody] CourseCreateDto newCourse)
        {
            try
            {
                await _courseDao.CreateCourse(newCourse);
                return StatusCode(201, newCourse);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("courses/{id}")]
        public async Task<IActionResult> DeleteCourseById([FromRoute] Guid id)
        {
            try
            {
                var course = await _courseDao.GetCourseById(id);

                if (course == null)
                {
                    return StatusCode(404);
                }

                await _courseDao.DeleteCourseById(id);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        [Route("courses")]
        public async Task<IActionResult> UpdateCourseById([FromBody] Course updateRequest)
        {
            try
            {
                var course = await _courseDao.GetCourseById(updateRequest.Id);

                if (course == null)
                {
                    return StatusCode(404);
                }

                await _courseDao.UpdateCourseById(updateRequest);
                return StatusCode(200);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }
    }
}
