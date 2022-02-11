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
    public class TeacherController : ControllerBase
    {
        private readonly TeacherContext _context;

        public TeacherController(TeacherContext context)
        {
            _context = context;

            if (_context.Teachers.Any()) return;

            TeacherSeed.InitData(context);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IQueryable<Teacher>> GetTeachers()
        {
            var result = _context.Teachers as IQueryable<Teacher>;

            return Ok(result
              .OrderBy(p => p.Id));
        }
    }
}
