using MyData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OrdersDetailDal
    {
        public DataTable GetByOrdersId(Model.OrdersDetail ordersDetail)
        {
            String sql = "select * from OrdersDetail where 1=1 ";
            if (!String.IsNullOrWhiteSpace(ordersDetail.OrdersId))
            {
                sql += " and OrdersId='" + ordersDetail.OrdersId + "'";
            }
            return DataBase.Base_dt(sql);
        }
        public bool Insert(Model.OrdersDetail ordersdetail)
        {
            if (!String.IsNullOrWhiteSpace(ordersdetail.CreatePerson))
            {
                String sql = @"INSERT INTO [dbo].[OrdersDetail]
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
                           ,[UpdatePerson],[State])
                     VALUES ('{0}','{1}','{2}','{3}',{4},{5},{6},'{7}','{8}','{9}','{10}',{11} );";
                sql = String.Format(sql, ordersdetail.Id, ordersdetail.OrdersId, ordersdetail.ProductId, ordersdetail.ProductName, ordersdetail.UnitPrice, ordersdetail.Num,
                    ordersdetail.Price, ordersdetail.CreateTime, ordersdetail.CreatePerson, ordersdetail.UpdateTime, ordersdetail.UpdatePerson, ordersdetail.State);
                return DataBase.Base_cmd(sql);
            }
            return false;
        }
        public bool Delete(String id)
        {
            String sql = "delete [dbo].[OrdersDetail] where Id='" + id + "'";
            return DataBase.Base_cmd(sql);
        }

        //产品销售汇总
        public DataTable HuiZongOrdersDetail(int yearMonth)
        {
            String sql = @"select MAX(a.ProductName) as ProductName,MAX(a.UnitPrice) as UnitPrice,sum(a.Num) as Num,SUM(a.Price) as Price from OrdersDetail a 
                            left join Orders b on a.OrdersId = b.Id
                            where a.State = 1 and b.State=1 and b.YearMonth = " + yearMonth + "group by a.ProductId";
            return DataBase.Base_dt(sql);
        }
        public DataTable HuiZongOrders(int yearMonth)
        {
            String sql = @"select a.AgentId,MAX(b.Name) as AgentName,MAX(b.CareerStatus) as CareerStatus,Count(a.Id)as CountOrders,
                            MAX(b.Rank) as Rank,MAX(b.State) as State,Max(AgentsStatus) as AgentsStatus,MAX(a.Price) as Price
                              from [dbo].[Orders] a 
                            left join [dbo].[Agents] b on a.AgentId=b.Id
                            where a.State=1 and a.YearMonth=" + yearMonth + "group by a.AgentId";
            return DataBase.Base_dt(sql);
        }
    }
}
