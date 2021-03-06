﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aGrouponClasses.Models;

namespace aGrouponClasses.Repositories {
    public class OrderRepositoryEF : IOrderRepository {
        aGrouponModelsDataContext context = new aGrouponModelsDataContext();
        public IQueryable<tOrder> All {
            get { return context.tOrders; }
        }

        public tOrder Find(int id) {
            return context.tOrders.Where(t => t.IDOrder.Equals(id)).FirstOrDefault();
        }

        public tOrder Find(int IDOrder, int UserID) {
            return context.tOrders.Where(t => t.IDOrder.Equals(IDOrder) && t.IDUserBought.Equals(UserID)).FirstOrDefault();
        }

        public void InsertOrUpdate(tOrder order) {
            if (order.IDOrder == default(int)) {
                // New entity
                context.tOrders.InsertOnSubmit(order);
            } else {
                // Existing entity
                tOrder orderToUpdate = Find(order.IDOrder);
                if (orderToUpdate != null && orderToUpdate.IDOrder > 0) {
                    orderToUpdate.AmountPaid = order.AmountPaid;
                    orderToUpdate.BuyerNotes = order.BuyerNotes;
                    orderToUpdate.IDCoupon = order.IDCoupon;
                    orderToUpdate.IDDeal = order.IDDeal;
                    orderToUpdate.IDUserBought = order.IDUserBought;
                    orderToUpdate.MobilePhoneNo = order.MobilePhoneNo;
                    orderToUpdate.OrderNotes = order.OrderNotes;
                    orderToUpdate.OrderStatus = order.OrderStatus;
                    orderToUpdate.PaymentType = order.PaymentType;
                    orderToUpdate.Quantity = order.Quantity;
                    orderToUpdate.RefundStatus = order.RefundStatus;
                    orderToUpdate.Options = order.OrderNotes;
                    orderToUpdate.ReferrerAddress = order.ReferrerAddress;
                }
            }
        }

        public void Delete(int id) {
            tOrder user = Find(id);
            if (user != null && user.IDOrder > 0)
                context.tOrders.DeleteOnSubmit(user);
            context.SubmitChanges();
        }

        public void Save() {
            context.SubmitChanges();
        }

        public tOrder GetOrderByIDDeal(int IDDeal)
        {
            return context.tOrders.Where(t => t.IDDeal.Equals(IDDeal)).FirstOrDefault();
        }

        public tOrder GetOrderByIDDealIDUser(int IDDeal, int UserID)
        {
            return context.tOrders.Where(t => t.IDDeal.Equals(IDDeal) && t.IDUserBought.Equals(UserID)).FirstOrDefault();
        }

        public List<tOrder> GetListByPaidStatus(int PaidStatus)
        {
            return context.tOrders.Where(t => t.OrderStatus.Equals(PaidStatus)).ToList();
        }

        public List<tOrder> GetListByPartner(int UserID)
        {
            return context.tOrders.Where(t => t.tDeal.IDPartner.Equals(UserID)).ToList();
        }
        public List<tOrder> GetListByIDUserWhoBought(int UserID) {
            return context.tOrders.Where(t => t.IDUserBought.Equals(UserID)).ToList();
        }
    }

    public interface IOrderRepository
    {
        IQueryable<tOrder> All { get; }
        tOrder Find(int id);
        void InsertOrUpdate(tOrder user);
        void Delete(int id);
        void Save();

        tOrder GetOrderByIDDeal(int IDDeal);

        tOrder GetOrderByIDDealIDUser(int p, int UserID);

        List<tOrder> GetListByPaidStatus(int p);

        List<tOrder> GetListByPartner(int UserID);

        List<tOrder> GetListByIDUserWhoBought(int UserID);

        tOrder Find(int IDOrder, int UserID);
    }
}
