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
    public partial class PasswordForm : Form
    {
        public PasswordForm()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            View.DisplayEvaluation(textBox4.Text.Trim(),meter,label4);
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            View.EnterTextBox(textBox4, "Password", true);
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            View.LeaveTextBox(textBox4,"Password",true);
        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            label6.Text = (hScrollBar1.Value/10*2).ToString();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (label6.Text == "0")
            {
                MessageBox.Show("Please select password length!", "Password Length", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                View.DisplayGeneratedPassword(textBox1.Text.Trim(), int.Parse(label6.Text), textBox3);
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            View.EnterTextBox(textBox1,"Forbidden Characters",false);
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            View.LeaveTextBox(textBox1, "Forbidden Characters", false);
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
