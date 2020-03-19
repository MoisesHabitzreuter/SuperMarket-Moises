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
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Insert()
        {
            return View();
        }
        public IActionResult Buscarporcpf()
        {
            return View();
        }
        public async Task<IActionResult> Buscarporcpf(EmployeeQueryViewModel viewmodel)
        {
            List<EmployeeDTO> employees = await this._employeeService.GetEmployeeByCPF();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EmployeeDTO, EmployeeQueryViewModel>();
            });
            IMapper mapper = configuration.CreateMapper();
            // new SERService().GetSERByID(4);
            //Transforma o ClienteInsertViewModel em um ClienteDTO
            List<EmployeeQueryViewModel> employeeviewmodel =
                mapper.Map<List<EmployeeQueryViewModel>>(employees);
            ViewBag.Employees = employeeviewmodel;
            return View();
        }
        public IActionResult Buscarporrg()
        {
            return View();
        }
        public async Task<IActionResult> Buscarporrg(EmployeeQueryViewModel viewmodel)
        {
            List<EmployeeDTO> employees = await this._employeeService.GetEmployeeByRG();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EmployeeDTO, EmployeeQueryViewModel>();
            });
            IMapper mapper = configuration.CreateMapper();
            // new SERService().GetSERByID(4);
            //Transforma o ClienteInsertViewModel em um ClienteDTO
            List<EmployeeQueryViewModel> employeeviewmodel =
                mapper.Map<List<EmployeeQueryViewModel>>(employees);
            ViewBag.Employees = employeeviewmodel;
            return View();
        }
        public IActionResult Buscarporemail()
        {
            return View();
        }
        public async Task<IActionResult> Buscarporemail(EmployeeQueryViewModel viewmodel)
        {
            List<EmployeeDTO> employees = await this._employeeService.GetEmployeeByEmail();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EmployeeDTO, EmployeeQueryViewModel>();
            });
            IMapper mapper = configuration.CreateMapper();
            // new SERService().GetSERByID(4);
            //Transforma o ClienteInsertViewModel em um ClienteDTO
            List<EmployeeQueryViewModel> employeeviewmodel =
                mapper.Map<List<EmployeeQueryViewModel>>(employees);
            ViewBag.Employees = employeeviewmodel;
            return View();
        }
        public IActionResult Buscar()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Buscar(EmployeeQueryViewModel viewmodel)
        {
            List<EmployeeDTO> employees = await this._employeeService.GetEmployee();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EmployeeDTO, EmployeeQueryViewModel>();
            });
            IMapper mapper = configuration.CreateMapper();
            // new SERService().GetSERByID(4);
            //Transforma o ClienteInsertViewModel em um ClienteDTO
            List<EmployeeQueryViewModel> employeeviewmodel =
                mapper.Map<List<EmployeeQueryViewModel>>(employees);
            ViewBag.Employees = employeeviewmodel;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Insert(EmployeeInsertViewModel viewModel)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EmployeeInsertViewModel, EmployeeDTO>();
            });
            IMapper mapper = configuration.CreateMapper();
            // new SERService().GetSERByID(4);
            //Transforma o ClienteInsertViewModel em um ClienteDTO
            EmployeeDTO dto = mapper.Map<EmployeeDTO>(viewModel);
            try
            {
                await _employeeService.Insert(dto);
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