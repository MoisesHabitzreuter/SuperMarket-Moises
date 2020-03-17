using BLL.Interfaces;
using Commom.Security;
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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<Response> Authenticate(string email, string passWord)
        {
           return await this._userRepository.Authenticate(email, passWord);
        }

        public async Task<List<UserDTO>> GetUser()
        {
            return await this._userRepository.GetUsers();
        }

        public async Task<Response> Insert(UserDTO user)
        {
            
                Response response = new Response();

                if (string.IsNullOrWhiteSpace(user.Name))
                {
                    response.Errors.Add("O nome deve ser informado");
                }
                else if (user.Name.Length < 2 && user.Name.Length > 45)
                {
                    response.Errors.Add("O nome deve conter entre 2 e 45 caracteres");
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
                await this._userRepository.Insert(user);
                    
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

