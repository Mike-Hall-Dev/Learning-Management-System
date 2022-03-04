using Lms.Daos;
using Lms.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lms.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class CourseController : ControllerBase
    {
        private readonly LmsContext _context;

        public CourseController(LmsContext context)
        {
            _context = context;

            if (_context.Courses.Any()) return;

            LmsSeed.InitData(context);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IQueryable<Course>> GetCourses()
        {
            var result = _context.Courses as IQueryable<Course>;

            return Ok(result
              .OrderBy(p => p.Id));
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Course> GetCourseByIdNumber([FromRoute] int id)
        {
            var course = _context.Courses
                .FirstOrDefault(p => p.Id.Equals(id));

            if (course == null) return NotFound();

            return Ok(course);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Course> PostCourse([FromBody] Course course)
        {
            try
            {
                _context.Courses.Add(course);
                _context.SaveChanges();

                return new CreatedResult($"/course/{course.Id}", course);
            }
            catch (Exception e)
            {
                // Typically an error log is produced here
                return ValidationProblem(e.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Course> PutCourse([FromRoute] int id, [FromBody] Course newCourse)
        {
            try
            {
                var courseList = _context.Courses as IQueryable<Course>;
                var course = courseList.First(p => p.Id.Equals(id));

                _context.Courses.Remove(course);
                _context.Courses.Add(newCourse);
                _context.SaveChanges();

                return new CreatedResult($"/course/{newCourse.Id}", newCourse);
            }
            catch (Exception e)
            {
                // Typically an error log is produced here
                return ValidationProblem(e.Message);
            }
        }

        [HttpPatch]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Course> PatchCourse([FromRoute] int id, [FromBody] CoursePatch newCourse)
        {
            try
            {
                var courseList = _context.Courses as IQueryable<Course>;
                var course = courseList.First(p => p.Id.Equals(id));

                course.Name = newCourse.Name ?? course.Name;
                //course.TeacherId = newCourse.TeacherId ?? course.TeacherId;
                course.Active = newCourse.Active;

                if (newCourse.TeacherId != 0)
                {
                    course.TeacherId = newCourse.TeacherId;
                }


                _context.Courses.Update(course);
                _context.SaveChanges();

                return new CreatedResult($"/course/{course.Id}", course);
            }
            catch (Exception e)
            {
                // Typically an error log is produced here
                return ValidationProblem(e.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Course> DeleteCourse([FromRoute] int id)
        {
            try
            {
                var courseList = _context.Courses as IQueryable<Course>;
                var course = courseList.First(p => p.Id.Equals(id));

                _context.Courses.Remove(course);
                _context.SaveChanges();

                return new CreatedResult($"/course/{course.Id}", course);
            }
            catch (Exception e)
            {
                // Typically an error log is produced here
                return ValidationProblem(e.Message);
            }
        }

    }
}
