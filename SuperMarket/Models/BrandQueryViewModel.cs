using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuperMarketPresentationLayer.Models
{
    public class BrandQueryViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "O nome da marca deve ser informada")]
        [StringLength(maximumLength: 20, ErrorMessage = "O nome deve conter entre 2 e 20 caracteres", MinimumLength = 2)]
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
