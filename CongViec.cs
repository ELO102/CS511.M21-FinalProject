using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS511.M21_FinalProject
{
    internal class CongViec
    {
        public int ID; // STT
        public string Ten; // Ten cong viec ngan
        public DateTime NgayCapNhat; // < Now 
        public DateTime NgayDenHan; // 
        public bool IsDone; // true/false - true == Done
        public string ChiTiet; // rich text
        public int DoQuanTrong;  // 1 2 3 4 5 

        public CongViec()
        {
            ID = 0;
            Ten = "";
            NgayCapNhat = DateTime.Now;
            NgayDenHan = DateTime.Now.AddDays(2);
            IsDone = false;
            ChiTiet = "";
            DoQuanTrong = 0;
        }
        public CongViec(int id, string ten, DateTime ngaydenhan, string chitiet, int doquantrong)
        {
            ID = id;
            Ten = ten;
            NgayCapNhat = DateTime.Now;
            NgayDenHan = ngaydenhan;
            IsDone = false;
            ChiTiet = chitiet;
            DoQuanTrong = doquantrong;
        }
        public CongViec(int id, string ten, DateTime ngaycapnhat, DateTime ngaydenhan, bool isdone, string chitiet, int doquantrong)
        {
            ID = id;
            Ten = ten;
            NgayCapNhat = ngaycapnhat;
            NgayDenHan = ngaydenhan;
            IsDone = isdone;
            ChiTiet = chitiet;
            DoQuanTrong = doquantrong;
        }
        public void Update_curNgayCapNhat()
        {
            NgayCapNhat = DateTime.Now;
        }

        public string TrangThaiCongViec()
        {
            if (IsDone)
            {
                return "Đã hoàn thành";
            }

            if (NgayDenHan < DateTime.Now)
            {
                return "Đã quá hạn";
            }
            else
            {
                return "Chưa đến hạn";
            }
        }
    }
}
