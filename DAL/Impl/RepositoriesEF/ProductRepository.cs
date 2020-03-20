using DAL.Interfaces;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<ProductDTO>> GetProductsByBrand(BrandDTO brand)
        {
            return await _context.Products.Where(c => c.BrandID == brand.ID).ToListAsync();
        }

        public async Task<List<ProductDTO>> GetProductsByCategory(int category)
        {
            return await _context.Products.Where(c => c.ProductCategory.Any(c => c.CategoryID == category)).ToListAsync();
        }

        public async Task<List<ProductDTO>> GetProductsByPrice(double price)
        {
            return await _context.Products.Where(c => c.Price < price).ToListAsync();
        }
        public async Task Insert(ProductDTO product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task Update(ProductDTO product)
        {
             _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
