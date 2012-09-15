using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aGrouponClasses.Utils {
   public class Enums {
       public enum enmRoles
       {
           Admin = 1,
           Partner = 2,
           User = 3,
           Moderator = 4
       }

       public enum enmCategoryTypes
       {
           City = 1,
           DealCategory = 2,
           ForumCategory = 3,
           PartnersCategory = 4,
           UserGrade = 5,
           Content = 6
       }

       public enum enmCoupontype
       {
           Usable= 1,
           Used = 2,
           Expired = 3
       }

       public enum enmDealStatus
       {
           TodayDeals =1,
           TippedDeals=2,
           FailedDeals =3
       }

       public enum enmCouponStatus
       {
           Unused = 0,
           Used = 1
       }

       public enum enmCouponConsumeStatus {
           NotConsumed = 0,
           Consumed = 1
       }

       public enum enmOrderStatus
       {
           Paid = 1,
           ToBePaid = 2
       }

       public enum enmPaymentType
       {
           DemirBankCreditCard = 1,
           BeelineKG = 2
       }

       public enum enmRefundStatus
       {
           OrderSuccessful=1,
           RefundedToAccountBalance = 2,
           RefundedByOtherMeans = 3
       }

       public enum enmCategories
       {
           Tour = 21
       }
    }

    public class EnumMessage
    {
        public static string OrderStatusMessage(int OrderStatus)
        {
            switch (OrderStatus)
            {
                case (int)Enums.enmOrderStatus.Paid:
                    return "Paid";
                case (int)Enums.enmOrderStatus.ToBePaid:
                    return "To Be Paid";

                default:
                    return "";
            }
        }

        public static string PaymentTypeMessage(int PaymentType) {
            switch (PaymentType) {
                case (int)Enums.enmPaymentType.BeelineKG:
                    return "SMS (Beeline KG)";
                case (int)Enums.enmPaymentType.DemirBankCreditCard:
                    return "Credit Card";

                default:
                    return "Unknown";
            }
        }

        public static string RefundStatusMessage(int PaymentType) {
            switch (PaymentType) {
                case (int)Enums.enmRefundStatus.OrderSuccessful:
                    return "Order Succesful";
                case (int)Enums.enmRefundStatus.RefundedByOtherMeans:
                    return "Refund (Other Means)";
                case (int)Enums.enmRefundStatus.RefundedToAccountBalance:
                    return "Refunded to Account Balance";

                default:
                    return "Unknown";
            }
        }
    }
}
