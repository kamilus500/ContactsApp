﻿using Microsoft.AspNetCore.Http;

namespace ContactsApp.Application.Auth.Commands
{
    public class RegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile Image { get; set; }
    }
}