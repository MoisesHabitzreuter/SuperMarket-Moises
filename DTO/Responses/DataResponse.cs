using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Responses
{
    public class DataResponse<T> : Response
    {
        public T Data { get; set; }
    }
}
