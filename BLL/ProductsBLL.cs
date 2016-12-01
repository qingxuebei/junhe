using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class ProductsBLL
    {
        public DataTable GetAllProducts(Model.Products products)
        {
            ProductsDal productDal = new ProductsDal();

            return productDal.GetAllProducts(products);
        }
        public bool Insert(Model.Products products)
        {
            ProductsDal productDal = new ProductsDal();
            return productDal.Insert(products);
        }
        public bool Update(Model.Products products)
        {
            ProductsDal productDal = new ProductsDal();
            return productDal.Update(products);
        }
    }
}
