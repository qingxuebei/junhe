using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyData;

namespace DAL
{
    public class DictRegionDal
    {
        public DataTable getRegion(String strWhere)
        {
            String sql = "select * from DictRegion where 1=1 " + strWhere;
            return DataBase.Base_dt(sql);
        }
    }
}
