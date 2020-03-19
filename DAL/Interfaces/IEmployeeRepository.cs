using DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IEmployeeRepository
    {
        public Task Insert(EmployeeDTO employee);
        public Task<List<EmployeeDTO>> GetEmployees();
        public Task<EmployeeDTO> GetEmployeeByCPF(string cpf);
        public Task<EmployeeDTO> GetEmployeeByRG(string rg);
        public Task<EmployeeDTO> GetEmployeeByEmail(string email);
        public Task Update(EmployeeDTO employee);

    }
}
