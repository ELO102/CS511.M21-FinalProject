using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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
            Load_FriendUI();
        }

        private Account_Class acc = new Account_Class();
        private AccountService accService = new AccountService();
        private MessengerService messengerService;
        private List<string> curr_MessageBox;
        private string curr_target = "";
        private Thread Check_new_Message;

        private void Load_FriendUI()
        {
            label3.Text = acc.Ten;
            button1.Visible = false;
            foreach (int port in accService.list_port)
            {
                if (port.ToString("0000") != acc.port)
                {
                    FriendList_UCItem friend_item = new FriendList_UCItem(port.ToString("0000"));
                    friend_item.Name = port.ToString("0000");

                    Panel panel = new Panel();
                    panel.Size = friend_item.Size;
                    panel.Padding = new Padding(0);
                    panel.Click += new System.EventHandler(FriendItem_Click);
                    panel.Controls.Add(friend_item);
                    foreach (Control ctl in panel.Controls)
                    {
                        ctl.Click += new System.EventHandler(FriendItem_Click);
                        foreach (Control c in ctl.Controls)
                        {
                            c.Name = port.ToString("0000");
                            c.Click += new System.EventHandler(FriendItem_Click);
                        }
                    }
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
            if (Check_new_Message != null) Check_new_Message.Abort();
            curr_target = ((Control)sender).Name;
            curr_MessageBox = messengerService.GetMessage_Port(curr_target);
            Check_new_Message = new Thread(Check_and_Update_MessageBox);
            Check_new_Message.IsBackground = true;
            Check_new_Message.Start();

            Account_Class temp_acc = new Account_Class();
            temp_acc.LoadAccountPort(curr_target);
            label1.Text = temp_acc.Ten;
            
            ShowMessageText();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Load_FriendUI();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Send_message();
        }

        public void Send_message()
        {
            string message = richTextBox2.Text;
            messengerService.Send(message, curr_target);
            ShowMessageText();
            richTextBox2.Clear();
        }
        private void Check_and_Update_MessageBox()
        {
            while (true)
            {
                if (curr_target != "")
                {
                    if (curr_MessageBox.Count() != messengerService.GetMessage_Port(curr_target).Count())
                    {
                        curr_MessageBox = messengerService.GetMessage_Port(curr_target);
                        ShowMessageText();
                    }
                }
            }
        }
        private void ShowMessageText()
        {
            richTextBox1.Lines = curr_MessageBox.ToArray();
        }
    }
}
