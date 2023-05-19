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
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
            ControlExtension.Draggable(this, true);
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            View.EnterTextBox(textBox4,"First Name",false);
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            View.EnterTextBox(textBox5, "Last Name", false);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            View.EnterTextBox(textBox1, "Username", false);
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            View.EnterTextBox(textBox2, "Password", true);
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            View.EnterTextBox(textBox3, "E-mail", false);
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            View.LeaveTextBox(textBox4,"First Name",false);
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            View.LeaveTextBox(textBox5, "Last Name", false);
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            View.LeaveTextBox(textBox1, "Username", false);
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            View.LeaveTextBox(textBox2, "Password", true);
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            View.LeaveTextBox(textBox3, "E-mail", false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            View.Transition(this,new LoginForm());
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" || textBox4.Text.Trim() == "" || textBox5.Text.Trim() == "" || textBox1.Text == "Username" || textBox2.Text == "Password" || textBox3.Text == "E-mail" || textBox4.Text == "First Name" || textBox5.Text == "Last Name") MessageBox.Show("Please fill all fields!", "Fields missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                if(View.Register(textBox4.Text, textBox5.Text, textBox1.Text, textBox2.Text, textBox3.Text, false))
                {
                    View.Transition(this, new MainForm());
                }
            }
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
