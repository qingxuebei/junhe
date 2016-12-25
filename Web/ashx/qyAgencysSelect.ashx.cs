using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Web.ashx
{
    /// <summary>
    /// qyAgencysSelect 的摘要说明
    /// </summary>
    public class qyAgencysSelect : Base
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
                List<Agents> reAgentsList = new List<Agents>();
                sql = " and  AgencyId='" + agentId + "' order by CreateTime ";
                List<Agents> agentsList1 = new BLL.AgentsBLL().GetAgentsList(sql);
                if (agentsList1 != null && agentsList1.Count > 0)//一级
                {
                    foreach (Agents agents1 in agentsList1)
                    {
                        reAgentsList.Add(agents1);
                        if (agents1.Rank.StartsWith("D") || agents1.Rank.StartsWith("P"))
                        {
                            sql = " and  AgencyId='" + agents1.Id + "' order by CreateTime ";
                            List<Agents> agentsList2 = new BLL.AgentsBLL().GetAgentsList(sql);
                            if (agentsList2 != null && agentsList2.Count > 0)//二级
                            {
                                foreach (Agents agents2 in agentsList2)
                                {
                                    agents2._parentId = agents2.AgencyId;
                                    reAgentsList.Add(agents2);
                                    if (agents2.Rank.StartsWith("D") || agents2.Rank.StartsWith("P"))
                                    {
                                        sql = " and  AgencyId='" + agents2.Id + "' order by CreateTime ";
                                        List<Agents> agentsList3 = new BLL.AgentsBLL().GetAgentsList(sql);
                                        if (agentsList3 != null && agentsList3.Count > 0)//三级
                                        {
                                            foreach (Agents agents3 in agentsList3)
                                            {
                                                agents3._parentId = agents3.AgencyId;
                                                reAgentsList.Add(agents3);

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return MyData.Utils.EasyuiDataGridJsonForList(reAgentsList);

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