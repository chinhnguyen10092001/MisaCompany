using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;
using MISA_Web05_NVCHINH.Model;

namespace MISA_Web05_NVCHINH.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        public static new MySqlConnection MysqlConnect()
        {
            // khai bao thong tin csdl
            var connectionString = "Host=3.0.89.182;" +
                "Port=3306;" +
                "User ID=dev;" +
                "Password=12345678;" +
                "Database= MISA.WEB05.NVCHINH;";
            //khoi tao ket noi
            var sqlConnection = new MySqlConnection(connectionString);
            return sqlConnection;
        }
        [HttpGet]
        public IActionResult GetDepartments()
        {
            try
            {
                

                //lay su lieu
                var sqlQuery = "Select*from Department";
                var departments = MysqlConnect().Query<Department>(sqlQuery);
                //tra du lieu ve cho client
                return Ok(departments);// ok tra ve ma 200
            }
            catch (Exception)
            {
                var res = new
                {
                    devMsg = new Exception().Message,
                    userMsg = "Co loi say ra vui long lien he MISA"
                };
                return StatusCode(500, res);
            }
        }
        // tim kiem theo ID (dung queryString)
        [HttpGet("seach")]
        public IActionResult GetDepartmentById(Guid departmentId)
        {
            try
            {
                //lay su lieu
                var sqlQuery = $"Select*from Department where DepartmentId='{departmentId.ToString()}'";
                var departments = MysqlConnect().QueryFirstOrDefault<Department>(sqlQuery);
                //tra du lieu ve cho client
                return Ok(departments);// ok tra ve ma 200
            }
            catch (Exception)
            {
                var res = new
                {
                    devMsg = new Exception().Message,
                    userMsg = "Co loi say ra vui long lien he MISA"
                };
                return StatusCode(500,res);
            }
        }
        // them moi 1 ban ghi
        [HttpPost]
        public IActionResult PostDepartment(Department department)
        {
            try
            {
                //validate
                //1. ten phong ban khong duoc de trong
                if (string.IsNullOrEmpty(department.DepartmentName))
                {
                    var res = new
                    {
                        userMsg = "Ten phong ban khong duoc de trong!"
                    };
                    return BadRequest(res);
                }
                //lay su lieu
                var sqlQuery = "Proc_InsertDepartment";
                var departments = MysqlConnect().Execute(sqlQuery,param:department,commandType:System.Data.CommandType.StoredProcedure);
                //tra du lieu ve cho client
                return StatusCode(201,department);// ok tra ve ma 200
            }
            catch (Exception)
            {
                var res = new
                {
                    devMsg = new Exception().Message,
                    userMsg = "Co loi say ra vui long lien he MISA"
                };
                return StatusCode(500, res);
            }
        }
    }
}
