using DTO;
using DTO.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProviderService
    {
        Task<Response> Insert(ProviderDTO provider);
        Task<DataResponse<List<ProviderDTO>>> GetProvider();
        Task<DataResponse<ProviderDTO>> GetProviderbyCNPJ(string cnpj);
        Task<DataResponse<ProviderDTO>> GetProviderbyEmail(string email);
        Task<Response> Update(ProviderDTO provider);
    }
}
