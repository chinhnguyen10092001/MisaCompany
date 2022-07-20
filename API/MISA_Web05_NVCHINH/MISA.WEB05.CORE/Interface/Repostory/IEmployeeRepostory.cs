using MISA.WEB05.COMMON.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB05.CORE.Interface.Repostory
{
    public interface IEmployeeRepostory:IBaseRepostory<Employee>
    {
        string get_NewId();
        // tìm kiếm và phân trang
        IEnumerable<Employee> Fillter(string txtSeach, int pageSize, int pageNumber,ref int totalRecord,ref int totalPage);
        int DeleteEmployees(string arrayId);
    }
}
