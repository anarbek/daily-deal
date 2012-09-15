using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aGrouponClasses.Payment {
    public interface IPayment
    {
        string PaymentType { get; }
        decimal AmountToBePaid { get; }
        string PaymentCode { get; }
        void SetAmountToBePaid(decimal Amount);
        string StatusMessage { get; set; }
        bool MakePayment();
    }
}
