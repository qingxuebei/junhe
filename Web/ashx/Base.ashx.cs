using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Web.ashx
{
    /// <summary>
    /// Base 的摘要说明
    /// </summary>
    public class Base : IHttpHandler
    {
        public String userId, userName;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                userId = "123456";
                userName = "test";

                switch (context.Request.Params["type"].ToString())
                {
                    case "get":
                        context.Response.Write(get(context));
                        break;
                    case "add":
                        context.Response.Write(add(context));
                        break;
                    case "update":
                        context.Response.Write(update(context));
                        break;
                    case "delete":
                        context.Response.Write(del(context));
                        break;
                    case "export":
                        context.Response.Write(export(context));
                        break;
                    case "getBank":
                        context.Response.Write(getBank(context));
                        break;
                    case "getAgents":
                        context.Response.Write(getAgents(context));
                        break;
                    case "getRegion":
                        context.Response.Write(getRegion(context));
                        break;
                    case "getDetailByOrdersId":
                        context.Response.Write(getDetailByOrdersId(context));
                        break;
                    case "getProducts":
                        context.Response.Write(getProducts(context));
                        break;
                    case "HuiZongOrdersDetail":
                        context.Response.Write(HuiZongOrdersDetail(context));
                        break;
                    case "HuiZongOrders":
                        context.Response.Write(HuiZongOrders(context));
                        break;
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("系统错误！");
            }
        }

        public virtual String get(HttpContext context)
        {
            return null;
        }
        public virtual String add(HttpContext context)
        {
            return null;
        }
        public virtual String update(HttpContext context)
        {
            return null;
        }
        public virtual String del(HttpContext context)
        {
            return null;
        }
        public virtual String export(HttpContext context)
        {
            return null;
        }
        public String getBank(HttpContext context)
        {
            DataTable dt = new BLL.DictBankBLL().getBank(" and State=1");
            return MyData.Utils.EasuuiComboxJson(dt);
        }
        public String getAgents(HttpContext context)
        {
            DataTable dt = new BLL.AgentsBLL().GetAgents(" and State!=0");
            return MyData.Utils.EasuuiComboxJson(dt);

        }
        public String getProducts(HttpContext context)
        {
            DataTable dt = new BLL.ProductsBLL().getProducts();
            return MyData.Utils.EasuuiComboxJson(dt);
        }
        public String getRegion(HttpContext context)
        {
            String strWhere = " and State=1 ";
            String st = context.Request.Params["st"].ToString();
            if (st == "p")
            {
                strWhere += " and ParentCode=''";
            }
            else if (st == "reload")
            {
                strWhere += "and ParentCode!=''";
            }
            else
            {
                String id = context.Request.Params["id"].ToString();
                strWhere += " and ParentCode='" + id + "'";
            }
            DataTable dt = new BLL.DictRegionBLL().getRegion(strWhere);
            return MyData.Utils.EasuuiComboxJson(dt);
        }
        public String getDetailByOrdersId(HttpContext context)
        {
            Model.OrdersDetail ordersDetail = new Model.OrdersDetail();
            String orderId = context.Request.Params["orderId"].ToString();
            if (!String.IsNullOrWhiteSpace(orderId))
            {
                ordersDetail.OrdersId = orderId;
            }
            else { ordersDetail.OrdersId = ""; }
            DataTable dt = new BLL.OrdersDetailBLL().GetByOrdersId(ordersDetail);
            return MyData.Utils.EasyuiDataGridJson(dt);
        }
        public String HuiZongOrdersDetail(HttpContext context)
        {
            String yearMonth = context.Request.Params["yearMonth"].ToString();
            if (!String.IsNullOrWhiteSpace(yearMonth))
            {
                DataTable dt = new BLL.OrdersDetailBLL().HuiZongOrdersDetail(Convert.ToInt32(yearMonth));
                return MyData.Utils.EasyuiDataGridJson(dt);
            }
            return null;
        }
        public String HuiZongOrders(HttpContext context)
        {
            String yearMonth = context.Request.Params["yearMonth"].ToString();
            if (!String.IsNullOrWhiteSpace(yearMonth))
            {
                DataTable dt = new BLL.OrdersDetailBLL().HuiZongOrders(Convert.ToInt32(yearMonth));
                return MyData.Utils.EasyuiDataGridJson(dt);
            }
            return null;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}