﻿using System.ComponentModel.DataAnnotations;

namespace S19L1.Models
{
    public class Student
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Surname { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }
}
