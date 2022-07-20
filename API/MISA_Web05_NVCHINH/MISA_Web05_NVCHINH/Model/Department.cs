using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/// <summary>
///  Phong ban
///  createdby: Nguyen Van Chinh - 25/06/2022
/// </summary>
namespace MISA_Web05_NVCHINH.Model
{
    public class Department:Base
    {
        public Department()
        {
            this.DepartmentId = Guid.NewGuid();
        }
        public Guid DepartmentId { get; set; }//khoa chinh
        public string? DepartmentName { get; set; }//Ten phong ban
    }
}
