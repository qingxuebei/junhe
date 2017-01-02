using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using MyData;

namespace DAL
{
    public partial class LogMonthCreateDal
    {
        public List<LogMonthCreate> getLogMonthCreateList(String sqlWhere)
        {
            String sql = "select * from LogMonthCreate where 1=1 " + sqlWhere;
            return DataBase.Base_list<LogMonthCreate>(sql);
        }
        public void Insert(Model.LogMonthCreate logMonthCreate)
        {
            String sql = @"INSERT INTO [dbo].[LogMonthCreate]
                                   ([Id]
                                   ,[Name]
                                   ,[CreateTime]
                                   ,[CreatePerson]
                                   ,[UpdateTime]
                                   ,[UpdatePerson]
                                   ,[State])
                             VALUES
                                   ('{0}'
                                   ,'{1}'
                                   ,'{2}'
                                   ,'{3}'
                                   ,'{4}'
                                   ,'{5}'
                                   ,{6}";
            sql = String.Format(sql, logMonthCreate.Id, logMonthCreate.YearMonth, logMonthCreate.CreateTime, logMonthCreate.CreatePerson, logMonthCreate.UpdateTime,
                logMonthCreate.UpdatePerson, logMonthCreate.State);
            DataBase.Base_cmd(sql);
        }
        public void UpdateState(String id)
        {
            String sql = "update [dbo].[LogMonthCreate] set State=1 where Id='" + id + "'";
            DataBase.Base_cmd(sql);
        }
    }
}
