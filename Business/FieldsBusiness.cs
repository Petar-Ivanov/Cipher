using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Model;

namespace Business
{
    public class FieldsBusiness
    {
        private CipherDBEntities cipherDB;

        public void Add(Field field)
        {
            using (cipherDB = new CipherDBEntities())
            {
                cipherDB.Fields.Add(field);
                cipherDB.SaveChanges();
            }
        }
        public Field Get(int id)
        {
            using (cipherDB = new CipherDBEntities())
            {
                return cipherDB.Fields.Find(id);
            }
        }
        public void Update(Field field)
        {
            using (cipherDB = new CipherDBEntities())
            {

                var item = cipherDB.Fields.Find(field.ID);
                if (item != null)
                {
                    cipherDB.Entry(item).CurrentValues.SetValues(field);
                    cipherDB.SaveChanges();
                }

            }
        }
        public List<Field> GetAll()
        {
            using (cipherDB = new CipherDBEntities())
            {
                return cipherDB.Fields.ToList();
            }
        }

        public void Delete(int id)
        {
            using (cipherDB = new CipherDBEntities())
            {
                var field = cipherDB.Fields.Find(id);
                if (field != null)
                {
                    cipherDB.Fields.Remove(field);
                    cipherDB.SaveChanges();
                }
            }
        }
    }
}
