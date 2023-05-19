using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Model;

namespace Business
{
    public class Fields_Credentials_Business
    {
        // Contains the keys to decrypting "Fields"

        private CipherDBEntities cipherDB;

        public void Add(Fields_Credentials credentials)
        {
            using (cipherDB = new CipherDBEntities())
            {
                cipherDB.Fields_Credentials.Add(credentials);
                cipherDB.SaveChanges();
            }
        }
        public Fields_Credentials Get(int id)
        {
            using (cipherDB = new CipherDBEntities())
            {
                return cipherDB.Fields_Credentials.Find(id);
            }
        }
        public void Update(Fields_Credentials credentials)
        {
            using (cipherDB = new CipherDBEntities())
            {

                var item = cipherDB.Fields_Credentials.Find(credentials.Field_ID);
                if (item != null)
                {
                    cipherDB.Entry(item).CurrentValues.SetValues(credentials);
                    cipherDB.SaveChanges();
                }

            }
        }
        public List<Fields_Credentials> GetAll()
        {
            using (cipherDB = new CipherDBEntities())
            {
                return cipherDB.Fields_Credentials.ToList();
            }
        }

        public void Delete(int id)
        {
            using (cipherDB = new CipherDBEntities())
            {
                var credentials = cipherDB.Fields_Credentials.Find(id);
                if (credentials != null)
                {
                    cipherDB.Fields_Credentials.Remove(credentials);
                    cipherDB.SaveChanges();
                }
            }
        }
    }
}
