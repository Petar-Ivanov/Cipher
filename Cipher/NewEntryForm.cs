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
    public partial class NewEntryForm : Form
    {
        public NewEntryForm()
        {
            InitializeComponent();
            ControlExtension.Draggable(this, true);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            View.EnterTextBox(textBox1, "Title", false);
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            View.LeaveTextBox(textBox1,"Title",false);
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            View.EnterTextBox(textBox2,"Additional Information (Link)",false);
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            View.LeaveTextBox(textBox2, "Additional Information (Link)",false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "Title")
            {
                View.AddNewEntry(textBox1.Text, textBox2.Text);
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter title!");
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
