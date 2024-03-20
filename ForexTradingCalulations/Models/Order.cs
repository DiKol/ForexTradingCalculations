using ForexTradingCalculations.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForexTradingCalculations.Models
{
    public class Order
    {
        public int Id { get; set; }
        public OrderDirection Direction { get; set; }
        public OrderStatus Status { get; set; }
        public OrderType Type { get; set; }
        public decimal? RequestedPrice { get; set; }
        public decimal? OpenPrice { get; set; }
        public decimal? ClosePrice { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Volume { get; set; } = 2M;
        public decimal? TradeSize { get; set; }
        public decimal? ProfitLoss { get; set; }
        public decimal Leverage { get; set; }
        public decimal Margin { get; set; }
        public decimal PipMove { get; set; } = 0.0001M;
        public int? LotSize { get; set; } = 100000;
        public ProfitLoss? ProfitLossModel { get; set; }
    }
}
