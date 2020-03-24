using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using DTO;
using DTO.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Impl
{
    public class EmployeeService : IEmployeeService, IService<EmployeeDTO>
    {

        private IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository, MarketContext context)
        {

            this._employeeRepository = employeeRepository;
        }

        public async Task<DataResponse<List<EmployeeDTO>>> GetEmployee()
        {
            DataResponse<List<EmployeeDTO>> response = new DataResponse<List<EmployeeDTO>>();
            try
            {
                response.Success = true;
                response.Data = await _employeeRepository.GetEmployees();
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                File.WriteAllText("Log.txt", ex.Message);
                return response;
            }
        }

        public async Task<DataResponse<EmployeeDTO>> GetEmployeeByCPF(string cpf)
        {
            DataResponse<EmployeeDTO> response = new DataResponse<EmployeeDTO>();
            try
            {
                response.Success = true;
                response.Data = await _employeeRepository.GetEmployeeByCPF(cpf);
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                File.WriteAllText("Log.txt", ex.Message);
                return response;
            }
        }

        public async Task<DataResponse<EmployeeDTO>> GetEmployeeByEmail(string email)
        {
            DataResponse<EmployeeDTO> response = new DataResponse<EmployeeDTO>();
            try
            {
                response.Success = true;
                response.Data = await _employeeRepository.GetEmployeeByEmail(email);
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                File.WriteAllText("Log.txt", ex.Message);
                return response;
            }
        }

        public async Task<Response> Insert(EmployeeDTO employee)
        {
            Response response = new Response();
            response.Errors = Validate(employee);
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

        public async Task<Response> Update(EmployeeDTO dto)
        {
            Response response = new Response();
            response.Errors = Validate(dto);
            if (response.Errors.Count != 0)
            {
                response.Success = false;
                return response;
            }

            try
            {
                await _employeeRepository.Insert(dto);

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

        public List<string> Validate(EmployeeDTO obj)
        {
            List<string> errors = new List<string>();
            if (string.IsNullOrWhiteSpace(obj.Name))
            {
                errors.Add("O funcionário deve ser informado");
            }
            else if (obj.Name.Length < 2 && obj.Name.Length > 45)
            {
                errors.Add("O funcionário deve conter entre 2 e 45 caracteres");
            }
            if (string.IsNullOrEmpty(obj.Password))
            {
                errors.Add("A senha deve ser informada");
            }
            if (string.IsNullOrEmpty(obj.Phone))
            {
                errors.Add("O numero de telefone deve ser informado");
            }
            if (string.IsNullOrEmpty(obj.RG))
            {
                errors.Add("O RG do Funcionario deve ser informado");
            }
            if (string.IsNullOrEmpty(obj.Email))
            {
                errors.Add("O Email deve ser informado");
            }
            
            return errors;
        }
    }
}

