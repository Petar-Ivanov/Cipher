using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers
{
    public static class FieldsManager
    {
        // The class manages the CRUD operations of the data fields inside an entry
        // The class encrypts and decrypts data from the database

        // Returns all data fields inside the selected entry
        public static List<Field> LoadAll(int entryId)
        {

            FieldsBusiness fieldsBusiness = new FieldsBusiness();
            Fields_Credentials_Business fieldsCredentialsBusiness = new Fields_Credentials_Business();
            List<Field> fields = fieldsBusiness.GetAll().Where(x => x.Entry_ID == entryId).ToList();
            if (fields.Count != 0)
            {
                foreach (Field field in fields)
                {
                    AES aes = new AES();
                    aes.Key = Convert.FromBase64String(fieldsCredentialsBusiness.Get(field.ID).Name_Key);
                    aes.IV = Convert.FromBase64String(fieldsCredentialsBusiness.Get(field.ID).Name_Vector);
                    AES aes2 = new AES();
                    aes2.Key = Convert.FromBase64String(fieldsCredentialsBusiness.Get(field.ID).Data_Key);
                    aes2.IV = Convert.FromBase64String(fieldsCredentialsBusiness.Get(field.ID).Data_Vector);

                    field.Name = aes.Decrypt(field.Name);
                    field.Data = aes2.Decrypt(field.Data);
                }
            }
            return fields;
        }

        // Returns the count of the data fields that belong to the selected entry
        public static int EntryFieldsCount(int entryId)
        {
            return new FieldsBusiness().GetAll().Where(x => x.Entry_ID == entryId).Count();
        }

        // Deletes the selected field of the entry
        public static void RemoveField(int entryId, int controlIndex)
        {
            FieldsBusiness fieldsBusiness = new FieldsBusiness();
            Fields_Credentials_Business fieldsCredentialsBusiness = new Fields_Credentials_Business();
            List<Field> fields = fieldsBusiness.GetAll().Where(x => x.Entry_ID == entryId).ToList();

            fieldsBusiness.Delete(fields[controlIndex].ID);
            fieldsCredentialsBusiness.Delete(fields[controlIndex].ID);
            fields.RemoveAt(controlIndex);
        }

        // Removes all fields of the selected entry
        public static void RemoveAllFields(int entryId)
        {
            FieldsBusiness fieldsBusiness = new FieldsBusiness();
            Fields_Credentials_Business fieldsCredentialsBusiness = new Fields_Credentials_Business();
            List<Field> fields = fieldsBusiness.GetAll().Where(x => x.Entry_ID == entryId).ToList();
            foreach (Field field in fields)
            {
                fieldsBusiness.Delete(field.ID);
                fieldsCredentialsBusiness.Delete(field.ID);
            }
        }

        // Saves all changes made to a specific field
        public static void SaveChanges(int entryId, int controlIndex, string name, string data)
        {
            FieldsBusiness fieldsBusiness = new FieldsBusiness();
            Fields_Credentials_Business fieldsCredentialsBusiness = new Fields_Credentials_Business();

            List<Field> fields = fieldsBusiness.GetAll().Where(x => x.Entry_ID == entryId).ToList();
            Field field = fields[controlIndex];
            Fields_Credentials credentials = fieldsCredentialsBusiness.Get(field.ID);
            AES aes = new AES();
            field.Name = aes.Encrypt(name);
            credentials.Name_Key = Convert.ToBase64String(aes.Key);
            credentials.Name_Vector = Convert.ToBase64String(aes.IV);
            AES aes2 = new AES();
            field.Data = aes2.Encrypt(data);
            credentials.Data_Key = Convert.ToBase64String(aes2.Key);
            credentials.Data_Vector = Convert.ToBase64String(aes2.IV);

            fieldsBusiness.Update(field);
            fieldsCredentialsBusiness.Update(credentials);
        }

        // Creates a new data field for the selected entry
        public static void AddField(int entryId, string name, string data)
        {
            FieldsBusiness fieldsBusiness = new FieldsBusiness();
            Fields_Credentials_Business fieldsCredentialsBusiness = new Fields_Credentials_Business();

            AES aesTitle = new AES();
            AES aesData = new AES();
            Field field = new Field { Entry_ID = entryId, Name = aesTitle.Encrypt(name), Data = aesData.Encrypt(data) };
            fieldsBusiness.Add(field);
            Fields_Credentials credentials = new Fields_Credentials { Field_ID = field.ID, Name_Key = Convert.ToBase64String(aesTitle.Key), Name_Vector = Convert.ToBase64String(aesTitle.IV), Data_Key = Convert.ToBase64String(aesData.Key), Data_Vector = Convert.ToBase64String(aesData.IV) };
            fieldsCredentialsBusiness.Add(credentials);
        }

        // Returns those fields that are considered to contain password data
        public static Dictionary<int, string> GetAllPasswords(int userId)
        {
            Dictionary<int, string> passwords = new Dictionary<int, string>();
            EntriesBusiness entriesBusiness = new EntriesBusiness();
            Entries_Credentials_Business entriesCredentialsBusiness = new Entries_Credentials_Business();
            FieldsBusiness fieldsBusiness = new FieldsBusiness();
            Fields_Credentials_Business fieldsCredentialsBusiness = new Fields_Credentials_Business();

            List<Entry> entries = entriesBusiness.GetAll().Where(x => x.Account_ID == userId).ToList();
            AES aes = new AES();
            foreach (Entry entry in entries)
            {
                List<Field> fields = fieldsBusiness.GetAll().Where(x => x.Entry_ID == entry.ID).ToList();
                foreach (Field field in fields)
                {
                    aes.Key = Convert.FromBase64String(fieldsCredentialsBusiness.Get(field.ID).Name_Key);
                    aes.IV = Convert.FromBase64String(fieldsCredentialsBusiness.Get(field.ID).Name_Vector);
                    string name = aes.Decrypt(field.Name);

                    // Checks wether the field is a password field
                    if (name.ToLower().Contains("password") || name.ToLower().Contains("pasword") || name.ToLower().Contains("парола"))
                    {
                        aes.Key = Convert.FromBase64String(fieldsCredentialsBusiness.Get(field.ID).Data_Key);
                        aes.IV = Convert.FromBase64String(fieldsCredentialsBusiness.Get(field.ID).Data_Vector);
                        string password = aes.Decrypt(field.Data);
                        passwords.Add(entry.ID, password);
                    }

                }
            }
            return passwords;
        }
    }
}
