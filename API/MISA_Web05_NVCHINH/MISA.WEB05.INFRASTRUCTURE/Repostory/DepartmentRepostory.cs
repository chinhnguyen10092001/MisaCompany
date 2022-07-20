using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;
using MISA.WEB05.COMMON.Model;
using MISA.WEB05.CORE.Interface.Repostory;
using MISA.WEB05.INFRASTRUCTURE.Repostory;

namespace MISA.WEB05.INFRASTRUCTURE
{
    public class DepartmentRepostory:BaseRepostory<Department>,IDepartmentRepostory 
    {
        // lấy toàn bộ bản ghi
        public IEnumerable<Department> Get()
        {
            var res = GetAllData("Proc_SelectDepartment");
            return res;
        }
        // Lấy đối tượng theo khóa chính
        public Department GetbyId(Guid id)
        {
            var res = GetbyId("DepartmentId",id);
            return res;
        }
        // Thêm mới đối tượng
        public int Insert(Department department)
        {
            var res = InsertData("Proc_InsertDepartment", department);
            return res;
        }
        //Cập nhật đối tượng
        public int Update(Department department)
        {
            var res = UpdateData("Proc_UpdateDepartment", department);
            return res;
        }
        // Kiểm tra trùng khóa chính
        public bool Check_IdExit(string id)
        {
            return Check_IdExit(id, "Department","Id");
        }
        // Xóa đối tượng
        public int Delete(Guid id)
        {
            return DeleteData("Proc_DeleteDepartment", id, "DepartmentId");
        }
    }
}
