using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ashx
{
    /// <summary>
    /// logMonthCreate 的摘要说明
    /// </summary>
    public class logMonthCreate : Base
    {

        public override string get(HttpContext context)
        {
            BLL.LogMonthCreateBLL logMonthCreateBLL = new BLL.LogMonthCreateBLL();
            String strWhere = " order by CreateTime desc";

            List<Model.LogMonthCreate> logMonthCreateList = logMonthCreateBLL.getLogMonthCreateList(strWhere);
            return MyData.Utils.EasyuiDataGridJsonForList(logMonthCreateList);
        }
        public override string add(HttpContext context)
        {
            try
            {
                LogMonthCreate log = new LogMonthCreate();
                log.Id = System.Guid.NewGuid().ToString();
                log.YearMonth = MyData.Utils.getLastYearMonth();
                log.CreateTime = DateTime.Now;
                log.State = (int)MyData.LogMonthCreateState.执行中;
                log.UpdateTime = DateTime.Now;
                log.CreatePerson = "dingshi";
                log.UpdatePerson = "dingshi";
                new BLL.LogMonthCreateBLL().zhixing(log);
                return "0";
            }
            catch (Exception ex)
            {

            }
            return "执行失败！";
        }
    }
}