using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MISA.WEB05.COMMON.Model;
using MISA.WEB05.CORE.Interface.Sevice;
using MISA.WEB05.CORE.Interface.Repostory;
using Newtonsoft.Json;
using MISA.WEB05.CORE.Exceptions;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Threading;

namespace MISA.WEB05.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        /// <summary>
        /// Nguyen van CHinh: Khoi tao cho interface
        /// </summary>
        IEmployeeSevice sevice;
        IEmployeeRepostory repostory;
        public EmployeeController(IEmployeeSevice sevice, IEmployeeRepostory repostory)
        {
            // tiêm giá trị
            this.sevice = sevice;
            this.repostory = repostory;
        }
        /// <summary>
        /// Lấy toàn bộ bản ghi
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetEmployee()
        {
            try
            {
                var res = repostory.Get();
                // định dạng tên thuộc tính giống entyti
                var json = JsonConvert.SerializeObject(res, Formatting.Indented);
                return Ok(json);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// Tìm kiếm bằng mã nhân viên
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Employee GetById(Guid id)
        {
            var res = repostory.GetbyId(id);
            return res;
        }
        /// <summary>
        /// Xóa 1 nhân viên
        /// </summary>
        /// <param name="id">ID cần xóa</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public int DeleteEmployee(Guid id)
        {
            var res = repostory.Delete(id);
            return res;
        }
        /// <summary>
        /// Xóa nhiều nhân viên
        /// </summary>
        /// <param name="arrayId"></param>
        /// <returns></returns>
        [HttpDelete]
        public int DeleteEmployees([FromBody] ArrayDeleteId arrayId)
        {
            string listId = arrayId.listId;
            var res = repostory.DeleteEmployees(listId);
            return res;
        }
        /// <summary>
        /// Thêm mới 1 nhân viên
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult InsertEmployee(Employee employee)
        {
            try
            {
                var res= sevice.Insert(employee);
                return StatusCode(201,res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        [HttpGet("NewEmployeeCode")]
        public string get_NewId()
        {
            var res =repostory.get_NewId();
            return res;
        }
        /// <summary>
        /// Cập nhật 1 nhân viên
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateEmployee(Employee employee)
        {
            try
            {
                var res = sevice.Update(employee);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }
        /// <summary>
        /// Tìm kiếm theo mã,tên nhân viên, phân trang
        /// </summary>
        /// <param name="txtSeach">Mã,tên nhân viên cần tìm kiếm</param>
        /// <param name="pageSize">Số bản ghi/trang</param>
        /// <param name="pageNumber">Số thứ tự trang</param>
        /// <returns>A string status</returns>
        [HttpGet("fillter")]
        public IActionResult? FillterEmployee(string txtSeach, int pageSize, int pageNumber)
        {
            // nếu text seach == null thì trả về "" vì null không thể so sánh trong database
            if (txtSeach == null)
            {
                txtSeach = "";
            }
            int totalRecord = 0;
            int totalPage = 0;
            // nếu không truyền pagesize và pageNumber thì trả về 10 và 1
            if (pageSize == 0)
            {
                pageSize = 10;
            }
            if(pageNumber == 0)
            {
                pageNumber = 1;
            }
            // lấy dữ liệu
            var res = repostory.Fillter(txtSeach, pageSize, pageNumber, ref totalRecord,ref totalPage);
            var d = new {
                TotalPage = totalPage,
                TotalRecord = totalRecord,
                Data = res
            };
            var json = JsonConvert.SerializeObject(d, Formatting.Indented);
            return Ok(json);
        }
        /// <summary>
        /// Export file excel
        /// </summary>
        /// <returns></returns>
        
        [HttpGet("exportv2")]
        public async Task<IActionResult> ExportV2(CancellationToken cancellationToken)
        {
            // query data from database
            await Task.Yield();
            var stream = sevice.ExportExcel(cancellationToken);
            stream.Position = 0;
            string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
        // resfull
        private IActionResult HandleException(Exception ex)
        {
            // nếu lỗi validate thì trả về 400
            if(ex is MISAexception)
            {
                var res = new
                {
                    userMsg = ex.Message
                };
                return StatusCode(400, res);
            }
            //nếu không trả về 500
            else
            {
                var res = new
                {
                    devMsg= ex.Message,
                    userMsg = "Lỗi hệ thống, vui lòng liên hệ MISA"
                };
                return StatusCode(500, res);
            }
        }
    }
}
