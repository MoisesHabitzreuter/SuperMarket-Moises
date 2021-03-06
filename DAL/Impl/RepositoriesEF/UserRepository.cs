﻿using Commom.Security;
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

        public async Task<UserDTO> Authenticate(string email, string passWord)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email) && u.Password.Equals(Password.HashPassword(passWord))).ConfigureAwait(false);
            

           
        }

        public async Task<UserDTO> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<UserDTO> GetUserByID(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(c => c.ID == id);
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
