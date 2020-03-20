using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuperMarketPresentationLayer.Models
{
    public class ProviderQueryViewModel
    {
        [Required(ErrorMessage = "O nome do fornecedor deve ser informado")]
        [StringLength(maximumLength: 40, ErrorMessage = "O nome do fornecedor deve conter entre 2 e 40 caracteres", MinimumLength = 2)]
        public string FantasyName { get; set; }
        [Required(ErrorMessage = "O Email do fornecedor deve ser informado")]
        [StringLength(maximumLength: 70, ErrorMessage = "O Email do fornecedor deve conter entre 10 e 70 caracteres", MinimumLength = 10)]
        [EmailAddress(ErrorMessage = "formato de email incorreto")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O CNPJ do fornecedor deve ser informado")]
        public string CNPJ { get; set; }
        [Required(ErrorMessage = "O Telefone do fornecedor deve ser informado")]
        public string Phone { get; set; }
    }
}
