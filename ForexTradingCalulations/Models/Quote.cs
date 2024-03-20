using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForexTradingCalculations.Models
{
    public class Quote
    {
        public string? Pair { get; set; }

        /// <summary>
        /// Lowest price at which a seller will sell
        /// </summary>
        public decimal Ask { get; set; }


        /// <summary>
        /// Highest price a buyer will pay
        /// </summary>
        public decimal Bid { get; set; }

        public DateTime Time { get; set; }
    }
}
