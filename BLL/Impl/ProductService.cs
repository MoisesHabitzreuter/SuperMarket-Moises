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
    public class ProductService : IProductService
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

                if (string.IsNullOrWhiteSpace(product.Description))
                {
                    response.Errors.Add("O nome do produto deve ser informado");
                }
                else if (product.Description.Length < 2 && product.Description.Length > 40)
                {
                    response.Errors.Add("O nome do produto deve conter entre 2 e 40 caracteres");
                    response.Success = false;
                    return response;
                }

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

            if (string.IsNullOrWhiteSpace(product.Description))
            {
                response.Errors.Add("O nome do produto deve ser informado");
            }
            else if (product.Description.Length < 2 && product.Description.Length > 40)
            {
                response.Errors.Add("O nome do produto deve conter entre 2 e 40 caracteres");
                response.Success = false;
                return response;
            }

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
    }
}

