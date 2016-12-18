using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Income
    {
        public string YearMonth { get; set; }
        public string AgentId { get; set; }
        public string AgentName { get; set; }
        public string CareerStatus { get; set; }
        public string Rank { get; set; }
        public string RefereeId { get; set; }
        public string RefereeName { get; set; }
        public string AgencyId { get; set; }
        public string AgencyName { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string CreatePerson { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public string UpdatePerson { get; set; }
        public int State { get; set; }
        public decimal LastMonthMoney { get; set; }
        public decimal NearlyThreeMonthsMoney { get; set; }
        public decimal NearlySixMonthsMoney { get; set; }
        public decimal SalesMoney { get; set; }
        public decimal SalesServiceMoney { get; set; }
        public decimal PersonalMoney { get; set; }
        public decimal PersonalServiceMoney { get; set; }
        public decimal MarketMoney { get; set; }
        public decimal MarketServiceMoney { get; set; }
        public decimal OneMoney { get; set; }
        public decimal TwoMoney { get; set; }
        public decimal ThreeMoney { get; set; }
        public decimal RegionServiceMoney { get; set; }
        public decimal RegionYum { get; set; }
        public decimal RegionServiceYum { get; set; }
        public decimal IncomeMoney { get; set; }
    }
}
