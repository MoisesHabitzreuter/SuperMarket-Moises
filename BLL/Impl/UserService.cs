using BLL.Interfaces;
using Commom.Security;
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
    public class UserService : IUserService, IService<UserDTO>
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<Response> Authenticate(string email, string passWord)
        {
            Response response = new Response();
            if (response.Errors.Count != 0)
            {
                 response.Success = true;
                 await this._userRepository.Authenticate(email, passWord);
                 return response;
            }
            else
            {
                response.Success = false;
                response.GetErrorMessage();
                return response;
            }
        }


        public async Task<DataResponse<UserDTO>> GetUserByEmail(string email)
        {
            DataResponse<UserDTO> response = new DataResponse<UserDTO>();
            try
            {
                response.Success = true;
                response.Data = await _userRepository.GetUserByEmail(email);
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                await File.AppendAllTextAsync("Log.txt", ex.Message);
                return response;
            }
        }

        public async Task<Response> Insert(UserDTO user)
        {
            Response response = new Response();
            response.Errors = Validate(user);
            if (response.Errors.Count != 0)
            {
                response.Success = false;
                return response;
            }
            else
            {
                try
                {
                    response.Success = true;
                    await this._userRepository.Insert(user);
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

        public List<string> Validate(UserDTO obj)
        {
            Response response = new Response();
            List<string> errors = new List<string>();

            if (string.IsNullOrWhiteSpace(obj.Name))
            {
                errors.Add("O nome deve ser informado");
            }
            else if (obj.Name.Length < 2 && obj.Name.Length > 45)
            {
                errors.Add("O nome deve conter entre 2 e 45 caracteres");

            }



            return errors;
        }

        public async Task<DataResponse<List<UserDTO>>> GetUser()
        {
            DataResponse<List<UserDTO>> response = new DataResponse<List<UserDTO>>();
            try
            {
                response.Success = true;
                response.Data = await _userRepository.GetUsers();
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Response> Update(UserDTO user)
        {
            Response response = new Response();

            response.Errors = Validate(user);

            if (response.Errors.Count != 0)
            {
                response.Success = false;
                return response;
            }

            try
            {
                await this._userRepository.Update(user);

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

        public async Task<DataResponse<UserDTO>> GetUserByID(int id)
        {
            DataResponse<UserDTO> response = new DataResponse<UserDTO>();
            try
            {
                response.Success = true;
                response.Data = await _userRepository.GetUserByID(id);
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                await File.AppendAllTextAsync("Log.txt", ex.Message);
                return response;
            }
        }
    }
}

