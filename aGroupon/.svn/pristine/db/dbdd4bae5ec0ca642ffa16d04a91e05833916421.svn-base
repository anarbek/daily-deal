using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using aGrouponProjectMain.Models;

namespace aGrouponProjectMain.Controllers {
    public class AccountController : Controller {

        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }

        protected override void Initialize(RequestContext requestContext) {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }

        // **************************************
        // URL: /Account/LogOn
        // **************************************
        public ActionResult LogonNew() {
            ViewData["Login"] = "true";
            Session["theme"] = "AdminTheme";
            return View();
        }

        public ActionResult LogOn() {
            //ViewData["Login"] = "true"; 
            if (User.IsInRole("NotFoundRole"))
            {
                FormsService.SignOut();
                RedirectToAction("LogOn", "Account");
            }
            return View("Logon");
        }

        public ActionResult LogonAnasayfa() {
            return PartialView("_LogonAnasayfa");
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl) {
            if (ModelState.IsValid) {
                if (Membership.ValidateUser(model.UserName, model.Password)) {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\")) {
                        return Redirect(returnUrl);
                    } else {
                        return RedirectToAction("Index", "Home");
                    }
                } else {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        public JsonResult LogOnAjax(LogOnModel model, string ReturnUrl) {
            try {
                if (ModelState.IsValid) {
                    if (MembershipService.ValidateUser(model.UserName, model.Password)) {
                        FormsService.SignIn(model.UserName, model.RememberMe);
                        string TargetUrl = "/Category/GetCatListAdmin";
                        if (ReturnUrl != string.Empty && ReturnUrl != null) {
                            TargetUrl = Server.UrlDecode(ReturnUrl);
                        } else
                            TargetUrl = "/USERs/UserHome";
                        return Json(new { redirect = TargetUrl, valid = true, Message = "" });
                        //if (!String.IsNullOrEmpty(returnUrl))
                        //{
                        //    return Redirect(returnUrl);
                        //}
                        //else
                        //{
                        //    return RedirectToAction("Index", "Home");
                        //}
                    } else {
                        return Json(new { redirect = "none", valid = false, Message = "The user name or password provided is incorrect." });
                        // ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    }
                }
            } catch (Exception err) {
                // If we got this far, something failed, redisplay form
                return Json(new { redirect = "none", valid = false, Message = "Something Went Wrong. : " + err.Message });
                //return View(model);
            }

            // If we got this far, something failed, redisplay form
            return Json(new { redirect = "none", valid = false, Message = "Something Went Wrong. " });
            //return View(model);

        }

        // **************************************
        // URL: /Account/LogOff
        // **************************************

        public ActionResult LogOff() {
            FormsService.SignOut();
            HttpContext.Session.Abandon();
            
            return RedirectToAction("Index", "Home");
        }

        // **************************************
        // URL: /Account/Register
        // **************************************

        public ActionResult Register() {
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model) {
            if (ModelState.IsValid) {
                // Attempt to register the user
                MembershipCreateStatus createStatus = MembershipService.CreateUser(model.UserName, model.Password, model.Email);

                if (createStatus == MembershipCreateStatus.Success) {
                    FormsService.SignIn(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("UserHome", "USERs");
                } else {
                    ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View(model);
        }

        // **************************************
        // URL: /Account/ChangePassword
        // **************************************

        [Authorize]
        public ActionResult ChangePassword() {
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model) {
            if (ModelState.IsValid) {
                if (MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword)) {
                    return RedirectToAction("ChangePasswordSuccess");
                } else {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View(model);
        }

        // **************************************
        // URL: /Account/ChangePasswordSuccess
        // **************************************

        public ActionResult ChangePasswordSuccess() {
            return View();
        }


    }
}
