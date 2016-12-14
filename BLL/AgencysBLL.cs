using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AgencysBLL
    {
        public DataTable GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            DAL.AgencysDal agencysDal = new DAL.AgencysDal();
            return agencysDal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        public int GetRecordCount(string strWhere)
        {
            DAL.AgencysDal agencysDal = new DAL.AgencysDal();
            return agencysDal.GetRecordCount(strWhere);
        }
    }
}
