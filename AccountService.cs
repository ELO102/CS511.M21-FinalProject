using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS511.M21_FinalProject
{
    public partial class AccountService
    {
        public AccountService()
        {
            Load_Port_List();
            Load_TKMK_List();
        }

        List<int> list_port = new List<int> { };
        Dictionary<string, string> map_TK_MK = new Dictionary<string, string>();

        public void Load_Port_List() { }
        public void Save_Port_List() { }
        public void Load_TKMK_List() { }
        public void Save_TKMK_List() { }

        public void CreateAccount(string Ten, string TK, string MK)
        {
            Account_Class new_acc = new Account_Class();
            new_acc.Ten = Ten;
            new_acc.TK = TK;
            new_acc.MK = MK;
            int new_port = list_port.Max() + 1;
            new_acc.port = new_port.ToString("0000");

            list_port.Add(new_port);
            Save_Port_List();

            map_TK_MK.Add(TK, MK);
            Save_TKMK_List();

            new_acc.CreateAccountJSON();
        }
        public void LogInAccount(string TK, string MK)
        {
            try {
                string real_MK = map_TK_MK[TK];
                if (real_MK == MK)
                {
                    // LogIn - Load json to get port
                    // Connect
                    // Show ListFriend_Form
                }
                else
                {
                    // Show "Tài khoản hoặc mật khẩu sai"
                }
            }
            catch {
                // Show "Tài khoản hoặc mật khẩu sai"
            }
        }


    }
}