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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            ControlExtension.Draggable(this, true);
            View.displayP = DisplayPanel;
            View.VisualiseButtonClick(btn2, ind2, panel1);
            View.openChildForm(new ListOfEntriesForm(), DisplayPanel, activeForm);
            View.mainForm = this;
        }
        private Form activeForm = null;

        

        private void btn1_Click(object sender, EventArgs e)
        {
            View.VisualiseButtonClick(btn1, ind1, panel1);
            View.openChildForm(new ProfileForm(), DisplayPanel, activeForm);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            View.VisualiseButtonClick(btn2, ind2, panel1);
            View.openChildForm(new ListOfEntriesForm(), DisplayPanel, activeForm);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            View.VisualiseButtonClick(btn3, ind3, panel1);
            View.openChildForm(new PasswordForm(), DisplayPanel, activeForm);
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            View.VisualiseButtonClick(btn4, ind4, panel1);
            View.openChildForm(new SequrityStatisticForm(), DisplayPanel, activeForm);
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            View.VisualiseButtonClick(btn5, ind5, panel1);
            View.openChildForm(new InfoForm(), DisplayPanel, activeForm);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            View.ActiveUserID = 0;
            View.Transition(this,new LoginForm());
        }
        public Panel GetDisplay()
        {
            return DisplayPanel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            View.notifyIcon = notifyIcon1;
            notifyIcon1.Icon = Icon.FromHandle(Cipher.Properties.Resources.lockICON.GetHicon());
            this.ShowInTaskbar = false;
            notifyIcon1.Visible = true;
            this.WindowState = FormWindowState.Minimized;
            this.Visible = false;
            
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            InfoCardForm form10 = new InfoCardForm();
            form10.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = false;
            //this.ShowInTaskbar = true;
            //notifyIcon1.Visible = false;
            //this.Visible = true;
        }
    }
}
