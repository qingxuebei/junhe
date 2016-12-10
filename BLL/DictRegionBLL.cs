using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DictRegionBLL
    {
        public DataTable getRegion(String strWhere)
        {
            return new DAL.DictRegionDal().getRegion(strWhere);
        }
    }
}
