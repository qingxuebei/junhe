using MyData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Web.ashx
{
    /// <summary>
    /// income 的摘要说明
    /// </summary>
    public class income : Base
    {

        public override String get(HttpContext context)
        {
            String strWhere = "";
            BLL.IncomeBLL incomeBLL = new BLL.IncomeBLL();
            string yearMonth = context.Request.Params["yearMonth"].ToString();
            strWhere += " and YearMonth=" + yearMonth;
            String type = context.Request.Params["u_type"].ToString();
            if (type == "0")
            {
                //代理人
                strWhere += " and Rank like 'S%'";
            }
            else if (type == "1")
            {
                //代理商
                strWhere += " and Rank like 'D%'";
            }
            else if (type == "2")
            {
                //合伙人
                strWhere += " and Rank like 'P%'";
            }
            DataTable dt = incomeBLL.getIncomeByYearmonth(strWhere);

            return MyData.Utils.EasyuiDataGridJson(dt);
        }
        public override String add(HttpContext context)
        {
            return null;
        }
        public override String update(HttpContext context)
        {
            return null;
        }
        public override String del(HttpContext context)
        {
            return null;
        }
        public override String export(HttpContext context)
        {
            String strWhere = "";
            BLL.IncomeBLL incomeBLL = new BLL.IncomeBLL();
            string yearMonth = context.Request.Params["yearMonth"].ToString();
            strWhere += " and YearMonth=" + yearMonth;
            String type = context.Request.Params["u_type"].ToString();
            if (type == "0")
            {
                //代理人
                strWhere += " and Rank like 'S%'";
            }
            else if (type == "1")
            {
                //代理商
                strWhere += " and Rank like 'D%'";
            }
            else if (type == "2")
            {
                //合伙人
                strWhere += " and Rank like 'P%'";
            }
            DataTable dt = incomeBLL.getIncomeByYearmonth(strWhere);
            ExcelHelper.Excel_daochu(dt, System.Web.HttpContext.Current.Server.MapPath(@"\"), true);
            return @"\数据导出.xls";
        }
    }
}