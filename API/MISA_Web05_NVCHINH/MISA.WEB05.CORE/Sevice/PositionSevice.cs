using MISA.WEB05.COMMON.Model;
using MISA.WEB05.CORE.Exceptions;
using MISA.WEB05.CORE.Interface.Repostory;
using MISA.WEB05.CORE.Interface.Sevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB05.CORE.Sevice
{
    public class PositionSevice : BaseSevice<Positions>,IPositionSevice
    {
        IPositionRepostory repostory;
        public PositionSevice(IPositionRepostory repostory):base(repostory)
        {
            this.repostory = repostory;
        }
        public override int? Insert(Positions en)
        {
            // validate dữ liệu
            if (string.IsNullOrEmpty(en.PositionName))
            {
                throw new MISAexception("Tên chức vụ không được để trống");
            }
            // nếu ok thì thực hiện thêm dữ liệu
            else
            {
                var res = repostory.Insert(en);
                return res;
            }
        }

        public override int? Update(Positions en)
        {
            // validate dữ liệu
            if (string.IsNullOrEmpty(en.PositionName))
            {
                throw new MISAexception("Tên chức vụ không được để trống");
            }
            // Nếu ok thì cập nhật
            else
            {
                var res = repostory.Update(en);
                return res;
            }
        }
    }
}
