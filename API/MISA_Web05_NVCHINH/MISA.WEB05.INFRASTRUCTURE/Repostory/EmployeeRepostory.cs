using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Dapper;
using MISA.WEB05.INFRASTRUCTURE.Repostory;
using MISA.WEB05.CORE.Interface.Repostory;
using MISA.WEB05.COMMON.Model;
using System.Text.RegularExpressions;
using System.Data;

namespace MISA.WEB05.INFRASTRUCTURE.Repostory
{
    public class EmployeeRepostory : BaseRepostory<Employee>, IEmployeeRepostory
    {
        // hàm xóa đối tượng
        public int Delete(Guid id)
        {
            var res = DeleteData("Proc_DeleteEmployee",id,"EmployeeId");
            return res;
        }
        //Lấp toàn bộ đối tượng
        public IEnumerable<Employee> Get()
        {
            var res = GetAllData("Proc_SelectEmployee");
            return res;
        }
        //Lấy đối tượng theo Id
        public Employee GetbyId(Guid id)
        {
            var res = GetbyId("EmployeeId",id);
            return res;
        }
        //Thêm mới đối tượng
        public int Insert(Employee employee)
        {
            var res=InsertData("Proc_InsertEmpolyee", employee);
            return res;
        }
        //Cập nhật đối tượng
        public int Update(Employee employee)
        {
            return base.UpdateData("Proc_UpdateEmployee", employee);
        }
        //Kiểm tra trùng khóa chính
        public bool Check_IdExit(string id)
        {
            return base.Check_IdExit(id, "Employee","Code");
        }
        // Tách ký tự số trong chuỗi
        public int get_NumberId(string id)
        {
            id = Regex.Match(id, @"\d+").Value;
            return Int32.Parse(id);
        }
        //Lấy mã mới nhân viên
        public string get_NewId()
        {
            string id = null;
            // lấy tất cả nhân viên trong database
            List<Employee> res= (List<Employee>)GetAllData("Proc_SelectEmployee");
            //sắp xếp theo ký tự số (Mã) giảm dần
            if (res.Count == 0)
            {
                id = "NV0001";
            }
            else
            {
                res.Sort((x, y) => get_NumberId(y.EmployeeCode).CompareTo(get_NumberId(x.EmployeeCode)));
                // lấy ra mã lớn nhất
                id = res[0].EmployeeCode;
                int i = get_NumberId(id);
                // tăng mã lên 1 để không bị trùng
                int j = i + 1;
                // tạo mã mới
                id = "NV" + j;
            }
            return id;
        }
        // Tìm kiếm và phân trang dữ liệu
        public IEnumerable<Employee> Fillter(string txtSeach,int pageSize,int pageNumber, ref int totalRecord,ref int totalPage)
        {
            var sqlQuery = "Proc_FillterEmployee";
            DynamicParameters param = new DynamicParameters();
            //add param Input
            param.Add("@TxtSearch", txtSeach);
            param.Add("@PageSize", pageSize);
            param.Add("@Offset", pageNumber);
            //add param Output
            param.Add("@TotalRecord",dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("@TotalPage",dbType: DbType.Int32, direction: ParameterDirection.Output);
            var res = conn.Query<Employee>(sqlQuery,param:param, commandType: System.Data.CommandType.StoredProcedure);
            //Lấy kết quả trả về biến Out trong procedue.
            totalRecord= param.Get<int>("@TotalRecord");
            totalPage = param.Get<int>("@TotalPage");
            return res;
        }
        //Xóa nhiều nhân viên
        public int DeleteEmployees(string arrayId)
        {
            var res = DeleteDatas("Proc_DeleteEmployees", arrayId, "listID");
            return res;
        }
        
    }
}
