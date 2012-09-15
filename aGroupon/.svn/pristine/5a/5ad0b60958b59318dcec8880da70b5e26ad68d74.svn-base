using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aGrouponClasses.Models;

namespace aGrouponClasses.Repositories {
    public class PaymentMessageRepositoryEF : IPaymentMessageRepository {
        aGrouponModelsDataContext context = new aGrouponModelsDataContext();
        public IQueryable<tPaymentMessage> All {
            get { return context.tPaymentMessages; }
        }

        public tPaymentMessage Find(int id) {
            return context.tPaymentMessages.Where(t => t.IDPaymentMessage.Equals(id)).FirstOrDefault();
        }

        public tPaymentMessage Find(string uniqueid)
        {
            return context.tPaymentMessages.Where(t => t.UniqueID.Equals(uniqueid)).FirstOrDefault();
        }

        public void InsertOrUpdate(tPaymentMessage user) {
            if (user.IDPaymentMessage == default(int)) {
                // New entity
                context.tPaymentMessages.InsertOnSubmit(user);
            } else {
                // Existing entity
                tPaymentMessage userToUpdate = Find(user.IDPaymentMessage);
                if (userToUpdate != null && userToUpdate.IDPaymentMessage > 0) {
                    userToUpdate.IDDeal = user.IDDeal;
                    userToUpdate.SMSCode = user.SMSCode;
                    userToUpdate.Approved = user.Approved;
                }
            }
            context.SubmitChanges();
        }

        public void Delete(int id) {
            tPaymentMessage user = Find(id);
            if (user != null && user.IDPaymentMessage > 0)
                context.tPaymentMessages.DeleteOnSubmit(user);
            context.SubmitChanges();
        }

        public void Save() {
            context.SubmitChanges();
        }
    }
    public interface IPaymentMessageRepository
    {
        IQueryable<tPaymentMessage> All { get; }
        tPaymentMessage Find(int id);
        tPaymentMessage Find(string uniqueid);
        void InsertOrUpdate(tPaymentMessage user);
        void Delete(int id);
        void Save();
    }
}
