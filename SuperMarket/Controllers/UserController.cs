using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Impl;
using BLL.Interfaces;
using DTO;
using DTO.Responses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperMarketPresentationLayer.Models;

namespace SuperMarketPresentationLayer.Controllers
{
    
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            this._userService = userService;
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
        public async Task<IActionResult> Buscar(UserQueryViewModel viewmodel)
        {
            DataResponse<List<UserDTO>> sales = await this._userService.GetUser();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDTO, UserQueryViewModel>();
            });
            IMapper mapper = configuration.CreateMapper();
            // new SERService().GetSERByID(4);
            //Transforma o ClienteInsertViewModel em um ClienteDTO
            List<UserQueryViewModel> userqueryviewmodel =
                mapper.Map<List<UserQueryViewModel>>(sales);
            ViewBag.Users = userqueryviewmodel;
            return View();
        }
        public IActionResult BuscarporEmail()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> BuscarporEmail(UserQueryViewModel viewmodel)
        {
            DataResponse<UserDTO> response = await _userService.GetUserByEmail(viewmodel.Email);

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDTO, UserQueryViewModel>();
            });
            IMapper mapper = configuration.CreateMapper();
            // new SERService().GetSERByID(4);
            //Transforma o ClienteInsertViewModel em um ClienteDTO
            List<UserQueryViewModel> userqueryviewmodel =
                mapper.Map<List<UserQueryViewModel>>(response.Data);
            ViewBag.Users = userqueryviewmodel;
            return View();
        }
        public async Task<IActionResult> Login(string email, string passWord)
        {

            if (await _userService.Authenticate(email, passWord) != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, email)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties();
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();
                ViewBag.UsuarioLogado = true;
                return RedirectToAction("Insert", "Category");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert(UserInsertViewModel viewmodel)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserInsertViewModel, UserDTO>();
            });
            IMapper mapper = configuration.CreateMapper();
            // new SERService().GetSERByID(4);
            //Transforma o ClienteInsertViewModel em um ClienteDTO
            UserDTO dto = mapper.Map<UserDTO>(viewmodel);
            try
            {
                await _userService.Insert(dto);
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