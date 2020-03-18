using Commom.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuperMarketPresentationLayer.Models
{
    public class ClientInsertViewModel
    {
        [Required(ErrorMessage = "O nome do cliente deve ser informado")]
        [StringLength(maximumLength: 50, ErrorMessage = "O nome deve conter entre 2 e 50 caracteres", MinimumLength = 2)]
        public string Name { get; set; }
        [Required(ErrorMessage = "O email do cliente deve ser informado")]
        [StringLength(maximumLength: 70, ErrorMessage = "O nome deve conter entre 2 e 70 caracteres", MinimumLength = 2)]
        public string Email { get; set; }
        [Required(ErrorMessage = "O email do cliente deve ser informado")]
        public string CPF { get; set; }
        public string RG { get; set; }
        public string Phone { get; set; }
        public DateTime DateBirth { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
