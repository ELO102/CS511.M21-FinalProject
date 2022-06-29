using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace CS511.M21_FinalProject
{
    public partial class LogIn_Form : Form
    {
        public LogIn_Form()
        {
            InitializeComponent();
        }

        AccountService accountService = new AccountService();
        string endl = Environment.NewLine;
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.PasswordChar == '*')
            {
                button1.Image = Properties.Resources.visible;
                textBox3.PasswordChar = '\0';
            }
            else
            {
                button1.Image = Properties.Resources.unvisible;
                textBox3.PasswordChar = '*';
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register_Form register_Form = new Register_Form();
            register_Form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Reload if s.o registed
            accountService.Load_Port_List();
            accountService.Load_TKMK_List();

            label6.Text = "";
            bool error = false;

            if (string.IsNullOrEmpty(textBox2.Text))
            {
                label6.Text += "Vui lòng nhập Tên tài khoản" + endl;
                error = true;
                if (string.IsNullOrEmpty(textBox3.Text))
                {
                    label6.Text += "Vui lòng nhập Mật khẩu" + endl;
                    error = true;
                }
            }
            else if (!accountService.map_TK_MK.ContainsKey(textBox2.Text))
            {
                label6.Text += "Tài khoản không tồn tại" + endl;
                error = true;
            }
            else if (accountService.map_TK_MK[textBox2.Text].Item1 != textBox3.Text)
            {
                label6.Text += "Sai mật khẩu" + endl;
                error = true;
            }

            if (error) return;

            try
            {
                FriendList_Form friendList_Form = new FriendList_Form(accountService.GetPort_TK(textBox2.Text));
                friendList_Form.Show();
            }
            catch
            {
                label6.Text += "Tài khoản đang được đăng nhập";
                return;
            }

            this.Hide();
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                textBox3.Text = textBox3.Text.Split('\n')[0];
                button3_Click(sender, new EventArgs() );
            }
        }
    }
}
