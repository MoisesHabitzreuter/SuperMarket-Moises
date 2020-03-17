using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Impl
{
    public class EmployeeService : IEmployeeService
    {
        
        private IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository, MarketContext context)
        {
            
            this._employeeRepository = employeeRepository;
        }

        public async Task<List<EmployeeDTO>> GetEmployee()
        {
            return await _employeeRepository.GetEmployees();
        }

        public async Task<Response> Insert(EmployeeDTO employee)
        {
            
                Response response = new Response();

                if (string.IsNullOrWhiteSpace(employee.Name))
                {
                    response.Errors.Add("O funcionário deve ser informado");
                }
                else if (employee.Name.Length < 2 && employee.Name.Length > 45)
                {
                    response.Errors.Add("O funcionário deve conter entre 2 e 45 caracteres");
                    response.Success = false;
                    return response;
                }

                if (response.Errors.Count != 0)
                {
                    response.Success = false;
                    return response;
                }

                try
                {
                    await _employeeRepository.Insert(employee);
                    
                    response.Success = true;
                    return response;
                }
                catch (Exception ex)
                {
                    response.Errors.Add("Erro no banco contate o adm");
                    response.Success = false;
                    File.WriteAllText("Log.txt", ex.Message);
                    return response;
                }
            }
        }

        
        
    }

