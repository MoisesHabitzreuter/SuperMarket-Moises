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
    public class ProviderRepository : IProviderRepository
    {
        private readonly MarketContext _context;
        public ProviderRepository(MarketContext context)
        {
            _context = context;
        }
        public async Task<ProviderDTO> GetProviderByCNPJ(string cnpj)
        {
             return await this._context.Providers.FirstOrDefaultAsync(c => c.CNPJ == cnpj);
        }

        public async Task<ProviderDTO> GetProviderByEmail(string email)
        {
            return await this._context.Providers.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<List<ProviderDTO>> GetProviders()
        {
            return await this._context.Providers.ToListAsync();
        }
        public async Task Insert(ProviderDTO provider)
        {
            _context.Providers.Add(provider);
            await _context.SaveChangesAsync();
        }

        public async Task Update(ProviderDTO provider)
        {
            _context.Providers.Update(provider);
            await _context.SaveChangesAsync();
        }
    }
}