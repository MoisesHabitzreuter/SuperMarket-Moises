using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuperMarketPresentationLayer.Models.Updates
{
    public class ProductUpdateViewModel
    {
        [Required(ErrorMessage = "A descrição do Produto deve ser informada")]
        [StringLength(maximumLength: 40, ErrorMessage = "A descrição do Produto deve conter entre 2 e 40 caracteres", MinimumLength = 2)]
        public string Description { get; set; }
        [Required(ErrorMessage = "A Marca do Produto deve ser informada")]
        public string Brand { get; set; }
        [Required(ErrorMessage = "O Fornecedor do Produto deve ser informada")]
        public string Provider { get; set; }
        [Required(ErrorMessage = "A categoria do Produto deve ser informada")]
        public string Category { get; set; }
        [Required(ErrorMessage = "O Preço do Produto deve ser informada")]
        [Range(1, 5000, ErrorMessage = "O preço do produto está incorreto")]
        public double Price { get; set; }
    }
}
