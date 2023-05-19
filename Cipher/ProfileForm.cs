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
    public partial class ProfileForm : Form
    {
        public ProfileForm()
        {
            InitializeComponent();
            View.LoadProfilePage(panel3,label1,textBox1,textBox2,textBox3,textBox4);
            
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            View.AddNewAddition(panel3);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            View.SaveProfileChanges(panel3,textBox1.Text,textBox2.Text,textBox3.Text,textBox4.Text);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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
