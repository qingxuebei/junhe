using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class OrdersBLL
    {
        public bool Insert(Model.Orders orders, List<Model.OrdersDetail> ordersDetailList)
        {
            DAL.OrdersDal ordersDal = new DAL.OrdersDal();
            //先把订单添加到数据库
            ordersDal.Insert(orders, ordersDetailList);
            //判断是否为代理人
            if (new DAL.AgentsDal().IsAgents(orders.AgentId))
            {
                Model.Agents agents = new Model.Agents();
                agents.Id = orders.AgentId;
                //计算截至目前当月个人订单
                decimal sumPrice = ordersDal.SumPrice(orders.AgentId);
                if (sumPrice >= 2500)
                {
                    agents.State = Convert.ToInt32(MyData.AgentsState.正常);
                    agents.Rank = "S1";//新用户进来职级都为S1
                    agents.CareerStatus = "A";
                    if (sumPrice >= 10000)
                    {
                        agents.Rank = "S2";
                    }
                }
                new DAL.AgentsDal().UpdateAgents(agents);
            }
            return true;
        }

        //已过期的人是不可以添加订单的

        public DataTable GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            DAL.OrdersDal ordersDal = new DAL.OrdersDal();
            return ordersDal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        public int GetRecordCount(string strWhere)
        {
            DAL.OrdersDal ordersDal = new DAL.OrdersDal();
            return ordersDal.GetRecordCount(strWhere);
        }

        public bool Update(Model.Orders orders)
        {
            DAL.OrdersDal ordersDal = new DAL.OrdersDal();
            ordersDal.Update(orders);

            //判断是否为代理人
            if (new DAL.AgentsDal().IsAgents(orders.AgentId))
            {
                Model.Agents agents = new Model.Agents();
                agents.Id = orders.AgentId;
                //计算截至目前当月个人订单
                decimal sumPrice = ordersDal.SumPrice(orders.AgentId);
                if (sumPrice >= 2500)
                {
                    agents.State = Convert.ToInt32(MyData.AgentsState.正常);
                    agents.Rank = "S1";//新用户进来职级都为S1
                    agents.CareerStatus = "A";
                    if (sumPrice >= 10000)
                    {
                        agents.Rank = "S2";
                    }
                }
                new DAL.AgentsDal().UpdateAgents(agents);
            }
            return true;
        }
    }
}
