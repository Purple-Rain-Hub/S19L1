using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using S19L1.Models;
using S19L1.Services;

namespace S19L1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentsService _studentsService;
        public StudentsController(StudentsService studentsService)
        {
            _studentsService = studentsService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Student student)
        {
            var result = await _studentsService.CreateStudentAsync(student);

            if (!result)
            {
                return BadRequest(new
                {
                    message = "Something went wrong!"
                });
            }
            return Ok(new
            {
                message = "Student added with success"
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var studentsList = await _studentsService.GetStudentsAsync();

            if (studentsList == null)
                return BadRequest(new
                {
                    message = "Something went wrong!"
                });
            if (!studentsList.Any())
            {
                return NoContent();
            }

            var count = studentsList.Count();

            var text = count == 1 ? $"{count} student found" : $"{count} students found";

            return Ok(new
            {
                message = text,
                students = studentsList
            });
        }
    }
}
