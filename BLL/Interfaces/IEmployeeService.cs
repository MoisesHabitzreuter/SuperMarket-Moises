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
        Task<DataResponse<List<EmployeeDTO>>> GetEmployee();
        Task<DataResponse<EmployeeDTO>> GetEmployeeByID(int id);
        Task<DataResponse<EmployeeDTO>> GetEmployeeByCPF(string cpf);
        Task<DataResponse<EmployeeDTO>> GetEmployeeByEmail(string email);
        Task<Response> Update(EmployeeDTO dto);
    }
}
