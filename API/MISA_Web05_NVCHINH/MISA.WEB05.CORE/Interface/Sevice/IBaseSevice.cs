using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB05.CORE.Interface.Sevice
{
    public interface IBaseSevice<T>
    {
        // thêm 
        int? Insert(T en);
        // sửa
        int? Update(T en);
    }
}
