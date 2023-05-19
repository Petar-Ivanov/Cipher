using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Model;

namespace Business
{
    public class Account_Credentials_Business
    {
        // Contains the keys to decrypting "Accounts"
        
        private CipherDBEntities cipherDB;

        public void Add(Account_Credentials credentials)
        {
            using (cipherDB = new CipherDBEntities())
            {
                cipherDB.Account_Credentials.Add(credentials);
                cipherDB.SaveChanges();
            }
        }
        public Account_Credentials Get(int id)
        {
            using (cipherDB = new CipherDBEntities())
            {
                return cipherDB.Account_Credentials.Find(id);
            }
        }
        public void Update(Account_Credentials credentials)
        {
            using (cipherDB = new CipherDBEntities())
            {

                var item = cipherDB.Account_Credentials.Find(credentials.Account_ID);
                if (item != null)
                {
                    cipherDB.Entry(item).CurrentValues.SetValues(credentials);
                    cipherDB.SaveChanges();
                }

            }
        }
        public List<Account_Credentials> GetAll()
        {
            using (cipherDB = new CipherDBEntities())
            {
                return cipherDB.Account_Credentials.ToList();
            }
        }
    }
}
