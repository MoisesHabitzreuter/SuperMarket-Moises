using DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IBrandRepository
    { 
        public Task Insert(BrandDTO brand);
        public Task<List<BrandDTO>> GetBrands();
        public Task Update(BrandDTO brand);
    }
}
