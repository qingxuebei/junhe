﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using MyData;
namespace MyData
{
    public class DataBase
    {
        public static string OleDbClass()
        {
            return ConfigurationManager.AppSettings["OleDbClass"];
        }
        public static OleDbConnection Conn()
        {
            string lb = OleDbClass();
            string constr = "";

            if (lb == "0")
                constr = ConfigurationManager.AppSettings["SqlConnStr"];
            else if (lb == "1")
                constr = ConfigurationManager.AppSettings["OracleConnStr"];
            else if (lb == "2")
                constr = ConfigurationManager.AppSettings["AccessConnStr"];
            OleDbConnection Conn = new OleDbConnection(constr);
            return Conn;
        }


        #region 返会数据对象

        /// <summary> 
        /// 运行SQL语句返回DataReader 
        /// </summary> 
        /// <param name="str_sql"></param> 
        /// <returns>SqlDataReader对象.</returns> 
        public static OleDbDataReader Base_Dr(string str_sql, OleDbConnection Conn)
        {
            OleDbCommand Cmd = new OleDbCommand(str_sql, Conn);
            OleDbDataReader Dr;
            try
            {
                Dr = Cmd.ExecuteReader();
            }
            catch
            {
                throw new Exception(str_sql);
            }
            return Dr;
        }


        /// <summary> 
        /// 返回DataTable对象
        /// </summary> 
        /// <returns></returns> 
        public static DataTable Base_dt(string str_sql)
        {
            OleDbConnection conn = DataBase.Conn();
            OleDbDataAdapter Da = new OleDbDataAdapter(str_sql, conn);
            conn.Open();
            DataTable Dt = new DataTable();

            try
            {
                Da.Fill(Dt);
            }
            catch (Exception Err)
            {
                throw Err;
            }
            conn.Close();
            return Dt;
        }
        public static List<T> Base_list<T>(String str_sql) where T : new()
        {
            DataTable dt = Base_dt(str_sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                return MyData.Utils.ConvertToList<T>(dt).ToList();
            }
            return null;
        }

        public static T Base_getFirst<T>(String str_sql) where T : new()
        {
            DataTable dt = Base_dt(str_sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                return MyData.Utils.GetModelByDataRow<T>(dt.Rows[0]);
            }
            return new T();
        }

        public static DataTable Base_dt(string str_sql, OleDbTransaction tr)
        {
            OleDbDataAdapter Da = new OleDbDataAdapter(str_sql, tr.Connection);
            Da.SelectCommand.Transaction = tr;
            DataTable Dt = new DataTable();

            try
            {
                Da.Fill(Dt);
            }
            catch (Exception Err)
            {
                throw Err;
            }
            return Dt;
        }
        public static List<T> Base_list<T>(String str_sql, OleDbTransaction tr) where T : new()
        {
            DataTable dt = Base_dt(str_sql, tr);
            if (dt != null && dt.Rows.Count > 0)
            {
                return MyData.Utils.ConvertToList<T>(dt).ToList();
            }
            return null;
        }

        public static T Base_getFirst<T>(String str_sql, OleDbTransaction tr) where T : new()
        {
            DataTable dt = Base_dt(str_sql, tr);
            if (dt != null && dt.Rows.Count > 0)
            {
                return MyData.Utils.GetModelByDataRow<T>(dt.Rows[0]);
            }
            return new T();
        }

        public static void Base_dt(string str_sql, System.Data.DataTable dt)
        {
            OleDbConnection conn = DataBase.Conn();
            OleDbDataAdapter Da = new OleDbDataAdapter(str_sql, conn);
            conn.Open();
            try
            {
                Da.Fill(dt);
            }
            catch (Exception Err)
            {
                throw Err;
            }
            conn.Close();
        }

        /// <summary>
        /// 查询数据集
        /// </summary>
        /// <param name="str_sql"></param>
        /// <param name="DS"></param>
        /// <param name="Tab_name"></param>
        public static void Base_dt(string str_sql, System.Data.DataSet DS, string Tab_name)
        {
            OleDbConnection conn = Conn();
            OleDbDataAdapter Da = new OleDbDataAdapter(str_sql, conn);
            conn.Open();
            try
            {
                Da.Fill(DS, Tab_name);
            }
            catch (Exception Err)
            {
                throw Err;
            }
            conn.Close();
        }

        #endregion

        #region 返回单值

        /// <summary>
        /// 执行cmd语句返回单个值
        /// </summary>
        /// <param name="str_sql">sql语句</param>
        public static string Base_Scalar(string str_sql)
        {
            object str;
            OleDbConnection conn = DataBase.Conn();
            OleDbCommand cmd = new OleDbCommand(str_sql, conn);
            conn.Open();

            try
            {
                str = cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                str = null;
            }
            conn.Close();
            if (str == null)
            {
                return "";
            }
            else
            {
                return str.ToString();
            }
        }

        /// <summary>
        /// 执行cmd语句返回单个值
        /// </summary>
        /// <param name="str_sql">sql语句</param>
        public static string Base_Scalar(string str_sql, OleDbTransaction tr)
        {
            object str;
            OleDbCommand cmd = new OleDbCommand(str_sql, tr.Connection);
            cmd.Transaction = tr;

            try
            {
                str = cmd.ExecuteScalar();

            }
            catch
            {
                str = null;
            }

            if (str == null)
            {
                return "";
            }
            else
            {
                return str.ToString();
            }
        }

        /// <summary> 
        /// 查询sql语句,返回记录个数
        /// </summary> 
        /// <param name="tablename">表名</param> 
        public static int Base_count(string tablename)
        {
            OleDbConnection conn = Conn();
            OleDbCommand cmd = new OleDbCommand("select count(*) from " + tablename, conn);
            conn.Open();
            int i;
            try
            {
                i = int.Parse(cmd.ExecuteScalar().ToString());
                conn.Close();
                return i;
            }
            catch
            {
                conn.Close();
                return -1;
            }
        }


        /// <summary> 
        /// 查询sql语句,返回记录个数
        /// </summary> 
        /// <param name="tablename">表名</param> 
        /// <param name="where">条件</param>
        public static int Base_count(string tablename, string where)
        {
            OleDbConnection conn = Conn();
            OleDbCommand cmd = new OleDbCommand("select count(*) from " + tablename + " where " + where, conn);
            conn.Open();
            int i;
            try
            {
                i = int.Parse(cmd.ExecuteScalar().ToString());
                conn.Close();
                return i;
            }
            catch (System.Exception ex)
            {
                conn.Close();
                return -1;
            }

        }


        /// <summary> 
        /// 查询sql语句,返回记录个数
        /// </summary> 
        /// <param name="tablename">表名</param> 
        /// <param name="where">条件</param>
        public static int Base_count(string str_sql, OleDbTransaction tr)//
        {
            OleDbCommand cmd = new OleDbCommand(str_sql, tr.Connection);
            cmd.Transaction = tr;

            int i;
            try
            {
                i = int.Parse(cmd.ExecuteScalar().ToString());

                return i;

            }
            catch
            {
                return -1;
            }

        }
        /// <summary>
        /// 执行SQL语句并返回受影响的行数
        /// </summary>
        /// <param name="str_sql">sql语句</param>
        /// <param name="tr"></param>
        /// <returns></returns>
        public static int Base_ExecuteNonQuery(string str_sql, OleDbTransaction tr)
        {
            OleDbCommand cmd = new OleDbCommand(str_sql, tr.Connection);
            cmd.Transaction = tr;
            int i;
            try
            {
                i = cmd.ExecuteNonQuery();

                return i;
            }
            catch
            {

                return -1;
            }



        }
        #endregion

        #region 执行SQL




        /// <summary>
        /// 更新大文本
        /// </summary>
        /// <param name="table"></param>
        /// <param name="col"></param>
        /// <param name="cont"></param>
        /// <param name="tr"></param>
        public static void Base_UPTxt(string table, string col, string cont, string where, OleDbTransaction tr)
        {
            string StrSql = "update " + table + " set " + col + "= ? where " + where;

            OleDbCommand cmd = new OleDbCommand(StrSql, tr.Connection);
            cmd.Transaction = tr;

            cmd.Parameters.Add(":" + col, OleDbType.VarChar, cont.Length, col).Value = cont;

            cmd.ExecuteNonQuery();

        }

        /// <summary>
        /// 执行cmd语句
        /// </summary>
        /// <param name="str_sql">sql语句</param>
        /// <param name="tr"></param>
        public static void Base_cmd(string str_sql, OleDbTransaction tr)
        {
            OleDbCommand cmd = new OleDbCommand(str_sql, tr.Connection);
            cmd.Transaction = tr;
            cmd.ExecuteNonQuery();
        }


        /// <summary> 
        /// 运行SQL语句 
        /// </summary> 
        /// <param name="SQL"></param> 
        public static bool Base_cmd(string str_sql)
        {
            bool ok;
            OleDbConnection conn = Conn();

            OleDbCommand Cmd = new OleDbCommand(str_sql, conn);
            conn.Open();
            OleDbTransaction tr;
            tr = conn.BeginTransaction();
            Cmd.Transaction = tr;
            try
            {
                Cmd.ExecuteNonQuery();
                tr.Commit();
                ok = true;
            }
            catch (System.Exception ex)
            {
                tr.Rollback();
                ok = false;
            }
            conn.Close();
            return ok;
        }


        /// <summary>
        /// 批量修改数据
        /// </summary>
        /// <param name="str_sql"></param>
        /// <param name="conn"></param>
        /// <param name="tr"></param>
        public static void Base_cmd(string[] str_sql, OleDbConnection conn, OleDbTransaction tr)
        {
            for (int i = 0; i < str_sql.Length; i++)
            {
                OleDbCommand cmd = new OleDbCommand(str_sql[i], conn);
                cmd.Transaction = tr;
                cmd.ExecuteNonQuery();
            }
        }

        #endregion


        /// <summary>
        /// 绑定
        /// </summary>
        /// <param name="gcode"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string Bind_SqlGcode(string gcode, string num)
        {
            string fhstr = " and " + gcode + "='" + num + "'";
            string f = "";
            string[] value = value = DataBase.Base_Scalar("select value1 from sys_config where title='福江汉岳'").Split('#');
            for (int i = 0; i < value.Length - 1; i++)
            {
                if (value[i].IndexOf(num.ToString() + ",") > -1)
                {
                    string[] va = value[i].Split(',');
                    f = " and ( ";
                    for (int j = 0; j < va.Length - 1; j++)
                    {
                        f += " " + gcode + "='" + va[j].ToString() + "' or";
                    }
                    fhstr = Utils.DelLastChar(f, "or") + ")";

                }
            }
            return fhstr;
        }

        #region 存储过程操作

        /// <summary>
        /// 执行无参数的存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <returns></returns>
        public static DataTable ZXCCGC(string storedProcName)
        {
            OleDbConnection sqlconn = DataBase.Conn();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = sqlconn;
            cmd.CommandText = storedProcName;
            cmd.CommandType = CommandType.StoredProcedure;
            OleDbDataAdapter dp = new OleDbDataAdapter(cmd);
            DataTable ds = new DataTable();
            dp.Fill(ds);
            return ds;
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>OleDbDataReader</returns>
        public static OleDbDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            OleDbConnection connection = DataBase.Conn();
            OleDbDataReader returnReader;
            connection.Open();
            OleDbCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.CommandType = CommandType.StoredProcedure;
            returnReader = command.ExecuteReader();
            return returnReader;
        }


        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            using (OleDbConnection connection = DataBase.Conn())
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                OleDbDataAdapter sqlDA = new OleDbDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }


        /// <summary>
        /// 构建 OleDbCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>OleDbCommand</returns>
        private static OleDbCommand BuildQueryCommand(OleDbConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            OleDbCommand command = new OleDbCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (OleDbParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
            return command;
        }

        /// <summary>
        /// 执行存储过程，返回影响的行数		
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns></returns>
        public static int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            using (OleDbConnection connection = DataBase.Conn())
            {
                int result;
                connection.Open();
                OleDbCommand command = BuildIntCommand(connection, storedProcName, parameters);
                rowsAffected = command.ExecuteNonQuery();
                result = (int)command.Parameters["ReturnValue"].Value;
                //Connection.Close();
                return result;
            }
        }

        /// <summary>
        /// 创建 OleDbCommand 对象实例(用来返回一个整数值)	
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>OleDbCommand 对象实例</returns>
        private static OleDbCommand BuildIntCommand(OleDbConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            OleDbCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new OleDbParameter("ReturnValue",
                OleDbType.Integer, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }
        #endregion

        /// <summary>
        /// 记录操作日志
        /// </summary>
        /// <param name="userid">操作人员</param>
        /// <param name="btable">操作数据表名</param>
        /// <param name="bcode">操作数据CODE</param>
        /// <param name="sm">说明</param>
        /// <param name="czlx">操作类型(增加、删除、修改、查询)</param>
        /// <param name="bz">备注</param>
        public static void Base_czrz(string userid, string btable, string bcode, string sm, string czlx, string bz)
        {

            string HostName = Dns.GetHostName();
            IPHostEntry MyEntry = Dns.GetHostByName(Dns.GetHostName());
            IPAddress MyAddress = new IPAddress(MyEntry.AddressList[0].Address);

            Base_cmd("insert into sys_czrz(userid,btable,bcode,sm,czlx,bz,cztime,ip)values('" + userid + "','" + btable + "','" + bcode + "','" + sm + "','" + czlx + "','" + bz + "',getdate(),'" + MyAddress + "')");
        }

        /// <summary>
        /// 查询前N条数据
        /// </summary>
        /// <param name="sql">查询sql语句</param>
        /// <param name="dqy">当前页</param>
        /// <param name="num">每页记录条数</param>
        /// <returns></returns>
        public static string TopQuery(string sql, string dqy, string num)
        {
            string zys = "", topnum = "", colums = "", mess = "N";
            int count = DataBase.Base_count("(" + sql + ")a", " 1=1 ");
            if (String.IsNullOrEmpty(dqy))
                dqy = "1";
            if (String.IsNullOrEmpty(num))
                num = "10";
            if (count % Convert.ToInt32(num) == 0)
                zys = (count / Convert.ToInt32(num)).ToString();
            else
                zys = ((count / Convert.ToInt32(num)) + 1).ToString();
            colums = sql.Substring(6, sql.Length - 6);
            topnum = (Convert.ToInt32(dqy) * Convert.ToInt32(num)).ToString();
            DataTable dt = DataBase.Base_dt(" select top(" + topnum + ") '" + zys + "' as zys,'" + dqy + "' as dqy," + count + " as sum," + colums);
            if (dt.Rows.Count < 1)
                return mess;
            else
            {
                for (;;)
                {
                    if (dt.Rows.Count <= Convert.ToInt32(num))
                        break;
                    dt.Rows.RemoveAt(0);
                }
                return Newtonsoft.Json.JsonConvert.SerializeObject(dt);
            }
        }
    }
}
