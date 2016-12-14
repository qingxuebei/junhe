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
    }
}
