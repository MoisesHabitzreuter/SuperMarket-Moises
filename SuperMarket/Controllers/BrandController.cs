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
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            this._brandService = brandService;
        }
        public IActionResult Buscar()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Buscar(BrandQueryViewModel viewModel)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BrandQueryViewModel, BrandDTO>();
            });
            IMapper mapper = configuration.CreateMapper();
            // new SERService().GetSERByID(4);
            //Transforma o ClienteInsertViewModel em um ClienteDTO
            BrandDTO dto = mapper.Map<BrandDTO>(viewModel);

            try
            {
                await _brandService.GetBrands(dto);
                return RedirectToAction("Index", "Category");
            }
            catch (Exception ex)
            {
                ViewBag.Erros = ex.Message;
            }
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Insert(BrandInsertViewModel viewmodel)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BrandInsertViewModel, BrandDTO>();
            });
            IMapper mapper = configuration.CreateMapper();
            // new SERService().GetSERByID(4);
            //Transforma o ClienteInsertViewModel em um ClienteDTO
            BrandDTO dto = mapper.Map<BrandDTO>(viewmodel);
            
            try
            {
                await _brandService.Insert(dto);
                return RedirectToAction("Index", "Category");
            }
            catch (Exception ex)
            {
                ViewBag.Erros = ex.Message;
            }
            return View();
        }
        public async Task<IActionResult> Get()
        {
            DataResponse<BrandDTO> response = new DataResponse<BrandDTO>();
            response = await _brandService.GetBrands();
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BrandDTO, BrandQueryViewModel>();
            });
            IMapper mapper = configuration.CreateMapper();
            List<BrandQueryViewModel> dados = mapper.Map<List<BrandQueryViewModel>>(response.Data);
            return View(dados);
        }
    }

}