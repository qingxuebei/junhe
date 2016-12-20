using MyData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OrdersDal
    {
        public bool Insert(Model.Orders orders, OleDbTransaction tr)
        {
            String sql1 = @"INSERT INTO [dbo].[Orders]
                           ([Id]
                           ,[AgentId]
                           ,[AgentName]
                           ,[YearMonth]
                           ,[Price]
                           ,[CreateTime]
                           ,[CreatePerson]
                           ,[UpdateTime]
                           ,[UpdatePerson]
                           ,[State],[YearMonthDate])
                     VALUES ('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}',{9},'{10}');";
            sql1 = String.Format(sql1, orders.Id, orders.AgentId, orders.AgentName, orders.YearMonth, orders.Price, orders.CreateTime, orders.CreatePerson, orders.UpdateTime, orders.UpdatePerson, orders.State, orders.YearMonthDate);

            String sql2 = "update OrdersDetail set State=1 where OrdersId='" + orders.Id + "'";
            MyData.DataBase.Base_cmd(sql1 + sql2, tr);
            return true;
        }

        public Decimal SumPrice(String str_sql)
        {
            String sql = "select sum(Price) from Orders where State=1 " + str_sql;
            return Convert.ToDecimal(MyData.DataBase.Base_Scalar(sql));
        }

        public DataTable GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.CreateTime desc");
            }
            strSql.Append(")AS Row, T.*  from Orders T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DataBase.Base_dt(strSql.ToString());
        }

        public int GetRecordCount(string strWhere)
        {
            return DataBase.Base_count("Orders", strWhere);
        }
        public bool Update(Model.Orders orders)
        {
            String sql = "update Orders set State=0 where Id='" + orders.Id + "';";
            String sql2 = "update OrdersDetail set State=0 where OrdersId='" + orders.Id + "'";
            return DataBase.Base_cmd(sql + sql2);
        }
    }
}
