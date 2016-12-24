using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyData;
using System.Data.OleDb;
using System.Data;

namespace DAL
{
    public class IncomeDal
    {
        public Income getIncomebyId(Int32 yearMonth, String agentsId, OleDbTransaction tr)
        {
            return MyData.DataBase.Base_getFirst<Income>("select * from Income where YearMonth=" + yearMonth + " and AgentId='" + agentsId + "'", tr);
        }
        public DataTable getIncomeByYearmonth(String strWhere)
        {
            return MyData.DataBase.Base_dt("select * from Income where 1=1 " + strWhere);
        }
        public void InsertOrUpdateIncome(OleDbTransaction tr, Model.Income income, bool isInsert)
        {
            if (isInsert)
            {
                String sql = @"INSERT INTO [dbo].[Income]
                               ([YearMonth]
                               ,[AgentId]
                               ,[AgentName]
                               ,[CareerStatus]
                               ,[Rank]
                               ,[RefereeId]
                               ,[RefereeName]
                               ,[AgencyId]
                               ,[AgencyName]
                               ,[CreateTime]
                               ,[CreatePerson]
                               ,[UpdateTime]
                               ,[UpdatePerson]
                               ,[State]
                               ,[AllMonthMoney]
                               ,[LastMonthMoney]
                               ,[NearlyThreeMonthsMoney]
                               ,[NearlySixMonthsMoney]
                               ,[SalesMoney]
                               ,[SalesServiceMoney]
                               ,[PersonalMoney]
                               ,[PersonalServiceMoney]
                               ,[MarketMoney]
                               ,[MarketServiceMoney]
                               ,[OneMoney]
                               ,[TwoMoney]
                               ,[ThreeMoney]
                               ,[RegionServiceMoney]
                               ,[RegionYum]
                               ,[RegionServiceYum]
                               ,[IncomeMoney])
                         VALUES ({0} ,'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}'
                                   ,'{11}','{12}',{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30});";
                sql = String.Format(sql, income.YearMonth
                                            , income.AgentId
                                            , income.AgentName
                                            , income.CareerStatus
                                            , income.Rank
                                            , income.RefereeId
                                            , income.RefereeName
                                            , income.AgencyId
                                            , income.AgencyName
                                            , income.CreateTime
                                            , income.CreatePerson
                                            , income.UpdateTime
                                            , income.UpdatePerson
                                            , income.State
                                            , income.AllMonthMoney
                                            , income.LastMonthMoney
                                            , income.NearlyThreeMonthsMoney
                                            , income.NearlySixMonthsMoney
                                            , income.SalesMoney
                                            , income.SalesServiceMoney
                                            , income.PersonalMoney
                                            , income.PersonalServiceMoney
                                            , income.MarketMoney
                                            , income.MarketServiceMoney
                                            , income.OneMoney
                                            , income.TwoMoney
                                            , income.ThreeMoney
                                            , income.RegionServiceMoney
                                            , income.RegionYum
                                            , income.RegionServiceYum
                                            , income.IncomeMoney);

                DataBase.Base_cmd(sql, tr);
            }
            else
            {
                String sql = @"UPDATE [dbo].[Income]
                               SET [AgentName] = '{0}'
                                  ,[CareerStatus] = '{1}'
                                  ,[Rank] = '{2}'
                                  ,[RefereeId] = '{3}'
                                  ,[RefereeName] = '{4}'
                                  ,[AgencyId] = '{5}'
                                  ,[AgencyName] = '{6}'
                                  ,[CreateTime] = '{7}'
                                  ,[CreatePerson] = '{8}'
                                  ,[UpdateTime] = '{9}'
                                  ,[UpdatePerson] = '{10}'
                                  ,[State] = {11}
                                  ,[AllMonthMoney] = {12}
                                  ,[LastMonthMoney] = {13}
                                  ,[NearlyThreeMonthsMoney] = {14}
                                  ,[NearlySixMonthsMoney] = {15}
                                  ,[SalesMoney] = {16}
                                  ,[SalesServiceMoney] = {17}
                                  ,[PersonalMoney] = {18}
                                  ,[PersonalServiceMoney] = {19}
                                  ,[MarketMoney] = {20}
                                  ,[MarketServiceMoney] = {21}
                                  ,[OneMoney] = {22}
                                  ,[TwoMoney] = {23}
                                  ,[ThreeMoney] = {24}
                                  ,[RegionServiceMoney] = {25}
                                  ,[RegionYum] = {26}
                                  ,[RegionServiceYum] = {27}
                                  ,[IncomeMoney] = {28}
                             WHERE YearMonth=" + income.YearMonth + " and AgentId='" + income.AgentId + "'";
                sql = String.Format(sql, income.AgentName
                                            , income.CareerStatus
                                            , income.Rank
                                            , income.RefereeId
                                            , income.RefereeName
                                            , income.AgencyId
                                            , income.AgencyName
                                            , income.CreateTime
                                            , income.CreatePerson
                                            , income.UpdateTime
                                            , income.UpdatePerson
                                            , income.State
                                            , income.AllMonthMoney
                                            , income.LastMonthMoney
                                            , income.NearlyThreeMonthsMoney
                                            , income.NearlySixMonthsMoney
                                            , income.SalesMoney
                                            , income.SalesServiceMoney
                                            , income.PersonalMoney
                                            , income.PersonalServiceMoney
                                            , income.MarketMoney
                                            , income.MarketServiceMoney
                                            , income.OneMoney
                                            , income.TwoMoney
                                            , income.ThreeMoney
                                            , income.RegionServiceMoney
                                            , income.RegionYum
                                            , income.RegionServiceYum
                                            , income.IncomeMoney);
                DataBase.Base_cmd(sql, tr);
            }
        }
    }
}
