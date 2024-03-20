using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForexTradingCalculations.Enums
{
    public enum OrderStatus
    {
        Created = 0,
        Opened = 1,
        Closed = 2,
        Canceled = 3,
        Rejected = 4,
        Expired = 5
    }
}
