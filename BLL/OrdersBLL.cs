using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class OrdersBLL
    {
        public bool Insert(Model.Orders orders)
        {
            DAL.OrdersDal ordersDal = new DAL.OrdersDal();
            OleDbConnection conn = MyData.DataBase.Conn();
            OleDbTransaction tr = conn.BeginTransaction();
            //先把订单添加到数据库
            ordersDal.Insert(orders,tr);

            return UpdateAgents(ordersDal, orders);
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

            return UpdateAgents(ordersDal, orders);
        }
        public bool UpdateAgents(DAL.OrdersDal ordersDal, Model.Orders orders)
        {
            //判断是否为代理人
            DataTable dt = new DAL.AgentsDal().GetAgents(" and Id = '" + orders.AgentId + "' and AgentsStatus = 0 and State != 0");
            if (dt.Rows.Count > 0)
            {
                Model.Agents agents = new Model.Agents();
                agents.Id = orders.AgentId;
                agents.State = Convert.ToInt32(MyData.AgentsState.正常);
                agents.State = Convert.ToInt32(dt.Rows[0]["State"].ToString());
                agents.Rank = dt.Rows[0]["Rank"].ToString();
                agents.CareerStatus = dt.Rows[0]["CareerStatus"].ToString();
                //计算截至目前当月个人订单
                decimal sumPrice = ordersDal.SumPrice(orders.AgentId);
                if (sumPrice >= 2500)
                {
                    agents.Rank = "S1";//新用户进来职级都为S1
                    agents.CareerStatus = "A";
                    if (sumPrice >= 10000)
                    {
                        agents.Rank = "S2";
                    }
                }
                return new DAL.AgentsDal().UpdateAgents(agents);
            }
            return false;
        }

        public Decimal getCurrentPrice(String agentId)
        {
            return new DAL.OrdersDal().SumPrice(" and AgentId='" + agentId + "' and YearMonthDate>'" + MyData.Utils.getMonthFirstDay() + "'");
        }
        public Decimal getOneMonthPrice(String agentId)
        {
            return new DAL.OrdersDal().SumPrice(" and AgentId='" + agentId + "' and YearMonthDate between '"
                + MyData.Utils.getMonthFirstDay().AddMonths(-1) + "' and '"
                + MyData.Utils.getMonthFirstDay() + "'");
        }
        public Decimal getThreeMonthPrice(String agentId)
        {
            return new DAL.OrdersDal().SumPrice(" and AgentId='" + agentId + "' and YearMonthDate between '"
                + MyData.Utils.getMonthFirstDay().AddMonths(-3) + "' and '"
                + MyData.Utils.getMonthFirstDay() + "'");
        }
        public Decimal getSixMonthPrice(String agentId)
        {
            return new DAL.OrdersDal().SumPrice(" and AgentId='" + agentId + "' and YearMonthDate between '"
                + MyData.Utils.getMonthFirstDay().AddMonths(-6) + "' and '"
                + MyData.Utils.getMonthFirstDay() + "'");
        }
        public Decimal getAllMonthPrice(String agentId)
        {
            return new DAL.OrdersDal().SumPrice(" and AgentId='" + agentId + "'");
        }
    }
}
