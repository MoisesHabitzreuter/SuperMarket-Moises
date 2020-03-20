using DTO;
using DTO.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICategoryService
    {
        Task<Response> Insert(CategoryDTO category);
        Task<DataResponse<CategoryDTO>> GetCategory();


    }
}
