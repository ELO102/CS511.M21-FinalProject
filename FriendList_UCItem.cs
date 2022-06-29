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
using System.Net.NetworkInformation;

namespace CS511.M21_FinalProject
{
    public partial class FriendList_UCItem : UserControl
    {
        public FriendList_UCItem(string port)
        {
            InitializeComponent(); 
            acc.LoadAccountPort(port);
            Load_UI();
            if (!checkServerStatus(port))
            {
                button1.Image = Properties.Resources.remove;
                button1.Text = "Offline";
            }
        }
        private Account_Class acc = new Account_Class();

        private void Load_UI()
        {
            label1.Text = acc.Ten;
        }
        private bool checkServerStatus(string port)
        {
            TcpClient tcpClient = new TcpClient();

            try
            {
                tcpClient.Connect("127.0.0.1", int.Parse(port));
                tcpClient.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void FriendList_UCItem_MouseHover(object sender, EventArgs e)
        {
            this.BackColor = Color.LightYellow;
        }

        private void FriendList_UCItem_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
        }
        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.FlatAppearance.MouseOverBackColor = button1.BackColor;
            button1.BackColor = Color.WhiteSmoke;
        }
    }
}
