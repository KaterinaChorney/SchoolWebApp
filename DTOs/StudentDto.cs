﻿namespace SchoolWebApplication.DTOs
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? ClassName { get; set; }
    }
}