﻿namespace AuthAPI.Models.DTOs
{
    public class LoginResponseDto
    {
        public UserDto? User { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
