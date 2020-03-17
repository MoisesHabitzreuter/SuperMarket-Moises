using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class ClientDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string Phone { get; set; }
        public DateTime DateBirth { get; set; }
        public string Password { get; set; }
        public bool IsActive {get;set;} = true;

        public ClientDTO(int id, string name, string email, string cpf, string rg, string hone, DateTime dateBirth, bool isActive, string password)
        {
            this.ID = id;
            this.Name = name;
            this.Email = email;
            this.CPF = cpf;
            this.RG = rg;
            this.Phone = Phone;
            this.DateBirth = dateBirth;
            this.IsActive = isActive;
            this.Password = password;
        }
    }
}
