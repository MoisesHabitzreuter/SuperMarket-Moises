
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
namespace DTO
{
    public class ProviderDTO
    {
        public int ID { get; set; }
        public string FantasyName { get; set; }
        public string Email { get; set; }
        public string CNPJ { get; set; }
        public string Phone { get; set; }
        public virtual ICollection<ProductDTO> Products { get; set; }
        public bool IsActive {get;set;} = true;
        public ProviderDTO(int id, string fantasyName, string email, string cnpj, string phone, bool isActive)
        {
            this.ID = id;
            this.FantasyName = fantasyName;
            this.Email = email;
            this.CNPJ = cnpj;
            this.Phone = phone;
            this.IsActive = isActive;

        }
    }
}
