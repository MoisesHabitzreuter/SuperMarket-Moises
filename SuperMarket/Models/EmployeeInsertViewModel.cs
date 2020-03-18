using DTO.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuperMarketPresentationLayer.Models
{
    public class EmployeeInsertViewModel
    {
        [Required(ErrorMessage = "O nome do Funcionario deve ser informado")]
        [StringLength(maximumLength: 50, ErrorMessage = "O nome deve conter entre 2 e 50 caracteres", MinimumLength = 2)]
        public string Name { get; set; }
        [Required(ErrorMessage = "O email do Funcionario deve ser informado")]
        [StringLength(maximumLength: 70, ErrorMessage = "O Email deve conter entre 10 e 70 caracteres", MinimumLength = 10)]
        public string Email { get; set; }
        [Required(ErrorMessage = "O CPF do Funcionario deve ser informado")]
        public string CPF { get; set; }
        [Required(ErrorMessage = "O RG do Funcionario deve ser informado")]
        public string RG { get; set; }
        [Required(ErrorMessage = "O telefone do Funcionario deve ser informado")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "A data de nascimento do Funcionario deve ser informada")]
        public DateTime DateBirth { get; set; }
        [Required(ErrorMessage = "A Senha do Funcionario deve ser informada")]
        public string Password { get; set; }
        [Required(ErrorMessage = "A Função do Funcionario deve ser informada")]
        public Function Function { get; set; }
    }
}
