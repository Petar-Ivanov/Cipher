using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Model;

namespace Business
{
    public class AdditionsBusiness
    {
        private CipherDBEntities cipherDB;

        public void Add(Account_Additions addition)
        {
            using (cipherDB = new CipherDBEntities())
            {
                cipherDB.Account_Additions.Add(addition);
                cipherDB.SaveChanges();
            }
        }
        public Account_Additions Get(int id)
        {
            using (cipherDB = new CipherDBEntities())
            {
                return cipherDB.Account_Additions.Find(id);
            }
        }
        public void Update(Account_Additions addition)
        {
            using (cipherDB = new CipherDBEntities())
            {

                var item = cipherDB.Account_Additions.Find(addition.ID);
                if (item != null)
                {
                    cipherDB.Entry(item).CurrentValues.SetValues(addition);
                    cipherDB.SaveChanges();
                }

            }
        }
        public List<Account_Additions> GetAll()
        {
            using (cipherDB = new CipherDBEntities())
            {
                return cipherDB.Account_Additions.ToList();
            }
        }

        public void Delete(int id)
        {
            using (cipherDB = new CipherDBEntities())
            {
                var addition = cipherDB.Account_Additions.Find(id);
                if (addition != null)
                {
                    cipherDB.Account_Additions.Remove(addition);
                    cipherDB.SaveChanges();
                }
            }
        }
    }
}
