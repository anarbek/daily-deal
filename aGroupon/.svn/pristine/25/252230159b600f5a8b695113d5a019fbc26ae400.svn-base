using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aGrouponClasses.Payment {
    public class PaymentBLL {
        public string StatusMessage { get; set; }
        public string PaymentCode { get; set; }
        public bool MakePayment(decimal amountToBePaid, IPayment paymentAgent) {
            paymentAgent.SetAmountToBePaid(amountToBePaid);
            bool result = paymentAgent.MakePayment(); //ödeme yapılamazsa kesin error message'ini göster
            StatusMessage = paymentAgent.StatusMessage;
            PaymentCode = paymentAgent.PaymentCode;
            return result;
        }
    }
}
