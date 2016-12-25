using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AgentsBLL
    {
        public DataTable GetAgents(String strWhere)
        {
            DAL.AgentsDal agentsDal = new DAL.AgentsDal();
            return agentsDal.GetAgents(strWhere);
        }
        public List<Agents> GetAgentsList(String strWhere)
        {
            DAL.AgentsDal agentsDal = new DAL.AgentsDal();
            return agentsDal.GetAgentsList(strWhere);
        }
        public DataTable GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            DAL.AgentsDal agentsDal = new DAL.AgentsDal();
            return agentsDal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        public int GetRecordCount(string strWhere)
        {
            DAL.AgentsDal agentsDal = new DAL.AgentsDal();
            return agentsDal.GetRecordCount(strWhere);
        }
        public bool Insert(Model.Agents agents)
        {
            DataTable dt_Agents = GetAgents(" and Id='" + agents.RefereeId + "'");
            if (dt_Agents != null && dt_Agents.Rows.Count > 0)
            {
                agents.RefereeName = dt_Agents.Rows[0]["AgentsName"].ToString();

                String rank = dt_Agents.Rows[0]["Rank"].ToString();
                if (rank.StartsWith("S"))//如果推荐人是代理人，则资深代理商为推荐人的资深代理商
                {
                    agents.AgencyId = dt_Agents.Rows[0]["AgencyId"].ToString();
                    agents.AgencyName = dt_Agents.Rows[0]["AgencyName"].ToString();
                }
                else if (rank.StartsWith("D") || rank.StartsWith("P"))//如果推荐人是代理商或者合伙人，则资深代理商为推荐人
                {
                    agents.AgencyId = dt_Agents.Rows[0]["AgentsId"].ToString();
                    agents.AgencyName = dt_Agents.Rows[0]["AgentsName"].ToString();
                }
            }

            return new DAL.AgentsDal().Insert(agents);
        }
        public bool Update(Model.Agents agents)
        {
            DataTable dt_Agents = GetAgents(" and Id='" + agents.RefereeId + "'");
            if (dt_Agents != null && dt_Agents.Rows.Count > 0)
            {
                agents.RefereeName = dt_Agents.Rows[0]["AgentsName"].ToString();
                agents.AgencyId = dt_Agents.Rows[0]["AgencyId"].ToString();
                agents.AgencyName = dt_Agents.Rows[0]["AgencyName"].ToString();
            }
            return new DAL.AgentsDal().Update(agents);
        }
    }
}
