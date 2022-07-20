using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB05.INFRASTRUCTURE
{
    public class BaseConnect
    {
        public static new MySqlConnection MysqlConnect()
        {
            // khai bao thong tin csdl
            var connectionString = "Host=3.0.89.182;" +
                "Port=3306;" +
                "User ID=dev;" +
                "Password=12345678;" +
                "Database= MISA.WEB05.NVCHINH; AllowUserVariables = True";
            //khoi tao ket noi
            var sqlConnection = new MySqlConnection(connectionString) ;
            return sqlConnection;
        }
    }
}
