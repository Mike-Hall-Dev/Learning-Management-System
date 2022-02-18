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
    public class StudentController : ControllerBase
    {
        private readonly LmsContext _context;

        public StudentController(LmsContext context)
        {
            _context = context;

            if (_context.Students.Any()) return;

            LmsSeed.InitData(context);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IQueryable<Student>> GetStudents()
        {
            var result = _context.Students as IQueryable<Student>;

            return Ok(result
              .OrderBy(p => p.Id));
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Student> GetStudentByIdNumber([FromRoute] int id)
        {
            var student = _context.Students
                .FirstOrDefault(p => p.Id.Equals(id));

            if (student == null) return NotFound();

            return Ok(student);
        }

            [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Student> PostStudent([FromBody] Student student)
        {
            try
            {
                _context.Students.Add(student);
                _context.SaveChanges();

                return new CreatedResult($"/students/{student.Id}", student);
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
        public ActionResult<Student> PutStudent([FromRoute] int id, [FromBody] Student newStudent)
        {
            try
            {
                var studentlist = _context.Students as IQueryable<Student>;
                var student = studentlist.First(p => p.Id.Equals(id));

                _context.Students.Remove(student);
                _context.Students.Add(newStudent);
                _context.SaveChanges();

                return new CreatedResult($"/student/{newStudent.Id}", newStudent);
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
        public ActionResult<Student> PatchStudent([FromRoute] int id, [FromBody] StudentPatch newStudent)
        {
            try
            {
                var studentList = _context.Students as IQueryable<Student>;
                var student = studentList.First(p => p.Id.Equals(id));

                //if (newStudent.Id != 0) {student.Id = newStudent.Id;};

                student.Name = newStudent.Name ?? student.Name;
                student.Courses = newStudent.Courses ?? student.Courses;

                _context.Students.Update(student);
                _context.SaveChanges();

                return new CreatedResult($"/student/{student.Id}", newStudent);
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
        public ActionResult<Student> DeleteStudent([FromRoute] int id)
        {
            try
            {
                var studentList = _context.Students as IQueryable<Student>;
                var student = studentList.First(p => p.Id.Equals(id));

                _context.Students.Remove(student);
                _context.SaveChanges();

                return new CreatedResult($"/students/{student.Id}", student);
            }
            catch (Exception e)
            {
                // Typically an error log is produced here
                return ValidationProblem(e.Message);
            }
        }

    }
}
