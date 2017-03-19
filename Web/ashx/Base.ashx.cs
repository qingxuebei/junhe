using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Web.ashx
{
    /// <summary>
    /// Base 的摘要说明
    /// </summary>
    public class Base : IHttpHandler, IRequiresSessionState
    {
        public String userId = "", userName = "";

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                if (context.Session["Username"] != null && context.Session["Username"].ToString() != "")
                {
                    userName = context.Session["Username"].ToString();
                }
                else { return; }

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
                    case "sumPrice":
                        context.Response.Write(sumPrice(context));
                        break;
                    case "sumPerson":
                        context.Response.Write(sumPerson(context));
                        break;
                    case "loginout":
                        context.Response.Write(loginout(context));
                        break;
                    case "editpassword":
                        context.Response.Write(editpassword(context));
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
        public String sumPrice(HttpContext context)
        {
            List<Decimal> list = new List<decimal>();
            //计算当天订单金额
            String str1 = " and State=1 and CreateTime>='" + DateTime.Now.ToShortDateString() + "'";
            list.Add(new BLL.OrdersBLL().SumPrice(str1));
            //计算当月的订单金额
            String str2 = " and State=1 and YearMonth=" + MyData.Utils.getYearMonth();
            list.Add(new BLL.OrdersBLL().SumPrice(str2));
            return Newtonsoft.Json.JsonConvert.SerializeObject(list);
        }
        public String sumPerson(HttpContext context)
        {
            List<int> list = new List<int>();
            String str1 = "  State=1 and CreateTime>='" + DateTime.Now.ToShortDateString() + "'";
            list.Add(new BLL.AgentsBLL().GetRecordCount(str1));
            String str2 = "  State=1 and CreateTime>='" + MyData.Utils.getMonthFirstDay() + "'";
            list.Add(new BLL.AgentsBLL().GetRecordCount(str2));
            return Newtonsoft.Json.JsonConvert.SerializeObject(list);
        }
        public String loginout(HttpContext context)
        {
            context.Session["Username"] = null;
            return "1";
        }
        public String editpassword(HttpContext context)
        {
            String oldpwd = context.Request.Params["oldpwd"].ToString();
            String newpwd = context.Request.Params["newpwd"].ToString();
            String username = context.Session["Username"].ToString();
            if (new BLL.SysUserBLL().getCount(username, oldpwd) > 0)
            {
                if (new BLL.SysUserBLL().editPwd(username, newpwd))
                {
                    return "1";
                }
                else { return "修改失败！"; }
            }
            return "原密码错误！";
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