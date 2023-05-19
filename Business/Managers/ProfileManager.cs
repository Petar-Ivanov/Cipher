using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public static class ProfileManager
    {
        // The class manages the CRUD operations of the profile data
        // The class encrypts and decrypts data from the database

        // Logs into and existing account
        public static int Login(string username, string password)
        {
            AccountBusiness accBusiness = new AccountBusiness();
            Account_Credentials_Business accCredentialsBusiness = new Account_Credentials_Business();
            List<Account> accounts = accBusiness.GetAll();
            if (accounts.Count != 0)
            {
                foreach (Account acc in accounts)
                {
                    AES aes = new AES();
                    AES aes2 = new AES();
                    aes.Key = Convert.FromBase64String(accCredentialsBusiness.Get(acc.ID).Username_Key);
                    aes.IV = Convert.FromBase64String(accCredentialsBusiness.Get(acc.ID).Username_Vector);
                    aes2.Key = Convert.FromBase64String(accCredentialsBusiness.Get(acc.ID).Password_Key);
                    aes2.IV = Convert.FromBase64String(accCredentialsBusiness.Get(acc.ID).Password_Vector);
                    if (aes.Decrypt(acc.Username) == username && aes2.Decrypt(acc.Password) == password)
                    {
                        //if (acc.C2FA == true)
                        //{

                        //}
                        //ActiveUserID = acc.ID;
                        //return true;
                        return acc.ID;
                    }
                }

            }
            return -1;
        }

        // Checks wether an Account with the given username already exists inside the database
        public static bool ContainsAccount(string username)
        {
            AccountBusiness accBusiness = new AccountBusiness();
            Account_Credentials_Business accCredentialsBusiness = new Account_Credentials_Business();
            List<Account> accounts = accBusiness.GetAll();
            AES aes = new AES();
            foreach (Account acc in accounts)
            {
                aes.Key = Convert.FromBase64String(accCredentialsBusiness.Get(acc.ID).Username_Key);
                aes.IV = Convert.FromBase64String(accCredentialsBusiness.Get(acc.ID).Username_Vector);
                if (aes.Decrypt(acc.Username) == username) return true;
            }
            return false;

        }

        // Adds a new account
        public static int Register(string firstName, string lastName, string username, string password, string email, bool twoFA)
        {
            AccountBusiness accBusiness = new AccountBusiness();
            Account_Credentials_Business accCredentialsBusiness = new Account_Credentials_Business();

            AES aes = new AES();
            username = aes.Encrypt(username);
            string username_key = Convert.ToBase64String(aes.Key);
            string username_vector = Convert.ToBase64String(aes.IV);

            AES aes1 = new AES();
            firstName = aes1.Encrypt(firstName);
            string firstName_key = Convert.ToBase64String(aes1.Key);
            string firstName_vector = Convert.ToBase64String(aes1.IV);

            AES aes2 = new AES();
            lastName = aes2.Encrypt(lastName);
            string lastName_key = Convert.ToBase64String(aes2.Key);
            string lastName_vector = Convert.ToBase64String(aes2.IV);

            AES aes3 = new AES();
            email = aes3.Encrypt(email);
            string email_key = Convert.ToBase64String(aes3.Key);
            string email_vector = Convert.ToBase64String(aes3.IV);

            AES aes4 = new AES();
            password = aes4.Encrypt(password);
            string password_key = Convert.ToBase64String(aes4.Key);
            string password_vector = Convert.ToBase64String(aes4.IV);

            Account account = new Account 
            { 
                Username = username,
                First_Name = firstName, 
                Last_Name = lastName, 
                E_mail = email, 
                Password = password, 
                C2FA = twoFA 
            };
            accBusiness.Add(account);

            Account_Credentials credentials = new Account_Credentials
            {
                Account_ID = account.ID,
                Username_Key = username_key,
                Username_Vector = username_vector,
                First_Name_Key = firstName_key,
                First_Name_Vector = firstName_vector,
                Last_Name_Key = lastName_key,
                Last_Name_Vector = lastName_vector,
                E_mail_Key = email_key,
                E_mail_Vector = email_vector,
                Password_Key = password_key,
                Password_Vector = password_vector
            };

            accCredentialsBusiness.Add(credentials);
            return account.ID;
        }

        // Loads the profile data of the acive user
        public static Account LoadProfileData(int userId)
        {
            AccountBusiness accountBusiness = new AccountBusiness();
            Account_Credentials_Business accountCredentialsBusiness = new Account_Credentials_Business();
            Account account = accountBusiness.Get(userId);
            Account_Credentials accCredentials = accountCredentialsBusiness.Get(userId);

            AES aes = new AES();
            aes.Key = Convert.FromBase64String(accCredentials.Username_Key);
            aes.IV = Convert.FromBase64String(accCredentials.Username_Vector);
            account.Username = aes.Decrypt(account.Username);

            aes.Key = Convert.FromBase64String(accCredentials.First_Name_Key);
            aes.IV = Convert.FromBase64String(accCredentials.First_Name_Vector);
            account.First_Name = aes.Decrypt(account.First_Name);

            aes.Key = Convert.FromBase64String(accCredentials.Last_Name_Key);
            aes.IV = Convert.FromBase64String(accCredentials.Last_Name_Vector);
            account.Last_Name = aes.Decrypt(account.Last_Name);

            aes.Key = Convert.FromBase64String(accCredentials.E_mail_Key);
            aes.IV = Convert.FromBase64String(accCredentials.E_mail_Vector);
            account.E_mail = aes.Decrypt(account.E_mail);

            aes.Key = Convert.FromBase64String(accCredentials.Password_Key);
            aes.IV = Convert.FromBase64String(accCredentials.Password_Vector);
            account.Password = aes.Decrypt(account.Password);

            return account;
        }

        // Saves any changes to the profile data
        public static void SaveChanges(int userId, string firstName, string lastName, string email, string password)
        {
            AccountBusiness accBusiness = new AccountBusiness();
            Account_Credentials_Business accCredentialsBusiness = new Account_Credentials_Business();
            Account account = accBusiness.Get(userId);
            Account_Credentials accountCredentials = accCredentialsBusiness.Get(userId);

            AES aes = new AES();
            account.First_Name = aes.Encrypt(firstName);
            accountCredentials.First_Name_Key = Convert.ToBase64String(aes.Key);
            accountCredentials.First_Name_Vector = Convert.ToBase64String(aes.IV);

            AES aes2 = new AES();
            account.Last_Name = aes2.Encrypt(lastName);
            accountCredentials.Last_Name_Key = Convert.ToBase64String(aes2.Key);
            accountCredentials.Last_Name_Vector = Convert.ToBase64String(aes2.IV);

            AES aes3 = new AES();
            account.E_mail = aes3.Encrypt(email);
            accountCredentials.E_mail_Key = Convert.ToBase64String(aes3.Key);
            accountCredentials.E_mail_Vector = Convert.ToBase64String(aes3.IV);

            AES aes4 = new AES();
            account.Password = aes4.Encrypt(password);
            accountCredentials.Password_Key = Convert.ToBase64String(aes4.Key);
            accountCredentials.Password_Vector = Convert.ToBase64String(aes4.IV);

            accBusiness.Update(account);
            accCredentialsBusiness.Update(accountCredentials);
        }
    }
}
