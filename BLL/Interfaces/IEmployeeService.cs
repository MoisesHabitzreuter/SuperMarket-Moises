using DTO;
using DTO.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IEmployeeService
    {
        Task<Response> Insert(EmployeeDTO employee);
        Task<List<EmployeeDTO>> GetEmployee();
        Task<DataResponse> GetEmployeeByCPF(string cpf);
        Task<DataResponse> GetEmployeeByEmail(string email);
        Task<Response> Update(EmployeeDTO dto);
    }
}
