using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class OrdersDetailBLL
    {
        public DataTable GetByOrdersId(Model.OrdersDetail ordersDetail)
        {
            DAL.OrdersDetailDal ordersDetailDal = new DAL.OrdersDetailDal();
            return ordersDetailDal.GetByOrdersId(ordersDetail);
        }
        public bool Insert(Model.OrdersDetail ordersdetail)
        {
            return new DAL.OrdersDetailDal().Insert(ordersdetail);
        }
        public bool Delete(String id)
        {
            return new DAL.OrdersDetailDal().Delete(id);
        }
        public DataTable HuiZongOrdersDetail(int yearMonth)
        {
            return new DAL.OrdersDetailDal().HuiZongOrdersDetail(yearMonth);
        }
        public DataTable HuiZongOrders(int yearMonth)
        {
            return new DAL.OrdersDetailDal().HuiZongOrders(yearMonth);
        }
    }
}
