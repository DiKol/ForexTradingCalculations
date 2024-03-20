using ForexTradingCalculations.Enums;
using ForexTradingCalculations.Models;

namespace ForexTradingCalculations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var order = new Order
            {
                Direction = OrderDirection.Buy,
                Leverage = 100,

            };

            var quote = new Quote
            {
                Ask = 1.0568M,
                Bid = 1.0568M,
            };
            
            Console.WriteLine("pip move    =  {0}", order.PipMove);
            Console.WriteLine("lot size    =  {0}", order.LotSize);
            Console.WriteLine("----------------------------");

            Example1(order, quote);
       
            Console.ReadLine();
        }


        // Example 1: https://www.cmcmarkets.com/en/trading-guides/what-is-a-pip-in-trading
        // Let’s say a trader places a $100,000 long trade on USD/CAD when it’s trading at 1.0548.
        // The value of USD/CAD rises to 1.0568. In this instance, one pip is a movement of 0.0001, so the trader has made a profit of 20 pips (1.0568 – 1.0548 = 0.0020 which is the equivalent of 20 pips).
        // The pip value in USD is (0.0001 x 100,000) / 1.0568 = $9.46
        // To calculate the profit or loss on the trade, we multiply the number of pips gained by the value of each pip.
        // In this example, the trader made a profit of 20 x $9.46 = $189.20.  The pip value in USD is (0.0001 x 100,000) / 1.0568 = $9.46
        // To calculate the profit or loss on the trade, we multiply the number of pips gained by the value of each pip.
        // In this example, the trader made a profit of 20 x $9.46 = $189.20.*/
        static void Example1(Order order, Quote quote)
        {
            order.Volume = 1;
            order.TradeSize = order.Volume * order.LotSize;

            order.OpenPrice = 1.0548M;
            var pipInBaseCurrency = (order.PipMove / quote.Bid * order.TradeSize);

            order.Amount = order.TradeSize * order.OpenPrice;

            Console.WriteLine("direction   =  {0}", order.Direction);
            Console.WriteLine("leverage    =  {0:0}", order.Leverage);
            Console.WriteLine("amount      =  {0:0.00}$", order.Amount);
            Console.WriteLine("trade size  =  {0:0.00}$", order.TradeSize);
            Console.WriteLine("pip cost    =  {0:0.00}$", pipInBaseCurrency);



            Console.WriteLine("volume      =  {0:0.00}", order.Volume);


            Console.WriteLine("open price  =  {0:0.0000}$", order.OpenPrice);
            Console.WriteLine("quote bid   =  {0:0.0000}$", quote.Bid);
            Console.WriteLine("quote ask   =  {0:0.0000}$", quote.Ask);


            if (order.Direction == OrderDirection.Buy) // means long and we should sell to ge profit
            {
                if (order.OpenPrice < quote.Bid) // we need sell
                {
                    //
                    var spread = quote.Bid - order.OpenPrice;

                    var pipsCount = spread / order.PipMove;
                    // now we can calculate profit in $ (currency)
                    // in pips 
                    order.ProfitLoss = pipInBaseCurrency * pipsCount;
                    order.ProfitLossModel = new ProfitLoss()
                    {
                        Price = quote.Bid,
                        Pips = (int)pipsCount

                    };

                    Console.WriteLine("profit pips =  {0:0}", pipsCount);
                    Console.WriteLine("profit      =  {0:0.00}$", order.ProfitLoss);
                    //Console.WriteLine("profit $    =  {0:0.0000}", pipInCurrency * pipsCount);

                    Console.WriteLine("Roman margin      =  {0:0.0000}", (order.OpenPrice * order.LotSize * order.Volume) / order.Leverage);
                    Console.WriteLine("Roman profit      =  {0:0.0000}$", spread * order.Volume * order.TradeSize);

                    //Margin = (Volume × ContractSize × Price) / Leverage
                    //Profit / Loss = (OpenPrice) × Volume × ContractSize

                }
                else if (order.OpenPrice > quote.Ask)
                {
                    var spread = quote.Ask - order.OpenPrice;
                    var pipsCount = spread / order.PipMove;
                    Console.WriteLine("loss pips  =  {0:0}", pipsCount);
                    Console.WriteLine("loss $     =  {0:0.0000}", pipInBaseCurrency * pipsCount);

                }
            }
        }

        // Example 2: https://www.cmcmarkets.com/en/trading-guides/what-is-a-pip-in-trading
        // Let’s say the trader places a $10,000 long trade on USD/CAD when it’s trading at 1.0570.
        // The value of USD/CAD falls to 1.0540. In this instance, one pip is a movement of 0.0001, so the trader has made a loss of 30 pips (1.0570 – 1.0540 = 0.0030 which is the equivalent of 30 pips).
        // The pip value in USD is (0.0001 x 10,000) / 1.0540 = $0.94
        // In this example, the trader made a loss of 30 x $0.94 = $28.20.
        static void Example2(Order order, Quote quote)
        {

            order.Direction = OrderDirection.Buy;

            // if you're trading 1 lot size with leverage
            order.Amount = 100;
            order.TradeSize = order.Amount * order.Leverage;

            order.OpenPrice = 1.0570M;
            var pipInBaseCurrency = (order.PipMove / quote.Bid * order.TradeSize);
            quote.Ask = quote.Bid = 1.0540M;

            Console.WriteLine("direction   =  {0}", order.Direction);
            Console.WriteLine("leverage    =  {0:0}", order.Leverage);
            Console.WriteLine("amount      =  {0:0.00}$", order.Amount);
            Console.WriteLine("trade size  =  {0:0.00}$", order.TradeSize);
            Console.WriteLine("pip cost    =  {0:0.00}$", pipInBaseCurrency);
            order.Volume = order.TradeSize / order.LotSize;
            Console.WriteLine("volume      =  {0:0.00}", order.Volume);


            Console.WriteLine("open price  =  {0:0.0000}$", order.OpenPrice);
            Console.WriteLine("quote bid   =  {0:0.0000}$", quote.Bid);
            Console.WriteLine("quote ask   =  {0:0.0000}$", quote.Ask);

            if (order.Direction == OrderDirection.Buy) // means long and we should sell to ge profit
            {
                if (order.OpenPrice < quote.Bid) // we need sell
                {
                    //
                    var spread = quote.Bid - order.OpenPrice;

                    var pipsCount = spread / order.PipMove;
                    // now we can calculate profit in $ (currency)
                    // in pips 

                    Console.WriteLine("profit pips =  {0:0}", pipsCount);
                    Console.WriteLine("profit      =  {0:0.00}$", pipInBaseCurrency * pipsCount);
                    //Console.WriteLine("profit $    =  {0:0.0000}", pipInCurrency * pipsCount);

                    Console.WriteLine("Roman margin      =  {0:0.0000}", (order.OpenPrice * order.LotSize * order.Volume) / order.Leverage);
                    Console.WriteLine("Roman profit      =  {0:0.0000}$", spread * order.Volume * order.TradeSize);

                    //Margin = (Volume × ContractSize × Price) / Leverage
                    //Profit / Loss = (OpenPrice) × Volume × ContractSize

                }
                else if (order.OpenPrice > quote.Ask)
                {
                    var spread = quote.Ask - order.OpenPrice;
                    var pipsCount = spread / order.PipMove;
                    Console.WriteLine("loss pips  =  {0:0}", pipsCount);
                    Console.WriteLine("loss $     =  {0:0.0000}", pipInBaseCurrency * pipsCount);

                }
            }
        }

        static void Example3(Order order, Quote quote)
        {

            order.Direction = OrderDirection.Sell;

            // if you're trading 1 lot size with leverage
            order.Amount = 100;
            order.TradeSize = order.Amount * order.Leverage;

            order.OpenPrice = 1.0570M;
            var pipInBaseCurrency = (order.PipMove / quote.Bid * order.TradeSize);
            quote.Ask = quote.Bid = 1.0540M;

            Console.WriteLine("direction   =  {0}", order.Direction);
            Console.WriteLine("leverage    =  {0:0}", order.Leverage);
            Console.WriteLine("amount      =  {0:0.00}$", order.Amount);
            Console.WriteLine("trade size  =  {0:0.00}$", order.TradeSize);
            Console.WriteLine("pip cost    =  {0:0.00}$", pipInBaseCurrency);
            order.Volume = order.TradeSize / order.LotSize;
            Console.WriteLine("volume      =  {0:0.00}", order.Volume);


            Console.WriteLine("open price  =  {0:0.0000}$", order.OpenPrice);
            Console.WriteLine("quote bid   =  {0:0.0000}$", quote.Bid);
            Console.WriteLine("quote ask   =  {0:0.0000}$", quote.Ask);




            if (order.Direction == OrderDirection.Sell) // means long and we should sell to ge profit
            {
                if (order.OpenPrice < quote.Ask) // we need sell
                {
                    //
                    var spread = quote.Ask - order.OpenPrice;

                    var pipsCount = spread / order.PipMove;
                    // now we can calculate profit in $ (currency)
                    // in pips 


                    Console.WriteLine("loss pips  =  {0:0}", -pipsCount);
                    Console.WriteLine("loss       =  {0:0.00}$", -pipInBaseCurrency * pipsCount);
                    //Console.WriteLine("profit $    =  {0:0.0000}", pipInCurrency * pipsCount);



                    //Margin = (Volume × ContractSize × Price) / Leverage
                    //Profit / Loss = (OpenPrice) × Volume × ContractSize

                }
                else if (order.OpenPrice > quote.Ask)
                {
                    var spread = quote.Ask - order.OpenPrice;
                    var pipsCount = spread / order.PipMove;
                    Console.WriteLine("profit pips  =  {0:0}", -pipsCount);
                    Console.WriteLine("profit $     =  {0:0.0000}", -pipInBaseCurrency * pipsCount);

                }
            }
        }
    }
}
