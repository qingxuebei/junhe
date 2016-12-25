using Model;
using MyData;
using System;
using System.Collections.Generic;
using System.Data;
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
            #region 首先取出这个人的信息
            Agents agents = DataBase.Base_getFirst<Agents>("select * from Agents where  Id = '" + orders.AgentId + "' and State != 0", tr);
            agents.State = Convert.ToInt32(MyData.AgentsState.正常);

            Model.Income income = new Income();
            income = incomeDal.getIncomebyId(orders.YearMonth, agents.Id, tr);
            bool isinsert = false;//判断数据是插入还是修改
            bool isTrue = false;//推荐人或者代理人或者合伙人是否存在
            if (income == null || String.IsNullOrWhiteSpace(income.AgentId))
            {
                isinsert = true;
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
                income.CreatePerson = orders.CreatePerson;
                income.LastMonthMoney = orderBLL.getOneMonthPrice(agents.Id, tr);
                income.NearlyThreeMonthsMoney = orderBLL.getThreeMonthPrice(agents.Id, tr);
                income.NearlySixMonthsMoney = orderBLL.getSixMonthPrice(agents.Id, tr);
                income.AllMonthMoney = orderBLL.getAllMonthPrice(agents.Id, tr);
                income.State = Convert.ToInt32(MyData.IncomeState.正常);
            }
            else { income.AllMonthMoney += orders.Price; }
            //计算截至目前当月个人订单分成
            income.UpdateTime = DateTime.Now;
            income.UpdatePerson = orders.UpdatePerson;
            income.PersonalMoney += orders.Price;
            //计算个人订单分成
            jisuanGerenDingdanFencheng(income);
            //计算总收入=VIP顾客销售奖金+个人订单返利+市场推广服务费+区域管理服务费+业绩分红
            income.IncomeMoney = income.SalesServiceMoney + income.PersonalServiceMoney + income.MarketServiceMoney + income.RegionServiceMoney + income.RegionServiceYum;
            incomeDal.InsertOrUpdateIncome(tr, income, isinsert);
            #endregion
            //Step 2
            #region 计算推荐人的销售奖金
            if (!String.IsNullOrWhiteSpace(agents.RefereeId))
            {
                Model.Income income_t = new Income();
                income_t = incomeDal.getIncomebyId(orders.YearMonth, agents.RefereeId, tr);
                isinsert = false;
                if (income_t == null || String.IsNullOrWhiteSpace(income_t.AgentId))
                {
                    isinsert = true;
                    Agents agents_t = DataBase.Base_getFirst<Agents>("select * from Agents where  Id = '" + agents.RefereeId + "' and State != 0", tr);
                    if (agents_t != null && !String.IsNullOrWhiteSpace(agents_t.Id))
                    {
                        isTrue = true;
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
                        income_t.CreatePerson = orders.CreatePerson;
                        income_t.LastMonthMoney = orderBLL.getOneMonthPrice(agents_t.Id, tr);
                        income_t.NearlyThreeMonthsMoney = orderBLL.getThreeMonthPrice(agents_t.Id, tr);
                        income_t.NearlySixMonthsMoney = orderBLL.getSixMonthPrice(agents_t.Id, tr);
                        income_t.AllMonthMoney = orderBLL.getAllMonthPrice(agents_t.Id, tr);
                        income_t.State = Convert.ToInt32(MyData.IncomeState.正常);
                    }
                }
                else { isTrue = true; }
                if (isTrue == true)
                {
                    income_t.UpdateTime = DateTime.Now;
                    income_t.UpdatePerson = orders.UpdatePerson;
                    income_t.SalesMoney += orders.Price;
                    //计算个人VIP顾客销售奖金
                    jisuanTuijianrenVipJiangjin(income_t);

                    //如果为代理商，由此订单产生的市场推广服务费加到个人身上
                    if (agents.Rank.StartsWith("P") || agents.Rank.StartsWith("D"))
                    {
                        income_t.MarketMoney += orders.Price;
                        jisuanShichangTuiguangFuwufei(income_t);
                    }

                    //计算总收入=VIP顾客销售奖金+个人订单返利+市场推广服务费+区域管理服务费+业绩分红
                    income_t.IncomeMoney = income_t.SalesServiceMoney + income_t.PersonalServiceMoney + income_t.MarketServiceMoney + income_t.RegionServiceMoney + income_t.RegionServiceYum;
                    incomeDal.InsertOrUpdateIncome(tr, income_t, isinsert);
                }
            }
            #endregion
            //Step 3
            #region 计算市场服务费分成
            Agents agents_s = new Agents();
            if (!String.IsNullOrWhiteSpace(agents.AgencyId))
            {
                if (agents.Rank.StartsWith("S"))//如果为代理人，市场推广服务费添加到资深代理商身上
                {
                    Model.Income income_s = new Income();
                    income_s = incomeDal.getIncomebyId(orders.YearMonth, agents.AgencyId, tr);
                    isinsert = false; isTrue = false;
                    if (income_s == null || String.IsNullOrWhiteSpace(income_s.AgentId))
                    {
                        isinsert = true;
                        agents_s = DataBase.Base_getFirst<Agents>("select * from Agents where   Id = '" + agents.AgencyId + "' and State != 0", tr);
                        if (agents_s != null && !String.IsNullOrWhiteSpace(agents_s.Id))
                        {
                            isTrue = true;
                            income_s.YearMonth = orders.YearMonth;
                            income_s.AgentId = agents_s.Id;
                            income_s.AgentName = agents_s.Name;
                            income_s.CareerStatus = agents_s.CareerStatus;
                            income_s.Rank = agents_s.Rank;
                            income_s.RefereeId = agents_s.RefereeId;
                            income_s.RefereeName = agents_s.RefereeName;
                            income_s.AgencyName = agents_s.AgencyName;
                            income_s.AgencyId = agents_s.AgencyId;
                            income_s.CreateTime = DateTime.Now;
                            income_s.UpdateTime = DateTime.Now;
                            income_s.LastMonthMoney = orderBLL.getOneMonthPrice(agents_s.Id, tr);
                            income_s.NearlyThreeMonthsMoney = orderBLL.getThreeMonthPrice(agents_s.Id, tr);
                            income_s.NearlySixMonthsMoney = orderBLL.getSixMonthPrice(agents_s.Id, tr);
                            income_s.AllMonthMoney = orderBLL.getAllMonthPrice(agents_s.Id, tr);
                            income_s.State = Convert.ToInt32(MyData.IncomeState.正常);
                        }
                    }
                    else { isTrue = true; }
                    if (isTrue == true)
                    {
                        income_s.UpdateTime = DateTime.Now;
                        income_s.UpdatePerson = orders.UpdatePerson;
                        income_s.MarketMoney += orders.Price;
                        jisuanShichangTuiguangFuwufei(income_s);
                        //计算总收入=VIP顾客销售奖金+个人订单返利+市场推广服务费+区域管理服务费+业绩分红
                        income_s.IncomeMoney = income_s.SalesServiceMoney + income_s.PersonalServiceMoney + income_s.MarketServiceMoney + income_s.RegionServiceMoney + income_s.RegionServiceYum;

                        incomeDal.InsertOrUpdateIncome(tr, income_s, isinsert);
                    }
                }
                //如果为代理商则加到自己身上
            }
            #endregion
            //step 4
            //计算区域管理服务费
            //只有当前订单所有人是代理人才会计算区域管理服务费
            if (agents_s != null && !String.IsNullOrWhiteSpace(agents_s.AgencyId))//判断资深代理商是否存在
            {
                isinsert = false; isTrue = false;
                //查找资深代理商的资深代理商（A）,添加到A的一级区域管理服务费
                Agents agents_s1 = DataBase.Base_getFirst<Agents>("select * from Agents where   Id = '" + agents_s.AgencyId + "' and State != 0", tr);
                if (agents_s1 != null && !String.IsNullOrWhiteSpace(agents_s1.Id))
                {
                    //计算一级区域管理费，如果为初级代理商，则不计算区域管理费
                    if ((agents_s1.Rank.StartsWith("D") || agents_s1.Rank.StartsWith("P")) && agents_s1.Rank != "D1")
                    {
                        isTrue = true;
                        Model.Income income_s1 = new Income();
                        income_s1 = incomeDal.getIncomebyId(orders.YearMonth, agents_s1.AgencyId, tr);

                        if (income_s1 == null || String.IsNullOrWhiteSpace(income_s1.AgentId))
                        {
                            isinsert = true;

                            income_s1.YearMonth = orders.YearMonth;
                            income_s1.AgentId = agents_s1.Id;
                            income_s1.AgentName = agents_s1.Name;
                            income_s1.CareerStatus = agents_s1.CareerStatus;
                            income_s1.Rank = agents_s1.Rank;
                            income_s1.RefereeId = agents_s1.RefereeId;
                            income_s1.RefereeName = agents_s1.RefereeName;
                            income_s1.AgencyName = agents_s1.AgencyName;
                            income_s1.AgencyId = agents_s1.AgencyId;
                            income_s1.CreateTime = DateTime.Now;
                            income_s1.UpdateTime = DateTime.Now;
                            income_s1.LastMonthMoney = orderBLL.getOneMonthPrice(agents_s1.Id, tr);
                            income_s1.NearlyThreeMonthsMoney = orderBLL.getThreeMonthPrice(agents_s1.Id, tr);
                            income_s1.NearlySixMonthsMoney = orderBLL.getSixMonthPrice(agents_s1.Id, tr);
                            income_s1.AllMonthMoney = orderBLL.getAllMonthPrice(agents_s1.Id, tr);
                            income_s1.State = Convert.ToInt32(MyData.IncomeState.正常);
                        }

                        income_s1.UpdateTime = DateTime.Now;
                        income_s1.UpdatePerson = orders.UpdatePerson;
                        income_s1.OneMoney += orders.Price;
                        jisuanQuyuGuanliFuwufei(income_s1);
                        //计算总收入=VIP顾客销售奖金+个人订单返利+市场推广服务费+区域管理服务费+业绩分红
                        income_s1.IncomeMoney = income_s1.SalesServiceMoney + income_s1.PersonalServiceMoney + income_s1.MarketServiceMoney
                            + income_s1.RegionServiceMoney + income_s1.RegionServiceYum;

                        incomeDal.InsertOrUpdateIncome(tr, income_s1, isinsert);
                    }
                    //计算二级区域管理费
                    Agents agents_s2 = DataBase.Base_getFirst<Agents>("select * from Agents where   Id = '" + agents_s1.AgencyId + "' and State != 0", tr);
                    if (agents_s2 != null && !String.IsNullOrWhiteSpace(agents_s2.Id))
                    {
                        //如果为初级代理商，则不计算区域管理费
                        if ((agents_s2.Rank.StartsWith("D") || agents_s2.Rank.StartsWith("P")) && agents_s2.Rank != "D1")
                        {
                            isTrue = true;
                            Model.Income income_s2 = new Income();
                            income_s2 = incomeDal.getIncomebyId(orders.YearMonth, agents_s2.AgencyId, tr);

                            if (income_s2 == null || String.IsNullOrWhiteSpace(income_s2.AgentId))
                            {
                                isinsert = true;

                                income_s2.YearMonth = orders.YearMonth;
                                income_s2.AgentId = agents_s2.Id;
                                income_s2.AgentName = agents_s2.Name;
                                income_s2.CareerStatus = agents_s2.CareerStatus;
                                income_s2.Rank = agents_s2.Rank;
                                income_s2.RefereeId = agents_s2.RefereeId;
                                income_s2.RefereeName = agents_s2.RefereeName;
                                income_s2.AgencyName = agents_s2.AgencyName;
                                income_s2.AgencyId = agents_s2.AgencyId;
                                income_s2.CreateTime = DateTime.Now;
                                income_s2.UpdateTime = DateTime.Now;
                                income_s2.LastMonthMoney = orderBLL.getOneMonthPrice(agents_s2.Id, tr);
                                income_s2.NearlyThreeMonthsMoney = orderBLL.getThreeMonthPrice(agents_s2.Id, tr);
                                income_s2.NearlySixMonthsMoney = orderBLL.getSixMonthPrice(agents_s2.Id, tr);
                                income_s2.AllMonthMoney = orderBLL.getAllMonthPrice(agents_s2.Id, tr);
                                income_s2.State = Convert.ToInt32(MyData.IncomeState.正常);
                            }

                            income_s2.UpdateTime = DateTime.Now;
                            income_s2.UpdatePerson = orders.UpdatePerson;
                            income_s2.OneMoney += orders.Price;
                            jisuanQuyuGuanliFuwufei(income_s2);
                            //计算总收入=VIP顾客销售奖金+个人订单返利+市场推广服务费+区域管理服务费+业绩分红
                            income_s2.IncomeMoney = income_s2.SalesServiceMoney + income_s2.PersonalServiceMoney + income_s2.MarketServiceMoney
                                + income_s2.RegionServiceMoney + income_s2.RegionServiceYum;

                            incomeDal.InsertOrUpdateIncome(tr, income_s2, isinsert);
                        }
                        //计算三级区域管理费
                        Agents agents_s3 = DataBase.Base_getFirst<Agents>("select * from Agents where   Id = '" + agents_s2.AgencyId + "' and State != 0", tr);
                        if (agents_s3 != null && !String.IsNullOrWhiteSpace(agents_s3.Id))
                        {
                            //如果为初级代理商，则不计算区域管理费
                            if ((agents_s3.Rank.StartsWith("D") || agents_s3.Rank.StartsWith("P")) && agents_s3.Rank != "D1")
                            {
                                isTrue = true;
                                Model.Income income_s3 = new Income();
                                income_s3 = incomeDal.getIncomebyId(orders.YearMonth, agents_s3.AgencyId, tr);

                                if (income_s3 == null || String.IsNullOrWhiteSpace(income_s3.AgentId))
                                {
                                    isinsert = true;

                                    income_s3.YearMonth = orders.YearMonth;
                                    income_s3.AgentId = agents_s3.Id;
                                    income_s3.AgentName = agents_s3.Name;
                                    income_s3.CareerStatus = agents_s3.CareerStatus;
                                    income_s3.Rank = agents_s3.Rank;
                                    income_s3.RefereeId = agents_s3.RefereeId;
                                    income_s3.RefereeName = agents_s3.RefereeName;
                                    income_s3.AgencyName = agents_s3.AgencyName;
                                    income_s3.AgencyId = agents_s3.AgencyId;
                                    income_s3.CreateTime = DateTime.Now;
                                    income_s3.UpdateTime = DateTime.Now;
                                    income_s3.LastMonthMoney = orderBLL.getOneMonthPrice(agents_s3.Id, tr);
                                    income_s3.NearlyThreeMonthsMoney = orderBLL.getThreeMonthPrice(agents_s3.Id, tr);
                                    income_s3.NearlySixMonthsMoney = orderBLL.getSixMonthPrice(agents_s3.Id, tr);
                                    income_s3.AllMonthMoney = orderBLL.getAllMonthPrice(agents_s3.Id, tr);
                                    income_s3.State = Convert.ToInt32(MyData.IncomeState.正常);
                                }

                                income_s3.UpdateTime = DateTime.Now;
                                income_s3.UpdatePerson = orders.UpdatePerson;
                                income_s3.OneMoney += orders.Price;
                                jisuanQuyuGuanliFuwufei(income_s3);
                                //计算总收入=VIP顾客销售奖金+个人订单返利+市场推广服务费+区域管理服务费+业绩分红
                                income_s3.IncomeMoney = income_s3.SalesServiceMoney + income_s3.PersonalServiceMoney + income_s3.MarketServiceMoney
                                    + income_s3.RegionServiceMoney + income_s3.RegionServiceYum;

                                incomeDal.InsertOrUpdateIncome(tr, income_s3, isinsert);
                            }
                        }
                    }
                }
            }


            //step 6 修改agents
            UpdateAgents(agents, income, tr);

        }
        public void jisuanGerenDingdanFencheng(Model.Income income)
        {
            //VIP顾客无个人订单返利
            if (income.Rank == "S1")
            {
                income.PersonalServiceMoney = 0;
            }
            else//其他级别一般情况下都是10%
            {
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

        public void jisuanShichangTuiguangFuwufei(Model.Income income_s)
        {
            //当月个人订单不足1000，次月停发推广服务费及管理服务费
            if (income_s.LastMonthMoney < 1000)
            {
                income_s.MarketServiceMoney = (decimal)0;
                return;
            }
            //1k - 1w 10 %
            if (income_s.MarketMoney >= 1000 && income_s.MarketMoney < 10000)
            {
                income_s.MarketServiceMoney = income_s.MarketMoney * (decimal)0.1;
            }
            //1w-3w 15%
            else if (income_s.MarketMoney >= 10000 && income_s.MarketMoney < 30000)
            {
                income_s.MarketServiceMoney = income_s.MarketMoney * (decimal)0.15;
            }
            //3w以上20%
            else if (income_s.MarketMoney >= 30000)
            {
                income_s.MarketServiceMoney = income_s.MarketMoney * (decimal)0.2;
            }

        }
        public void jisuanQuyuGuanliFuwufei(Model.Income incomes)
        {
            //当月个人订单不足1000，次月停发推广服务费及管理服务费
            if (incomes.LastMonthMoney < 1000)
            {
                incomes.RegionServiceMoney = (decimal)0;
                return;
            }
            //初级代理商计算0
            if (incomes.Rank == "D1")
            {
                incomes.RegionServiceMoney = 0;
            }
            //高级代理商计算一级5%
            else if (incomes.Rank == "D2")
            {
                incomes.RegionServiceMoney = incomes.OneMoney * (decimal)0.05;
            }
            //区域代理商一级5%、二级2%
            else if (incomes.Rank == "D3")
            {
                incomes.RegionServiceMoney = incomes.OneMoney * (decimal)0.05 + incomes.TwoMoney * (decimal)0.02;
            }
            //高级区域代理商一级5%、二级3%、三级1%
            else if (incomes.Rank == "D4")
            {
                incomes.RegionServiceMoney = incomes.OneMoney * (decimal)0.05 + incomes.TwoMoney * (decimal)0.02 + incomes.ThreeMoney * (decimal)0.01;
            }
            //总代理商一级5 %、二级4%、三级3%
            else if (incomes.Rank == "D5")
            {
                incomes.RegionServiceMoney = incomes.OneMoney * (decimal)0.05 + incomes.TwoMoney * (decimal)0.04 + incomes.ThreeMoney * (decimal)0.03;
            }
        }

        public void UpdateAgents(Model.Agents agents, Model.Income income, OleDbTransaction tr)
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
                new DAL.AgentsDal().UpdateAgents(agents);
            }
        }

        public DataTable getIncomeByYearmonth(String strWhere)
        {
            return new DAL.IncomeDal().getIncomeByYearmonth(strWhere);
        }
    }
}
