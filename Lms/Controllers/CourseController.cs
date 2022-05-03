using Lms.Daos;
using Lms.Dtos;
using Lms.Extensions;
using Lms.Models;
using Microsoft.AspNetCore.Mvc;
using System;
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

        /// <summary>
        /// Gets courses with optional query params. Returns max of 25 courses. 
        /// </summary>
        [HttpGet]
        [Route("courses")]
        public async Task<IActionResult> GetCoursesWithParams([FromQuery] CourseRequestForParamsDto courseParams)
        {
            try
            {
                var courses = await _courseDao.GetCoursesWithOptionalParams(courseParams);

                if (courses == null)
                {
                    return StatusCode(404);
                }

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
        /// <param name="id">ID for a specific course</param>
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
        /// <param name="id">ID for a specific course</param>
        [HttpGet]
        [Route("courses/{id}/roster")]
        public async Task<IActionResult> GetClassRoster([FromRoute] Guid id)
        {
            try
            {
                var roster = await _courseDao.GetRosterByCourseId(id);

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
        /// <param name="newCourse">JSON object for creation of a new course</param>
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
        /// <param name="courseId">ID for a specific course</param>
        /// <param name="studentId">ID for a specific student</param>
        /// <responses>201</responses>
        [HttpPost]
        [Route("courses/{courseId}/enroll")]
        public async Task<IActionResult> CreateNewEnrollment([FromRoute] Guid courseId, [FromBody] Guid studentId)
        {
            try
            {
                var activeEnrollmentCheck = await _courseDao.CheckForExistingActiveEnrollment(courseId, studentId);

                if (activeEnrollmentCheck != null)
                {
                    return ValidationProblem($"Student already has an active enrollment in this course.");
                }

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
        /// <param name="courseId">ID for a specific course</param>
        /// <param name="studentId">ID for a specific student</param>
        [HttpPatch]
        [Route("courses/{courseId}/unenroll")]
        public async Task<IActionResult> UnenrollFromActive([FromRoute] Guid courseId, [FromBody] Guid studentId)
        {
            try
            {
                var enrollment = await _courseDao.CheckForExistingActiveEnrollment(courseId, studentId);

                if (enrollment == null)
                {
                    return StatusCode(404);
                }

                await _courseDao.UnenrollByCourseId(courseId, studentId);

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
        /// <param name="id">ID for a specific course</param>
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
        /// <param name="id">ID for a specific course</param>
        /// <param name="updateRequest">JSON object with updated data for course</param>
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

                await _courseDao.UpdateCourseById(id, updateRequest);

                return StatusCode(204);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }
    }
}
