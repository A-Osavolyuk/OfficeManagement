﻿namespace EmployeesAPI.Models.DTOs
{
    public class ResponseDto
    {
        public object? Result { get; set; }
        public bool IsSucceeded { get; set; } = false;
        public string Message { get; set; } = string.Empty;
    }
}
