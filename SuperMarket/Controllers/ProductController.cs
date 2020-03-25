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
using SuperMarketPresentationLayer.Models.Updates;

namespace SuperMarketPresentationLayer.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly IProviderService _providerService;
        private readonly IProductService _productService;
        public ProductController(ICategoryService categoryservice, IBrandService brandService, IProviderService providerService, IProductService productService)
        {
            this._categoryService = categoryservice;
            this._brandService = brandService;
            this._providerService = providerService;
            this._productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            DataResponse<List<ProductDTO>> response = await _productService.GetProduct();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductDTO, ProductQueryViewModel>();
            });
            IMapper mapper = configuration.CreateMapper();
            List<ProductQueryViewModel> dados = mapper.Map<List<ProductQueryViewModel>>(response.Data);
            return View(dados);
        }

        public async Task<IActionResult> Buscar(ProductQueryViewModel model)
        {
            DataResponse<List<ProductDTO>> response = await _productService.GetProduct();
            
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductDTO, ProductQueryViewModel>();
            });
            IMapper mapper = configuration.CreateMapper();
            List<ProductQueryViewModel> dados = mapper.Map<List<ProductQueryViewModel>>(response.Data);
            return View(dados);
        }
   
        [HttpPost]
        public async Task<IActionResult> Buscarporcategoria(ProductQueryViewModel viewmodel)
        {
            DataResponse<List<ProductDTO>> response = await _productService.GetProductsByCategory(viewmodel.CategoryID);
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductDTO, ProductQueryViewModel>();
            });
            IMapper mapper = configuration.CreateMapper();
            List<ProductQueryViewModel> dados = mapper.Map<List<ProductQueryViewModel>>(response.Data);
            return View(dados);
        }
        public async Task<IActionResult> Insert()
        {
            ViewBag.Brands = await _brandService.GetBrands();
            ViewBag.Provider = await _providerService.GetProvider();
            ViewBag.Category = await _categoryService.GetCategory();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Insert(ProductInsertViewModel viewmodel)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductInsertViewModel, ProductDTO>();
            });
            IMapper mapper = configuration.CreateMapper();
            ProductDTO dto = mapper.Map<ProductDTO>(viewmodel);
            try
            {
                await _productService.Insert(dto);
                return RedirectToAction("Buscar", "Product");
            }
            catch (Exception ex)
            {
                ViewBag.Erros = ex.Message;
            }
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            DataResponse<ProductDTO> response = await _productService.GetProductByID(id);
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductDTO, ProductUpdateViewModel>();
            });
            IMapper mapper = configuration.CreateMapper();
            return View(mapper.Map<ProductUpdateViewModel>(response.Data));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductUpdateViewModel viewModel)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductUpdateViewModel, ProductDTO>();
            });
            IMapper mapper = configuration.CreateMapper();
            ProductDTO dto = mapper.Map<ProductDTO>(viewModel);
            dto.ID = id;
            try
            {
                await _productService.Update(dto);
                return RedirectToAction("Buscar", "Product");
            }
            catch (Exception ex)
            {
                ViewBag.Erros = ex.Message;
            }
            return View();
        }
    }
}