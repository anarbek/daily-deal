using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Configuration;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Configuration;
using aGrouponClasses.Models;
using aGrouponClasses.Utils;
using B2B.Models;
using NREticaret.Models;

namespace NREticaret.MembershipProviders {
    public class NRMembershipProvider : MembershipProvider {
        #region Class Variables

        private int newPasswordLength = 8;
        private string connectionString;
        private string applicationName;
        private bool enablePasswordReset;
        private bool enablePasswordRetrieval;
        private bool requiresQuestionAndAnswer;
        private bool requiresUniqueEmail;
        private int maxInvalidPasswordAttempts;
        private int passwordAttemptWindow;
        private MembershipPasswordFormat passwordFormat;
        private int minRequiredNonAlphanumericCharacters;
        private int minRequiredPasswordLength;
        private string passwordStrengthRegularExpression;
        private MachineKeySection machineKey; //Used when determining encryption key values.

        public IUSERRepository _UserRep { get; set; }

        #endregion

        #region Enums

        private enum FailureType {
            Password = 1,
            PasswordAnswer = 2
        }

        #endregion

        #region Properties

        public override string ApplicationName {
            get {
                return applicationName;
            }
            set {
                applicationName = value;
            }
        }

        public override bool EnablePasswordReset {
            get {
                return enablePasswordReset;
            }
        }

        public override bool EnablePasswordRetrieval {
            get {
                return enablePasswordRetrieval;
            }
        }

        public override bool RequiresQuestionAndAnswer {
            get {
                return requiresQuestionAndAnswer;
            }
        }

        public override bool RequiresUniqueEmail {
            get {
                return requiresUniqueEmail;
            }
        }

        public override int MaxInvalidPasswordAttempts {
            get {
                return maxInvalidPasswordAttempts;
            }
        }

        public override int PasswordAttemptWindow {
            get {
                return passwordAttemptWindow;
            }
        }

        public override MembershipPasswordFormat PasswordFormat {
            get {
                return passwordFormat;
            }
        }

        public override int MinRequiredNonAlphanumericCharacters {
            get {
                return minRequiredNonAlphanumericCharacters;
            }
        }

        public override int MinRequiredPasswordLength {
            get {
                return minRequiredPasswordLength;
            }
        }

        public override string PasswordStrengthRegularExpression {
            get {
                return passwordStrengthRegularExpression;
            }
        }

        #endregion

        #region Initialization
        public override void Initialize(string name, NameValueCollection config) {
            if (config == null) {
                throw new ArgumentNullException("config");
            }

            if (name == null || name.Length == 0) {
                name = "HDIMembershipProvider";
            }

            if (String.IsNullOrEmpty(config["description"])) {
                config.Remove("description");
                config.Add("description", "How Do I: Sample Membership provider");
            }

            //Initialize the abstract base class.
            base.Initialize(name, config);

            applicationName = GetConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            maxInvalidPasswordAttempts = Convert.ToInt32(GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));
            passwordAttemptWindow = Convert.ToInt32(GetConfigValue(config["passwordAttemptWindow"], "10"));
            minRequiredNonAlphanumericCharacters = Convert.ToInt32(GetConfigValue(config["minRequiredAlphaNumericCharacters"], "1"));
            minRequiredPasswordLength = Convert.ToInt32(GetConfigValue(config["minRequiredPasswordLength"], "7"));
            passwordStrengthRegularExpression = Convert.ToString(GetConfigValue(config["passwordStrengthRegularExpression"], String.Empty));
            enablePasswordReset = Convert.ToBoolean(GetConfigValue(config["enablePasswordReset"], "true"));
            enablePasswordRetrieval = Convert.ToBoolean(GetConfigValue(config["enablePasswordRetrieval"], "true"));
            requiresQuestionAndAnswer = Convert.ToBoolean(GetConfigValue(config["requiresQuestionAndAnswer"], "false"));
            requiresUniqueEmail = Convert.ToBoolean(GetConfigValue(config["requiresUniqueEmail"], "true"));

            string temp_format = config["passwordFormat"];
            if (temp_format == null) {
                temp_format = "Hashed";
            }

            switch (temp_format) {
                case "Hashed":
                    passwordFormat = MembershipPasswordFormat.Hashed;
                    break;
                case "Encrypted":
                    passwordFormat = MembershipPasswordFormat.Encrypted;
                    break;
                case "Clear":
                    passwordFormat = MembershipPasswordFormat.Clear;
                    break;
                default:
                    throw new ProviderException("Password format not supported.");
            }

            ConnectionStringSettings ConnectionStringSettings = ConfigurationManager.ConnectionStrings[config["connectionStringName"]];

            if ((ConnectionStringSettings == null) || (ConnectionStringSettings.ConnectionString.Trim() == String.Empty)) {
                throw new ProviderException("Connection string cannot be blank.");
            }

            connectionString = ConnectionStringSettings.ConnectionString;

            //Get encryption and decryption key information from the configuration.
            System.Configuration.Configuration cfg = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            machineKey = cfg.GetSection("system.web/machineKey") as MachineKeySection;

            if (machineKey.ValidationKey.Contains("AutoGenerate")) {
                if (PasswordFormat != MembershipPasswordFormat.Clear) {
                    throw new ProviderException("Hashed or Encrypted passwords are not supported with auto-generated keys.");
                }
            }
        }

        private string GetConfigValue(string configValue, string defaultValue) {
            if (String.IsNullOrEmpty(configValue)) {
                return defaultValue;
            }

            return configValue;
        }
        #endregion

        public override bool ChangePassword(string username, string oldPassword, string newPassword) {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer) {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status) {
            _UserRep = BootStrapper.GetCurrentUserRepository();

            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, password, true);

            OnValidatingPassword(args);

            if (args.Cancel) {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            if ((RequiresUniqueEmail && (GetUserNameByEmail(email) != String.Empty))) {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            MembershipUser membershipUser = GetUser(username, false);

            if (membershipUser == null) {
                System.DateTime createDate = DateTime.Now;

                tUser user = new tUser();
                user.UserName = username;
                user.Password = password;
                user.UserName = email;
                user.Approved = isApproved;
                //user.Address = "";
                //user.AdSoyad = "";
                //user.Avatar = "";
                //user.BirthDate = new DateTime(1900, 1, 1);
                //user.CellNo = "";
                //user.City = "";
                user.DateAdded = DateTime.Now;
                user.LastLoginDate = DateTime.Now;
                //user.Role = "User";
                //user.Semt = "";
                //user.TellNo = "";
                //user.UserStatus = (int)B2B.Core.Enums.enmUserStatus.Active;
                user.IDRole = (int) Enums.enmRoles.User;

                try {
                    _UserRep.InsertOrUpdate(user);
                    int result = 1;
                    if (result > 0) {
                        _UserRep.Save();
                        status = MembershipCreateStatus.Success;
                    } else {
                        status = MembershipCreateStatus.UserRejected;
                    }
                } catch (Exception err) {
                    //Add exception handling here.

                    status = MembershipCreateStatus.ProviderError;
                }

                return GetUser(username, false);
            } else {
                status = MembershipCreateStatus.DuplicateUserName;
            }

            return null;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData) {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords) {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords) {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords) {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline() {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer) {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline) {
            MembershipUser membershipUser = null;
            _UserRep = BootStrapper.GetCurrentUserRepository();
            tUser user = _UserRep.GetSingleByUserName(username);
            if (user != null) {
                membershipUser = MakeMembershipUser(user);
                if (userIsOnline) {
                    //Update Last Activity Date;
                }
            }

            return membershipUser;
        }

        private MembershipUser MakeMembershipUser(tUser user) {
            object userID = user.IDUser;
            string username = user.UserName;
            string email = user.UserName;

            string passwordQuestion = String.Empty;
            //if (sqlDataReader.GetValue(3) != DBNull.Value)
            //{
            //    passwordQuestion = sqlDataReader.GetString(3);
            //}

            string comment = String.Empty;
            //if (sqlDataReader.GetValue(4) != DBNull.Value)
            //{
            //    comment = sqlDataReader.GetString(4);
            //}

            bool isApproved = (bool)user.Approved;
            bool isLockedOut = false;
            DateTime creationDate = (DateTime)user.DateAdded;

            DateTime lastLoginDate = new DateTime();
            if (user.LastLoginDate != null) {
                lastLoginDate = (DateTime)user.LastLoginDate;
            }

            DateTime lastActivityDate = DateTime.Now;//sqlDataReader.GetDateTime(9);
            DateTime lastPasswordChangedDate = DateTime.Now; // sqlDataReader.GetDateTime(10);

            DateTime lastLockedOutDate = new DateTime();
            //if (sqlDataReader.GetValue(11) != DBNull.Value)
            //{
            //    lastLockedOutDate = sqlDataReader.GetDateTime(11);
            //}

            MembershipUser membershipUser = new MembershipUser(
              this.Name,
             username,
             userID,
             email,
             passwordQuestion,
             comment,
             isApproved,
             isLockedOut,
             creationDate,
             lastLoginDate,
             lastActivityDate,
             lastPasswordChangedDate,
             lastLockedOutDate
              );

            return membershipUser;
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline) {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email) {
            string username = String.Empty;
            _UserRep = BootStrapper.GetCurrentUserRepository();
            tUser user = _UserRep.GetSingleByEmail(email);
            if (user == null) {
                return String.Empty;
            } else {
                username = user.UserName.Trim();
            }

            return username;
        }

        public override string ResetPassword(string username, string answer) {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName) {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user) {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password) {
            bool isValid = false;
            _UserRep = BootStrapper.GetCurrentUserRepository();
            tUser user = _UserRep.GetSingleByUsernamePassword(username, password);
            if (user != null) {
                isValid = (bool)user.Approved;// && user.UserStatus == (int)B2B.Core.Enums.enmUserStatus.Active;
            }
            return isValid;
        }
    }
}