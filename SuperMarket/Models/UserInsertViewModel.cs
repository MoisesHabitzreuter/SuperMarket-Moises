using DTO.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuperMarketPresentationLayer.Models
{
    public class UserInsertViewModel
    {
        [Required(ErrorMessage = "O Email do Usuario deve ser informado")]
        [StringLength(maximumLength: 70, ErrorMessage = "O Email do Usuario deve conter entre 10 e 70 caracteres", MinimumLength = 10)]
        public string Email { get; set; }
        [Required(ErrorMessage = "O nome do usuario deve ser informado")]
        [StringLength(maximumLength: 40, ErrorMessage = "O nome do usuario deve conter entre 2 e 40 caracteres", MinimumLength = 2)]
        public string Name { get; set; }
        [Required(ErrorMessage = "A senha do usuario deve ser informado")]
        public string Password { get; set; }
        [Required(ErrorMessage = "As senhas nao coincidem")]
        public string ConfirmPassword { get; set; }

    }
}
