﻿using DAL.Interfaces;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Impl
{
    public class ClientRepository : IClientRepository
    {
        private readonly MarketContext _context;
        public ClientRepository(MarketContext context)
        {
            _context = context;
        }
        public async Task Insert(ClientDTO client)
        {
                _context.Clients.Add(client);
                await _context.SaveChangesAsync();
        }
        public async Task<List<ClientDTO>> GetClientsPage(int page, int size)
        {
            return await _context.Clients.Skip((page - 1) * size).Take(size).ToListAsync();
        }

        public async Task Update(ClientDTO client)
        {
             _context.Clients.Update(client);
            await _context.SaveChangesAsync();

        }

        public async Task<List<ClientDTO>> GetClients()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<ClientDTO> GetClientsByCPF(string cpf)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.CPF == cpf);
        }

        public async Task<ClientDTO> GetClientsByID(int id)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.ID == id);
        }
    }
}
