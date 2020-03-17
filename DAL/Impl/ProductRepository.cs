using DAL.Interfaces;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Impl
{
    public class ProductRepository : IProductRepository
    {
        private readonly MarketContext _context;
        public ProductRepository(MarketContext context)
        {
            _context = context;
        }
        public async Task<List<ProductDTO>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public Task Insert(ProductDTO product)
        {
            throw new NotImplementedException();
        }
    }
}
