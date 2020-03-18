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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MarketContext _context;
        public EmployeeRepository(MarketContext context)
        {
            _context = context;
        }

        public async Task GetEmployeeByCPF(string cpf)
        {
             await _context.Employees.Where(c => c.CPF == cpf).ToListAsync();
        }

        public async Task<List<EmployeeDTO>> GetEmployees()
        {
            return await this._context.Employees.ToListAsync();

        }

        public async Task Insert(EmployeeDTO employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }
    }
}
