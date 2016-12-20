using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyData
{
    public enum CareerStatus
    {
        A = 0,
        P = 1,
        T = 2
    }
    public enum AgentsStatus
    {
        代理人 = 0,
        代理商 = 1,
        合伙人 = 2
    }
    public enum AgentsState
    {
        新添加 = -1,
        已过期 = 0,
        正常 = 1
    }
    public enum OrdersState
    {
        已作废 = 0,
        正常 = 1
    }
    public enum OrdersDetailState
    {
        添加中 = -1,
        正常 = 1
    }
    public enum IncomeState
    {
        正常 = 1
    }
}

