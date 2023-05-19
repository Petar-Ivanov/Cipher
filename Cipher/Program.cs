using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cipher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CheckAndInitializeDatabase();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }

        static void CheckAndInitializeDatabase()
        {
            using (var context = new CipherDBEntities())
            {
                if (!context.Database.Exists())
                {
                    context.Database.Create();
                }
            }
        }
    }
}
