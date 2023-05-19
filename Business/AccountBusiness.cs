using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Model;

namespace Business
{
    public class AccountBusiness
    {
        private CipherDBEntities cipherDB;

        public void Add(Account account)
        {
            using (cipherDB = new CipherDBEntities())
            {
                cipherDB.Accounts.Add(account);
                cipherDB.SaveChanges();
            }
        }
        public Account Get(int id)
        {
            using (cipherDB = new CipherDBEntities())
            {
                return cipherDB.Accounts.Find(id);
            }
        }
        public void Update(Account acc)
        {
            using (cipherDB = new CipherDBEntities())
            {

                var item = cipherDB.Accounts.Find(acc.ID);
                if (item != null)
                {
                    cipherDB.Entry(item).CurrentValues.SetValues(acc);
                    cipherDB.SaveChanges();
                }

            }
        }
        public List<Account> GetAll()
        {
            using (cipherDB = new CipherDBEntities())
            {
                return cipherDB.Accounts.ToList();
            }
        }
    }
}
