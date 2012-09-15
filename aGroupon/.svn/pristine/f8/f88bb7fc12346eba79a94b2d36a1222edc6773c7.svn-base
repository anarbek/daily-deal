using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using aGrouponClasses.Repositories;
using aGrouponClasses.Utils;
using aGrouponProjectMain.Controllers;
using B2B.Models;
using System.Web.Security;
using NREticaret.MembershipProviders;
using NREticaret.Models;
using aGrouponClasses.Models;

namespace B2B.BLL {
    public class MembershipHelper {
        private static IUSERRepository _userRepository = new USERRepositoryEF();
        private static ICategoryRepository _categoryRepository = new CategoryRepositoryEF();
        public static MembershipUser GetCurrenUser() {
            MembershipProvider _provider = Membership.Provider;
            MembershipUser mu = _provider.GetUser(HttpContext.Current.User.Identity.Name, true);
            return mu;
        }

        public static UserInfo CurrentUser {
            get {
                if (HttpContext.Current.Session["UserInfo"] == null) {
                    MembershipUser mu = GetCurrenUser();
                    int IDUser = (int)mu.ProviderUserKey;
                    tUser userData = _userRepository.Find(IDUser);
                    UserInfo info = new UserInfo();
                    if (userData != null) {
                        info.NameSurname = userData.UserName;
                        info.UserName = userData.UserName;
                        info.CurrentSelectedCity = CurrentCity;
                    }
                    HttpContext.Current.Session["UserInfo"] = info;
                    return info;
                }
                return (UserInfo)HttpContext.Current.Session["UserInfo"];
            }
            set {
                HttpContext.Current.Session["UserInfo"] = value;
            }
        }

        public static tCategory CurrentCity {
            get {
                if (HttpContext.Current.Session["CurrentCity"] == null) {
                    tCategory cat = _categoryRepository.GetListByIDCategoryType((int)Enums.enmCategoryTypes.City).
                                FirstOrDefault();

                    if (cat == null)
                        cat = new tCategory() { Name = "City Not Found", IDCategory = 0 };
                    HttpContext.Current.Session["CurrentCity"] = cat;
                    return cat;
                } else
                    return (tCategory)HttpContext.Current.Session["CurrentCity"];
            }
            set {
                if(value!=null)
                    HttpContext.Current.Session["CurrentCity"] = value;
                else
                {
                    tCategory cat = _categoryRepository.GetListByIDCategoryType((int)Enums.enmCategoryTypes.City).
                                FirstOrDefault();

                    if (cat == null)
                        cat = new tCategory() { Name = "City Not Found", IDCategory = 0 };
                    HttpContext.Current.Session["CurrentCity"] = cat;
                }
            }
        }
    }

    public class RoleHelper {
        public static string[] GetRolesForCurrentUser() {
            RoleProvider _provider = (NRRoleProvider)Roles.Provider;
            return _provider.GetRolesForUser(MembershipHelper.GetCurrenUser().UserName);
        }

        public static List<tRole> GetRoleObjectsForCurrentUser() {
            RoleProvider _provider = (NRRoleProvider)Roles.Provider;
            IUSERRepository _UserRep = BootStrapper.GetCurrentUserRepository();
            IROLERepository _RoleRep = BootStrapper.GetCurrentRoleRepository();
            //IUSER_ROLERepository _UserRoleRep = BootStrapper.GetCurrentUserRoleRepository();
            // throw new NotImplementedException();
            tUser user = _UserRep.GetSingleByUserName(MembershipHelper.GetCurrenUser().UserName);
            //List<USER_ROLE> uroles = _UserRoleRep.GetListByUserID(user.UserID);
            List<tRole> roles = new List<tRole>();
            //int i = 0;
            //foreach (USER_ROLE item in uroles)
            //{
            //    ROLE role = _RoleRep.Find(item.RoleID);
            //    roles.Add(role);
            //}
            tRole role = _RoleRep.Find(user.IDRole);
            roles.Add(role);
            return roles;
            // throw new NotImplementedException();
        }
    }
}