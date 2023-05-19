using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers
{
    public static class EntriesManager
    {
        // The class manages the CRUD operations of the data entries
        // The class encrypts and decrypts data from the database

        // Returns all entries of the active user
        public static List<Entry> LoadAll(int userId)
        {
            EntriesBusiness entriesBusiness = new EntriesBusiness();
            Entries_Credentials_Business entriesCredentialsBusiness = new Entries_Credentials_Business();
            List<Entry> entries = entriesBusiness.GetAll().Where(x => x.Account_ID == userId).ToList();
            if (entries.Count != 0)
            {
                foreach (Entry entry in entries)
                {
                    AES aes = new AES();
                    aes.Key = Convert.FromBase64String(entriesCredentialsBusiness.Get(entry.ID).Name_Key);
                    aes.IV = Convert.FromBase64String(entriesCredentialsBusiness.Get(entry.ID).Name_Vector);
                    AES aes2 = new AES();
                    aes2.Key = Convert.FromBase64String(entriesCredentialsBusiness.Get(entry.ID).Additional_Info_Key);
                    aes2.IV = Convert.FromBase64String(entriesCredentialsBusiness.Get(entry.ID).Additional_Info_Vector);

                    entry.Name = aes.Decrypt(entry.Name);
                    entry.Additional_Info = aes2.Decrypt(entry.Additional_Info);
                    
                }
            }
            return entries;
        }

        // Returns the data of the selected entry
        public static Entry LoadEntry(int entryId)
        {
            EntriesBusiness entriesBusiness = new EntriesBusiness();
            Entries_Credentials_Business entriesCredentialsBusiness = new Entries_Credentials_Business();
            Entry selectedEntry = entriesBusiness.Get(entryId);
            AES aes = new AES();
            aes.Key = Convert.FromBase64String(entriesCredentialsBusiness.Get(selectedEntry.ID).Name_Key);
            aes.IV = Convert.FromBase64String(entriesCredentialsBusiness.Get(selectedEntry.ID).Name_Vector);
            AES aes2 = new AES();
            aes2.Key = Convert.FromBase64String(entriesCredentialsBusiness.Get(selectedEntry.ID).Additional_Info_Key);
            aes2.IV = Convert.FromBase64String(entriesCredentialsBusiness.Get(selectedEntry.ID).Additional_Info_Vector);
            selectedEntry.Name = aes.Decrypt(selectedEntry.Name);
            selectedEntry.Additional_Info = aes2.Decrypt(selectedEntry.Additional_Info);
            
            return selectedEntry;
        }

        // Saves any changes in the data of the selected entry
        public static void SaveChanges(int entryId, string name, string additional)
        {
            EntriesBusiness entriesBusiness = new EntriesBusiness();
            Entries_Credentials_Business entriesCredentialsBusiness = new Entries_Credentials_Business();
            Entry currentEntry = entriesBusiness.Get(entryId);
            Entries_Credentials entriesCredentials = entriesCredentialsBusiness.Get(currentEntry.ID);

            AES aesName = new AES();
            currentEntry.Name = aesName.Encrypt(name);
            entriesCredentials.Name_Key = Convert.ToBase64String(aesName.Key);
            entriesCredentials.Name_Vector = Convert.ToBase64String(aesName.IV);

            AES aesAdditional = new AES();
            currentEntry.Additional_Info = aesAdditional.Encrypt(additional);
            entriesCredentials.Additional_Info_Key = Convert.ToBase64String(aesAdditional.Key);
            entriesCredentials.Additional_Info_Vector = Convert.ToBase64String(aesAdditional.IV);

            entriesBusiness.Update(currentEntry);
            entriesCredentialsBusiness.Update(entriesCredentials);
        }

        // Creates a new entry for the active user
        public static void AddEntry(int userId, string name, string additional)
        {
            EntriesBusiness entriesBusiness = new EntriesBusiness();
            Entries_Credentials_Business entriesBusinessCredentials = new Entries_Credentials_Business();

            AES aes = new AES();
            name = aes.Encrypt(name);
            string titleKey = Convert.ToBase64String(aes.Key);
            string titleVector = Convert.ToBase64String(aes.IV);

            AES aes2 = new AES();
            additional = aes2.Encrypt(additional);
            string additionalKey = Convert.ToBase64String(aes2.Key);
            string additionalVector = Convert.ToBase64String(aes2.IV);

            Entry entry = new Entry { Account_ID = userId, Name = name, Additional_Info = additional };
            entriesBusiness.Add(entry);
            Entries_Credentials credentials = new Entries_Credentials { Entry_ID = entry.ID, Name_Key = titleKey, Name_Vector = titleVector, Additional_Info_Key = additionalKey, Additional_Info_Vector = additionalVector };
            entriesBusinessCredentials.Add(credentials);
        }

        // Deletes an existing entry
        public static void RemoveEntry(int entryId)
        {
            EntriesBusiness entriesBusiness = new EntriesBusiness();
            entriesBusiness.Delete(entryId);
            Entries_Credentials_Business entriesCredentials = new Entries_Credentials_Business();
            entriesCredentials.Delete(entryId);

            FieldsManager.RemoveAllFields(entryId);
        }
    }
}
