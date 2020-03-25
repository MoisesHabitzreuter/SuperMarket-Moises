using DTO;
using DTO.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProductService
    {
        Task<Response> Insert(ProductDTO product);
        Task<DataResponse<List<ProductDTO>>> GetProduct();
        Task<DataResponse<ProductDTO>> GetProductByID(int id);
        Task<DataResponse<List<ProductDTO>>> GetProductsByCategory(int category);
        Task<Response> Update(ProductDTO product);
        
    }
}
