using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DictBankBLL
    {
        public DataTable getBank(String strWhere)
        {
            return new DAL.DictBankDal().getBank(strWhere);
        }
    }
}
