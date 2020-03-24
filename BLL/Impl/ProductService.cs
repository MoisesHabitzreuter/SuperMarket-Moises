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
    public class ProductService : IProductService,IService<ProductDTO>
    {
        
        private IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository, MarketContext context)
        {
            
            this._productRepository = productRepository;
        }

        public async Task<DataResponse<List<ProductDTO>>> GetProductsByCategory(int category)
        {
            DataResponse<List<ProductDTO>> response = new DataResponse<List<ProductDTO>>();
            try
            {
                response.Success = true;
                response.Data = await _productRepository.GetProductsByCategory(category);
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                File.WriteAllText("Log.txt", ex.Message);
                return response;
            }
        }

        public async Task<Response> Insert(ProductDTO product)
        {
            
                Response response = new Response();
            response.Errors = Validate(product);
                if (response.Errors.Count != 0)
                {
                    response.Success = false;
                    return response;
                }

                try
                {
                await this._productRepository.Insert(product);
                   
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
        public async Task<Response> Update(ProductDTO product)
        {

            Response response = new Response();

            response.Errors = Validate(product);

            if (response.Errors.Count != 0)
            {
                response.Success = false;
                return response;
            }

            try
            {
                await this._productRepository.Update(product);

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

        public async Task<DataResponse<List<ProductDTO>>> GetProduct()
        {
            DataResponse<List<ProductDTO>> response = new DataResponse<List<ProductDTO>>();
            try
            {
                response.Success = true;
                response.Data = await _productRepository.GetProducts();
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                await File.AppendAllTextAsync("Log.txt", ex.Message);
                return response;
            }
        }
        public List<string> Validate(ProductDTO obj)
        {
            List<string> errors = new List<string>();
            if (string.IsNullOrWhiteSpace(obj.Description))
            {
                errors.Add("O Produto deve ser informado");
            }
            else if (obj.Description.Length < 2 && obj.Description.Length > 45)
            {
                errors.Add("O Produto deve conter entre 2 e 45 caracteres");
            }
            return errors;
        }
    }
}

