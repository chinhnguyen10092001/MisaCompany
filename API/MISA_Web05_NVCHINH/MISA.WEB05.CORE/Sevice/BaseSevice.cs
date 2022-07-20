using MISA.WEB05.CORE.Interface.Repostory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB05.CORE.Sevice
{
    public class BaseSevice<T>
    {
        IBaseRepostory<T> repostory;
        public BaseSevice(IBaseRepostory<T> repostory)
        {
            // tiêm dữ liệu
            this.repostory = repostory;
        }
        // Kiểm tra thêm dữ liệu
        public virtual int? Insert(T en)
        {
            return repostory.Insert(en);
        }
        // kiểm tra sửa dữ liệu
        public virtual int? Update(T en)
        {
            return repostory.Update(en);
        }

    }
}
