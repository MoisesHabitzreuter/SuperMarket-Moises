using DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IClientRepository
    {
        public Task Insert(ClientDTO client);
        public Task<List<ClientDTO>> GetClientsPage(int page, int size);
        public Task<ClientDTO> GetClientsByCPF(string cpf);
        public Task<List<ClientDTO>> GetClients();
        public Task Update(ClientDTO client);
    }
}
