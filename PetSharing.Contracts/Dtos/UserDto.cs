﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PetSharing.Contracts.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
    }
}
