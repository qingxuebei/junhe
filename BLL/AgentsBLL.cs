using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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
                    agents.AgencyId = dt_Agents.Rows[0]["Id"].ToString();
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

        public bool jisuan()
        {
            List<Agents> agentsList = GetAgentsList(" and State=1");
            List<Income> incomeList = new BLL.IncomeBLL().getIncomeList(" and YearMonth=" + MyData.Utils.getLastYearMonth());

            bool ok;
            OleDbConnection conn = MyData.DataBase.Conn();
            conn.Open();
            OleDbTransaction tr = conn.BeginTransaction();
            try
            {
                //先计算修改agentsList
                jisuan(agentsList, incomeList, tr);
                tr.Commit();
                ok = true;
            }
            catch (Exception ex)
            {
                tr.Rollback();
                ok = false;
            }
            conn.Close();
            return ok;
        }

        private void jisuan(List<Agents> agentsList, List<Income> incomeList, OleDbTransaction tr)
        {
            //1.先计算代理人，该升级为代理商的升级为代理商，该过期的过期
            //2.再计算代理商的降级（如果降级，则以此人为代理商的所有人的代理商都换为此人的代理商，且不可恢复）
            //3.最后算代理商的升级
            //step1
            foreach (Agents agents in agentsList)
            {
                //如果为代理人
                if (agents.Rank.StartsWith("S"))
                {
                    Income income = incomeList.Find(o => o.AgentId == agents.Id);
                    if (income != null && !String.IsNullOrWhiteSpace(income.AgentId))
                    {   //判断近六个月的订单
                        if (income.NearlyFiveMonthsMoney + income.PersonalMoney > 0)
                        {
                            //判断近三个月的订单
                            if (income.NearlyTwoMonthsMoney + income.PersonalMoney > 0)
                            {
                                //判断其是否可以成为代理商
                                if (income.AllMonthMoney >= 10000 && income.AllSalesMoney >= 50000)
                                {
                                    agents.Rank = "D1";
                                    jisuanAgentsToAgency(incomeList, agentsList, agents, agents);
                                }
                            }
                            else
                            {
                                agents.CareerStatus = "P";
                            }
                        }
                        else
                        {
                            agents.State = (int)MyData.AgentsState.已过期;
                            agents.CareerStatus = "T";
                        }
                    }
                    else
                    {
                        decimal sixmoney = new BLL.OrdersBLL().getSixMonthPrice(agents.Id, tr);
                        if (sixmoney == 0)
                        {
                            agents.State = (int)MyData.AgentsState.已过期;
                            agents.CareerStatus = "T";
                        }
                    }
                }
            }
            //step2
            foreach (Agents agents in agentsList)
            {
                //如果为代理商
                if (agents.Rank.StartsWith("D"))
                {
                    Income income = incomeList.Find(o => o.AgentId == agents.Id);
                    if (income != null && !String.IsNullOrWhiteSpace(income.AgentId))
                    {   //每月个人订单金额保持≥1000
                        if (income.PersonalMoney < 1000)
                        {
                            agents.CareerStatus = "P";//上月个人订单少于1000，当月事业状态降为P

                            //判断近三个月的订单(连续三月个人订单不足1000，取消代理商资格，只保留VIP贵宾)
                            if (income.LastMonthMoney < (decimal)1000 && income.NearlyTwoMonthsMoney - income.LastMonthMoney < (decimal)1000)
                            {
                                agents.Rank = "S2";
                                agents.CareerStatus = "A";
                                //以此人为代理商的人的代理商都换为此人的代理商
                                foreach (Agents s_agents in agentsList)
                                {
                                    if (s_agents.AgencyId == agents.Id)
                                    {
                                        s_agents.AgencyId = agents.AgencyId;
                                        s_agents.AgencyName = agents.AgencyName;
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        decimal onemoney = new BLL.OrdersBLL().getOneMonthPrice(agents.Id, tr);

                        //每月个人订单金额保持≥1000
                        if (onemoney < 1000)
                        {
                            agents.CareerStatus = "P";//上月个人订单少于1000，当月事业状态降为P

                            decimal twomoney = new BLL.OrdersBLL().getTwoMonthPrice(agents.Id, tr);
                            decimal threemoney = new BLL.OrdersBLL().getThreeMonthPrice(agents.Id, tr);
                            //连续三个月订单小于1000，降级为代理人
                            if (twomoney - onemoney < (decimal)1000 && threemoney - twomoney < (decimal)1000)
                            {
                                agents.Rank = "S2";
                                agents.CareerStatus = "A";
                                //以此人为代理商的人的代理商都换为此人的代理商
                                foreach (Agents s_agents in agentsList)
                                {
                                    if (s_agents.AgencyId == agents.Id)
                                    {
                                        s_agents.AgencyId = agents.AgencyId;
                                        s_agents.AgencyName = agents.AgencyName;
                                    }
                                }
                            }
                        }

                    }
                }
            }
            //step3
            foreach (Agents agents in agentsList)
            {
                //如果为代理商
                if (agents.Rank.StartsWith("D"))
                {
                    Income income = incomeList.Find(o => o.AgentId == agents.Id);
                    if (income != null && !String.IsNullOrWhiteSpace(income.AgentId))
                    {   //判断上月订单大于等于1000时
                        if (income.PersonalMoney >= (decimal)1000)
                        {
                            //判断一级代理商的数量
                            List<Agents> agentsList1 = agentsList.FindAll(o => o.AgencyId == agents.Id && o.Rank.StartsWith("D"));
                            if (agentsList1.Count == 0) { agents.Rank = "D1"; }
                            else if (agentsList1.Count >= 1 && agentsList1.Count <= 2)
                            {
                                agents.Rank = "D2";
                            }
                            else if (agentsList1.Count >= 3 && agentsList1.Count <= 4)
                            {
                                agents.Rank = "D3";
                            }
                            else if (agentsList1.Count >= 5)
                            {
                                agents.Rank = "D4";
                                int count2 = 0;
                                int count3 = 0;
                                //判断二级代理商和三级代理商
                                foreach (Agents agents1 in agentsList1)
                                {
                                    List<Agents> agentsList2 = agentsList.FindAll(o => o.AgencyId == agents1.Id && o.Rank.StartsWith("D"));
                                    if (agentsList2.Count > 0)
                                    {
                                        count2 += agentsList2.Count;
                                        foreach (Agents agents2 in agentsList2)
                                        {
                                            List<Agents> agentsList3 = agentsList.FindAll(o => o.AgencyId == agents2.Id && o.Rank.StartsWith("D"));
                                            count3 += agentsList3.Count;
                                        }
                                    }
                                }
                                if (agentsList1.Count + count2 >= 20 && agentsList1.Count + count2 + count3 >= 50)
                                {
                                    agents.Rank = "D5";
                                }
                            }
                        }
                    }
                }
            }
            //修改数据库
            new DAL.AgentsDal().UpdateForJisuan(agentsList, tr);

        }
        //代理人升级为代理商时，其下的代理人的代理商都要变
        private void jisuanAgentsToAgency(List<Income> incomeList, List<Agents> fAgentsList, Agents fAgenct, Agents useAgent)
        {
            List<Agents> zAgentsList = fAgentsList.FindAll(o => o.RefereeId == fAgenct.Id);
            if (zAgentsList.Count > 0)
            {
                foreach (Agents agents in zAgentsList)
                {
                    if (agents.Rank.StartsWith("S"))
                    {
                        Agents zagents = fAgentsList.Find(o => o.Id == agents.Id);
                        zagents.AgencyId = useAgent.Id;
                        zagents.AgencyName = useAgent.Name;
                        jisuanAgentsToAgency(incomeList, fAgentsList, agents, useAgent);
                    }
                }
            }
        }
    }
}
