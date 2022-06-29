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
    public partial class MessageBox_UCItem : UserControl
    {
        public MessageBox_UCItem(string title, Font msgFont, Color color, string msg)
        {
            InitializeComponent();
            label1.Text = title;
            label2.Font = msgFont;
            label2.ForeColor = color;
            label2.Text = msg;
        }
    }
}
