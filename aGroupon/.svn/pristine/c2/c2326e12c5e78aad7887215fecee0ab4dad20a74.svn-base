using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aGrouponClasses.Models;

namespace aGrouponClasses.Utils {
    public class tDealUtils {
        public static string GetLeftTSStrForDeal(tDeal deal) {
            if (deal.DateEnding < DateTime.Now)
                return "Deal Is Over";
            TimeSpan ts = deal.DateEnding - DateTime.Now;
            return ts.Days + " days " + ts.Hours + " hrs " + ts.Minutes + " min " + ts.Seconds + " sec";
        }

        public static bool DealIsActive(tDeal deal) {
            if (deal.DateEnding < DateTime.Now)
                return false;
            return true;
        }

        public static string DiscountStr(tDeal deal)
        {
            decimal currPrice = deal.DealPrice;
            decimal marketPrice = deal.MarketPrice;
            decimal discount = 100 - ((currPrice*100)/marketPrice);
            discount = Math.Ceiling(discount);
            return String.Format("{0:0.##} %", discount); 
        }
    }
}
