using Lms.Daos;
using Lms.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Lms.Extensions;
using Lms.Dtos;

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
        /// <summary>
        /// Not finished
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("courses")]
        public async Task<IActionResult> GetAllCourses([FromQuery] CourseSearchDto courseParams)
        {
            try
            {
                bool hasQueryParam = Request.QueryString.HasValue;

                var courses = await _courseDao.GetAllCourses(courseParams, hasQueryParam);

                return Ok(courses.ConvertToDtoList());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        /// <summary>
        /// Gets a course by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("courses/{id}", Name = "GetCourseById")]
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
        /// <summary>
        /// Gets a list of students enrolled in a course
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Creates a new course
        /// </summary>
        /// <responses>201</responses>
        /// <param name="newCourse"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("courses")]
        public async Task<IActionResult> CreateNewCourse([FromBody] CourseRequestDto newCourse)
        {
            try
            {
                var createdCourse = await _courseDao.CreateCourse(newCourse);
                var createdCourseDto = createdCourse.ConvertToDto();

                return CreatedAtRoute(nameof(GetCourseById), new { id = createdCourseDto.Id }, createdCourseDto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        /// <summary>
        /// Creates a new active enrollment
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="studentId"></param>
        /// <responses>201</responses>
        /// <returns></returns>
        [HttpPost]
        [Route("courses/{courseId}/enroll")]
        public async Task<IActionResult> CreateNewEnrollment([FromRoute] Guid courseId, [FromBody] Guid studentId)
        {
            try
            {
                await _courseDao.CreateEnrollment(courseId, studentId);
                return StatusCode(201);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        /// <summary>
        /// Sets a current enrollment to inactive
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("enrollments/{courseId}/unenroll")]
        public async Task<IActionResult> UpdateEnrollmentActiveStatus([FromRoute] Guid courseId, [FromBody] Guid studentId)
        {
            try
            {
                var enrollment = await _courseDao.VerifyEnrollment(courseId, studentId);
                if (enrollment == null)
                {
                    return StatusCode(404);
                }

                await _courseDao.UpdateEnrollmentActiveStatusById(courseId, studentId);
                return StatusCode(200);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }
        /// <summary>
        /// Deletes a course by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        /// <summary>
        /// Updates a course by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("courses/{id}")]
        public async Task<IActionResult> UpdateCourseById([FromRoute] Guid id, CourseRequestDto updateRequest)
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
