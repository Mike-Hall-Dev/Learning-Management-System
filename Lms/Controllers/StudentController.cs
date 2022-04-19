﻿using Lms.Daos;
using Lms.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lms.Controllers
{
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentDao _studentDao;

        public StudentController(StudentDao studentDao)
        {
            _studentDao = studentDao;
        }

        [HttpGet]
        [Route("students")]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var students = await _studentDao.GetAllStudents();
                return Ok(students);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("students/{id}")]
        public async Task<IActionResult> GetStudentById([FromRoute] Guid id)
        {
            try
            {
                var student = await _studentDao.GetStudentById(id);
                if (student == null)
                {
                    return StatusCode(404);
                }
                return Ok(student);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("students/{id}/enrollments")]
        public async Task<IActionResult> GetEnrollmentsById([FromRoute] Guid id, [FromQuery] bool isActive)
        {
            try
            {
                bool hasQueryParam = Request.QueryString.HasValue;

                var result = await _studentDao.GetEnrollmentsById(id, isActive, hasQueryParam);

                if (result == null)
                {
                    return StatusCode(404);
                }
                return Ok(result);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Route("students")]
        public async Task<IActionResult> CreateNewStudent([FromBody] StudentCreateDto newStudent)
        {
            try
            {
                await _studentDao.CreateStudent(newStudent);
                return StatusCode(201, newStudent);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("students/{id}")]
        public async Task<IActionResult> DeleteStudentById([FromRoute] Guid id)
        {
            try
            {
                var student = await _studentDao.GetStudentById(id);
                if (student == null)
                {
                    return StatusCode(404);
                }

                await _studentDao.DeleteStudentById(id);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        [Route("students/{id}")]
        public async Task<IActionResult> UpdateStudentById([FromRoute] Guid id, [FromBody] Student updateRequest)
        {
            try
            {
                var student = await _studentDao.GetStudentById(id);
                if (student == null)
                {
                    return StatusCode(404);
                }

                await _studentDao.UpdateStudentById(id, updateRequest);
                return StatusCode(200);

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }
    }
}
