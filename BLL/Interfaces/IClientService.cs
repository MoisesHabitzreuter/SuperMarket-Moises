using DTO;
using DTO.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IClientService
    {
        Task<Response> Insert(ClientDTO client);
        Task<Response> GetClient(int page, int size);
        Task<DataResponse<List<ClientDTO>>> GetClient();
        Task<DataResponse<ClientDTO>> GetClientByCPF(string cpf);
    }
}
