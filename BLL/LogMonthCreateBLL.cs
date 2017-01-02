using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LogMonthCreateBLL
    {
        public List<LogMonthCreate> getLogMonthCreateList(String sqlWhere)
        {
            return new DAL.LogMonthCreateDal().getLogMonthCreateList(sqlWhere);
        }
        public String zhixing(LogMonthCreate logMonthCreate)
        {
            //判断本月是否存在已生成记录
            String sql = " and YearMonth=" + MyData.Utils.getLastYearMonth();
            List<LogMonthCreate> logMonthCreateList = getLogMonthCreateList(sql);
            if (logMonthCreateList.Count > 0)
            {
                var logMonthCreate1 = logMonthCreateList.OrderByDescending(o => o.CreateTime).First();
                if (logMonthCreate1.State != (int)MyData.LogMonthCreateState.执行中)
                {
                    DAL.LogMonthCreateDal logdal = new DAL.LogMonthCreateDal();
                    //step1 添加一条记录到表
                    logdal.Insert(logMonthCreate);
                    //执行操作
                    new BLL.AgentsBLL().jisuan();
                    //修改状态
                    logdal.UpdateState(logMonthCreate.Id);
                    return "执行成功！";
                }
            }
            else
            {
                DAL.LogMonthCreateDal logdal = new DAL.LogMonthCreateDal();
                //step1 添加一条记录到表
                logdal.Insert(logMonthCreate);
                //执行操作
                new BLL.AgentsBLL().jisuan();
                //修改状态
                logdal.UpdateState(logMonthCreate.Id);
                return "执行成功！";
            }
            return "执行失败！";
        }
    }
}
