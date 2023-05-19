using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Model;

namespace Business
{
    public class Entries_Credentials_Business
    {
        // Contains the keys to decrypting "Entries"

        private CipherDBEntities cipherDB;

        public void Add(Entries_Credentials credentials)
        {
            using (cipherDB = new CipherDBEntities())
            {
                cipherDB.Entries_Credentials.Add(credentials);
                cipherDB.SaveChanges();
            }
        }
        public Entries_Credentials Get(int id)
        {
            using (cipherDB = new CipherDBEntities())
            {
                return cipherDB.Entries_Credentials.Find(id);
            }
        }
        public void Update(Entries_Credentials credentials)
        {
            using (cipherDB = new CipherDBEntities())
            {

                var item = cipherDB.Entries_Credentials.Find(credentials.Entry_ID);
                if (item != null)
                {
                    cipherDB.Entry(item).CurrentValues.SetValues(credentials);
                    cipherDB.SaveChanges();
                }

            }
        }
        public List<Entries_Credentials> GetAll()
        {
            using (cipherDB = new CipherDBEntities())
            {
                return cipherDB.Entries_Credentials.ToList();
            }
        }

        public void Delete(int id)
        {
            using (cipherDB = new CipherDBEntities())
            {
                var credentials = cipherDB.Entries_Credentials.Find(id);
                if (credentials != null)
                {
                    cipherDB.Entries_Credentials.Remove(credentials);
                    cipherDB.SaveChanges();
                }
            }
        }
    }
}
