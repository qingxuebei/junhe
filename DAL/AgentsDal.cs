using Model;
using MyData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AgentsDal
    {
        public DataTable GetAgents(String strWhere)
        {
            String sql = "select Id,(Id+'--'+Name) as Name,Name as AgentsName,AgencyId,AgencyName from Agents where 1=1" + strWhere;
            return DataBase.Base_dt(sql);
        }
        public DataTable GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.CreateTime desc");
            }
            strSql.Append(")AS Row, T.*  from Agents T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DataBase.Base_dt(strSql.ToString());
        }
        public int GetRecordCount(string strWhere)
        {
            return DataBase.Base_count("Agents", strWhere);
        }
        public bool Insert(Model.Agents agents)
        {
            String sql = @"INSERT INTO [dbo].[Agents]
                           ([Id]
                           ,[Name]
                           ,[Province]
                           ,[City]
                           ,[Village]
                           ,[Birthday]
                           ,[CareerStatus]
                           ,[JoinDate]
                           ,[Rank]
                           ,[RefereeId]
                           ,[RefereeName]
                           ,[AgencyId]
                           ,[AgencyName]
                           ,[AccountBank]
                           ,[AccountBankBranch]
                           ,[Account]
                           ,[Address]
                           ,[ZipCode]
                           ,[Phone]
                           ,[CreateTime]
                           ,[CreatePerson]
                           ,[UpdateTime]
                           ,[UpdatePerson]
                           ,[State],[AgentsStatus])
                     VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}')";
            sql = String.Format(sql, agents.Id
                                    , agents.Name
                                    , agents.Province
                                    , agents.City
                                    , agents.Village
                                    , agents.Birthday
                                    , agents.CareerStatus
                                    , agents.JoinDate
                                    , agents.Rank
                                    , agents.RefereeId
                                    , agents.RefereeName
                                    , agents.AgencyId
                                    , agents.AgencyName
                                    , agents.AccountBank
                                    , agents.AccountBankBranch
                                    , agents.Account
                                    , agents.Address
                                    , agents.ZipCode
                                    , agents.Phone
                                    , agents.CreateTime
                                    , agents.CreatePerson
                                    , agents.UpdateTime
                                    , agents.UpdatePerson
                                    , agents.State, agents.AgentsStatus);
            return DataBase.Base_cmd(sql);
        }
        public bool Update(Model.Agents agents)
        {
            String sql = @"UPDATE [dbo].[Agents]
                           SET [Name] = '{0}'
                              ,[Province] = '{1}'
                              ,[City] = '{2}'
                              ,[Birthday] = '{3}'
                              ,[JoinDate] = '{4}'
                              ,[RefereeId] = '{5}'
                              ,[RefereeName] = '{6}'
                              ,[AccountBank] = '{7}'
                              ,[AccountBankBranch] = '{8}'
                              ,[Account] =  '{9}'
                              ,[Address] =  '{10}'
                              ,[ZipCode] =  '{11}'
                              ,[Phone] =  '{12}'
                              ,[UpdateTime] =  '{13}'
                              ,[UpdatePerson] =  '{14}'
                         WHERE Id='{16}'";
            sql = String.Format(sql, agents.Name
                                , agents.Province
                                , agents.City
                                , agents.Birthday
                                , agents.JoinDate
                                , agents.RefereeId
                                , agents.RefereeName
                                , agents.AccountBank
                                , agents.AccountBankBranch
                                , agents.Account
                                , agents.Address
                                , agents.ZipCode
                                , agents.Phone
                                , agents.UpdateTime
                                , agents.UpdatePerson, agents.Id);
            return DataBase.Base_cmd(sql);
        }
    }
}
