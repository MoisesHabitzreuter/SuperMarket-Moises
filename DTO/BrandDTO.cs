using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO
{
    public class BrandDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<ProductDTO> Products { get; set; }
        public bool IsActive { get; set; } = true;
        public BrandDTO(int id, string name, bool isActive)
        {
            this.ID = id;
            this.Name = name;
            this.IsActive = isActive;
        }
        public BrandDTO()
        {

        }
    }
}
