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
    }
}
