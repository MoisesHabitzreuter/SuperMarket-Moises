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
    public class CategoryService : ICategoryService, IService<CategoryDTO>
    {
        private ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryrepository)
        {
            this._categoryRepository = categoryrepository;
        }
        public async Task<DataResponse> GetCategory()
        {
            DataResponse response = new DataResponse();
            try
            {
                response.Data = await _categoryRepository.GetCategories();
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

        public async Task<Response> Insert(CategoryDTO category)
        {
            Response response = new Response();
            response.Errors = Validate(category);
            if (response.Errors.Count != 0)
            {
                response.Success = false;
                return response;
            }
            else
            {
                try
                {
                    await _categoryRepository.Insert(category);
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

        public List<string> Validate(CategoryDTO obj)
        {
            Response response = new Response();
            List<string> errors = new List<string>();

            if (string.IsNullOrWhiteSpace(obj.Name))
            {
                errors.Add("O nome da categoria deve ser informad");
            }
            else if (obj.Name.Length < 2 && obj.Name.Length > 20)
            {
                errors.Add("O nome da marca deve conter entre 2 e 20 caracteres =3");
            }
            return errors;
        }
    }
}

