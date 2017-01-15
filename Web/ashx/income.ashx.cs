using log4net;
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
        private static readonly ILog logs = LogManager.GetLogger(typeof(income));
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
            string[] st = context.Request.Params["wherestr"].ToString().Split(',');
            if (!String.IsNullOrWhiteSpace(st[0]))
            {
                strWhere += " and AgentId='" + st[0].Trim() + "'";
            }
            if (!String.IsNullOrWhiteSpace(st[1]))
            {
                strWhere += " and AgentName like '%" + st[1].Trim() + "%'";
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
            String style = context.Request.Params["style"].ToString();
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
            string[] st = context.Request.Params["wherestr"].ToString().Split(',');
            if (!String.IsNullOrWhiteSpace(st[0]))
            {
                strWhere += " and AgentId='" + st[0].Trim() + "'";
            }
            if (!String.IsNullOrWhiteSpace(st[1]))
            {
                strWhere += " and AgentName like '%" + st[1].Trim() + "%'";
            }
            var list = incomeBLL.getIncomeList(strWhere);
            if (style == "mx")
            {
                String savePath = @"\DataExport\Shouru_MX" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                ExcelHelper.Excel_IncomeMX(yearMonth, list, System.Web.HttpContext.Current.Server.MapPath(@"\Template\Shouru_MX.xls"),
                    System.Web.HttpContext.Current.Server.MapPath(savePath));
                return savePath;

            }
            else if (style == "gy")
            {
                String savePath = @"\DataExport\Huiyuan_GY" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                ExcelHelper.Excel_HuiyuanGY(yearMonth, list, System.Web.HttpContext.Current.Server.MapPath(@"\Template\Huiyuan_GY.xls"),
                    System.Web.HttpContext.Current.Server.MapPath(savePath));
                return savePath;
            }
            return null;
        }
    }
}