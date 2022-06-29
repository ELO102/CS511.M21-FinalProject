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
using System.Net.Sockets;
using System.Net;
using System.Net.NetworkInformation;

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
            Text = acc.Ten;

            CheckForIllegalCrossThreadCalls = false;
        }

        private Account_Class acc = new Account_Class();
        private AccountService accService = new AccountService();
        private MessengerService messengerService;
        private string curr_target = "";

        Thread Update_mess;

        private void Load_FriendUI()
        {
            label3.Text = acc.Ten;
            flowLayoutPanel1.Controls.Clear();
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
        public void Send_message()
        {
            string message = richTextBox2.Text;
            messengerService.Send(message, curr_target, acc.Ten);
            //ShowMessageText();
            richTextBox2.Clear();
        }

        private void FriendList_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void FriendItem_Click(object sender, EventArgs e)
        {
            pictureBox3.Visible = false;
            curr_target = ((Control)sender).Name;

            if (Update_mess != null) Update_mess.Abort();
            Update_mess = new Thread(() =>
            {
                while (true)
                {
                    if (curr_target != "")
                    {
                        if (messengerService.MessageOfTarget.ContainsKey(curr_target))
                        {
                            if (messengerService.MessageOfTarget[curr_target].Count != richTextBox1.Lines.Length)
                            {
                                richTextBox1.Lines = new string[messengerService.MessageOfTarget[curr_target].Count];
                                richTextBox1.Lines = messengerService.MessageOfTarget[curr_target].ToArray();
                            }
                        }
                        else
                        {
                            richTextBox1.Lines = new string[0];
                        }
                    }
                }
            });
            Update_mess.IsBackground = true;
            Update_mess.Start();

            Account_Class temp_acc = new Account_Class();
            temp_acc.LoadAccountPort(curr_target);
            label1.Text = temp_acc.Ten;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Load_FriendUI();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Send_message();
        }

        private void richTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter)
            {
                richTextBox2.Text = richTextBox2.Text.Split('\n')[0];
                Send_message();
                richTextBox2.Clear();
            }
        }

        private bool checkServerStatus(string port)
        {
            TcpClient tcpClient = new TcpClient();

            try
            {
                tcpClient.Connect("127.0.0.1", int.Parse(port));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.FlatAppearance.MouseOverBackColor = button1.BackColor;
            button1.BackColor = Color.WhiteSmoke;
        }
    }
}
