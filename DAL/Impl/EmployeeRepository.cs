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

        public async Task<EmployeeDTO> GetEmployeeByCPF(string cpf)
        {
             return await _context.Employees.FirstOrDefaultAsync(c => c.CPF == cpf);
        }

        public async Task<EmployeeDTO> GetEmployeeByEmail(string email)
        {
            return await _context.Employees.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<EmployeeDTO> GetEmployeeByRG(string rg)
        {
            return await _context.Employees.FirstOrDefaultAsync(c => c.RG == rg);
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

        public async Task Update(EmployeeDTO employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }
    }
}
