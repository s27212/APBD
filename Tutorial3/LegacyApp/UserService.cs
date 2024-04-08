using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            if (!UserValidator.ValidateUserData(user)) return false;
            
            using (var userCreditService = new UserCreditService())
            {
                userCreditService.SetCreditLimit(user, client);
            }

            if (UserValidator.CreditLimitIsIncorrect(user)) return false;

            UserDataAccess.AddUser(user);
            return true;
        }
    }
}
