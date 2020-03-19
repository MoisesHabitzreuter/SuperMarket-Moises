using DAL.Interfaces;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Impl
{
    public class BrandRepository : IBrandRepository
    {
        private readonly MarketContext _context;

        public BrandRepository(MarketContext context)
        {
            this._context = context;
        }
        public async Task<List<BrandDTO>> GetBrands()
        {
            return await _context.Brands.ToListAsync();
        }

        public async Task Insert(BrandDTO brand)
        {
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
        }
     
    }
}
