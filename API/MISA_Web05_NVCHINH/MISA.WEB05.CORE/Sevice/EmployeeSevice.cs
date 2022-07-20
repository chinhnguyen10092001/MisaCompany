using Microsoft.AspNetCore.Http;
using MISA.WEB05.COMMON.Model;
using MISA.WEB05.CORE.Exceptions;
using MISA.WEB05.CORE.Interface.Repostory;
using MISA.WEB05.CORE.Interface.Sevice;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MISA.WEB05.CORE.Sevice
{
    public class EmployeeSevice :BaseSevice<Employee> ,IEmployeeSevice
    {
        IEmployeeRepostory repostory;
        string msg = null;
        public EmployeeSevice(IEmployeeRepostory repostory):base(repostory)
        {
            this.repostory = repostory;
        }
        // xử lý trước khi export
        public Stream ExportExcel(CancellationToken cancellationToken)
        {
            var list = repostory.Get();
            Stream stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                // thêm sheet cho file
                var workSheet = package.Workbook.Worksheets.Add("Danh sách nhân viên");
                // khởi tạo rowData và stt;
                int rowIndex = 4;
                int stt = 1;
                // Megre theo định dạng
                using (var range = workSheet.Cells["A1:I1"])
                {
                    range.Merge = true;
                    range.Value = "DANH SÁCH NHÂN VIÊN";
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    range.Style.Font.Name = "Arial";
                    range.Style.Font.Size = 16;
                }
                workSheet.Cells["A2:I2"].Merge = true;
                // set màu cho tiêu đề
                var couleur = System.Drawing.Color.FromArgb(170, 170, 170);
                workSheet.Cells["A3:I3"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                workSheet.Cells["A3:I3"].Style.Fill.BackgroundColor.SetColor(couleur);


                // Tạo tiêu đề
                workSheet.Cells[3, 1].Value = "STT";
                workSheet.Cells[3, 2].Value = "Mã nhân viên";
                workSheet.Cells[3, 3].Value = "Tên nhân viên";
                workSheet.Cells[3, 4].Value = "Giới tính";
                workSheet.Cells[3, 5].Value = "Ngày sinh";
                workSheet.Cells[3, 6].Value = "Chức danh";
                workSheet.Cells[3, 7].Value = "Tên đơn vị";
                workSheet.Cells[3, 8].Value = "Số tài khoản";
                workSheet.Cells[3, 9].Value = "Tên ngân hàng";
                // thiết lập độ dài
                workSheet.Cells[3, 1].AutoFitColumns(5, 5);
                workSheet.Cells[3, 2].AutoFitColumns(15, 15);
                workSheet.Cells[3, 3].AutoFitColumns(30, 40);
                workSheet.Cells[3, 4].AutoFitColumns(10, 10);
                workSheet.Cells[3, 5].AutoFitColumns(15, 15);
                workSheet.Cells[3, 6].AutoFitColumns(20, 30);
                workSheet.Cells[3, 7].AutoFitColumns(30, 40);
                workSheet.Cells[3, 8].AutoFitColumns(20, 30);
                workSheet.Cells[3, 9].AutoFitColumns(20, 30);
                for (int i = 1; i <= 9; ++i)
                {
                    // css cho tiêu đề
                    workSheet.Cells[3, i].Style.Font.Name = "Arial";
                    workSheet.Cells[3, i].Style.Font.Bold = true;
                    workSheet.Cells[3, i].Style.Font.Size = 10;
                    workSheet.Cells[3, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    workSheet.Cells[3, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[3, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[3, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[3, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                }
                // nạp dữ liệu
                foreach (var emp in list)
                {
                    var arrayDate = emp.DateOfBirth.ToString().Split(" ");
                    //nạp dữ liệu cho từng ô của từng dòng
                    workSheet.Cells[rowIndex, 1].Value = stt++;
                    workSheet.Cells[rowIndex, 2].Value = emp.EmployeeCode;
                    workSheet.Cells[rowIndex, 3].Value = emp.EmployeeName;
                    workSheet.Cells[rowIndex, 4].Value = emp.GenderName;
                    workSheet.Cells[rowIndex, 5].Value = arrayDate[0];
                    workSheet.Cells[rowIndex, 6].Value = emp.PositionName;
                    workSheet.Cells[rowIndex, 7].Value = emp.DepartmentName;
                    workSheet.Cells[rowIndex, 8].Value = emp.BankAccountNumber;
                    workSheet.Cells[rowIndex, 9].Value = emp.BankName;
                    // css cho bảng
                    for (int i = 1; i <= 9; ++i)
                    {
                        workSheet.Cells[rowIndex, i].Style.Font.Name = "Times New Roman";
                        workSheet.Cells[rowIndex, i].Style.Font.Size = 11;
                        workSheet.Cells[rowIndex, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[rowIndex, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[rowIndex, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        workSheet.Cells[rowIndex, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    }
                    // thiết lập đọ dài và căn lề cho cells
                    workSheet.Cells[rowIndex, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    workSheet.Cells[rowIndex, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    rowIndex++;
                }
                package.Save();
            }
            return stream;
        }
        // validate thêm nhân viên
        public override int? Insert(Employee employee)
        {
            if (check_vali(employee))
            {
                if (repostory.Check_IdExit(employee.EmployeeCode) == false)
                {
                    throw new MISAexception("Mã nhân viên bị trùng");
                }
                return repostory.Insert(employee);
            }
            else
            {
                throw new MISAexception(msg);
            }
        }
        // validate sửa nhân viên
        public override int? Update(Employee employee)
        {
            if (check_vali(employee))
            {
                return repostory.Update(employee);
            }
            else
            {
                throw new MISAexception(msg);
            }
        }
        // base validate
        private bool check_vali(Employee employee)
        {
            bool check = true;
            // Check mã nhân viên
            if (string.IsNullOrEmpty(employee.EmployeeCode))
            {
                check = false;
                msg=msg+"Mã nhân viên không được để trống,";
            }
            // check tên nhân viên
            if (string.IsNullOrEmpty(employee.EmployeeName))
            {
                check = false;
                
                msg=msg+"Tên nhân viên không được để trống,";
            }
            // check phòng ban
            if (string.IsNullOrEmpty(employee.DepartmentId.ToString()))
            {
                check = false;
                msg=msg+"Tên phòng ban không được để trống,";
            }
            if(employee.Email!=null&&!Regex.IsMatch(employee.Email, "^\\S+@\\S+\\.\\S+$"))
            {
                check = false;
                msg=msg+"Email không hợp lệ,";
            }
            // bat loi ngay thang
            if (employee.DateOfBirth!= null)
            {
                DateTime res = (DateTime)employee.DateOfBirth;
                if (res > DateTime.Now)
                {
                    check = false;
                    msg = msg + $"Ngày tháng không hợp lệ {res.ToString().Split(" ")[0]}";
                }
            }
            return check;
        }
    }
    
}
