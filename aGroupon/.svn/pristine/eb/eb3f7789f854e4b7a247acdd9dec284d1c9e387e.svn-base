using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using aGrouponClasses.Models;
using aGrouponClasses.Repositories;
using B2B.BLL;

namespace aGrouponProjectMain.Controllers
{
    public partial class UserController : Controller
    {
      
        //
        // GET: /User/
        [Authorize]
        public ActionResult MyOrders()
        {
            int UserID = (int)MembershipHelper.GetCurrenUser().ProviderUserKey;
            List<tOrder> ordersOfThisUser = _orderRepository.GetListByIDUserWhoBought(UserID);
            return View(ordersOfThisUser);
        }

        [Authorize]
        public ActionResult OrderDetails(int? IDOrder)
        {
            IDOrder = IDOrder ?? 0;
            int UserID = (int)MembershipHelper.GetCurrenUser().ProviderUserKey;
            tOrder order = _orderRepository.Find((int)IDOrder, UserID);
            if (order == null)
                return RedirectToAction("OrderDataNotFound");
            return View(order);
        }

        [Authorize]
        public ActionResult MyCoupons()
        {
            int UserID = (int)MembershipHelper.GetCurrenUser().ProviderUserKey;
            List<tCoupon> couponsOfThisUser = _couponRepository.GetListByIDUserWhoBought(UserID);
            return View(couponsOfThisUser);
        }

    }
}
