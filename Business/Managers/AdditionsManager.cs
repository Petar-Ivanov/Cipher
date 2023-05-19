using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers
{
    public static class AdditionsManager
    {
        // The class manages the CRUD operations of the additional info fields of the user profile
        // The class encrypts and decrypts data from the database

        // Returns the additional account info of the current user
        public static List<Account_Additions> LoadAll(int userId)
        {
            AdditionsBusiness additionsBusiness = new AdditionsBusiness();
            Additions_Credentials_Business additionsCredentialsBusiness = new Additions_Credentials_Business();
            List<Account_Additions> additions = additionsBusiness.GetAll().Where(x => x.Account_ID == userId).ToList();
            foreach (Account_Additions addition in additions)
            {
                AES aes = new AES();
                aes.Key = Convert.FromBase64String(additionsCredentialsBusiness.Get(addition.ID).Name_Key);
                aes.IV = Convert.FromBase64String(additionsCredentialsBusiness.Get(addition.ID).Name_Vector);
                AES aes2 = new AES();
                aes2.Key = Convert.FromBase64String(additionsCredentialsBusiness.Get(addition.ID).Data_Key);
                aes2.IV = Convert.FromBase64String(additionsCredentialsBusiness.Get(addition.ID).Data_Vector);

                addition.Name = aes.Decrypt(addition.Name);
                addition.Data = aes2.Decrypt(addition.Data);
            }
            return additions;
        }

        // Returns the additional account info of the current user IF it is selected as displayable in Taskbar mode
        public static List<Account_Additions> LoadAllOnCard(int userId)
        {
            AdditionsBusiness additionsBusiness = new AdditionsBusiness();
            Additions_Credentials_Business additionsCredentialsBusiness = new Additions_Credentials_Business();
            List<Account_Additions> additions = additionsBusiness.GetAll().Where(x => x.Account_ID == userId && x.is_on_card == true).ToList();
            foreach (Account_Additions addition in additions)
            {
                AES aes = new AES();
                aes.Key = Convert.FromBase64String(additionsCredentialsBusiness.Get(addition.ID).Name_Key);
                aes.IV = Convert.FromBase64String(additionsCredentialsBusiness.Get(addition.ID).Name_Vector);
                AES aes2 = new AES();
                aes2.Key = Convert.FromBase64String(additionsCredentialsBusiness.Get(addition.ID).Data_Key);
                aes2.IV = Convert.FromBase64String(additionsCredentialsBusiness.Get(addition.ID).Data_Vector);

                addition.Name = aes.Decrypt(addition.Name);
                addition.Data = aes2.Decrypt(addition.Data);
            }
            return additions;
        }

        // Returns the count of all additional info fields
        public static int AccountAdditionsCount(int userId)
        {
            AdditionsBusiness additionsBusiness = new AdditionsBusiness();
            return additionsBusiness.GetAll().Where(x => x.Account_ID == userId).Count();
        }

        // Deletes a selected additional info field
        public static void RemoveAddition(int userId, int controlIndex)
        {
            AdditionsBusiness additionsBusiness = new AdditionsBusiness();
            Additions_Credentials_Business additionsCredentialsBusiness = new Additions_Credentials_Business();
            List<Account_Additions> additions = additionsBusiness.GetAll().Where(x => x.Account_ID == userId).ToList();

            additionsBusiness.Delete(additions[controlIndex].ID);
            additionsCredentialsBusiness.Delete(additions[controlIndex].ID);
            additions.RemoveAt(controlIndex);
        }

        // Saves any changes made to the additional info fields
        public static void SaveChanges(int userId, int controlIndex, string name, string data, bool isOnCard)
        {

            AdditionsBusiness additionsBusiness = new AdditionsBusiness();
            Additions_Credentials_Business additionsCredentialsBusiness = new Additions_Credentials_Business();
            
            List<Account_Additions> additions = additionsBusiness.GetAll().Where(x => x.Account_ID == userId).ToList();
            Account_Additions addition = additions[controlIndex];

            Additions_Credentials credentials = additionsCredentialsBusiness.Get(addition.ID);
            AES aesTitle = new AES();
            addition.Name = aesTitle.Encrypt(name);
            credentials.Name_Key = Convert.ToBase64String(aesTitle.Key);
            credentials.Name_Vector = Convert.ToBase64String(aesTitle.IV);
            AES aesValue = new AES();
            addition.Data = aesValue.Encrypt(data);
            credentials.Data_Key = Convert.ToBase64String(aesValue.Key);
            credentials.Data_Vector = Convert.ToBase64String(aesValue.IV);
            addition.is_on_card = isOnCard;

            additionsBusiness.Update(addition);
            additionsCredentialsBusiness.Update(credentials);
        }

        // Creates a new additional info field
        public static void AddAddition(int userId, string name, string data, bool isOnCard)
        {
            AdditionsBusiness additionsBusiness = new AdditionsBusiness();
            Additions_Credentials_Business additionsCredentialsBusiness = new Additions_Credentials_Business();

            AES aesTitle = new AES();
            AES aesData = new AES();
            Account_Additions addition = new Account_Additions { Account_ID = userId, Name = aesTitle.Encrypt(name), Data = aesData.Encrypt(data), is_on_card = isOnCard };
            additionsBusiness.Add(addition);
            Additions_Credentials credentials = new Additions_Credentials { Addition_ID = addition.ID, Name_Key = Convert.ToBase64String(aesTitle.Key), Name_Vector = Convert.ToBase64String(aesTitle.IV), Data_Key = Convert.ToBase64String(aesData.Key), Data_Vector = Convert.ToBase64String(aesData.IV) };
            additionsCredentialsBusiness.Add(credentials);
        }
    }
}
