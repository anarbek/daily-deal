using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using aGrouponClasses.Models;

namespace aGrouponProjectMain.Controllers {
    public class CommonUtils
    {
        public static CommonUtils Instance = new CommonUtils();
        public tCategory SelectedCity {
            get {
                tCategory currcity = null;
                if (HttpContext.Current.Session["SelectedCity"] == null)
                {
                    currcity = new tCategory();
                    HttpContext.Current.Session["SelectedCity"] = currcity;
                }
                else
                {
                    currcity = (tCategory) HttpContext.Current.Session["SelectedCity"];
                }
                return currcity;
            }
            set { HttpContext.Current.Session["SelectedCity"] = value; }
        }
        public tShoppingCart ShoppingCart {
            get {
                tShoppingCart currCart = null;
                if (HttpContext.Current.Session["ShoppingCart"] == null) {
                    currCart = new tShoppingCart();
                    HttpContext.Current.Session["ShoppingCart"] = currCart;
                } else {
                    currCart = (tShoppingCart)HttpContext.Current.Session["ShoppingCart"];
                }
                return currCart;
            }
            set { HttpContext.Current.Session["ShoppingCart"] = value; }
        }
        
    }
    public class tShoppingCart
    {
        public int IDDeal { get; set; }
        public int Quantity { get; set; }
        public string BuyerNotes { get; set; }
        public string OrderNotes { get; set; }
        public string MobilePhone { get; set; }
    }

    public class UserInfo
    {
        public string UserName { get; set; }
        public string NameSurname { get; set; }
        public tCategory CurrentSelectedCity { get; set; }
    }
}