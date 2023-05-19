using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data;
using Data.Model;
using Business;
using Cipher.Properties;
using System.Net;
using System.Windows.Forms.DataVisualization.Charting;
using System.ComponentModel;
using System.Net.Mail;
using Business.Managers;
using System.IO;

namespace Cipher
{
    public static class View
    {
        public static int ActiveUserID = 0;
        public static int SelectedEntryID = 0;
        public static List<Panel> EntriesVirtual = new List<Panel>();
        public static Panel displayP = null;
        public static Form mainForm;
        public static NotifyIcon notifyIcon;
       
        // Extracts an icon from URL to display in the form
        public static void ShowIcon(PictureBox pictureBox, string url)
        {
            try
            {
                url = url.Replace("//", "/");
                string[] urlTockens = url.Split('/').ToArray();
                string newUrl = $"{urlTockens[0]}//{urlTockens[1]}/favicon.ico";
                var request = WebRequest.Create(newUrl);
                request.Timeout = 1000;
                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    pictureBox.Image = Bitmap.FromStream(stream);
                }
            }
            catch
            {

            }
        }

        // Visualizes all of the Icons
        public static void LoadAllIcons(Panel panel)
        {
            foreach(Control ctrl in panel.Controls)
            {
                try
                {
                    ShowIcon((PictureBox)ctrl.Controls[2],ctrl.Controls[0].Text);
                }
                catch
                {

                }
            }
        }

        // Controls the text box behaviour upon clicking in
        public static void EnterTextBox(TextBox textbox, string exampleText, bool isPassword)
        {
            if (textbox.Text == exampleText)
            {
                textbox.Text = "";
                textbox.ForeColor = Color.Black;
                if (isPassword) textbox.PasswordChar = '*';
            }
        }

        // Controls the text box behaviour upon clicking out
        public static void LeaveTextBox(TextBox textbox, string exampleText, bool isPassword)
        {
            if (textbox.Text.Trim() == "")
            {
                textbox.Text = exampleText;
                textbox.ForeColor = SystemColors.InactiveCaption;
                if (isPassword) textbox.PasswordChar = '\0';
            }
        }

        // Registers a new user with the form data if the username is free
        public static bool Register(string firstName, string lastName, string username,string password,string email, bool twoFA)
        {
            if (ProfileManager.ContainsAccount(username))
            {
                MessageBox.Show("Username is already in use!");
                return false;
            }
            else
            {
                ActiveUserID = ProfileManager.Register(firstName, lastName, username, password, email, twoFA);
                MessageBox.Show("Registration successful!");
                return true;
            }
            
        }

        // Gets userId if Login with form data is successfull
        public static bool Login(string username,string password)
        {
            int loginResult = ProfileManager.Login(username, password);
            if (loginResult != -1)
            {
                ActiveUserID = loginResult;
                return true;
            }

            MessageBox.Show("The username and the password do not match!");
            return false;
            
        }

        // Adds a subform inside the main form
        public static void openChildForm(Form childForm,Panel display,Form active)
        {
            
            if (active != null) active.Close();
            active = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            display.Controls.Add(childForm);
            display.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        // Changes the button appearance after a click
        public static void VisualiseButtonClick(Button button, PictureBox indicator, Panel field)
        {
                indicator.Visible = true;
                button.BackColor = Color.FromArgb(92, 146, 255);
                foreach (Control ctr in field.Controls)
                {
                    if (ctr.GetType() == button.GetType() && ctr != button) ctr.BackColor = Color.FromArgb(62, 138, 237);
                    else if (ctr.GetType() == indicator.GetType() && ctr != indicator) ctr.Visible = false;
                }
        }

        // Generates and stylizes a new field
        public static void DisplayField(Panel panel,string title,string information)
        { 
            Panel field = new Panel();
            panel.Controls.Add(field);
            field.Dock = DockStyle.Top;
            field.BorderStyle = BorderStyle.FixedSingle;
            field.Height = 35;
            field.HorizontalScroll.Enabled = true;
            //
            TextBox value = new TextBox();
            value.Text = information;
            value.BorderStyle = BorderStyle.None;
            value.BackColor = SystemColors.Control;
            value.Font = new Font(value.Font.Name, 15);
            field.Controls.Add(value);
            value.Dock = DockStyle.Left;
            value.AutoSize = true;
            value.Height = field.Height;
            value.Width = 220;
            value.Enter += new EventHandler(textBox_value_Enter);
            value.Leave += new EventHandler(textBox_value_Leave);
            value.TextChanged += new EventHandler(ResizeTextBox);
            //
            Label column = new Label();
            column.Text = ":";
            column.Font = new Font(column.Font.Name, 15);
            column.Height = field.Height;
            column.BorderStyle = BorderStyle.None;
            field.Controls.Add(column);
            column.Dock = DockStyle.Left;
            column.Width = 15;
            //
            TextBox name = new TextBox();
            name.Text = title;
            name.BorderStyle = BorderStyle.None;
            name.BackColor = SystemColors.Control;
            name.Font = new Font(name.Font.Name, 15);      
            field.Controls.Add(name);
            name.Dock = DockStyle.Left;
            name.AutoSize = true;
            name.Height = field.Height;
            name.Enter += new EventHandler(textBox_name_Enter);
            name.Leave += new EventHandler(textBox_name_Leave);
            name.TextChanged += new EventHandler(ResizeTextBox);
            //
            PictureBox header = new PictureBox();
            header.BackColor = SystemColors.Control;
            header.BorderStyle = BorderStyle.None;
            header.Image = Resources.header;
            header.SizeMode = PictureBoxSizeMode.Zoom;
            header.Height = field.Height;
            field.Controls.Add(header);
            header.Dock = DockStyle.Left;
            header.Width = 30;   
            
        }

        // Generates and stylizes a new addition control of the profile
        public static void DisplayAdditions(Panel panel, string title, string information, bool isOnCardParam)
        {
            Panel addition = new Panel();
            panel.Controls.Add(addition);
            addition.Dock = DockStyle.Top;
            addition.BorderStyle = BorderStyle.FixedSingle;
            addition.Height = 35;
            addition.HorizontalScroll.Enabled = true;
            //
            TextBox value = new TextBox();
            value.Text = information;
            value.BorderStyle = BorderStyle.None;
            value.BackColor = SystemColors.Control;
            value.Font = new Font(value.Font.Name, 15);
            addition.Controls.Add(value);
            value.Dock = DockStyle.Left;
            value.AutoSize = true;
            value.Height = addition.Height;
            value.Width = 220;
            value.Enter += new EventHandler(textBox_value_Enter);
            value.Leave += new EventHandler(textBox_value_Leave);
            value.TextChanged += new EventHandler(ResizeTextBox);
            //
            Label column = new Label();
            column.Text = ":";
            column.Font = new Font(column.Font.Name, 15);
            column.Height = addition.Height;
            column.BorderStyle = BorderStyle.None;
            addition.Controls.Add(column);
            column.Dock = DockStyle.Left;
            column.Width = 15;

            TextBox name = new TextBox();
            name.Text = title;
            name.BorderStyle = BorderStyle.None;
            name.BackColor = SystemColors.Control;
            name.Font = new Font(name.Font.Name, 15);
            addition.Controls.Add(name);
            name.Dock = DockStyle.Left;
            name.AutoSize = true;
            name.Height = addition.Height;
            name.Enter += new EventHandler(textBox_name_Enter);
            name.Leave += new EventHandler(textBox_name_Leave);
            name.TextChanged += new EventHandler(ResizeTextBox);

            CheckBox isOnCard = new CheckBox();
            isOnCard.Text = "";
            isOnCard.BackColor = SystemColors.Control;
            isOnCard.Checked = isOnCardParam;
            addition.Controls.Add(isOnCard);
            isOnCard.Dock = DockStyle.Left;
            isOnCard.Height = addition.Height;
            isOnCard.Font = new Font(isOnCard.Font.Name, 15);
            isOnCard.Width = 15;
        }

        // Creates a new element in the form for each addition to the selected profile in the database
        private static void LoadAllAdditions(Panel panel)
        {
            List<Account_Additions> additions = AdditionsManager.LoadAll(ActiveUserID);
            foreach (Account_Additions addition in additions)
            {
                DisplayAdditions(panel, addition.Name, addition.Data, (bool)addition.is_on_card);
            }
        }

        // Loads general profile info from the database into the form
        public static void LoadProfilePage(Panel panel,Label Username,TextBox FirstName,TextBox LastName, TextBox Email, TextBox password)
        {
            Account account = ProfileManager.LoadProfileData(ActiveUserID);

            Username.Text = account.Username;
            FirstName.Text = account.First_Name;
            LastName.Text = account.Last_Name;
            Email.Text = account.E_mail;
            password.Text = account.Password;

            LoadAllAdditions(panel);
        }

        // Changes the size of the text box
        private static void ResizeTextBox(object sender,EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            Size size = TextRenderer.MeasureText(textBox.Text, textBox.Font);
            if (size.Width >= 10) textBox.Width = size.Width;
            if (size.Height >= 10) textBox.Height = size.Height;
        }

        // Text box mask
        private static void textBox_name_Enter(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            EnterTextBox(textBox,"(title)",false);
        }

        // Text box mask
        private static void textBox_name_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            LeaveTextBox(textBox,"(title)",false);
        }

        // Text box mask
        private static void textBox_value_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            LeaveTextBox(textBox, "(value)", false);
        }

        // Text box mask
        private static void textBox_value_Enter(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            EnterTextBox(textBox, "(value)", false);
        }

        // Creates a new empty field control in the form
        public static void AddNewField(Panel panel) 
        {
            DisplayField(panel, "(title)", "(value)");
        }

        // Creates a new empty addition control in the form
        public static void AddNewAddition(Panel panel)
        {
            DisplayAdditions(panel,"(title)","(value)",false);
        }

        // Generates and stylizes a new entry control
        public static void DisplayEntry(Panel panel,string title,string information)
        {
            
            Panel entry = new Panel();
            panel.Controls.Add(entry);
            entry.Dock = DockStyle.Top;
            entry.BorderStyle = BorderStyle.FixedSingle;
            entry.Height = 35;
            entry.Click += new EventHandler(entry_clicked);
            //
            LinkLabel info = new LinkLabel();
            info.Text = information;
            info.Font = new Font(info.Font.Name, 15);
            entry.Controls.Add(info);
            info.Dock = DockStyle.Left;
            info.AutoSize = true;
            info.Click += new EventHandler(info_clicked);
            //
            Label name = new Label();
            name.Text = title;
            name.Font = new Font(name.Font.Name, 18);
            entry.Controls.Add(name);
            name.Dock = DockStyle.Left;
            name.AutoSize = true;
            name.Enabled = false;
            //
            PictureBox entryIcon = new PictureBox();
            entryIcon.Image = Cipher.Properties.Resources.lockICON;
            entryIcon.SizeMode = PictureBoxSizeMode.Zoom;
            entry.Controls.Add(entryIcon);
            entryIcon.Dock = DockStyle.Left;
            entryIcon.Width = 30;
            entryIcon.Height = 10;
            entryIcon.Enabled = false;
            //
            EntriesVirtual.Add(entry);
        }
        public static void info_clicked(object sender, EventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            try 
            {
                System.Diagnostics.Process.Start(linkLabel.Text);
            }
            catch
            {
              
            }
            
        }

        // Opens an EntryForm for the selected entry upon a click
        public static void entry_clicked(object sender, EventArgs e)
        {
            Panel panel = sender as Panel;

            EntriesBusiness entriesBusiness = new EntriesBusiness();
            List<Entry> entries = entriesBusiness.GetAll().Where(x=>x.Account_ID == ActiveUserID).ToList();
            SelectedEntryID = entries[EntriesVirtual.IndexOf(panel)].ID;
            EntriesVirtual.Clear();
            openChildForm(new EntryForm(),displayP,panel.FindForm());
                       
        }

        // Creates a new control in the form for every entry of the current user
        public static void LoadAllEntries(Panel panel)
        {
            EntriesVirtual.Clear();
            List<Entry> entries = EntriesManager.LoadAll(ActiveUserID);
            if (entries.Count != 0)
            {
                foreach (Entry entry in entries)
                {
                    DisplayEntry(panel, entry.Name, entry.Additional_Info);
                }
            }
            
        }

        // Creates a new control in the form for every field of the selected entry
        private static void LoadAllFields(Panel panel) 
        {
            List<Field> fields = FieldsManager.LoadAll(SelectedEntryID);
            if (fields.Count != 0)
            {
                foreach (Field field in fields)
                {
                    DisplayField(panel, field.Name, field.Data);
                }
            }
            
        }

        // Loads the entry page data
        public static void LoadEntryPage(Panel panel,TextBox title,TextBox additional) 
        {
            Entry selectedEntry = EntriesManager.LoadEntry(SelectedEntryID);
            title.Text = selectedEntry.Name;
            additional.Text = selectedEntry.Additional_Info;
            LoadAllFields(panel);
        }

        // Saves the changes made in the form to the database
        public static void SaveEntryChanges(Panel panel,string title,string additional)
        {
            // Saves changes to the entries
            EntriesManager.SaveChanges(SelectedEntryID, title, additional);//1

            // Removes empty fields
            foreach (Control ctrl in panel.Controls)//
            {
                if ((ctrl.Controls[2].Text.Trim() == "" || ctrl.Controls[2].Text.Trim() == "(title)") && (ctrl.Controls[0].Text.Trim() == "" || ctrl.Controls[0].Text.Trim() == "(value)"))
                {
                    int controlIndex = panel.Controls.IndexOf(ctrl);
                    if (panel.Controls.Count == FieldsManager.EntryFieldsCount(SelectedEntryID))
                    {
                        FieldsManager.RemoveField(SelectedEntryID, controlIndex);
                    }
                    panel.Controls.RemoveAt(controlIndex);
                }
            }

            // Saves changes to the fields
            for (int i = 0; i < FieldsManager.EntryFieldsCount(SelectedEntryID); i++)
            {
                FieldsManager.SaveChanges(SelectedEntryID, i, panel.Controls[i].Controls[2].Text, panel.Controls[i].Controls[0].Text);
            }

            // Saves any new fields in the form to the database
            if (panel.Controls.Count > FieldsManager.EntryFieldsCount(SelectedEntryID))
            {
                foreach (Control ctrl in panel.Controls)
                {
                    if (panel.Controls.IndexOf(ctrl) >= FieldsManager.EntryFieldsCount(SelectedEntryID))
                    {
                        FieldsManager.AddField(SelectedEntryID, panel.Controls[panel.Controls.IndexOf(ctrl)].Controls[2].Text, panel.Controls[panel.Controls.IndexOf(ctrl)].Controls[0].Text);
                    }
                }
            }
            MessageBox.Show("Changes saved!");
            

        }

        // Saves the changes made in the form to the database
        public static void SaveProfileChanges(Panel panel,string firstName,string lastName, string email,string password)
        {
            // Saves changes to the profile info
            ProfileManager.SaveChanges(ActiveUserID, firstName, lastName, email, password);

            // Removes empty additional fields
            foreach (Control ctrl in panel.Controls)//
            {
                if ((ctrl.Controls[2].Text.Trim() == "" || ctrl.Controls[2].Text.Trim() == "(title)") && (ctrl.Controls[0].Text.Trim() == "" || ctrl.Controls[0].Text.Trim() == "(value)"))
                {
                    int controlIndex = panel.Controls.IndexOf(ctrl);
                    if (panel.Controls.Count == AdditionsManager.AccountAdditionsCount(ActiveUserID))
                    {
                        AdditionsManager.RemoveAddition(ActiveUserID, controlIndex);

                    }
                    panel.Controls.RemoveAt(controlIndex);
                }
            }

            // Saves changes to the additional fields of the profile
            for (int i = 0; i < AdditionsManager.AccountAdditionsCount(ActiveUserID); i++)
            {
                CheckBox isOnCard = panel.Controls[i].Controls[3] as CheckBox;
                AdditionsManager.SaveChanges(ActiveUserID, i, panel.Controls[i].Controls[2].Text, panel.Controls[i].Controls[0].Text, isOnCard.Checked);
            }

            // Saves new additional fields of the form to the database
            if (panel.Controls.Count > AdditionsManager.AccountAdditionsCount(ActiveUserID))
            {
                foreach (Control ctrl in panel.Controls)
                {
                    if (panel.Controls.IndexOf(ctrl) >= AdditionsManager.AccountAdditionsCount(ActiveUserID))
                    {
                        CheckBox isOnCard = panel.Controls[panel.Controls.IndexOf(ctrl)].Controls[3] as CheckBox;
                        AdditionsManager.AddAddition(ActiveUserID, panel.Controls[panel.Controls.IndexOf(ctrl)].Controls[2].Text, panel.Controls[panel.Controls.IndexOf(ctrl)].Controls[0].Text, isOnCard.Checked);

                    }
                }
            }
            MessageBox.Show("Changes saved!");
            
        }      

        // Creates a new entry from form data
        public static void AddNewEntry(string title,string additional)
        {
            EntriesManager.AddEntry(ActiveUserID, title, additional);
            MessageBox.Show("Entry registered!");
            
        }

        // Filters the entries by name in accordance to wether they contain a substring
        public static void SearchEntry(Panel panel,string text)
        {
            text = text.Trim().ToLower();
            foreach(Control ctrl in panel.Controls)
            {
                ctrl.BackColor = SystemColors.Control;
                if(String.IsNullOrEmpty(text) == true)
                {
                    ctrl.Visible = true;
                }
                else
                {
                    string ctrlText = ctrl.Controls[1].Text.Trim().ToLower();
                    if (!ctrlText.Contains(text))
                    {
                        ctrl.Visible = false;
                    }
                    else
                    {
                        if (ctrlText.Contains(text) && String.IsNullOrEmpty(text) == false)
                        {
                            ctrl.BackColor = Color.FromArgb(237, 245, 91);
                        }
                        if (ctrlText == text)
                        {
                            ctrl.BackColor = Color.FromArgb(127, 214, 45);
                        }
                    }
                }  
            }
        }

        // Deletes the selected entry
        public static void DeleteEntry()
        {
            EntriesManager.RemoveEntry(SelectedEntryID);
        }

        // Evaluates a password
        public static void DisplayEvaluation(string password,Panel meter,Label scoreTxt)
        {
            int score = PasswordManager.Evaluate(password);
            meter.Width = score * 2;
            scoreTxt.Text = $"{score}/100";
        }

        // Generates and displays a password
        public static void DisplayGeneratedPassword(string forbidden,int length,TextBox textBox)
        {
            textBox.Text = PasswordManager.GeneratePassword(forbidden,length);
        }

        // Loads a password + evaluation field in the security form
        public static void DisplaySequrityInfos(Panel panel,string title,int evaluation)
        {
            Panel seq = new Panel();
            panel.Controls.Add(seq);
            seq.Dock = DockStyle.Top;
            seq.BorderStyle = BorderStyle.FixedSingle;
            seq.Height = 35;

            Label score = new Label();
            score.Text = $"| {evaluation}/100";
            score.Font = new Font(score.Font.Name, 18);
            seq.Controls.Add(score);
            score.Dock = DockStyle.Left;
            score.AutoSize = true;
            score.Enabled = false;

            Label name = new Label();
            name.Text = title;
            name.Font = new Font(name.Font.Name, 18);
            seq.Controls.Add(name);
            name.Dock = DockStyle.Left;
            name.AutoSize = true;
            name.Enabled = false;

            PictureBox icon = new PictureBox();
            icon.Image = Cipher.Properties.Resources.lockICON;
            icon.SizeMode = PictureBoxSizeMode.Zoom;
            seq.Controls.Add(icon);
            icon.Dock = DockStyle.Left;
            icon.Width = 30;
            icon.Height = 10;
            icon.Enabled = false;
        }
        
        // Loads all password + evaluation fields and generates a chart
        public static void LoadSequrityPage(Panel panel,Chart chart)
        {
            int weakPass = 0;
            int avrgPass = 0;
            int strongPass = 0;
            EntriesBusiness entriesBusiness = new EntriesBusiness();
            Entries_Credentials_Business entriesCredentialsBusiness = new Entries_Credentials_Business();
            Dictionary<int, string> passwords = FieldsManager.GetAllPasswords(ActiveUserID);
            AES aes = new AES();
            int index = 0;
            foreach(KeyValuePair<int,string> pass in passwords)
            {
                aes.Key = Convert.FromBase64String(entriesCredentialsBusiness.Get(pass.Key).Name_Key);
                aes.IV = Convert.FromBase64String(entriesCredentialsBusiness.Get(pass.Key).Name_Vector);
                string title = aes.Decrypt(entriesBusiness.Get(pass.Key).Name);
                int evaluation = PasswordManager.Evaluate(pass.Value);
                if (evaluation < 50) weakPass++;
                if (evaluation >= 50 && evaluation < 75) avrgPass++;
                if (evaluation >= 75) strongPass++;
                DisplaySequrityInfos(panel, title, evaluation);
                //
                aes.Key = Convert.FromBase64String(entriesCredentialsBusiness.Get(pass.Key).Additional_Info_Key);
                aes.IV = Convert.FromBase64String(entriesCredentialsBusiness.Get(pass.Key).Additional_Info_Vector);
                ShowIcon(panel.Controls[index].Controls[2] as PictureBox, aes.Decrypt(entriesBusiness.Get(pass.Key).Additional_Info));
                index++;
            }
            chart.Series["Passwords"].Points.AddXY("Weak",weakPass);
            chart.Series["Passwords"].Points.AddXY("Average", avrgPass);
            chart.Series["Passwords"].Points.AddXY("Strong", strongPass);
        }

        
        public static void Transition(Form currentForm,Form newForm)
        {
            newForm.StartPosition = FormStartPosition.Manual;
            newForm.Location = currentForm.Location;
            newForm.Activate();
            newForm.Show();
            currentForm.Hide();
            
        }

        // Loads data into the taskbar mode window
        public static void LoadCard(Panel panel,TextBox fName,TextBox lName,TextBox email)
        {
            Account account = ProfileManager.LoadProfileData(ActiveUserID);
            fName.Text = account.First_Name;
            lName.Text = account.Last_Name;
            email.Text = account.E_mail;

            List<Account_Additions> additions = AdditionsManager.LoadAllOnCard(ActiveUserID);
            foreach (Account_Additions addition in additions)
            {
                DisplayCardAdditions(panel, addition.Name, addition.Data);
            }
            
        }

        // Creates an additional info field control in the task bar mode form
        public static void DisplayCardAdditions(Panel panel,string title,string information)
        {
            Panel addition = new Panel();
            panel.Controls.Add(addition);
            addition.Dock = DockStyle.Top;
            addition.BorderStyle = BorderStyle.FixedSingle;
            addition.Height = 35;
            addition.HorizontalScroll.Enabled = true;
            //
            TextBox value = new TextBox();
            value.Text = information;
            value.BorderStyle = BorderStyle.None;
            value.BackColor = SystemColors.Control;
            value.Font = new Font(value.Font.Name, 15);
            addition.Controls.Add(value);
            value.Dock = DockStyle.Left;
            value.AutoSize = true;
            value.Height = addition.Height;
            value.Width = 220;
            value.Enter += new EventHandler(textBox_value_Enter);
            value.Leave += new EventHandler(textBox_value_Leave);
            value.TextChanged += new EventHandler(ResizeTextBox);
            //
            Label column = new Label();
            column.Text = ":";
            column.Font = new Font(column.Font.Name, 15);
            column.Height = addition.Height;
            column.BorderStyle = BorderStyle.None;
            addition.Controls.Add(column);
            column.Dock = DockStyle.Left;
            column.Width = 15;

            TextBox name = new TextBox();
            name.Text = title;
            name.BorderStyle = BorderStyle.None;
            name.BackColor = SystemColors.Control;
            name.Font = new Font(name.Font.Name, 15);
            addition.Controls.Add(name);
            name.Dock = DockStyle.Left;
            name.AutoSize = true;
            name.Height = addition.Height;
            name.Enter += new EventHandler(textBox_name_Enter);
            name.Leave += new EventHandler(textBox_name_Leave);
            name.TextChanged += new EventHandler(ResizeTextBox);

        }
        
    }
}
