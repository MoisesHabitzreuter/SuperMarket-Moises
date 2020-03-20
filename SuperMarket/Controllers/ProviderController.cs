﻿using System;
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
    public class ProviderController : Controller
    {
        private readonly IProviderService _providerService;
        public ProviderController(IProviderService providerService)
        {
            this._providerService = providerService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Insert()
        {
            return View();
        }
        public IActionResult Buscar()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Buscar(ProviderQueryViewModel viewmodel)
        {
            DataResponse<List<ProviderDTO>> response = await this._providerService.GetProvider();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProviderDTO, ProviderQueryViewModel>();
            });
            IMapper mapper = configuration.CreateMapper();
            // new SERService().GetSERByID(4);
            //Transforma o ClienteInsertViewModel em um ClienteDTO
            List<ProviderQueryViewModel> providerviewmodel =
                mapper.Map<List<ProviderQueryViewModel>>(response.Data);
            ViewBag.Providers = providerviewmodel;
            return View();
        }
        public IActionResult BuscarporCNPJ()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> BuscarporCNPJ(ProviderQueryViewModel viewmodel)
        {
            DataResponse<ProviderDTO> response = await _providerService.GetProviderbyCNPJ(viewmodel.CNPJ);

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProviderDTO, ProviderQueryViewModel>();
            });
            IMapper mapper = configuration.CreateMapper();
            // new SERService().GetSERByID(4);
            //Transforma o ClienteInsertViewModel em um ClienteDTO
            List<ProviderQueryViewModel> providerviewmodel =
                mapper.Map<List<ProviderQueryViewModel>>(response.Data);
            ViewBag.Providers = providerviewmodel;
            return View();
        }
        public IActionResult BuscarporEmail()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> BuscarporEmail(ProviderQueryViewModel viewmodel)
        {
            DataResponse<ProviderDTO> response = await _providerService.GetProviderbyEmail(viewmodel.Email);
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProviderDTO, ProviderQueryViewModel>();
            });
            IMapper mapper = configuration.CreateMapper();
            // new SERService().GetSERByID(4);
            //Transforma o ClienteInsertViewModel em um ClienteDTO
            List<ProviderQueryViewModel> providerviewmodel =
                mapper.Map<List<ProviderQueryViewModel>>(response.Data);
            ViewBag.Providers = providerviewmodel;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Insert(ProviderInsertViewModel viewmodel)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProviderInsertViewModel, ProviderDTO>();
            });
            IMapper mapper = configuration.CreateMapper();
            // new SERService().GetSERByID(4);
            //Transforma o ClienteInsertViewModel em um ClienteDTO
            ProviderDTO dto = mapper.Map<ProviderDTO>(viewmodel);

            try
            {
                await _providerService.Insert(dto);
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