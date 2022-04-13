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
        public async Task<IActionResult> GetCourseById([FromRoute] int id)
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
        // BUGGED
        //[HttpGet]
        //[Route("course/{id}")]
        //public async Task<IActionResult> GetClassRoster([FromRoute] int id)
        //{
        //    try
        //    {
        //        var roster = await _courseDao.GetClassRosterById(id);
        //        //if (course == null)
        //        //{
        //        //    return StatusCode(404);
        //        //}
        //        return Ok(roster);

        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message);
        //    }
        //}

        [HttpPost]
        [Route("courses")]
        public async Task<IActionResult> CreateNewCourse([FromBody] CoursePost newCourse)
        {
            try
            {
                await _courseDao.CreateCourse(newCourse);
                return Ok(newCourse);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("courses/{id}")]
        public async Task<IActionResult> DeleteCourseById([FromRoute] int id)
        {
            try
            {
                var course = GetCourseById(id);

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
