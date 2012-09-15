using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using aGrouponClasses.Models;
using B2B.Models;
using NREticaret.Models;

namespace NREticaret.MembershipProviders {
    public class NRRoleProvider : RoleProvider {
        private string _applicationName;



        public override string ApplicationName {
            get {
                return _applicationName;
            }
            set {
                _applicationName = value;
            }
        }
        public IROLERepository _RoleRep { get; set; }
        public IUSERRepository _UserRep { get; set; }
        //public IUSER_ROLERepository _UserRoleRep { get; set; }


        public override void AddUsersToRoles(string[] usernames, string[] roleNames) {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName) {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole) {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch) {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles() {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username) {
            _UserRep = BootStrapper.GetCurrentUserRepository();
            _RoleRep = BootStrapper.GetCurrentRoleRepository();
            // throw new NotImplementedException();
            tUser user = _UserRep.GetSingleByUserName(username);
            if (user == null) return new string[1]{"NotFoundRole"};
            //List<USER_ROLE> uroles = _UserRoleRep.GetListByUserID(user.UserID);
            string[] roles = new string[1];//new string[uroles.Count];
            int i = 0;
            //foreach (USER_ROLE item in uroles)
            //{
            //    ROLE role = _RoleRep.GetSingle(item.RoleID);
            //    roles[i] = role.RoleName;
            //    i++;
            //}
            tRole role = _RoleRep.Find(user.IDRole);
            roles[0] = role.Name;
            return roles;
            // throw new NotImplementedException();
        }



        public override string[] GetUsersInRole(string roleName) {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName) {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames) {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName) {
            throw new NotImplementedException();
        }
    }
}