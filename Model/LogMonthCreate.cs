using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class LogMonthCreate
    {
        public string Id { get; set; }

        public Int32 YearMonth { get; set; }

        public Nullable<System.DateTime> CreateTime { get; set; }

        public string CreatePerson { get; set; }

        public Nullable<System.DateTime> UpdateTime { get; set; }

        public string UpdatePerson { get; set; }

        public Int32 State { get; set; }
    }
}
