using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Impl
{
    public interface IService<T>
    {
        List<string> validate(T obj);
    }
}
