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
    public partial class FriendList_UCItem : UserControl
    {
        public FriendList_UCItem(string port)
        {
            InitializeComponent(); 
            acc.LoadAccountPort(port);
            Load_UI();
        }
        private Account_Class acc = new Account_Class();

        private void Load_UI()
        {
            label1.Text = acc.Ten;
            button1.Visible = false;
        }

        private void FriendList_UCItem_MouseHover(object sender, EventArgs e)
        {
            this.BackColor = Color.LightYellow;
        }

        private void FriendList_UCItem_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
        }
    }
}
