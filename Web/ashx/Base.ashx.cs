using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ashx
{
    /// <summary>
    /// Base 的摘要说明
    /// </summary>
    public class Base : IHttpHandler
    {
        public String userId, userName;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                userId = "123456";
                userName = "test";

                switch (context.Request.Params["type"].ToString())
                {
                    case "get":
                        context.Response.Write(get(context));
                        break;
                    case "add":
                        context.Response.Write(add(context));
                        break;
                    case "update":
                        context.Response.Write(update(context));
                        break;
                    case "delete":
                        context.Response.Write(del(context));
                        break;

                }
            }
            catch (Exception ex)
            {
                context.Response.Write("系统错误！");
            }
        }

        public virtual String get(HttpContext context)
        {
            return null;
        }
        public virtual String add(HttpContext context)
        {
            return null;
        }
        public virtual String update(HttpContext context)
        {
            return null;
        }
        public virtual String del(HttpContext context)
        {
            return null;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}