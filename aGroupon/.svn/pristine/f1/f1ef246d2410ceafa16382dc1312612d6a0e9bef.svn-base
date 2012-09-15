using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aGrouponClasses.Models;

namespace aGrouponClasses.Repositories {
    public class DealRepositoryEF : IDealRepository {
        aGrouponModelsDataContext context = new aGrouponModelsDataContext();
        public IQueryable<tDeal> All {
            get { return context.tDeals; }
        }

        public tDeal Find(int id) {
            return context.tDeals.Where(t => t.IDDeal.Equals(id)).FirstOrDefault();
        }

        public List<tDeal> GetListByIDPartner(int idPartner) {
            return context.tDeals.Where(t => t.tUser.IDUser.Equals(idPartner)).ToList();
        }

        public List<tDeal> GetListByCity(int idCity)
        {
            var query = from t in context.tDealCities 
                        join dl in context.tDeals on t.IDDeal equals dl.IDDeal
                        where t.IDCity == idCity orderby dl.DateStarting descending
                        select dl;
            return query.ToList();
            //return context.tDeals.Where(t => t.tUser.IDUser.Equals(idCity)).ToList();
        }

        public List<tDeal> GetListByCityAndGroup(int currIDCity, int idgr) {
            var query = from t in context.tDealCities
                        join dl in context.tDeals on t.IDDeal equals dl.IDDeal
                        where t.IDCity == currIDCity && dl.IDGroup == idgr
                        select dl;
            return query.ToList();
        }

        public tDeal GetLatestDeal(int IDCity)
        {
            List<tDeal> currDealsForSelectedCity = GetListByCity(IDCity);
            if (currDealsForSelectedCity != null && currDealsForSelectedCity.Count > 0)
                return currDealsForSelectedCity[0];
            return null;
        }

        public void InsertOrUpdate(tDeal user) {
            if (user.IDDeal == default(int)) {
                // New entity
                user.DateAdded = DateTime.Now;
                context.tDeals.InsertOnSubmit(user);
            } else {
                // Existing entity
                tDeal userToUpdate = Find(user.IDDeal);
                if (userToUpdate != null && userToUpdate.IDDeal > 0) {
                    userToUpdate.AllowRefundFlag = user.AllowRefundFlag;
                    userToUpdate.BiggestCardUse = user.BiggestCardUse;
                    userToUpdate.ConsumerRebates = user.ConsumerRebates;
                    userToUpdate.DateCouponExpiry = user.DateCouponExpiry;
                    userToUpdate.DateEnding = user.DateEnding;
                    if (userToUpdate.tDealCities.Count > 0)
                    {
                        for (int i = 0; i < userToUpdate.tDealCities.Count; i++)
                        {
                            context.tDealCities.DeleteOnSubmit(userToUpdate.tDealCities[i]);
                        }
                        context.SubmitChanges();
                        userToUpdate.tDealCities = user.tDealCities;
                    }

                    userToUpdate.DateStarting = user.DateStarting;
                    userToUpdate.DealPrice = user.DealPrice;
                    userToUpdate.DealTitle = user.DealTitle;
                    userToUpdate.Detail = user.Detail;
                    userToUpdate.FlvVideoFile = user.FlvVideoFile;

                    userToUpdate.IDDealCategory = user.IDDealCategory;
                    userToUpdate.IDDealType = user.IDDealType;
                    userToUpdate.IDGroup = user.IDGroup;
                    userToUpdate.IDPartner = user.IDPartner;
                    userToUpdate.Introduction = user.Introduction;

                    userToUpdate.InvitationRebate = user.InvitationRebate;
                    userToUpdate.LimitLower = user.LimitLower;
                    userToUpdate.LimitUpper = user.LimitUpper;
                    userToUpdate.LimitUser = user.LimitUser;
                    userToUpdate.MarketPrice = user.MarketPrice;

                    userToUpdate.ModeOfDelivery = user.ModeOfDelivery;
                    userToUpdate.PrductName = user.PrductName;
                    userToUpdate.ProductImage = user.ProductImage;
                    userToUpdate.ProductImage1 = user.ProductImage1;
                    userToUpdate.ProductImage2 = user.ProductImage2;

                    userToUpdate.ProductOptions = user.ProductOptions;
                    userToUpdate.QuantityMinimum = user.QuantityMinimum;
                    userToUpdate.SortOrder = user.SortOrder;

                    userToUpdate.Tips = user.Tips;
                    userToUpdate.UserReviews = user.UserReviews;
                    userToUpdate.VirtualBuyers = user.VirtualBuyers;

                }
            }
            context.SubmitChanges();
        }

        public void Delete(int id) {
            tDeal user = Find(id);
            if (user != null && user.IDDeal > 0)
            {
                for (int i = 0; i < user.tDealCities.Count; i++)
                {
                    context.tDealCities.DeleteOnSubmit(user.tDealCities[i]);
                }
                context.tDeals.DeleteOnSubmit(user);
            }
            context.SubmitChanges();
        }

        public void Save() {
            context.SubmitChanges();
        }


        public List<tDeal> GetListByDealStatus(int dealStatus) {
            return context.tDeals.ToList();
        }

        public List<tDeal> GetListByIDCategory(int iDCategory) {
            return context.tDeals.Where(t => t.IDDealCategory.Equals(iDCategory)).ToList();
        }

        public int Delete(int[] array) {
            tDeal[] deals =
                (from t in context.tDeals where array.Contains(t.IDDeal) select t).ToArray();
            context.tDeals.DeleteAllOnSubmit(deals);
            context.SubmitChanges();
            return 1;
        }
    }

    public interface IDealRepository
    {
        IQueryable<tDeal> All { get; }
        tDeal Find(int id);
        List<tDeal> GetListByIDPartner(int idPartner);
        List<tDeal> GetListByDealStatus(int dealStatus);
        List<tDeal> GetListByIDCategory(int iDCategory);
        List<tDeal> GetListByCity(int idCity);
        void InsertOrUpdate(tDeal user);
        void Delete(int id);
        void Save();

        int Delete(int[] array);

        List<tDeal> GetListByCityAndGroup(int currIDCity, int idgr);

        tDeal GetLatestDeal(int IDCity);
    }

    
}
