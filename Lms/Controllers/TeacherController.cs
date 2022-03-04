using Lms.Daos;
using Lms.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Data.EntityFrameworkCore;

namespace Lms.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class TeacherController : ControllerBase
    {
        private readonly LmsContext _context;

        public TeacherController(LmsContext context)
        {
            _context = context;

            if (_context.Teachers.Any()) return;

            LmsSeed.InitData(context);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IQueryable<Teacher>> GetTeachers()
        {
            var result = _context.Teachers.Include("Courses");

            return Ok(result
              .OrderBy(p => p.Id));
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Teacher> GetTeacherById([FromRoute] int id)
        {
            var teacher = _context.Teachers.Include("Courses").FirstOrDefault(p => p.Id.Equals(id));
           // var courses = _context.Courses.Where(c => c.TeacherId == id.ToString()).ToList();
            
            if (teacher == null) return NotFound();

            return Ok(teacher);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Teacher> PostTeacher([FromBody] Teacher teacher)
        {
            try
            {
                _context.Teachers.Add(teacher);
                _context.SaveChanges();

                return new CreatedResult($"/teacher/{teacher.Id}", teacher);
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
        public ActionResult<Teacher> PutTeacher([FromRoute] int id, [FromBody] Teacher newTeacher)
        {
            try
            {
                var teacherList = _context.Teachers as IQueryable<Teacher>;
                var teacher = teacherList.First(p => p.Id.Equals(id));

                _context.Teachers.Remove(teacher);
                _context.Teachers.Add(newTeacher);
                _context.SaveChanges();

                return new CreatedResult($"/teacher/{newTeacher.Id}", newTeacher);
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
        public ActionResult<Teacher> PatchTeacher([FromRoute] int id, [FromBody] TeacherPatch newTeacher)
        {
            try
            {
                var teacherList = _context.Teachers as IQueryable<Teacher>;
                var teacher = teacherList.First(p => p.Id.Equals(id));

                teacher.Name = newTeacher.Name ?? teacher.Name;
                teacher.Courses = newTeacher.Courses ?? teacher.Courses;

                _context.Teachers.Update(teacher);
                _context.SaveChanges();

                return new CreatedResult($"/teacher/{teacher.Id}", teacher);
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
        public ActionResult<Teacher> DeleteTeacher([FromRoute] int id)
        {
            try
            {
                var teacherList = _context.Teachers as IQueryable<Teacher>;
                var teacher = teacherList.First(p => p.Id.Equals(id));

                _context.Teachers.Remove(teacher);
                _context.SaveChanges();

                return new CreatedResult($"/teacher/{teacher.Id}", teacher);
            }
            catch (Exception e)
            {
                // Typically an error log is produced here
                return ValidationProblem(e.Message);
            }
        }

    }
}

