﻿using System.ComponentModel.DataAnnotations;

namespace S19L1.DTOs.Student
{
    public class CreateStudentResponseDto
    {
        [Required]
        public string Message { get; set; }
    }
}
