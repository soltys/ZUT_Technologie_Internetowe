using System;
namespace PSBlog.Authentication
{
    public interface IPSBlogMembershipProvider
    {
        string ApplicationName { get; set; }
        bool ChangePassword(string username, string oldPassword, string newPassword);
        bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer);
        global::System.Web.Security.MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out global::System.Web.Security.MembershipCreateStatus status);
        bool DeleteUser(string username, bool deleteAllRelatedData);
        bool EnablePasswordReset { get; }
        bool EnablePasswordRetrieval { get; }
        global::System.Web.Security.MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords);
        global::System.Web.Security.MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords);
        global::System.Web.Security.MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords);
        int GetNumberOfUsersOnline();
        string GetPassword(string username, string answer);
        global::System.Web.Security.MembershipUser GetUser(object providerUserKey, bool userIsOnline);
        global::System.Web.Security.MembershipUser GetUser(string username, bool userIsOnline);
        string GetUserNameByEmail(string email);
        int MaxInvalidPasswordAttempts { get; }
        int MinRequiredNonAlphanumericCharacters { get; }
        int MinRequiredPasswordLength { get; }
        int PasswordAttemptWindow { get; }
        global::System.Web.Security.MembershipPasswordFormat PasswordFormat { get; }
        string PasswordStrengthRegularExpression { get; }
        bool RequiresQuestionAndAnswer { get; }
        bool RequiresUniqueEmail { get; }
        string ResetPassword(string username, string answer);
        bool UnlockUser(string userName);
        void UpdateUser(global::System.Web.Security.MembershipUser user);
        bool ValidateUser(string username, string password);
    }
}
