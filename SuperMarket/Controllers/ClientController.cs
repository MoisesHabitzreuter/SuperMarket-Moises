using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Impl;
using BLL.Interfaces;
using Commom.Security;
using DTO;
using DTO.Responses;
using Microsoft.AspNetCore.Mvc;
using SuperMarketPresentationLayer.Models;

namespace SuperMarketPresentationLayer.Controllers
{
    public class ClientController : Controller
    {
        private IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            this._clientService = clientService;
        }
        public async Task<IActionResult> Index()
        {
            DataResponse<ClientDTO> response = await _clientService.GetClient();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ClientQueryViewModel, ClientDTO>();
            });
            IMapper mapper = configuration.CreateMapper();
            // new SERService().GetSERByID(4);
            //Transforma o ClienteInsertViewModel em um ClienteDTO
            List<ClientQueryViewModel> clientQueryViews =
                mapper.Map<List<ClientQueryViewModel>>(response);
            ViewBag.Clients = clientQueryViews;
            return View();
        }
        public IActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Insert(ClientInsertViewModel viewModel)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ClientInsertViewModel, ClientDTO>();
            });
            IMapper mapper = configuration.CreateMapper();
            // new SERService().GetSERByID(4);
            //Transforma o ClienteInsertViewModel em um ClienteDTO
            ClientDTO dto = mapper.Map<ClientDTO>(viewModel);

            try
            {
                await this._clientService.Insert(dto);
                return RedirectToAction("Index", "Client");
            }
            catch (Exception ex)
            {
                ViewBag.Erros = ex.Message;
            }
            return View();
        }
    }
}