using System;

namespace LegacyApp
{
    public static class UserValidator
    {
        public static bool ValidateUserData(User user)
        {
            return !UserNameMissing(user) && !EmailMissingAtSignOrDot(user) &&
                   !UserYoungerThan21(user);
        }

        private static bool UserNameMissing(User user)
        {
            return string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName);
        }

        private static bool EmailMissingAtSignOrDot(User user)
        {
            return !user.EmailAddress.Contains("@") && !user.EmailAddress.Contains(".");
        }

        private static bool UserYoungerThan21(User user)
        {
            return user.GetAge(DateTime.Now) < 21;
        }

        public static bool CreditLimitIsIncorrect(User user)
        {
            return user.HasCreditLimit && user.CreditLimit < 500;
        }
    }
}