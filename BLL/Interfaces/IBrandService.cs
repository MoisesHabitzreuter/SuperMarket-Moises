﻿using DTO;
using DTO.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IBrandService
    {
        Task<Response> Insert(BrandDTO brands);
        Task<DataResponse<BrandDTO>> GetBrands();
    }
}
