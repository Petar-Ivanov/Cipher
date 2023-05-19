using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Model;

namespace Business
{
    public class EntriesBusiness
    {
        private CipherDBEntities cipherDB;

        public void Add(Entry entry)
        {
            using (cipherDB = new CipherDBEntities())
            {
                cipherDB.Entries.Add(entry);
                cipherDB.SaveChanges();
            }
        }
        public Entry Get(int id)
        {
            using (cipherDB = new CipherDBEntities())
            {
                return cipherDB.Entries.Find(id);
            }
        }
        public void Update(Entry entry)
        {
            using (cipherDB = new CipherDBEntities())
            {

                var item = cipherDB.Entries.Find(entry.ID);
                if (item != null)
                {
                    cipherDB.Entry(item).CurrentValues.SetValues(entry);
                    cipherDB.SaveChanges();
                }

            }
        }
        public List<Entry> GetAll()
        {
            using (cipherDB = new CipherDBEntities())
            {
                return cipherDB.Entries.ToList();
            }
        }

        public void Delete(int id)
        {
            using (cipherDB = new CipherDBEntities())
            {
                var entry = cipherDB.Entries.Find(id);
                if (entry != null)
                {
                    cipherDB.Entries.Remove(entry);
                    cipherDB.SaveChanges();
                }
            }
        }
    }
}
