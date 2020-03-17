using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class CartDTO
    {
        public int ID { get; set; }
        public ICollection<ProductDTO> Products { get; set; }
    }
}
