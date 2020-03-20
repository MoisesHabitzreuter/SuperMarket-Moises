using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuperMarketPresentationLayer.Models
{
    public class CategoryQueryViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "O nome da categoria deve ser informada")]
        [StringLength(maximumLength: 30, ErrorMessage = "O nome deve conter entre 2 e 30 caracteres", MinimumLength = 2)]
        public string Name { get; set; }
    }
}
