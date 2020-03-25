using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using DTO;
using DTO.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Impl
{
    public class ProviderService : IProviderService, IService<ProviderDTO>
    {

        private IProviderRepository _providerRepository;
        public ProviderService(IProviderRepository providerRepository)
        {

            this._providerRepository = providerRepository;
        }

        
        public async Task<DataResponse<ProviderDTO>> GetProviderbyCNPJ(string cnpj)
        {
            DataResponse<ProviderDTO> response = new DataResponse<ProviderDTO>();
            try
            {
                response.Success = true;
                response.Data = await _providerRepository.GetProviderByCNPJ(cnpj);
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                File.WriteAllText("Log.txt", ex.Message);
                return response;
            }
        }

        public async Task<DataResponse<ProviderDTO>> GetProviderbyEmail(string email)
        {
            DataResponse<ProviderDTO> response = new DataResponse<ProviderDTO>();
            try
            {
                response.Success = true;
                response.Data = await this._providerRepository.GetProviderByEmail(email);
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                File.WriteAllText("Log.txt", ex.Message);
                return response;
            }
        }

        public async Task<Response> Insert(ProviderDTO provider)
        {

            Response response = new Response();
            response.Errors = Validate(provider);
            

            if (response.Errors.Count != 0)
            {
                response.Success = false;
                return response;
            }

            try
            {
                await this._providerRepository.Insert(provider);

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

        public async Task<DataResponse<List<ProviderDTO>>> GetProvider()
        {
            DataResponse<List<ProviderDTO>> dataResponse = new DataResponse<List<ProviderDTO>>();
            try
            {
                dataResponse.Success = true;
                dataResponse.Data = await _providerRepository.GetProviders();
                return dataResponse;
            }
            catch (Exception ex)
            {
                dataResponse.Success = false;
                File.WriteAllText("Log.txt", ex.Message);
                return dataResponse;
            }
        }

        public List<string> Validate(ProviderDTO obj)
        {
            List<string> errors = new List<string>();
            if (string.IsNullOrWhiteSpace(obj.FantasyName))
            {
                errors.Add("O Nome do fornecedor deve ser informado");
            }
            else if (obj.FantasyName.Length < 2 && obj.FantasyName.Length > 45)
            {
                errors.Add("O Nome do fornecedor deve conter entre 2 e 45 caracteres");
            }
            if (string.IsNullOrEmpty(obj.CNPJ))
            {
                errors.Add("O CNPJ do fornecedor deve ser informada");
            }
            if (string.IsNullOrEmpty(obj.Phone))
            {
                errors.Add("O numero de telefone deve ser informado");
            }
            if (string.IsNullOrEmpty(obj.Email))
            {
                errors.Add("O Email deve ser informado");
            }
            return errors;
        }

        public async Task<Response> Update(ProviderDTO provider)
        {
            Response response = new Response();

            response.Errors = Validate(provider);

            if (response.Errors.Count != 0)
            {
                response.Success = false;
                return response;
            }

            try
            {
                await this._providerRepository.Update(provider);

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

        public async Task<DataResponse<ProviderDTO>> GetProviderByID(int id)
        {
            DataResponse<ProviderDTO> response = new DataResponse<ProviderDTO>();
            try
            {
                response.Success = true;
                response.Data = await _providerRepository.GetProviderByID(id);
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                File.WriteAllText("Log.txt", ex.Message);
                return response;
            }
        }
    }
}

