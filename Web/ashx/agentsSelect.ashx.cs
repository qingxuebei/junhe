﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Web.ashx
{
    /// <summary>
    /// agentsSelect 的摘要说明
    /// </summary>
    public class agentsSelect : Base
    {
        public override string get(HttpContext context)
        {
            String agentId = context.Request.Params["agentId"].ToString();
            String style = context.Request.Params["style"].ToString();
            String sql = " and 1=2";

            if (style == "getOrderList")
            {
                sql = " and State=1 and AgentId='" + agentId + "' order by CreateTime desc";
                DataTable dt1 = new BLL.OrdersBLL().getOrdersByObject(sql);
                return MyData.Utils.EasyuiDataGridJson(dt1);
            }

            if (style == "getList")
            {
                sql = " and  RefereeId='" + agentId + "'order by CreateTime";
            }
            else if (style == "getSelf")
            {
                sql = " and Id='" + agentId + "'";
            }
            DataTable dt = new BLL.AgentsBLL().GetAgents(sql);
            return MyData.Utils.EasyuiDataGridJson(dt);
        }
    }
}