using MISA.WEB05.COMMON.Model;
using MISA.WEB05.CORE.Interface.Repostory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB05.INFRASTRUCTURE.Repostory
{
    public class PositionRepostory : BaseRepostory<Positions>, IPositionRepostory
    {
        // Kiểm tra trùng khóa chính
        public bool Check_IdExit(string id)
        {
            return Check_IdExit(id, "Position", "Id");
        }
        // xóa đối tượng
        public int Delete(Guid id)
        {
            var res = DeleteData("Proc_DeletePosition", id,"PositionId");
            return res;
        }
        // lấy toàn bộ bản ghi
        public IEnumerable<Positions> Get()
        {
            return GetAllData("Proc_SelectPosition");
        }
        // lấy đối tượng theo khóa chính
        public Positions GetbyId(Guid id)
        {
            var res = GetbyId("PositionId", id);
            return res;
        }
        //Thêm mới đối tượng
        public int Insert(Positions en)
        {
            var res = InsertData("Proc_InsertPosition", en);
            return res;
        }
        //Cập nhật đối tượng
        public int Update(Positions en)
        {
            var res = UpdateData("Proc_UpdatePosition", en);
            return res;
        }
    }
}
