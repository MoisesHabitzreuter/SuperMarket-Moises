using DTO;
using DTO.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task<Response> Insert(UserDTO user);
        Task<DataResponse<List<UserDTO>>> GetUser();
        Task<Response> Authenticate(string email, string passWord);
        Task<DataResponse<UserDTO>> GetUserByEmail(string email);
    }
}
