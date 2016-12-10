using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyData;

namespace DAL
{
    public class ProductsDal
    {
        public DataTable GetAllProducts(Model.Products products)
        {
            String sql = "select * from Products where 1=1 ";
            if (!String.IsNullOrWhiteSpace(products.ProductName))
            {
                sql += " and ProductName='" + products.ProductName + "'";
            }
            return DataBase.Base_dt(sql);
        }
        public bool Insert(Model.Products products)
        {
            String sql = "insert into Products ([Id],[ProductName],[Unit] ,[UnitPrice],[CreatePerson],[CreateTime],[UpdatePerson],[UpdateTime],[State]) values (";
            sql += "'" + products.Id + "',";
            sql += "'" + products.ProductName + "',";
            sql += "'" + products.Unit + "',";
            sql += "" + products.UnitPrice + ",";
            sql += "'" + products.CreatePerson + "',";
            sql += "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
            sql += "'" + products.UpdatePerson + "',";
            sql += "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
            sql += products.State + ")";
            return DataBase.Base_cmd(sql);
        }
        public bool Update(Model.Products products)
        {
            String sql = "update Products set ProductName='{0}',Unit='{1}',UnitPrice={2},UpdatePerson='{3}',UpdateTime='{4}',State={5} where Id='{6}'";
            sql = String.Format(sql, products.ProductName, products.Unit, products.UnitPrice, products.UpdatePerson, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                products.State, products.Id);
            return DataBase.Base_cmd(sql);

        }
    }
}
