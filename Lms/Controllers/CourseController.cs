using Lms.Daos;
using Lms.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lms.Extensions;

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
                return Ok(course.ConvertToDto());

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
                return Ok(roster.ConvertToDtoList());

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Route("courses")]
        public async Task<IActionResult> CreateNewCourse([FromBody] CourseRequestDto newCourse)
        {
            try
            {
                await _courseDao.CreateCourse(newCourse.ConvertToModel());
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
        [Route("courses/{id}")]
        public async Task<IActionResult> UpdateCourseById([FromRoute] Guid id, [FromBody] CourseRequestDto updateRequest)
        {
            try
            {
                var course = await _courseDao.GetCourseById(id);

                if (course == null)
                {
                    return StatusCode(404);
                }

                await _courseDao.UpdateCourseById(id, updateRequest.ConvertToModel());
                return StatusCode(204);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }
    }
}
