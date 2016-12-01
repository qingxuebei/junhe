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
        public DataTable GetAgents(Agents agents)
        {
            DAL.AgentsDal agentsDal = new DAL.AgentsDal();
            return agentsDal.GetAgents(agents);
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
    }
}
