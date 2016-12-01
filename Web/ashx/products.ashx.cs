using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Web.ashx
{
    /// <summary>
    /// products 的摘要说明
    /// </summary>
    public class products : Base
    {
        public override String get(HttpContext context)
        {
            BLL.ProductsBLL productsBLL = new BLL.ProductsBLL();
            Model.Products products = new Model.Products();
            string[] st = context.Request.Params["wherestr"].ToString().Split(',');
            products.ProductName = st[0];
            DataTable dt = productsBLL.GetAllProducts(products);
            return MyData.Utils.EasyuiDataGridJson(dt);
        }
        public override String add(HttpContext context)
        {
            BLL.ProductsBLL productsBLL = new BLL.ProductsBLL();
            Model.Products products = new Model.Products();
            products.Id = System.Guid.NewGuid().ToString();
            products.ProductName = context.Request.Params["ProductName"].ToString();
            products.Unit = context.Request.Params["Unit"].ToString();
            products.UnitPrice = Convert.ToDecimal(context.Request.Params["UnitPrice"].ToString());
            products.CreatePerson = userName;
            products.UpdatePerson = userName;
            products.State= Convert.ToInt32(context.Request.Params["State"].ToString());
            if (productsBLL.Insert(products))
            {
                return "0";
            }
            else
            {
                return "保存失败";
            }
        }
        public override String update(HttpContext context)
        {
            BLL.ProductsBLL productsBLL = new BLL.ProductsBLL();
            Model.Products products = new Model.Products();
            products.Id = context.Request.Params["Id"].ToString();
            products.ProductName = context.Request.Params["ProductName"].ToString();
            products.Unit = context.Request.Params["Unit"].ToString();
            products.UnitPrice = Convert.ToDecimal(context.Request.Params["UnitPrice"].ToString());
            products.UpdatePerson = userName;
            products.State = Convert.ToInt32(context.Request.Params["State"].ToString());
            if (productsBLL.Update(products))
            {
                return "0";
            }
            else
            {
                return "保存失败";
            }
        }
        public override String del(HttpContext context)
        {
            return null;
        }
    }
}