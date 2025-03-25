using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using S19L1.Data;
using S19L1.DTOs.Student;
using S19L1.Models;

namespace S19L1.Services
{
    public class StudentsService
    {
        private readonly S19L1DbContext _context;
        public StudentsService(S19L1DbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CreateStudentAsync(CreateStudentRequestDto studentDto)
        {
            try
            {
                var student = new Student()
                {
                    Name = studentDto.Name,
                    Surname = studentDto.Surname,
                    Email = studentDto.Email,
                };

                _context.Students.Add(student);
                return await SaveAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<GetStudentRequestDto>?> GetStudentsAsync()
        {
            try
            {
                var students = await _context.Students.ToListAsync();

                var studentsRequest = new List<GetStudentRequestDto>();

                foreach (var student in students)
                {
                    var request = new GetStudentRequestDto()
                    {
                        Id = student.Id,
                        Name = student.Name,
                        Surname = student.Surname,
                        Email = student.Email
                    };

                    studentsRequest.Add(request);
                }

                return studentsRequest;
            }
            catch
            {
                return null;
            }
        }

        public async Task<GetStudentRequestDto?> GetStudentByIdAsync(Guid id)
        {
            try
            {
                var existingStudent = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);

                if (existingStudent == null)
                {
                    return null;
                }

                var student = new GetStudentRequestDto()
                {
                    Id = existingStudent.Id,
                    Name = existingStudent.Name,
                    Surname = existingStudent.Surname,
                    Email = existingStudent.Email
                };

                return student;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteStudentAsync(Guid id)
        {
            try
            {
                var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);

                if (student == null)
                {
                    return false;
                }

                _context.Students.Remove(student);

                return await SaveAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateStudentAsync(Guid id, UpdateStudentRequestDto student)
        {
            try
            {
                var existingStudent = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);

                if (existingStudent == null)
                {
                    return false;
                }

                existingStudent.Name = student.Name;
                existingStudent.Surname = student.Surname;
                existingStudent.Email = student.Email;

                return await SaveAsync();
            }
            catch
            {
                return false;
            }
        }
    }
}
