using Commom.Security;
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
    public class UserRepository : IUserRepository
    {
        private readonly MarketContext _context;
        public UserRepository(MarketContext context)
        {
            _context = context;
        }

        public async Task<Response> Authenticate(string email, string passWord)
        {
            UserDTO user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email) && u.Password.Equals(Password.HashPassword(passWord))).ConfigureAwait(false);
            Response response = new Response();

            if (user == null)
            {
                response.Success = false;
                response.Errors.Add("Usuario nao encontrado");
                return response;
            }
            response.Success = true;
            return response;
        }

        public async Task<UserDTO> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<List<UserDTO>> GetUsers()
        {
            return await this._context.Users.ToListAsync();
        }

        public async Task Insert(UserDTO user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task Update(UserDTO user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
