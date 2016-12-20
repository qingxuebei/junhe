using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyData;
using System.Data.OleDb;

namespace DAL
{
   public class IncomeDal
    {
        public Income getIncomebyId(Int32 yearMonth, String agentsId)
        {
            return MyData.DataBase.Base_getFirst<Income>("select * from Income where YearMonth=" + yearMonth + " and AgentId='" + agentsId + "'");
        }
    }
}
