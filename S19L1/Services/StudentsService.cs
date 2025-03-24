﻿using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using S19L1.Data;
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

        public async Task<bool> CreateStudentAsync(Student student)
        {
            try
            {
                _context.Students.Add(student);
                return await SaveAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            try
            {
                return await _context.Students.ToListAsync();
            }
            catch
            {
                return null;
            }
        }
    }
}
