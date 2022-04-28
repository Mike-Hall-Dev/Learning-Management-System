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

        /// <summary>
        /// Is this working??
        /// </summary>
        /// <returns></returns>
  
        [HttpPost]
        [Route("enrollments")]
        public async Task<IActionResult> CreateNewEnrollment(EnrollmentRequestDto newEnrollment)
        {
            try
            {
                await _enrollmentDao.CreateEnrollment(newEnrollment);
                return StatusCode(201, newEnrollment);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Hell yeah
        /// </summary>
        /// <param name="updateRequest"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("enrollments/{id}")]
        public async Task<IActionResult> UpdateEnrollmentActiveStatus(EnrollmentRequestUpdateStatusDto updateRequest, [FromRoute] Guid id)
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
        public async Task<IActionResult> DeleteEnrollmentById([FromRoute] Guid id)
        {
            try
            {
                var enrollment = await _enrollmentDao.GetEnrollmentById(id);

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
