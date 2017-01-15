using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Model;
using MyData;

namespace Web.ashx
{
    /// <summary>
    /// hzorders 的摘要说明
    /// </summary>
    public class hzorders : Base
    {

        public override string export(HttpContext context)
        {
            String yearMonth = context.Request.Params["yearMonth"].ToString();
            String style = context.Request.Params["style"].ToString();
            if (!String.IsNullOrWhiteSpace(yearMonth))
            {
                if (style == "cp")
                {
                    DataTable dt = new BLL.OrdersDetailBLL().HuiZongOrdersDetail(Convert.ToInt32(yearMonth));
                    var list = MyData.Utils.ConvertToList<OrdersDetail>(dt).ToList();
                    String savePath = @"\DataExport\Chanpin_HZ" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                    ExcelHelper.Excel_ChanpinHZ(yearMonth, list, System.Web.HttpContext.Current.Server.MapPath(@"\Template\Chanpin_HZ.xls"),
                        System.Web.HttpContext.Current.Server.MapPath(savePath));
                    return savePath;
                }
                else if (style == "dd")
                {
                    DataTable dt = new BLL.OrdersDetailBLL().HuiZongOrders(Convert.ToInt32(yearMonth));
                    String savePath = @"\DataExport\Dingdan_HZ" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                    ExcelHelper.Excel_DingdanHZ(yearMonth, dt, System.Web.HttpContext.Current.Server.MapPath(@"\Template\Dingdan_HZ.xls"),
                        System.Web.HttpContext.Current.Server.MapPath(savePath));
                    return savePath;
                }
            }
            return null;
        }
    }
}