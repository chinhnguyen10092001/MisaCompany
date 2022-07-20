using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB05.COMMON.Model;
using MISA.WEB05.CORE.Exceptions;
using MISA.WEB05.CORE.Interface.Repostory;
using MISA.WEB05.CORE.Interface.Sevice;
using Newtonsoft.Json;
using System;

namespace MISA.WEB05.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        IPositionSevice sevice;
        IPositionRepostory repostory;
        public PositionController(IPositionSevice sevice, IPositionRepostory repostory)
        {
            this.sevice = sevice;
            this.repostory = repostory;
        }
        /// <summary>
        /// Lấy toàn bộ bản ghi
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        [HttpGet]
        public IActionResult? Get()
        {
            try
            {
                var department = repostory.Get();
                var json = JsonConvert.SerializeObject(department, Formatting.Indented);
                return Ok(json);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }
        /// <summary>
        /// Lấy phòng ban theo Id
        /// </summary>
        /// <param name="Id">Id chức vụ</param>
        /// <returns></returns>
        [HttpGet("seach")]
        public Positions GetById(Guid Id)
        {
            var res = repostory.GetbyId(Id);
            return res;
        }
        /// <summary>
        /// Thêm mới 1 chức vụ
        /// </summary>
        /// <param name="positions"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult? Insert(Positions positions)
        {
            try
            {
                //validate
                //nhet thang secive vao tra du lieu ve cho client
                var res = sevice.Insert(positions);
                return StatusCode(201, res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// Cập nhật 1 chức vụ
        /// </summary>
        /// <param name="positions"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult? Update(Positions positions)
        {
            try
            {
                //validate
                //nhet thang secive vao tra du lieu ve cho client
                var res = sevice.Update(positions);
                return StatusCode(201, res);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        /// <summary>
        /// Xóa 1 chức vụ
        /// </summary>
        /// <param name="id">Id chức vụ cần xóa</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var res = repostory.Delete(id);
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
            if (ex is MISAexception)
            {
                var res = new
                {
                    userMsg = ex.Message
                };
                return StatusCode(400, res);
            }
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
