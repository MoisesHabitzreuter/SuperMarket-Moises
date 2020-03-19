using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Impl;
using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;
using SuperMarketPresentationLayer.Models;

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
        
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Insert()
        {
            ViewBag.Brands = await _brandService.GetBrands();
            ViewBag.Provider = await _providerService.GetProvider();
            ViewBag.Category = await this._categoryService.GetCategory();

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
            // new SERService().GetSERByID(4);
            //Transforma o ClienteInsertViewModel em um ClienteDTO
            ProductDTO dto = mapper.Map<ProductDTO>(viewmodel);
            
            try
            {
                await _productService.Insert(dto);
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