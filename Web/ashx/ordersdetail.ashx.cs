using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Web.ashx
{
    /// <summary>
    /// ordersdetail 的摘要说明
    /// </summary>
    public class ordersdetail : Base
    {

        public override String get(HttpContext context)
        {
            return null;
        }
        public override String add(HttpContext context)
        {
            BLL.OrdersDetailBLL ordersDetailBll = new BLL.OrdersDetailBLL();
            Model.OrdersDetail ordersDetail = new Model.OrdersDetail();

            var productId = context.Request.Params["ProductId"].ToString();
            var orderId = context.Request.Params["OrdersId"].ToString();
            DataTable dt = new BLL.ProductsBLL().getProductsById(productId);
            if (dt.Rows.Count == 0)
            {
                return "产品不存在！";
            }
            DataRow dr = dt.Rows[0];
            ordersDetail.Id = System.Guid.NewGuid().ToString();
            ordersDetail.OrdersId = orderId;
            ordersDetail.ProductId = productId;
            ordersDetail.Num = Convert.ToInt32(context.Request.Params["Num"].ToString());
            ordersDetail.ProductName = dr["ProductName"].ToString();
            ordersDetail.Price = Convert.ToDecimal(dr["UnitPrice"].ToString()) * Convert.ToInt32(context.Request.Params["Num"].ToString());
            ordersDetail.UnitPrice = Convert.ToDecimal(dr["UnitPrice"].ToString());
            ordersDetail.CreatePerson = userName;
            ordersDetail.UpdatePerson = userName;
            ordersDetail.CreateTime = DateTime.Now;
            ordersDetail.UpdateTime = DateTime.Now;
            ordersDetail.State = -1;

            String ids = ""; int nums = 0; decimal allprice = 0;
            if (new BLL.OrdersDetailBLL().Insert(ordersDetail))
            {
                DataTable dd = new BLL.OrdersDetailBLL().GetByOrdersId(ordersDetail);
                foreach (DataRow dr1 in dd.Rows)
                {
                    ids += dr1["ProductId"].ToString() + ",";
                    nums += Convert.ToInt32(dr1["Num"].ToString());
                    allprice += Convert.ToDecimal(dr1["Price"].ToString());
                }
                return ids + "$共" + nums + "件$" + allprice;
            }
            return "保存失败";
        }
        public override String update(HttpContext context)
        {
            return null;
        }
        public override String del(HttpContext context)
        {
            var id = context.Request.Params["id"].ToString();
            var orderId = context.Request.Params["OrdersId"].ToString();
            String ids = ""; int nums = 0; decimal allprice = 0;
            if (new BLL.OrdersDetailBLL().Delete(id))
            {
                Model.OrdersDetail ordersDetail = new Model.OrdersDetail();
                ordersDetail.OrdersId = orderId;
                DataTable dd = new BLL.OrdersDetailBLL().GetByOrdersId(ordersDetail);
                foreach (DataRow dr1 in dd.Rows)
                {
                    ids += dr1["ProductId"].ToString() + ",";
                    nums += Convert.ToInt32(dr1["Num"].ToString());
                    allprice += Convert.ToDecimal(dr1["Price"].ToString());
                }
                return ids + "$共" + nums + "件$" + allprice;
            }
            return "删除失败";
        }
    }
}