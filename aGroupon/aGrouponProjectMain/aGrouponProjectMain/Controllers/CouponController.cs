using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using aGrouponClasses.Models;
using B2B.Models;
using aGrouponClasses.Utils;
using aGrouponClasses.Repositories;

namespace aGrouponProjectMain.Controllers {
    public class CouponController : Controller {
        private IUSERRepository _userRepository;
        private ICouponRepository _couponRepository;
        public CouponController(IUSERRepository userRepository, ICouponRepository couponRepository) {
            _userRepository = userRepository;
            _couponRepository = couponRepository;
        }

        public CouponController()
            : this(new USERRepositoryEF(), new CouponRepositoryEF()) {
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AdminIndex(int? CouponType) {
            if (CouponType == null) CouponType = (int)Enums.enmCoupontype.Usable;
            CouponAdminIndexModel customCouponModel = new CouponAdminIndexModel();
            customCouponModel.CouponType = (int)CouponType;
            if (CouponType == (int)Enums.enmCoupontype.Usable) {
                customCouponModel.Title = "Usable";
                customCouponModel.Coupons = new tCouponModelCustom {
                    TableName = "couponsUsable",
                    CouponType = (int)Enums.enmCoupontype.Usable
                };
                //CouponList = _couponRepository.GetCouponsUsable(),
            }
            if (CouponType == (int)Enums.enmCoupontype.Used) {
                customCouponModel.Title = "Used";
                customCouponModel.Coupons = new tCouponModelCustom {
                    TableName = "couponsUsable",
                    CouponType = (int)Enums.enmCoupontype.Used
                };
            }
            if (CouponType == (int)Enums.enmCoupontype.Expired) {
                customCouponModel.Title = "Expired";
                customCouponModel.Coupons = new tCouponModelCustom {
                    TableName = "couponsUsable",
                    CouponType = (int)Enums.enmCoupontype.Expired
                };
            }
            return View(customCouponModel);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create() {
            List<tUser> userData = _userRepository.GetListByIDRole((int)Enums.enmRoles.Partner);
            ViewBag.userData = userData;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult CreateAjax(tCoupon user, int Count) {

            if (ModelState.IsValid) {
                user.DateAdded = DateTime.Now;
                if (user.DateBegin < DateTime.Now)
                    return Json(new {
                        objectAddedName = user.Code,
                        valid = false,
                        Message = CustomMessages.ErrorMessage("Beginning date must be greater than now...")
                    });
                if (user.DateEnd < DateTime.Now)
                    return Json(new {
                        objectAddedName = user.Code,
                        valid = false,
                        Message = CustomMessages.ErrorMessage("Expiration date must be greater than now...")
                    });
                if (user.DateEnd <= user.DateBegin)
                    return Json(new {
                        objectAddedName = user.Code,
                        valid = false,
                        Message = CustomMessages.ErrorMessage("Expiration date must follow Beginning Date")
                    });
                if (Count <= 0)
                    return Json(new {
                        objectAddedName = user.Code,
                        valid = false,
                        Message = CustomMessages.ErrorMessage("Count Must be greater than 0")
                    });
                if (Count > 200)
                    return Json(new {
                        objectAddedName = user.Code,
                        valid = false,
                        Message = CustomMessages.ErrorMessage("Count must be less than 200")
                    });
                List<tCoupon> res = CreateCouponsMultiple(user, Count);
                _couponRepository.Insert(res);
                return Json(new {
                    objectAddedName = user.Code,
                    valid = true,
                    Message = CustomMessages.SuccessMessage("Coupons were added Succesfully")
                });
            }
            return Json(new {
                objectAddedName = "",
                valid = false,
                Message = CustomMessages.ErrorMessage("Fill All Fields please")
            });
        }

        private List<tCoupon> CreateCouponsMultiple(tCoupon user, int Count) {
            List<tCoupon> res = new List<tCoupon>();
            for (int i = 0; i < Count; i++) {

                string Finalcode = GenerateRandomCode(16);

                tCoupon couponToCreate = new tCoupon();
                couponToCreate.IDPartner = user.IDPartner;
                couponToCreate.Code = user.Code;
                couponToCreate.CustomCode = Finalcode;
                couponToCreate.CouponValue = user.CouponValue;
                couponToCreate.DateBegin = user.DateBegin;
                couponToCreate.DateEnd = user.DateEnd;
                couponToCreate.DateAdded = user.DateAdded;
                couponToCreate.CouponStatus = (int) Enums.enmCouponStatus.Unused;
                res.Add(couponToCreate);
            }
            return res;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult DeleteCoupon(int id) {
            _couponRepository.Delete(id);
            return Json(new {
                valid = true,
                Message = "Coupon was deleted succesfully",
                redirect = "/Coupon/AdminIndex"
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult DeleteCouponByCode(string Code) {
            _couponRepository.DeleteByCode(Code);
            return Json(new {
                valid = true,
                Message = "Coupons were deleted succesfully",
                redirect = "/Coupon/AdminIndex"
            });
        }

        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        public static int RandomNumber(int min, int max) {
            lock (syncLock) { // synchronize
                return random.Next(min, max);
            }
        }

        private string GenerateRandomCode(int Length) {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Length; ++i) {
                sb.Append(RandomNumber(1, 9));
            }
            return sb.ToString();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult DeleteMultiple(int[] array) {
            if (array == null)
                return Json(new {
                    valid = false,
                    Message = CustomMessages.ErrorMessage("Please provide coupons to be deleted"),
                    redirect = "none"
                });
            if (array.Length == 0)
                return Json(new {
                    valid = false,
                    Message = CustomMessages.ErrorMessage("Please provide coupons to be deleted"),
                    redirect = "none"
                });
            int deleted = _couponRepository.Delete(array);
            if (deleted == 0)
                return Json(new {
                    valid = false,
                    Message = CustomMessages.ErrorMessage("Coupons couldn't be deleted"),
                    redirect = "none"
                });
            return Json(new {
                valid = true,
                Message = CustomMessages.SuccessMessage(array.Length + " coupons were deleted succesfully"),
                redirect = "/Coupon/AdminIndex"
            });
        }

        [Authorize(Roles = "Admin")]
        public JsonResult GetCoupons(int? CouponType) {
            if (CouponType == null) CouponType = (int)Enums.enmCoupontype.Usable;
            List<tCoupon> couponList = new List<tCoupon>();
            if (CouponType == (int)Enums.enmCoupontype.Usable) {
                couponList = _couponRepository.GetCouponsUsable();
            } else if (CouponType == (int)Enums.enmCoupontype.Used) {
                couponList = _couponRepository.GetCouponsUsed();
            } else if (CouponType == (int)Enums.enmCoupontype.Expired) {
                couponList = _couponRepository.GetCouponsExpired();
            } else
                return Json(new {
                    valid = false,
                    Message = CustomMessages.ErrorMessage("Please provide CouponType"),
                    redirect = "none"
                });
            int i, n = couponList.Count;
            string[][] res = new string[n][];
            //res[0] = new string[n][];
            for (i = 0; i < n; i++) {
                //tCouponForAdminIndexJson obj = new tCouponForAdminIndexJson {
                //    Partner = couponList[i].tUser.UserName, Code = couponList[i].Code,
                //    CustomCode = couponList[i].CustomCode, CouponValue = couponList[i].CouponValue,
                //    DateBegin = couponList[i].DateBegin, DateEnd = couponList[i].DateEnd,
                //    DateAdded = couponList[i].DateAdded
                //};
                //tCouponForAdminIndexJson obj = new tCouponForAdminIndexJson();
                string[] str = new string[10];
                str[0] = "<input type=\"checkbox\" />";
                str[1] = couponList[i].IDCoupon.ToString();
                str[2] = couponList[i].tUser.UserName;
                str[3] = couponList[i].Code;
                str[4] = couponList[i].CustomCode;
                str[5] = couponList[i].CouponValue.ToString();
                str[6] = couponList[i].DateBegin.ToString("dd.MM.yyyy");
                str[7] = couponList[i].DateEnd.ToString("dd.MM.yyyy");
                str[8] = couponList[i].DateAdded.ToString("dd.MM.yyyy");
                string lnkDelete = "";
                if (CouponType == (int)aGrouponClasses.Utils.Enums.enmCoupontype.Usable || CouponType == (int)aGrouponClasses.Utils.Enums.enmCoupontype.Expired) {
                    lnkDelete = "<a name=\"RowDelete\" onclick=\"PrepareDelete(this);return false;\" href=\"#\">Delete</a>";
                }
                str[9] = lnkDelete;
                res[i] = str;
            }
            return Json(new {
                aaData = res
            }, JsonRequestBehavior.AllowGet);
        }
    }

    public class CouponAdminIndexModel {
        public int CouponType { get; set; }
        public string Title { get; set; }
        public tCouponModelCustom Coupons { get; set; }
    }

    public class tCouponModelCustom {
        public string TableName { get; set; }
        public List<tCoupon> CouponList { get; set; }
        public int CouponType { get; set; }
    }

    public class tCouponForAdminIndexJson {
        public string[] Info { get; set; }
        //public string Code { get; set; }
        //public string CustomCode { get; set; }
        //public decimal CouponValue { get; set; }
        //public DateTime DateBegin { get; set; }
        //public DateTime DateEnd { get; set; }
        //public DateTime DateAdded { get; set; }
    }
}
