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
    public class EnrollmentController : ControllerBase
    {
        private readonly EnrollmentDao _enrollmentDao;

        public EnrollmentController(EnrollmentDao enrollmentDao)
        {
            _enrollmentDao = enrollmentDao;
        }

        [HttpPost]
        [Route("enrollments")]
        public async Task<IActionResult> CreateNewEnrollment(EnrollmentPost newEnrollment)
        {
            try
            {
                await _enrollmentDao.CreateEnrollment(newEnrollment);
                return Ok(newEnrollment);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        [Route("enrollments/{id}")]
        public async Task<IActionResult> UpdateEnrollmentActiveStatus(EnrollmentPatch updateRequest, [FromRoute] int id)
        {
            try
            {
                var enrollment = await _enrollmentDao.GetEnrollmentById(id);
                if (enrollment == null)
                {
                    return StatusCode(404);
                }

                await _enrollmentDao.UpdateEnrollmentActiveStatus(updateRequest, id);
                return StatusCode(200);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        [HttpDelete]
        [Route("enrollments/{id}")]
        public async Task<IActionResult> DeleteEnrollmentById([FromRoute] int id)
        {
            try
            {
                var enrollment = _enrollmentDao.GetEnrollmentById(id);

                if (enrollment == null)
                {
                    return StatusCode(404);
                }

                await _enrollmentDao.DeleteEnrollmentById(id);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


    }
}
