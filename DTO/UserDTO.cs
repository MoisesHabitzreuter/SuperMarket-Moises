using DTO.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO
{
    public class UserDTO
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public Permissions Permissions { get; set; }
        public bool IsActive { get; set; } = true;
        public UserDTO(int id, string name, string email,Permissions permissions, bool isActive)
        {
            this.ID = id;
            this.Name = name;
            this.Email = email;
            this.Permissions = permissions;
            this.IsActive = isActive;
        }
    }
}
