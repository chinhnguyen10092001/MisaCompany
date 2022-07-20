using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB05.CORE.Interface.Repostory
{
    public interface IBaseRepostory<T>
    {
        // Lây toàn bộ dữ liệu
        IEnumerable<T> Get();
        // lấy đối tượng theo khóa chính
        T GetbyId(Guid id);
        // Thêm mới đỗi tượng
        int Insert(T en);
        // Cập nhật đối tượng
        int Update(T en);
        // Xóa đối tượng
        int Delete(Guid id);
        // Check trùng khóa chính
        bool Check_IdExit(string id);
        
    }
}
