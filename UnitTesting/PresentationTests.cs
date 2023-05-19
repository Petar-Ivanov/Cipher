using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business;
using Cipher;
using Data.Model;
using NUnit;
using NUnit.Framework;

namespace UnitTesting
{
    [TestFixture]
    public static class PresentationTests
    {
        [TestCase]
        public static void UrlFormating()
        {
            PictureBox pictureBox = new PictureBox();
            PictureBox pictureBox2 = new PictureBox();
            string url1 = "https://www.google.com/";
            string url2 = "https://www.google.com/favicon.ico";
            Cipher.View.ShowIcon(pictureBox, url1);
            Cipher.View.ShowIcon(pictureBox2, url2);
            Assert.AreEqual(pictureBox.Image!=null,pictureBox2.Image!=null);
        }
        [TestCase]
        public static void EnterTextBoxTextRemove()
        {
            string exmpleText = "Username";
            TextBox txt = new TextBox();
            txt.Text = "Username";
            Cipher.View.EnterTextBox(txt,exmpleText,false);
            Assert.AreEqual("",txt.Text);
        }
        [TestCase]
        public static void EnterTextBoxForeColorChange()
        {
            string exmpleText = "Username";
            TextBox txt = new TextBox();
            txt.Text = "Username";
            Cipher.View.EnterTextBox(txt, exmpleText, false);
            Assert.AreEqual(Color.Black, txt.ForeColor);
        }
        [TestCase]
        public static void LeaveTextBoxLoadText()
        {
            string exmpleText = "Username";
            TextBox txt = new TextBox();
            txt.Text = "";
            Cipher.View.LeaveTextBox(txt, exmpleText, false);
            Assert.AreEqual(exmpleText, txt.Text);
        }
        [TestCase]
        public static void OpenChildFormActiveIsDisposed()
        {
            Form active = new Form();
            Panel panel = new Panel();
            Form form = new Form();
            Cipher.View.openChildForm(form, panel, active);
            Assert.IsTrue(active.IsDisposed);
        }
        [TestCase]
        public static void OpenChildFormDockIsFill()
        {
            Form active = new Form();
            Panel panel = new Panel();
            Form form = new Form();
            Cipher.View.openChildForm(form, panel, active);
            Assert.IsTrue(form.Dock == DockStyle.Fill);
        }
        [TestCase]
        public static void VisualiseButtonClickIndicatorChange()
        {
            Button btn = new Button();
            PictureBox ind = new PictureBox();
            ind.Visible = false;
            Panel field = new Panel();
            field.Controls.Add(btn);
            field.Controls.Add(ind);
            Cipher.View.VisualiseButtonClick(btn,ind,field);
            Assert.IsTrue(ind.Visible);
        }
        [TestCase]
        public static void NewFieldIsDisplayed()
        {
            Panel panel = new Panel();
            string title = "Test";
            string information = "Test";
            Cipher.View.DisplayField(panel,title,information);
            Assert.IsFalse(panel.Controls.Count==0);
        }
        [TestCase]
        public static void NewFieldTitleIsCorrect()
        {
            Panel panel = new Panel();
            string title = "Test";
            string information = "Test";
            Cipher.View.DisplayField(panel, title, information);
            Assert.IsTrue(panel.Controls[0].Controls[2].Text == "Test");
        }
        [TestCase]
        public static void NewAdditionIsDisplayed()
        {
            Panel panel = new Panel();
            string title = "Test";
            string information = "Test";
            Cipher.View.DisplayAdditions(panel, title, information,false);
            Assert.IsFalse(panel.Controls.Count==0);
        }
        [TestCase]
        public static void NewAdditionTitleIsCorrect()
        {
            Panel panel = new Panel();
            string title = "Test";
            string information = "Test";
            Cipher.View.DisplayAdditions(panel, title, information, false);
            Assert.IsTrue(panel.Controls[0].Controls[2].Text == "Test");
        }
        [TestCase]
        public static void FieldIsAdded()
        {
            Panel panel = new Panel();
            Cipher.View.AddNewField(panel);
            Assert.IsFalse(panel.Controls.Count == 0);
        }
        [TestCase]
        public static void AdditionIsAdded()
        {
            Panel panel = new Panel();
            Cipher.View.AddNewAddition(panel);
            Assert.IsFalse(panel.Controls.Count == 0);
        }
        [TestCase]
        public static void NewEntryIsDisplayed()
        {
            Panel panel = new Panel();
            string title = "Test";
            string information = "Test";
            Cipher.View.DisplayEntry(panel, title, information);
            Assert.IsFalse(panel.Controls.Count==0);
        }
        [TestCase]
        public static void NewEntryInfoIsCorrect()
        {
            Panel panel = new Panel();
            string title = "Test";
            string information = "Test";
            Cipher.View.DisplayEntry(panel, title, information);
            Assert.IsTrue(panel.Controls[0].Controls[0].Text == "Test");
        }
        [TestCase]
        public static void EntrySearchFindsResult()
        {
            Panel panel = new Panel();
            Panel p1 = new Panel();
            Panel p2 = new Panel();
            panel.Controls.Add(p1);
            panel.Controls.Add(p2);
            TextBox tb0 = new TextBox();
            TextBox tb1 = new TextBox();
            tb1.Text = "Test1";
            p1.Controls.Add(tb0);
            p1.Controls.Add(tb1);
           
            TextBox tb2 = new TextBox();
            TextBox tb00 = new TextBox();
            tb2.Text = "Test2";
            p2.Controls.Add(tb00);
            p2.Controls.Add(tb2);
            
            string text = "test1";
            Cipher.View.SearchEntry(panel, text);
            Assert.IsTrue(p1.BackColor == Color.FromArgb(127, 214, 45));
        }
        [TestCase]
        public static void EvaluationIsDisplayedCorrectly()
        {
            string password = "123456";
            Panel panel = new Panel();
            Label scoreTxt = new Label();
            Cipher.View.DisplayEvaluation(password,panel,scoreTxt);
            Assert.IsTrue(panel.Width==0);
        }
        [TestCase]
        public static void GeneratedPasswordIsDisplayed()
        {
            string forbidden = "*&^#@'>`~";
            int length = 10;
            TextBox textBox = new TextBox();
            Cipher.View.DisplayGeneratedPassword(forbidden,length,textBox);
            Assert.IsTrue(textBox.Text!="");
        }
        [TestCase]
        public static void GeneratedPasswordIsValid()
        {
            string forbidden = "*&^#@'>`~";
            int length = 10;
            TextBox textBox = new TextBox();
            Cipher.View.DisplayGeneratedPassword(forbidden, length, textBox);
            Assert.IsFalse(textBox.Text.Contains('@') || textBox.Text.Contains('~'));
        }
        [TestCase]
        public static void SequrityInfosAreDisplayed()
        {
            Panel panel = new Panel();
            string title = "Test";
            int evaluation = 20;
            Cipher.View.DisplaySequrityInfos(panel,title,evaluation);
            Assert.IsTrue(panel.Controls.Count>0);
        }
        [TestCase]
        public static void SequrityInfosAreDisplayedCorrectly()
        {
            Panel panel = new Panel();
            string title = "Test";
            int evaluation = 20;
            Cipher.View.DisplaySequrityInfos(panel, title, evaluation);
            Assert.IsTrue(panel.Controls[0].Controls[0].Text== "| Strength: 20/100");
        }
        [TestCase]
        public static void NewFormLocationIsCorrect()
        {
            Form current = new Form();
            Form newForm = new Form();
            Cipher.View.Transition(current,newForm);
            Assert.AreEqual(current.Location, newForm.Location);
        }
        [TestCase]
        public static void NewFormIsDisplayed()
        {
            Form current = new Form();
            Form newForm = new Form();
            Cipher.View.Transition(current, newForm);
            Assert.IsTrue(newForm.Visible);
        }
        [TestCase]
        public static void CardAdditionsAreDisplayed()
        {
            Panel panel = new Panel();
            string title = "Test";
            string information = "Test";
            for(int i =1;i<=5;i++)
            {
                Cipher.View.DisplayCardAdditions(panel, title, information);
            }
            Assert.IsTrue(panel.Controls.Count == 5);
        }

    }
}
