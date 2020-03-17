using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DTO
{
    public class CategoryDTO
    {
        public CategoryDTO()
        {
        }

        public CategoryDTO(int iD, string name, ICollection<ProductCategoryDTO> productCategory)
        {
            ID = iD;
            Name = name;
            ProductCategory = productCategory;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ProductCategoryDTO> ProductCategory { get; set; }



    }
}
