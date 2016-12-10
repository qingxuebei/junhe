using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyData;

namespace DAL
{
    public class DictBankDal
    {
        public DataTable getBank(String strWhere)
        {
            String sql = "select * from DictBank where 1=1 ";
            if (String.IsNullOrWhiteSpace(strWhere))
            {
                sql += strWhere;
            }
            return DataBase.Base_dt(sql);
        }
    }
}
