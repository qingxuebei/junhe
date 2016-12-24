using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Web.ashx
{
    /// <summary>
    /// orders 的摘要说明
    /// </summary>
    public class orders : Base
    {

        public override String get(HttpContext context)
        {
            int pageRows, page; String order = "";
            pageRows = 20;
            page = 1;
            BLL.OrdersBLL orderBLL = new BLL.OrdersBLL();
            String strWhere = " 1=1";
            string[] st = context.Request.Params["wherestr"].ToString().Split(',');
            if (!String.IsNullOrWhiteSpace(st[0]))
            {
                strWhere += " and AgentId='" + st[0] + "'";
            }
            if (!String.IsNullOrWhiteSpace(st[1]))
            {
                strWhere += " and YearMonth=" + Convert.ToInt32(st[1]);
            }
            if (null != context.Request["rows"])
            {
                pageRows = int.Parse(context.Request["rows"].ToString().Trim());
            }
            if (null != context.Request["page"])
            {
                page = int.Parse(context.Request["page"].ToString().Trim());
            }
            if (null != context.Request["sort"])
            {
                order = context.Request["sort"].ToString().Trim();
            }

            //调用分页的GetList方法  
            DataTable dt = orderBLL.GetListByPage(strWhere.ToString(), order, (page - 1) * pageRows + 1, page * pageRows);
            int count = orderBLL.GetRecordCount(strWhere.ToString());//获取条数  
            return MyData.Utils.EasyuiDataGridJson(dt, count);
        }
        public override String add(HttpContext context)
        {
            Model.Orders orders = new Model.Orders();
            String AgentId = context.Request.Params["AgentId"].ToString();
            String Price = context.Request.Params["Price"].ToString();
            String OrdersId = context.Request.Params["OrdersId"].ToString();
            DataTable dt = new BLL.AgentsBLL().GetAgents(" and Id='" + AgentId + "'");
            if (dt.Rows.Count == 0)
            {
                return "代理人不存在";
            }
            orders.Id = OrdersId;
            orders.Price = Convert.ToDecimal(Price);
            orders.AgentName = dt.Rows[0]["AgentsName"].ToString();
            orders.State = Convert.ToInt32(MyData.OrdersState.正常);
            orders.AgentId = AgentId;
            orders.CreatePerson = userName;
            orders.CreateTime = DateTime.Now;
            orders.UpdatePerson = userName;
            orders.UpdateTime = DateTime.Now;
            orders.YearMonth = MyData.Utils.getYearMonth();
            orders.YearMonthDate = DateTime.Now;
            if (new BLL.OrdersBLL().Insert(orders))
            {
                return "0";
            }
            return "添加失败！";
        }
        public override String update(HttpContext context)
        {
            try
            {
                String id = context.Request.Params["id"].ToString();
                String agentsId = context.Request.Params["agentsId"].ToString();
                if (String.IsNullOrWhiteSpace(id) || String.IsNullOrWhiteSpace(agentsId)) { return "作废失败"; }
                Model.Orders orders = new Model.Orders();
                orders = new BLL.OrdersBLL().getOrdersById(id);
                if (orders == null || String.IsNullOrWhiteSpace(orders.Id)) { return "Id 错误！"; }
                if (new BLL.OrdersBLL().Update(orders)) { return "0"; }
                else return "修改失败";
            }
            catch (Exception ex) { }

            return "修改失败！";
        }
        public override String del(HttpContext context)
        {
            return null;
        }
    }
}