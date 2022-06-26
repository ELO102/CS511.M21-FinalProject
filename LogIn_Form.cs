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
    public partial class LogIn_Form : Form
    {
        public LogIn_Form()
        {
            InitializeComponent();
        }

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
            var register_Form = new Register_Form();
            register_Form.Show();
        }
    }
}
