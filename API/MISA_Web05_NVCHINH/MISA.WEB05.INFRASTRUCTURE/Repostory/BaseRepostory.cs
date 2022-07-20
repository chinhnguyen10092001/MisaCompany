using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Dapper;
using MISA.WEB05.CORE.Interface.Repostory;

namespace MISA.WEB05.INFRASTRUCTURE.Repostory
{
    public class BaseRepostory <T>
    {
        public string tableName = typeof(T).Name;
        public MySqlConnection conn = BaseConnect.MysqlConnect();
        //Khoi tao
        public IEnumerable<T> GetAllData(string Proc_name)
        {
            try
            {
                var sqlQuery = Proc_name;
                var res = conn.Query<T>(sqlQuery,commandType:System.Data.CommandType.StoredProcedure);
                return res;
            }
            catch (Exception ex)
            {

                throw ;
            }
            finally
            {
                conn.Close();
            }
        }
        // tim kiem theo id
        public T GetbyId(string columName,Guid id)
        {
            try
            {
                string sqlQuery = $"select * from   {tableName} where  {columName} = '{id}' "; ;
                var res = conn.QueryFirstOrDefault<T>(sqlQuery);
                return res;
            }
            catch (Exception)
            {

                throw ;
            }
            finally
            {
                conn.Close();
            }
        }
        // them doi tuong
        public int InsertData(string name_Proc,T table)
        {
            try
            {
                string sqlQuery = name_Proc ;
                var res = conn.Execute(sqlQuery,param:table,commandType:System.Data.CommandType.StoredProcedure);
                return res;
            }
            catch (Exception)
            {

                throw ;
            }
            finally
            {
                conn.Close();
            }
        }
        // sua thong tin ndoi tuong
        public int UpdateData(string name_Proc, T table)
        {
            try
            {
                string sqlQuery = name_Proc;
                var res = conn.Execute(sqlQuery, param: table, commandType: System.Data.CommandType.StoredProcedure);
                return res;
            }
            catch (Exception)
            {

                throw ;
            }
            finally
            {
                conn.Close();
            }
        }
        // xoa doi tuong
        public int DeleteData(string name_Proc, Guid id,string element)
        {
            try
            {
                string sqlQuery = name_Proc;
                DynamicParameters param = new DynamicParameters();
                param.Add($"@{element}", id);
                var res = conn.Execute(sqlQuery, param: param, commandType: System.Data.CommandType.StoredProcedure);
                return res;
            }
            catch (Exception)
            {

                throw ;
            }
            finally
            {
                conn.Close();
            }

        }
        // check khoa chinh trung
        public bool Check_IdExit(string id,string table,string element)
        {
            try
            {
                var sql = $"Select * from {table} where {table}{element}='{id}'";
                var res = conn.QueryFirstOrDefault(sql);
                if (res == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
             finally
            {
                conn.Close();
            }
        }
        // Xóa nhiều đối tượng
        public int DeleteDatas(string name_Proc,string arrayId,string element)
        {
            try
            {
                string sqlQuery = name_Proc;
                DynamicParameters param = new DynamicParameters();
                param.Add($"@{element}", arrayId);
                var res = conn.Execute(sqlQuery, param: param, commandType: System.Data.CommandType.StoredProcedure);
                return res;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

        }
    }
}
