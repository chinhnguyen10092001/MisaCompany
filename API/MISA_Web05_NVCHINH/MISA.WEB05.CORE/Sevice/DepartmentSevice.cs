using MISA.WEB05.COMMON.Model;
using MISA.WEB05.CORE.Exceptions;
using MISA.WEB05.CORE.Interface.Repostory;
using MISA.WEB05.CORE.Interface.Sevice;
using MISA.WEB05.CORE.Sevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB05.CORE
{
    public class DepartmentSevice :BaseSevice<Department>, IDepartmentSevice
    {
        IDepartmentRepostory respostory;
        public DepartmentSevice(IDepartmentRepostory respostory):base(respostory)
        {
            this.respostory = respostory;
        }
        public override int? Insert(Department department)
        {
            // validate dữ liệu
            if (string.IsNullOrEmpty(department.DepartmentName))
            {
                throw new MISAexception("Tên phòng ban không được để trống");
            }
            // nếu ok thì thực hiện thêm
            else
            {
                var res = respostory.Insert(department);
                return res;
            }

        }

        public override int? Update(Department department)
        {
            // validate dữ liệu
            if (string.IsNullOrEmpty(department.DepartmentName))
            {
                throw new MISAexception("Tên phòng ban không được để trống");
            }
            // nếu ok thì cập nhật
            else
            {
                var res = respostory.Update(department);
                return res;
            }
        }
    }
}
