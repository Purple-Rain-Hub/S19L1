using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using S19L1.DTOs.Student;
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
        public async Task<IActionResult> Create([FromBody] CreateStudentRequestDto studentDto)
        {
            try
            {
                var result = await _studentsService.CreateStudentAsync(studentDto);

                return result ? Ok(new CreateStudentResponseDto() { Message = "Student added successfully" })
                    : BadRequest(new CreateStudentResponseDto() { Message = "Something went wrong!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            try
            {
                var students = await _studentsService.GetStudentsAsync();

                if (students == null)
                {
                    return BadRequest(new
                    {
                        message = "Something went wrong"
                    });
                }

                var count = students.Count();

                var text = count == 1 ? $"{count} student found" : $"{count} students found";

                return Ok(new
                GetStudentResponseDto()
                {
                    Message = text,
                    Students = students
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetStudentById(Guid id)
        {
            try
            {
                var result = await _studentsService.GetStudentByIdAsync(id);

                return result != null ? Ok(new { message = "Customer found", Student = result })
                    : BadRequest(new { message = "Something went wrong" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            try
            {
                var result = await _studentsService.DeleteStudentAsync(id);

                return result ? Ok(new DeleteStudentResponseDto() { Message = "Student deleted successfully" })
                    : BadRequest(new DeleteStudentResponseDto() { Message = "Something went wrong" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudent([FromQuery] Guid id, [FromBody] UpdateStudentRequestDto student)
        {
            try
            {
                var result = await _studentsService.UpdateStudentAsync(id, student);

                return result ? Ok(new UpdateStudentResponseDto() { Message = "Student updated" })
                    : BadRequest(new UpdateStudentResponseDto() { Message = "Something went wrong" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
