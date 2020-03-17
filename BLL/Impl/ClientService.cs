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
    public class ClientService : IClientService, IService<ClientDTO>
    {

        private IClientRepository _clientRepository;
        public ClientService(IClientRepository clientRepository, MarketContext context)
        {

            this._clientRepository = clientRepository;
        }
        public async Task<Response> Insert(ClientDTO client)
        {
            Response response = new Response();
            response.Errors = validate(client);
            if (response.Errors.Count != 0)
            {
                response.Success = false;
                return response;
            }

            try
            {
                await _clientRepository.Insert(client);
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

        public async Task<List<ClientDTO>> GetClient(int page, int size)
        {
            return await _clientRepository.GetClientsPage(page, size);
        }


        public List<string> validate(ClientDTO obj)
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrWhiteSpace(obj.Name))
            {
                errors.Add("O cliente deve ser informado");
            }
            else if (obj.Name.Length < 2 && obj.Name.Length > 20)
            {
                errors.Add("O cliente deve conter entre 2 e 50 caracteres =3");
            }
            return errors;
        }

        public Task<List<ClientDTO>> GetClient()
        {
            throw new NotImplementedException();
        }
    }
}

    
