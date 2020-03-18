using DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IProviderRepository
    {
        public Task Insert(ProviderDTO provider);
        public Task<List<ProviderDTO>> GetProviders();
        public Task GetProviderByCNPJ(string cnpj);

    }
}
