using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS511.M21_FinalProject
{
    public partial class Register_Form : Form
    {
        public Register_Form()
        {
            InitializeComponent();
        }
        string endl = Environment.NewLine;
        AccountService accountService = new AccountService();
        private void button3_Click(object sender, EventArgs e)
        {
            {
                if (textBox5.PasswordChar == '*')
                {
                    button3.Image = Properties.Resources.visible;
                    textBox5.PasswordChar = '\0';
                }
                else
                {
                    button3.Image = Properties.Resources.unvisible;
                    textBox5.PasswordChar = '*';
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool error = false;
            label6.Text = "";
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                label6.Text += "Tên hiển thị không được để trống" + endl;
                error = true;
            }
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                label6.Text += "Tên tài khoản không được để trống" + endl;
                error = true;
            }
            else if (accountService.map_TK_MK.ContainsKey(textBox2.Text))
            {
                label6.Text += "Tên tài khoản đã được đăng ký" + endl;
                error = true;
            }
            if (string.IsNullOrEmpty(textBox5.Text))
            {
                label6.Text += "Mật khẩu không được để trống";
                error = true;
            }
            else if (textBox3.Text != textBox5.Text) {
                label6.Text += "Nhập lại mật khẩu không trùng khớp";
                error = true;
            }

            if (error) return;

            label6.Text = "Đăng ký thành công";
            accountService.CreateAccount(textBox1.Text, textBox2.Text, textBox5.Text);

            this.Close();
        }
    }
}
