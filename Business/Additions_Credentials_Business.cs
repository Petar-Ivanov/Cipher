using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Model;

namespace Business
{
    public class Additions_Credentials_Business
    {
        // Contains the keys to decrypting "Additions"

        private CipherDBEntities cipherDB;

        public void Add(Additions_Credentials credentials)
        {
            using (cipherDB = new CipherDBEntities())
            {
                cipherDB.Additions_Credentials.Add(credentials);
                cipherDB.SaveChanges();
            }
        }
        public Additions_Credentials Get(int id)
        {
            using (cipherDB = new CipherDBEntities())
            {
                return cipherDB.Additions_Credentials.Find(id);
            }
        }
        public void Update(Additions_Credentials credentials)
        {
            using (cipherDB = new CipherDBEntities())
            {

                var item = cipherDB.Additions_Credentials.Find(credentials.Addition_ID);
                if (item != null)
                {
                    cipherDB.Entry(item).CurrentValues.SetValues(credentials);
                    cipherDB.SaveChanges();
                }

            }
        }
        public List<Additions_Credentials> GetAll()
        {
            using (cipherDB = new CipherDBEntities())
            {
                return cipherDB.Additions_Credentials.ToList();
            }
        }

        public void Delete(int id)
        {
            using (cipherDB = new CipherDBEntities())
            {
                var credentials = cipherDB.Additions_Credentials.Find(id);
                if (credentials != null)
                {
                    cipherDB.Additions_Credentials.Remove(credentials);
                    cipherDB.SaveChanges();
                }
            }
        }
    }
}
