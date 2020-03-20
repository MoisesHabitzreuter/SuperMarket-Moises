using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IService<T>
    {
        List<string> Validate(T obj);
    }
}
