using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuperMarketPresentationLayer.Models.Updates
{
    public class ClientUpdateViewModel
    {
        [Required(ErrorMessage = "O nome do cliente deve ser informado")]
        [StringLength(maximumLength: 50, ErrorMessage = "O nome deve conter entre 2 e 50 caracteres", MinimumLength = 2)]
        public string Name { get; set; }
        [Required(ErrorMessage = "O email do cliente deve ser informado")]
        [StringLength(maximumLength: 70, ErrorMessage = "O Email deve conter entre 10 e 70 caracteres", MinimumLength = 10)]
        [EmailAddress(ErrorMessage = "formato de email incorreto")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O CPF do cliente deve ser informado")]

        public string CPF { get; set; }
        [Required(ErrorMessage = "O RG do cliente deve ser informado")]
        public string RG { get; set; }
        [Required(ErrorMessage = "O telefone do cliente deve ser informado")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "A data de nascimento do cliente deve ser informado")]
        public DateTime DateBirth { get; set; }
        [Required(ErrorMessage = "A senha deve ser informada deve ser informado")]
        [StringLength(maximumLength: 20, ErrorMessage = "A senha deve conter entre 2 e 70 caracteres", MinimumLength = 6)]
        public string Password { get; set; }
        [Required(ErrorMessage = "As senhas não coincidem")]
        public string ConfirmPassword { get; set; }
    }
}
