using DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {
        public Task Insert(UserDTO user);
        public Task<List<UserDTO>> GetUsers();
        public Task<UserDTO> GetUserByID(int id);
        public Task<UserDTO> Authenticate(string email, string passWord);
        public Task<UserDTO> GetUserByEmail(string email);
        public Task Update(UserDTO user);

    }
}
