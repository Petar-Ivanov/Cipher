using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cipher
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            ControlExtension.Draggable(this, true);
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            View.Transition(this,new RegistrationForm());
           
        }
        
        private void textBox1_Enter(object sender, EventArgs e)
        {
            View.EnterTextBox(textBox1,"Username", false);
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            View.EnterTextBox(textBox2,"Password",true);
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            View.LeaveTextBox(textBox1,"Username",false);
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            View.LeaveTextBox(textBox2,"Password",true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (View.Login(textBox1.Text, textBox2.Text))
            //{
                
            //    View.Transition(this,new Form3());
            //}
            //else
            //{
            //    MessageBox.Show("The username and the password do not match!");
            //}
            if (View.Login(textBox1.Text, textBox2.Text))
            {
                View.Transition(this, new MainForm());
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            //
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
