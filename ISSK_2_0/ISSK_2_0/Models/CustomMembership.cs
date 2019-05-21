using System;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Security;

namespace ISSK_2_0.Models
{
    public class CustomMembership : MembershipProvider
    {
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion,
            string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new System.NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new System.NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new System.NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new System.NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new System.NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return false;
            using (var db = new IsskDb())
            {
                var savedPasswordHash = (db.Conductors
                    .Where(us => string.Compare(username, us.Email, StringComparison.OrdinalIgnoreCase) == 0)
                    .Select(us => us.Password)).FirstOrDefault();

                if (savedPasswordHash == null) return false;

                var hashBytes = Convert.FromBase64String(savedPasswordHash);
                var salt = new byte[16];
                Array.Copy(hashBytes, 0, salt, 0, 16);
                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
                var hash = pbkdf2.GetBytes(20);
                var newHashBytes = new byte[36];
                Array.Copy(salt, 0, newHashBytes, 0, 16);
                Array.Copy(hash, 0, newHashBytes, 16, 20);
                var newSavedPasswordHash = Convert.ToBase64String(hashBytes);

                var user = (db.Conductors.Where(us =>
                        string.Compare(username, us.Email, StringComparison.OrdinalIgnoreCase) == 0 &&
                        string.Compare(newSavedPasswordHash, us.Password, StringComparison.OrdinalIgnoreCase) == 0 &&
                        us.IsActive))
                    .FirstOrDefault();
                return (user != null);

            }
        }

        public override bool UnlockUser(string userName)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            using (var dbContext = new IsskDb())
            {
                var user = (dbContext.Conductors.Where(us =>
                    string.Compare(username, us.Email, StringComparison.OrdinalIgnoreCase) == 0)).FirstOrDefault();

                if (user == null)
                    return null;
                var selectedUser = new CustomMembershipUser(user);
                return selectedUser;
            }
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override bool EnablePasswordRetrieval { get; }
        public override bool EnablePasswordReset { get; }
        public override bool RequiresQuestionAndAnswer { get; }
        public override string ApplicationName { get; set; }
        public override int MaxInvalidPasswordAttempts { get; }
        public override int PasswordAttemptWindow { get; }
        public override bool RequiresUniqueEmail { get; }
        public override MembershipPasswordFormat PasswordFormat { get; }
        public override int MinRequiredPasswordLength { get; }
        public override int MinRequiredNonAlphanumericCharacters { get; }
        public override string PasswordStrengthRegularExpression { get; }
    }
}