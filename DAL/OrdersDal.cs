using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OrdersDal
    {
        public bool Insert(Model.Orders orders, List<Model.OrdersDetail> ordersDetailList)
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
                           ,[State])
                     VALUES ('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}',{9});";
            sql1 = String.Format(sql1, orders.Id, orders.AgentId, orders.AgentName, orders.YearMonth, orders.Price, orders.CreateTime, orders.CreatePerson, orders.UpdateTime, orders.UpdatePerson, orders.State);

            String sql2 = @"INSERT INTO [dbo].[OrdersDetail]
                           ([Id]
                           ,[OrdersId]
                           ,[ProductId]
                           ,[ProductName]
                           ,[UnitPrice]
                           ,[Num]
                           ,[Price]
                           ,[CreateTime]
                           ,[CreatePerson]
                           ,[UpdateTime]
                           ,[UpdatePerson])
                     VALUES ";
            foreach (Model.OrdersDetail ordersdetail in ordersDetailList)
            {
                sql2 += " ('{0}','{1}','{2}','{3}',{4},{5},{6},'{7}','{8}','{9}','{10}' ),";
                sql2 = String.Format(sql2, ordersdetail.Id, ordersdetail.OrdersId, ordersdetail.ProductId, ordersdetail.ProductName, ordersdetail.UnitPrice, ordersdetail.Num,
                    ordersdetail.Price, ordersdetail.CreateTime, ordersdetail.CreatePerson, ordersdetail.UpdateTime, ordersdetail.UpdatePerson);
            }
            sql2 = sql2.TrimEnd(',') + ";";
            return MyData.DataBase.Base_cmd(sql1 + sql2);
        }

        public Decimal SumPrice(String agnetsId)
        {
            String sql = "select sum(Price) from Orders where AgentId='" + agnetsId + "'";
            return Convert.ToDecimal(MyData.DataBase.Base_Scalar(sql));
        }
    }
}
