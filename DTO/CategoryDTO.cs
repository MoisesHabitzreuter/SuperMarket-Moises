using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DTO
{
    public class CategoryDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ProductCategoryDTO> ProductCategory { get; set; }

        public CategoryDTO(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
        public CategoryDTO()
        {

        }

    }
}
