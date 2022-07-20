using Microsoft.AspNetCore.Http;
using MISA.WEB05.COMMON.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MISA.WEB05.CORE.Interface.Sevice
{
    public interface IEmployeeSevice:IBaseSevice<Employee>
    {
        Stream ExportExcel(CancellationToken cancellationToken);
    }
}
