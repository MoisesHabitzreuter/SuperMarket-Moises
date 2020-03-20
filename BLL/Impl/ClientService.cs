﻿using BLL.Interfaces;
using Commom.Security;
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
            response.Errors = Validate(client);
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


        public List<string> Validate(ClientDTO obj)
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrWhiteSpace(obj.Name))
            {
                errors.Add("O cliente deve ser informado");
            }
            else if (obj.Name.Length < 2 && obj.Name.Length > 20)
            {
                errors.Add("O cliente deve conter entre 2 e 50 caracteres");
            }
            if (!CpfValidator.IsCpf(obj.CPF))
            {
                errors.Add("CPF invalido");
            }
            if (string.IsNullOrEmpty(obj.Password))
            {
                errors.Add("A senha deve ser informada");
            }
            return errors;
        }

        public async Task<DataResponse> GetClient()
        {
            DataResponse response = new DataResponse();
            try
            {
                response.Data = await _clientRepository.GetClientsByCPF();
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

        public Task<Response> GetClient(int page, int size)
        {
            throw new NotImplementedException();
        }

        public async Task<DataResponse> GetClientByCPF(string cpf)
        {
            DataResponse response = new DataResponse();
            try
            {
                response.Success = true;
                response.Data = await _clientRepository.GetClientsByCPF(cpf);
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
    }
}

    
