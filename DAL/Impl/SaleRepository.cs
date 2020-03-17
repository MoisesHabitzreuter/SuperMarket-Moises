using DAL.Interfaces;
using DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Impl
{
    public class SaleRepository : ISaleRepository
    {
        private readonly MarketContext _context;
        public SaleRepository(MarketContext context)
        {
            _context = context;
        }

        public async Task<List<SaleDTO>> GetSales()
        {
            return await this._context.Sales.ToListAsync();
        }

        public async Task Insert(SaleDTO sale)
        {
            _context.Sales.Add(sale);

            foreach (ItemsSaleDTO item in sale.ItemsSales)
            {
                _context.ItemSales.Add(item);
            }
            await _context.SaveChangesAsync();
        }

    }
}
