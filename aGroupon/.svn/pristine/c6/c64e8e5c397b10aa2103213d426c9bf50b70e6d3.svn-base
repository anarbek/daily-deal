using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using aGrouponClasses.Models;
using aGrouponClasses.Repositories;
using aGrouponClasses.Utils;

namespace aGrouponProjectMain.Controllers {
    public class OrderController : Controller {
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository) {
            _orderRepository = orderRepository;
        }

        public OrderController() : this(new OrderRepositoryEF()) { }

        //
        // GET: /Order/
        [Authorize(Roles = "Admin")]
        public ActionResult AdminIndex(int? PaidStatus) {
            if (PaidStatus == null) PaidStatus = (int)Enums.enmOrderStatus.Paid;
            tOrderAdminModel customCouponModel = new tOrderAdminModel();
            customCouponModel.PaidStatus = (int)PaidStatus;
            customCouponModel.OrderStatusName = PaidStatus == (int) Enums.enmOrderStatus.Paid ? "Paid" : "Not Paid";
            return View(customCouponModel);
        }

        //[Authorize(Roles = "Admin")]
        //public JsonResult GetOrderJSonByPaidStatusOLD(int? PaidStatus) {
        //    if (PaidStatus == null) PaidStatus = (int)Enums.enmOrderStatus.Paid;
        //    List<tOrder> dealList = _orderRepository.GetListByPaidStatus((int)PaidStatus);
        //    List<tOrderAdminJson> res = new List<tOrderAdminJson>();

        //    int i, n = dealList.Count;
        //    //res[0] = new string[n][];
        //    for (i = 0; i < n; i++) {
        //        string lnkDetail =
        //            "<a name=\"RowDelete\" href=\"/Order/Edit/" + dealList[i].IDOrder + "\">Edit</a>";
        //        tOrderAdminJson obj = new tOrderAdminJson {
        //            ID = dealList[i].IDOrder.ToString(),
        //            Title = dealList[i].tDeal.DealTitle,
        //            UserName = dealList[i].tUser.UserName,
        //            Category = dealList[i].tDeal.tCategory.Name,
        //            Price = dealList[i].tDeal.DealPrice.ToString(),
        //            lnkDelete = lnkDetail
        //        };
        //        res.Add(obj);
        //    }
        //    return Json(new {
        //        sEcho = 1,
        //        iTotalRecords = res.Count,
        //        iTotalDisplayRecords = res.Count,
        //        aaData = res
        //    }, JsonRequestBehavior.AllowGet);
        //}

        [Authorize(Roles = "Admin")]
        public JsonResult GetOrderJSonByPaidStatus(int? PaidStatus) {
            if (PaidStatus == null) PaidStatus = (int)Enums.enmOrderStatus.Paid;
            List<tOrder> dealList = _orderRepository.GetListByPaidStatus((int)PaidStatus);

            int i, n = dealList.Count;
            string[][] res = new string[n][];
            for (i = 0; i < n; i++) {
                string[] str = new string[10];
                string lnkDetail =
                    "<a name=\"RowDelete\" href=\"/Order/Edit/" + dealList[i].IDOrder + "\">Edit</a>";

                str[0] = dealList[i].IDOrder.ToString();
                str[1] = dealList[i].tDeal.DealTitle;
                str[2] = dealList[i].tUser.UserName;
                str[3] = dealList[i].tDeal.tCategory.Name;
                str[4] = dealList[i].tDeal.DealPrice.ToString();
                str[5] = lnkDetail;
                res[i] = str;
            }
            return Json(new {
                aaData = res
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            ViewBag.RefundStatuses = GetRefundModel();
            tOrder currentOrder = _orderRepository.Find(id);
            return View(currentOrder);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult EditAjax(int IDOrder, string OrderNotes)
        {
            if (ModelState.IsValid)
            {
                tOrder oldOrder = _orderRepository.Find(IDOrder);
                oldOrder.OrderNotes = OrderNotes;
                _orderRepository.InsertOrUpdate(oldOrder);
                _orderRepository.Save();
                return Json(new {
                    objectAddedName = oldOrder.tDeal.DealTitle,
                    valid = true,
                    Message = "Order Data was updated Succesfully"
                });
            } else {
                return Json(new {
                    objectAddedName = "",
                    valid = false,
                    Message = "Fill All Fields please"
                });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult EditOrderRefundStatusAjax(int IDOrder, int RefundStatus) {
            if (ModelState.IsValid) {
                tOrder oldOrder = _orderRepository.Find(IDOrder);
                oldOrder.RefundStatus = RefundStatus;
                _orderRepository.InsertOrUpdate(oldOrder);
                _orderRepository.Save();
                return Json(new {
                    objectAddedName = oldOrder.tDeal.DealTitle,
                    valid = true,
                    Message = "Order Data was updated Succesfully"
                });
            } else {
                return Json(new {
                    objectAddedName = "",
                    valid = false,
                    Message = "Fill All Fields please"
                });
            }
        }

        private List<RefundModel> GetRefundModel()
        {
            List<RefundModel> res = new List<RefundModel>();
            res.Add(new RefundModel
                        {
                            IDRefundStatus = (int) Enums.enmRefundStatus.RefundedByOtherMeans,
                            Name = "Refunded by other means"
                        });
            res.Add(new RefundModel {
                IDRefundStatus = (int)Enums.enmRefundStatus.RefundedToAccountBalance,
                Name = "Refundeded to Account Balance"
            });
            return res;
        }
    }

    public class tOrderAdminModel
    {
        public int PaidStatus { get; set; }
        public string OrderStatusName { get; set; }
    }
    public class tOrderAdminJson
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public string Category { get; set; }
        public string Price { get; set; }
        public string lnkDelete { get; set; }
    }

    public class RefundModel
    {
        public int IDRefundStatus { get; set; }
        public string Name { get; set; }
    }
}
