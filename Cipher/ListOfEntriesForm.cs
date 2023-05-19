using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;

namespace Cipher
{
    public partial class ListOfEntriesForm : Form
    {
        public ListOfEntriesForm()
        {
            InitializeComponent();
            panel1.VerticalScroll.Enabled = true;
            panel1.VerticalScroll.Visible = true;
            View.EntriesVirtual.Clear();
            View.LoadAllEntries(panel1);
            View.LoadAllIcons(panel1);
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            NewEntryForm form6 = new NewEntryForm();
            form6.Show();
        }
        private Form activeForm = null;
        
        private void textBox4_Enter(object sender, EventArgs e)
        {
            View.EnterTextBox(textBox4, "Search", false);
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            View.LeaveTextBox(textBox4, "Search", false);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            View.EntriesVirtual.Clear();
           for(int i = panel1.Controls.Count-1;i>=0;i--)
           {
                panel1.Controls.RemoveAt(i);
           }
           View.LoadAllEntries(panel1);
            View.LoadAllIcons(panel1);
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            View.SearchEntry(panel1, textBox4.Text);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            View.mainForm.WindowState = FormWindowState.Minimized;
        }
    }
}
