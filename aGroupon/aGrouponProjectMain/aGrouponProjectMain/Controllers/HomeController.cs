using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using aGrouponClasses.Repositories;
using aGrouponClasses.Models;
using aGrouponClasses.Utils;
using B2B.BLL;
using System.ComponentModel.DataAnnotations;

namespace aGrouponProjectMain.Controllers {
    public class HomeController : Controller {
        private readonly IDealRepository _dealRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IContentRepository _contentRepository;
        public HomeController(IDealRepository dealRepository, ICategoryRepository categoryRepository, IContentRepository contentRepository) {
            _dealRepository = dealRepository;
            _categoryRepository = categoryRepository;
            _contentRepository = contentRepository;
        }

        public HomeController()
            : this(new DealRepositoryEF(),new CategoryRepositoryEF(),new ContentRepositoryEF()) {
        }
        public ActionResult Index() {
            if (MembershipHelper.CurrentCity == null || MembershipHelper.CurrentCity.IDCategory==0)
            {
                tCategory cat = _categoryRepository.GetListByIDCategoryType((int)Enums.enmCategoryTypes.City).
                                FirstOrDefault();

                if (cat == null)
                    cat = new tCategory() { Name = "City Not Found", IDCategory = 0 };
                MembershipHelper.CurrentCity = cat;
            }
            tDeal currentDeal = _dealRepository.GetLatestDeal(MembershipHelper.CurrentCity.IDCategory);
            ViewBag.LeftTSStr = tDealUtils.GetLeftTSStrForDeal(currentDeal);
            if (currentDeal == null) currentDeal = new tDeal();
            if (MembershipHelper.CurrentCity != null && currentDeal!=null)
                ViewBag.DealsBySameCategory = _dealRepository.GetListByIDCategory(currentDeal.IDDealCategory).Take(6).OrderByDescending(t=>t.DateAdded).ToList();
            else
                ViewBag.DealsBySameCategory = new List<tDeal>();
            return View(currentDeal);
        }

        public ActionResult About()
        {
            tContent contentData = _contentRepository.FindByCode("about");
            if (contentData == null) {
                contentData = new tContent { Name = "Not Found", Description = "Not Found" };
            }
            return View(contentData);
        }

        public ActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SendContactUsForm(ContactUsModel contactUsData)
        {
            if(ModelState.IsValid)
            {
                MailAddressCollection coll = new MailAddressCollection();
                coll.Add(new MailAddress(contactUsData.Email, contactUsData.Email));
                try
                {
                    MailSender.SendMail(contactUsData.Email, "Contact Form", "Body Here", coll);
                }
                catch (Exception ex)
                {
                    return Json(new {
                        objectAddedName = "",
                        valid = false,
                        Message = "Error Sending Mail : " + ex.Message
                    });
                }

               
                return Json(new
                                {
                                    objectAddedName = "",
                                    valid = true,
                                    Message = "Form Sent Succesfully"
                });
            }

            return Json(new {
                objectAddedName = "",
                valid = false,
                Message = "Fill All Fields please"
            });
        }
    }

    public class ContactUsModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
