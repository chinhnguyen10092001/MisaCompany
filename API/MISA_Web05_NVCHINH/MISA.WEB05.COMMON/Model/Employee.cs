using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.WEB05.COMMON.Model
{
    public class Employee:Base
    {
      // khởi tạo giá trị cho genderName
        public void setGenderName()
        {
            if (Gender == 1)
            {
                GenderName = "Nam";
            }
            else if (Gender == 0)
            {
                GenderName = "Nữ";
            }
            else if (Gender == 2)
            {
                GenderName = "Khác";
            }
            else
            {
                GenderName = null;
            }
        }
        // Khóa chính
        public Guid EmployeeId { get; set; }
        // mã nhân viên
        public string EmployeeCode { get; set; }
        // tên nhân viên
        public string EmployeeName { get; set; }
        // giới tính (0-nữ,1-nam,2-khác)
        private int? gender;
        public int? Gender { get { return gender; } set {gender=value;setGenderName(); } }
        // tên giới tính
        public string? GenderName { get; set; }
        // ngày sinh
        public DateTime? DateOfBirth { get; set; }
        // số điện thoại
        public string? PhoneNumber { get; set; }
        // email
        public string? Email { get; set; }
        // địa chỉ
        public string? Address { get; set; }
        // số CMND
        public string? IdentityNumber { get; set; }
        // Nới cấp
        public DateTime? IdentityDate { get; set; }
        // Ngày cấp
        public string? IdentityPlace { get; set; }
        // id phòng ban
        public Guid DepartmentId { get; set; }
        // tên phòng ban
        public string  DepartmentName { get; set; }
        // tên chức vụ
        public string? PositionName { get; set; }
        // số cố định
        public string? TelephoneNumber { get; set; }
        // số tài khoản ngân hàng
        public string? BankAccountNumber { get; set; }
        //tên ngân hàng
        public string? BankName { get; set; }
        // chi nhánh ngân hàng
        public string? BankBranchName { get; set; }
        
    }
}
