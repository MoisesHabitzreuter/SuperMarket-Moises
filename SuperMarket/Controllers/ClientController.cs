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
using SuperMarketPresentationLayer.Models.Updates;

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
            DataResponse<List<ClientDTO>> response = await _clientService.GetClient();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ClientQueryViewModel, ClientDTO>();
            });
            IMapper mapper = configuration.CreateMapper();
            // new SERService().GetSERByID(4);
            //Transforma o ClienteInsertViewModel em um ClienteDTO
            List<ClientQueryViewModel> clientQueryViews =
                mapper.Map<List<ClientQueryViewModel>>(response.Data);
            ViewBag.Clients = clientQueryViews;
            return View();
        }
        public IActionResult BuscarporCpf()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> BuscarporCpf(string cpf)
        {
            DataResponse<ClientDTO> response = await this._clientService.GetClientByCPF(cpf);

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ClientDTO, ClientQueryViewModel>();
            });
            IMapper mapper = configuration.CreateMapper();
            // new SERService().GetSERByID(4);
            //Transforma o ClienteInsertViewModel em um ClienteDTO
            List<ClientQueryViewModel> categoriasViewModel =
                mapper.Map<List<ClientQueryViewModel>>(response.Data);
            ViewBag.Clients = categoriasViewModel;
            return View();
        }
        public async Task<IActionResult> Buscar(ClientQueryViewModel viewModel)
        {
            DataResponse<List<ClientDTO>> response = await _clientService.GetClient();
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ClientDTO, ClientQueryViewModel>();
            });
            IMapper mapper = configuration.CreateMapper();
            List<ClientQueryViewModel> dados = mapper.Map<List<ClientQueryViewModel>>(response.Data);
            return View(dados);
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
                return RedirectToAction("Buscar", "Client");
            }
            catch (Exception ex)
            {
                ViewBag.Erros = ex.Message;
            }
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            DataResponse<ClientDTO> response = await _clientService.GetClientByID(id);
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ClientDTO, ClientUpdateViewModel>();
            });
            IMapper mapper = configuration.CreateMapper();
            return View(mapper.Map<ClientUpdateViewModel>(response.Data));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ClientUpdateViewModel viewModel)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ClientUpdateViewModel, ClientDTO>();
            });
            IMapper mapper = configuration.CreateMapper();
            ClientDTO dto = mapper.Map<ClientDTO>(viewModel);
            dto.ID = id;
            try
            {
                await _clientService.Update(dto);
                return RedirectToAction("Buscar", "Client");
            }
            catch (Exception ex)
            {
                ViewBag.Erros = ex.Message;
            }
            return View();
        }
    }
}