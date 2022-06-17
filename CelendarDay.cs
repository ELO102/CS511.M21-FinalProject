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
    public partial class CelendarDay : UserControl
    {
        public CelendarDay()
        {
            InitializeComponent();
        }
        public void days(int numday)
        {
            label_numDay.Text = numday.ToString();
        }
        public void soluongCongViec(int numCV)
        {
            if (numCV == 0)
            {
                BackColor = Color.White;
                return;
            }
            label1.Text = "Có " + numCV.ToString() + " công việc";
            if (numCV == 1)
            {
                BackColor = Color.LightYellow;
                return;
            }
            if ((numCV == 2) || (numCV == 3))
            {
                BackColor = Color.LemonChiffon;
                return;
            }
            BackColor = Color.Yellow;
            return;

        }
    }
}
