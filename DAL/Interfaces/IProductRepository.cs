using DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IProductRepository
    {
        public Task Insert(ProductDTO product);
        public Task<List<ProductDTO>> GetProducts();
        public Task<List<ProductDTO>> GetProductsByPrice(double price);
    }
}
