﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Web.ashx
{
    /// <summary>
    /// Summary description for agencys
    /// </summary>
    public class agencys : Base
    {

        public override String get(HttpContext context)
        {
            int pageRows, page; String order = "";
            pageRows = 20;
            page = 1;
            BLL.AgencysBLL agencysBLL = new BLL.AgencysBLL();
            String strWhere = " 1=1";
            string[] st = context.Request.Params["wherestr"].ToString().Split(',');
            if (!String.IsNullOrWhiteSpace(st[0]))
            {
                strWhere += " and Id='" + st[0] + "'";
            }
            if (null != context.Request["rows"])
            {
                pageRows = int.Parse(context.Request["rows"].ToString().Trim());
            }
            if (null != context.Request["page"])
            {
                page = int.Parse(context.Request["page"].ToString().Trim());
            }
            if (null != context.Request["sort"])
            {
                order = context.Request["sort"].ToString().Trim();
            }

            //调用分页的GetList方法  
            DataTable dt = agencysBLL.GetListByPage(strWhere.ToString(), order, (page - 1) * pageRows + 1, page * pageRows);
            int count = agencysBLL.GetRecordCount(strWhere.ToString());//获取条数  
            return MyData.Utils.EasyuiDataGridJson(dt, count);
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
    }
}