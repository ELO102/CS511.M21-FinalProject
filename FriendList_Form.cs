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
            Load_Font();
            Text = acc.Ten;

            CheckForIllegalCrossThreadCalls = false;
        }

        private Account_Class acc = new Account_Class();
        private AccountService accService = new AccountService();
        private MessengerService messengerService;
        private string curr_target = "";
        private (string, int) curr_Msg = ("", 0);
        private List<Thread> Threads = new List<Thread>();

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
            string font_name = comboBox2.SelectedItem == null ? richTextBox2.Font.Name : comboBox2.SelectedText;
            string font_size = comboBox1.SelectedItem == null ? richTextBox2.Font.Size.ToString() : comboBox1.SelectedItem.ToString();
            messengerService.Send(message, curr_target, acc.Ten, font_name, font_size, button4.BackColor);
            //ShowMessageText();
            richTextBox2.Clear();
        }

        private void FriendList_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void FriendItem_Click(object sender, EventArgs e)
        {
            //pictureBox3.Visible = false;
            richTextBox2.Enabled = true;
            button2.Enabled = true;
            curr_target = ((Control)sender).Name;

            if (Update_mess != null) Update_mess.Abort();
            Thread temp = new Thread(() =>
            {
                while (true)
                {
                    if (curr_target != "")
                    {
                        if (messengerService.MessageOfTarget.ContainsKey(curr_target))
                        {
                            if (curr_Msg.Item1 == curr_target)
                            {
                                if (curr_Msg.Item2 != messengerService.MessageOfTarget[curr_target].Count)
                                {
                                    flowLayoutPanel2.Controls.Clear();
                                    curr_Msg.Item2 = messengerService.MessageOfTarget[curr_target].Count;
                                    foreach (string msg in messengerService.MessageOfTarget[curr_target])
                                    {
                                        Tuple<string, Font, Color, string> data = messengerService.MsgSplitter(msg);
                                        
                                        if (this.InvokeRequired)
                                        {
                                            this.BeginInvoke((MethodInvoker)delegate ()
                                            {
                                                MessageBox_UCItem msgBox = new MessageBox_UCItem(data.Item1, data.Item2, data.Item3, data.Item4);
                                                flowLayoutPanel2.Controls.Add(msgBox);
                                            });
                                        }
                                        else
                                        {
                                            MessageBox_UCItem msgBox = new MessageBox_UCItem(data.Item1, data.Item2, data.Item3, data.Item4);
                                            flowLayoutPanel2.Controls.Add(msgBox);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                flowLayoutPanel2.Controls.Clear();
                                curr_Msg.Item1 = curr_target;
                                curr_Msg.Item2 = 0;
                            }
                        }
                        else
                        {
                            flowLayoutPanel2.Controls.Clear();
                        }
                    }
                }
            });
            Threads.Add(temp);
            Update_mess = temp;
            Update_mess.IsBackground = true;
            Update_mess.Start();

            Account_Class temp_acc = new Account_Class();
            temp_acc.LoadAccountPort(curr_target);
            label1.Text = temp_acc.Ten;
        }
        private void Load_Font()
        {

            comboBox2.Text = "--Select--";
            comboBox1.Text = "--Select--";
            foreach (FontFamily font in FontFamily.Families)
            {
                comboBox2.Items.Add(font.Name);
            }
            for (int i = 8; i < 21; i++)
            {
                comboBox1.Items.Add(i);
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
        /////////////////////////////////////////////////////////////////////////////////////
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
        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.FlatAppearance.MouseOverBackColor = button1.BackColor;
            button1.BackColor = Color.WhiteSmoke;
        }
        private void buttonColor_MouseHover(object sender, EventArgs e)
        {
            button4.FlatAppearance.MouseOverBackColor = button4.BackColor;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            //dlg.ShowDialog();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                button4.BackColor = dlg.Color;
                richTextBox2.ForeColor = dlg.Color;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox2.Font = new Font(richTextBox2.Font.FontFamily, float.Parse(comboBox1.SelectedItem.ToString()));
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox2.Font = new Font(comboBox2.SelectedText, richTextBox2.Font.Size);
        }
    }
}
