using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Impl;
using BLL.Interfaces;
using DTO;
using DTO.Responses;
using Microsoft.AspNetCore.Mvc;
using SuperMarketPresentationLayer.Models;

namespace SuperMarketPresentationLayer.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryservice)
        {
            this._categoryService = categoryservice;
        }

        public async Task<IActionResult> Index()
        {
            DataResponse response = new DataResponse();
            response = await _categoryService.GetCategory();
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BrandDTO, BrandQueryViewModel>();
            });
            IMapper mapper = configuration.CreateMapper();
            List<BrandQueryViewModel> dados = mapper.Map<List<BrandQueryViewModel>>(response.Data);
            return View(dados);
        }
        public IActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Insert(CategoryInsertViewModel viewModel)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryInsertViewModel, CategoryDTO>();
            });
            IMapper mapper = configuration.CreateMapper();
            // new SERService().GetSERByID(4);
            //Transforma o ClienteInsertViewModel em um ClienteDTO
            CategoryDTO dto = mapper.Map<CategoryDTO>(viewModel);
            try
            {
                await this._categoryService.Insert(dto);
                return RedirectToAction("Index", "Category");
            }
            catch (Exception ex)
            {
                ViewBag.Erros = ex.Message;
            }
            return View();
        }
    }
}