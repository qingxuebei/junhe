using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Web.ashx
{
    /// <summary>
    /// agencysSelect 的摘要说明
    /// </summary>
    public class agencysSelect : Base
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
                //List<Agents> reAgentsList = new List<Agents>();
                //sql = " and  AgencyId='" + agentId + "' order by CreateTime ";
                //List<Agents> agentsList = new BLL.AgentsBLL().GetAgentsList(sql);
                //if (agentsList != null && agentsList.Count > 0)
                //{
                //    foreach (Agents agents in agentsList)
                //    {
                //        if (agents.Rank.StartsWith("S"))
                //        {
                //            reAgentsList.Add(agents);
                //        }
                //    }
                //}
                sql = " and  AgencyId='" + agentId + "' and Rank in ('S1','S2') order by CreateTime ";
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