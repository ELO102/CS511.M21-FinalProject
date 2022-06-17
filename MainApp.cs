using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CS511.M21_FinalProject
{
    public partial class MainApp : Form
    {
        public MainApp()
        {
            InitializeComponent();
            //danhsachCongViec 
            full_dt = merge_all_datasets();
        }
        string project_path = @"E:\Study\CS511.M21\CS511.M21-FinalProject\";
        DanhSachCongViec danhsachCongViec = new DanhSachCongViec();
        DataTable full_dt = new DataTable();
        DataTable lw_dt = new DataTable();

        private void init_panel_Route()
        {
            this.panel_main.Controls.Add(this.panel_RouteHome);
            this.panel_main.Controls.Add(this.panel_Route1);
            this.panel_main.Controls.Add(this.panel_Route2);
            this.panel_main.Controls.Add(this.panel_Route3);
        }
        private void Clear_child_controls(Control container)
        {
            while (container.Controls.Count > 0)
            {
                container.Controls[0].Dispose();
            }
        }
        private void CelendarShow(int month, int year)
        {
            Clear_child_controls(tableLayoutPanel_Celendar);
            int days = DateTime.DaysInMonth(year, month); // days of month
            int startDayofweek = Convert.ToInt32(new DateTime(year, month, 1).DayOfWeek.ToString("d")) + 1;
            int count = 1;
            for (int i = 0; i <= 7; i++)
            {
                for (int j = 0; j <= 6; j++)
                {
                    if (count >= startDayofweek) 
                    {
                        if (days > 0)
                        {
                            days--;
                            CelendarDay celendarDay = new CelendarDay();
                            int dday = count - startDayofweek + 1;
                            celendarDay.days(dday);
                            string curdate = new DateTime(year, month, dday).ToString("d/M/yyyy");
                            IEnumerable<DataRow> selectedRows = full_dt.AsEnumerable().Where(row => (row.Field<string>("NgayDenHan") == curdate));
                            string temp =  full_dt.Rows[0].Field<string>("NgayDenHan");
                            celendarDay.soluongCongViec(selectedRows.Count());
                            
                            string today = DateTime.Now.ToString("d/M/yyyy");
                            if (curdate == today) celendarDay.BackColor = Color.Lime;
                            tableLayoutPanel_Celendar.Controls.Add(celendarDay, j, i);
                        }
                    }
                    count++;
                }
            }
        }

        private void Show_ListView_ToDoList()
        {
            listView_ToDoList.Items.Clear();
            foreach (CongViec congviec in danhsachCongViec.listCongViec)
            {
                listView_ToDoList.Items.Add(new ListViewItem(new string[] {
                    "",
                    congviec.ID.ToString(),
                    congviec.Ten,
                    congviec.NgayCapNhat.ToString("dd/MM/yyyy"),
                    congviec.NgayDenHan.ToString("dd/MM/yyyy"),
                    congviec.DoQuanTrong.ToString(),
                    congviec.TrangThaiCongViec()
                }));
            }
            
            return;
        }
        static List<CongViec> DataTable_to_listCongViec(DataTable dt)
        {
            List<CongViec> listCongViec_dt = new List<CongViec>();
            foreach (DataRow row in dt.Rows)
            {
                CongViec congViec = new CongViec();
                congViec.ID = Convert.ToInt32(row["ID"]);
                congViec.Ten = Convert.ToString(row["Ten"]);
                congViec.NgayCapNhat = Convert.ToDateTime(row["NgayCapNhat"]);
                congViec.NgayDenHan = Convert.ToDateTime(row["NgayDenHan"]);
                congViec.IsDone = Convert.ToBoolean(row["IsDone"]);
                congViec.ChiTiet = Convert.ToString(row["ChiTiet"]);
                congViec.DoQuanTrong = Convert.ToInt32(row["DoQuanTrong"]);
                listCongViec_dt.Add(congViec);
            }
            return listCongViec_dt;
        }
        static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                string[] headers = sr.ReadLine().Split(new string[] { "," }, StringSplitOptions.None);
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(new string[] { "," }, StringSplitOptions.None);
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }

            }

            return dt;

        }
        private DataTable merge_all_datasets()
        {
            //DataTable dt = ConvertCSVtoDataTable(project_path + "T5.csv");
            //dt.Merge(ConvertCSVtoDataTable(project_path + "T6.csv"), false);
            //dt.Merge(ConvertCSVtoDataTable(project_path + "T7.csv"), false);

            DataTable dt = ConvertCSVtoDataTable(project_path + "yourdatabase.csv");
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                dr["ID"] = i.ToString();
                i++;
            }
            return dt;
        }
        private void hide_all_panel_Route()
        {
            panel_Route1.Visible = false;
            panel_Route2.Visible = false;
            panel_RouteHome.Visible = false;
        }
        private void button_Route1_Click(object sender, EventArgs e)
        {
            hide_all_panel_Route();
            panel_Route1.Visible = true;
            danhsachCongViec.listCongViec = DataTable_to_listCongViec(full_dt);
            Show_ListView_ToDoList();
        }

        private void button_Route2_Click(object sender, EventArgs e)
        {
            hide_all_panel_Route();
            panel_Route2.Visible = true;
            DateTime nowDay = DateTime.Now;
            CelendarShow(nowDay.Month,nowDay.Year);
        }

        private void button_Search_Click(object sender, EventArgs e)
        {
            listView_ToDoList.Items.Clear();
            listView_ToDoList.Items.AddRange(danhsachCongViec.listCongViec.Where(
                i => (string.IsNullOrEmpty(textBox_ID.Text) || i.ID == Convert.ToInt32(textBox_ID.Text)) &&
                     (string.IsNullOrEmpty(textBox_Ten.Text) || i.Ten.StartsWith(textBox_Ten.Text)) &&
                     (dateTimePicker_NgayCapNhat.Value == new DateTime(2022,4,1) || dateTimePicker_NgayCapNhat.Value == i.NgayCapNhat) &&
                     (dateTimePicker_NgayDenHan.Value == new DateTime(2022,4,1) || dateTimePicker_NgayDenHan.Value == i.NgayDenHan) &&
                     ((!radioButton1.Checked && !radioButton2.Checked && !radioButton3.Checked && !radioButton4.Checked && !radioButton5.Checked) ||
                     (radioButton1.Checked && i.DoQuanTrong == 1) ||
                     (radioButton2.Checked && i.DoQuanTrong == 2) ||
                     (radioButton3.Checked && i.DoQuanTrong == 3) ||
                     (radioButton4.Checked && i.DoQuanTrong == 4) ||
                     (radioButton5.Checked && i.DoQuanTrong == 5)) &&
                     (!checkBox_IsDone.Checked || i.IsDone) &&
                     (!checkBox_IsOutDate.Checked || (!i.IsDone && i.NgayDenHan < DateTime.Now))
                ).Select(congviec => new ListViewItem(new string[]
                {
                    "",
                    congviec.ID.ToString(),
                    congviec.Ten,
                    congviec.NgayCapNhat.ToString("dd/MM/yyyy"),
                    congviec.NgayDenHan.ToString("dd/MM/yyyy"),
                    congviec.DoQuanTrong.ToString(),
                    congviec.TrangThaiCongViec()
                })).ToArray());

            textBox_ID.Text = null;
            textBox_Ten.Text = null;
            dateTimePicker_NgayCapNhat.Value = new DateTime(2022, 4, 1);
            dateTimePicker_NgayDenHan.Value = new DateTime(2022, 4, 1);
            radioButton1.Checked= false;
            radioButton2.Checked= false;
            radioButton3.Checked= false;
            radioButton4.Checked= false;
            radioButton5.Checked= false;
            checkBox_IsDone.Checked= false;
            checkBox_IsOutDate.Checked= false;
        }

        private void pictureBox_Logo_Click(object sender, EventArgs e)
        {
            hide_all_panel_Route();
            panel_RouteHome.Visible=true;
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (ListViewItem eachItem in listView_ToDoList.CheckedItems)
            {
                int idx = Convert.ToInt32(eachItem.SubItems[1].Text);
                danhsachCongViec.listCongViec.RemoveAt(idx - count);
                full_dt.Rows.RemoveAt(idx - count);
                count++;
            }
            count = 0;
            foreach (CongViec cv in danhsachCongViec.listCongViec)
            {
                cv.ID= count;
                count++;
            }
            count = 0;
            foreach (DataRow dr in full_dt.Rows)
            {
                dr["ID"]= count.ToString();
                count++;
            }
            Show_ListView_ToDoList();
        }

        private void button_AddConfirm_Click(object sender, EventArgs e)
        {
            CongViec congViec = new CongViec();
            congViec.ID = danhsachCongViec.listCongViec.Count;
            congViec.Ten = textBox1.Text;
            congViec.NgayCapNhat = DateTime.Now;
            congViec.NgayDenHan = dateTimePicker1.Value;
            congViec.IsDone = false;
            congViec.ChiTiet = "";
            if (radioButton10.Checked) congViec.DoQuanTrong = 1;
            else if (radioButton9.Checked) congViec.DoQuanTrong = 2;
            else if (radioButton8.Checked) congViec.DoQuanTrong = 3;
            else if (radioButton7.Checked) congViec.DoQuanTrong = 4;
            else congViec.DoQuanTrong = 5;
            danhsachCongViec.listCongViec.Add(congViec);
            groupBox_Add.Visible = false;

            DataRow dr = full_dt.NewRow();
            dr["ID"] = congViec.ID.ToString();
            dr["Ten"] = congViec.Ten;
            dr["NgayCapNhat"] = congViec.NgayCapNhat.ToString("dd/MM/yyyy");
            dr["NgayDenHan"] = congViec.NgayDenHan.ToString("dd/MM/yyyy");
            dr["IsDone"] = congViec.IsDone.ToString();
            dr["ChiTiet"] = congViec.ChiTiet;
            dr["DoQuanTrong"] = congViec.DoQuanTrong.ToString();

            Show_ListView_ToDoList();
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            groupBox_Add.Visible = true;
        }

        private void button_Route5_Click(object sender, EventArgs e)
        {
            this.Close();
            System.Windows.Forms.Application.Exit();
        }
    }
}
