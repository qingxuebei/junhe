using Model;
using MyData;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class IncomeBLL
    {
        public void alljisuan(Model.Orders orders, OleDbTransaction tr)
        {
            BLL.OrdersBLL orderBLL = new BLL.OrdersBLL();
            DAL.IncomeDal incomeDal = new DAL.IncomeDal();
            //Step 1
            //首先取出这个人的信息
            Agents agents = DataBase.Base_getFirst<Agents>(" and Id = '" + orders.AgentId + "' and State != 0");
            agents.State = Convert.ToInt32(MyData.AgentsState.正常);

            Model.Income income = new Income();
            income = incomeDal.getIncomebyId(orders.YearMonth, agents.Id);
            if (income == null)
            {
                income.YearMonth = orders.YearMonth;
                income.AgentId = agents.Id;
                income.AgentName = agents.Name;
                income.CareerStatus = agents.CareerStatus;
                income.Rank = agents.Rank;
                income.RefereeId = agents.RefereeId;
                income.RefereeName = agents.RefereeName;
                income.AgencyName = agents.AgencyName;
                income.AgencyId = agents.AgencyId;
                income.CreateTime = DateTime.Now;
                income.UpdateTime = DateTime.Now;
                income.LastMonthMoney = orderBLL.getOneMonthPrice(agents.Id);
                income.NearlyThreeMonthsMoney = orderBLL.getThreeMonthPrice(agents.Id);
                income.NearlySixMonthsMoney = orderBLL.getSixMonthPrice(agents.Id);
                income.AllMonthMoney = orderBLL.getAllMonthPrice(agents.Id);
                income.State = Convert.ToInt32(MyData.IncomeState.正常);
            }
            //计算截至目前当月个人订单
            income.PersonalMoney += orders.Price;
            jisuanGerenDingdanFencheng(income);

            //Step 2
            //计算推荐人的销售奖金
            if (!String.IsNullOrWhiteSpace(agents.RefereeId))
            {
                Model.Income income_t = new Income();
                income_t = incomeDal.getIncomebyId(orders.YearMonth, agents.RefereeId);
                if (income_t == null)
                {
                    Agents agents_t = DataBase.Base_getFirst<Agents>(" and Id = '" + orders.AgentId + "' and State != 0");
                    income_t.YearMonth = orders.YearMonth;
                    income_t.AgentId = agents_t.Id;
                    income_t.AgentName = agents_t.Name;
                    income_t.CareerStatus = agents_t.CareerStatus;
                    income_t.Rank = agents_t.Rank;
                    income_t.RefereeId = agents_t.RefereeId;
                    income_t.RefereeName = agents_t.RefereeName;
                    income_t.AgencyName = agents_t.AgencyName;
                    income_t.AgencyId = agents_t.AgencyId;
                    income_t.CreateTime = DateTime.Now;
                    income_t.UpdateTime = DateTime.Now;
                    income_t.LastMonthMoney = orderBLL.getOneMonthPrice(agents_t.Id);
                    income_t.NearlyThreeMonthsMoney = orderBLL.getThreeMonthPrice(agents_t.Id);
                    income_t.NearlySixMonthsMoney = orderBLL.getSixMonthPrice(agents_t.Id);
                    income_t.AllMonthMoney = orderBLL.getAllMonthPrice(agents_t.Id);
                    income_t.State = Convert.ToInt32(MyData.IncomeState.正常);
                }
                income_t.SalesMoney += orders.Price;
            }
            //Step 3


        }
        public void jisuanGerenDingdanFencheng(Model.Income income)
        {
            //VIP顾客无个人订单返利
            if (income.Rank == "S1")
            {
                income.PersonalServiceMoney = 0;
            }
            else
            {
                //其他级别一般情况下都是10%
                income.PersonalServiceMoney = income.PersonalMoney * (decimal)0.1;
            }
        }
        public void jisuanTuijianrenVipJiangjin(Model.Income income_t)
        {
            income_t.SalesServiceMoney = income_t.SalesMoney * (decimal)0.1;
            //如果是S2级别，且连续三个月订单为0，则奖金减半
            if (income_t.Rank == "S2" && income_t.NearlyThreeMonthsMoney == 0)
            {
                income_t.SalesServiceMoney = income_t.SalesServiceMoney * (decimal)0.5;
            }
        }

        public bool UpdateAgents(Model.Agents agents, Model.Income income, OleDbTransaction tr)
        {
            //判断是否为代理人
            if (agents.Rank == "S1" || agents.Rank == "S2")
            {
                if (income.AllMonthMoney >= 2500)
                {
                    agents.CareerStatus = "A";
                    if (income.AllMonthMoney >= 10000)
                    {
                        agents.Rank = "S2";
                    }
                }
                return new DAL.AgentsDal().UpdateAgents(agents);
            }
            return false;
        }
    }
}
