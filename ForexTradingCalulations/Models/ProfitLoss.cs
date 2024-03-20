using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForexTradingCalculations.Models
{
    public class ProfitLoss
    {
        public decimal? Price { get; set; }
        public int? Pips { get; set; }
        public int? Spread { get; set; }
    }
}
