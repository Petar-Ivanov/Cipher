using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cipher
{
    public partial class EntryForm : Form
    {
        public EntryForm()
        {
            InitializeComponent();
            View.LoadEntryPage(panel1,textBox1,textBox2);
            
            View.ShowIcon(icon,textBox2.Text);
        }
        private Form activeForm = null;
        void link_clicked(object sender, EventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            System.Diagnostics.Process.Start("https://www.google.com/");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            View.SaveEntryChanges(panel1,textBox1.Text,textBox2.Text);
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            View.AddNewField(panel1);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
           
        }

        private void textBox2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(textBox2.Text);
            }
            catch 
            {

            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Do you want to delete this entry?","Deleting Entry",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)==DialogResult.Yes)
            {
                View.DeleteEntry();
                View.openChildForm(new ListOfEntriesForm(), View.displayP, activeForm);
            }
            
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            View.mainForm.WindowState = FormWindowState.Minimized;
        }
    }
}
