﻿using BLL.Interfaces;
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
    public class BrandService : IBrandService, IService<BrandDTO>
    {
        
        private readonly IBrandRepository _brandRepository;
        public BrandService(IBrandRepository brandrepository)
        {
            
            this._brandRepository = brandrepository;
        }
        public async Task<Response> Insert(BrandDTO brands)
        {
            Response response = new Response();
            if (response.HasErrors())
            {
                response.Success = false;
                return response;
            }
            else
            {
                try
                {
                    await _brandRepository.Insert(brands);
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
        }
        public async Task<DataResponse<BrandDTO>> GetBrands()
        {
            DataResponse<BrandDTO> response = new DataResponse<BrandDTO>();
            try
            {
                response.Data = await _brandRepository.GetBrands();
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

        public List<string> Validate(BrandDTO obj)
        {
            Response response = new Response();
            List<string> errors = new List<string>();

            if (string.IsNullOrWhiteSpace(obj.Name))
            {
                errors.Add("O nome da marca deve ser informado");
            }
            else if (obj.Name.Length < 2 && obj.Name.Length > 20)
            {
                errors.Add("O nome da marca deve conter entre 2 e 20 caracteres =3");
            }
            return errors;
        }
    }
}
