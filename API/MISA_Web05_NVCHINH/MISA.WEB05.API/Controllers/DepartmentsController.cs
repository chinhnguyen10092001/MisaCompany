using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;
using MISA.WEB05.COMMON.Model;
using MISA.WEB05.CORE.Interface.Sevice;
using MISA.WEB05.CORE.Interface.Repostory;
using Newtonsoft.Json;
using MISA.WEB05.CORE.Exceptions;

namespace MISA_Web05_NVCHINH.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        IDepartmentSevice sevice;
        IDepartmentRepostory repostory;
        public DepartmentsController(IDepartmentSevice sevice, IDepartmentRepostory repostory)
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
        public IActionResult? GetDepartments()
        {
            var department = repostory.Get();
            // Lấy định dạng theo entyti
            var json=JsonConvert.SerializeObject(department, Formatting.Indented);
            return Ok(json);
        }
        /// <summary>
        /// Lấy phòng ban theo Id
        /// </summary>
        /// <param name="departmentId">Id phòng ban</param>
        /// <returns></returns>
        [HttpGet("seach")]
        public Department GetDepartmentById(Guid departmentId)
        {
            var department = repostory.GetbyId(departmentId);
            return department;
        }
        /// <summary>
        /// Thêm mới 1 phòng ban
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult PostDepartment(Department department)
        {
            try
            {
                var res = sevice.Insert(department);
                return StatusCode(201, res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// Cập nhật 1 phòng ban
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateDepartment(Department department)
        {
            try
            {
                //validate
                //nhet thang secive vao tra du lieu ve cho client
                var res = sevice.Update(department);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// Xóa 1 phòng ban
        /// </summary>
        /// <param name="id">Id phòng ban cần xóa</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteDepartment(Guid id)
        {
            try
            {
                var res= repostory.Delete(id);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        // resfull
        private IActionResult HandleException(Exception ex)
        {
            // nếu ex validate dữ liệu thì trả về res 400
            if (ex is MISAexception)
            {
                var res = new
                {
                    userMsg = ex.Message
                };
                return StatusCode(400, res);
            }
            // nếu khoogn trả về 500
            else
            {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = "Lỗi hệ thống, vui lòng liên hệ MISA"
                };
                return StatusCode(500, res);
            }
        }
    }
}
