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
    public partial class FriendList_Form : Form
    {
        public FriendList_Form(string port)
        {
            InitializeComponent();
            acc.LoadAccountPort(port);
            messengerService = new MessengerService(acc.port);
            Load_UI();
        }
        private Account_Class acc = new Account_Class();
        AccountService accService = new AccountService();
        private MessengerService messengerService;
        string curr;

        private void Load_UI()
        {
            label3.Text = acc.Ten;
            button1.Visible = false;
            foreach (int port in accService.list_port)
            {
                if (port.ToString("0000") != acc.port)
                {
                    FriendList_UCItem friend_item = new FriendList_UCItem(port.ToString("0000"));
                    Panel panel = new Panel();
                    panel.Size = friend_item.Size;
                    panel.Padding = new Padding(0);
                    panel.Click += new System.EventHandler(FriendItem_Click);
                    panel.Controls.Add(friend_item);
                    panel.Name = port.ToString("0000");
                    
                    flowLayoutPanel1.Controls.Add(panel);
                }
            }
        }
        private void FriendList_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void FriendItem_Click(object sender, EventArgs e)
        {
            curr = ((Panel)sender).Name;
            label1.Text = curr;
        }
    }
}
