using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace CS511.M21_FinalProject
{
    public partial class AccountService
    {
        public AccountService()
        {
            Load_Port_List();
            Load_TKMK_List();
        }

        public List<int> list_port = new List<int> { 1000 };
        public Dictionary<string, (string,string)> map_TK_MK = new Dictionary<string, (string, string)>();
        string DataPath = @"E:\Study\CS511.M21\CS511.M21-FinalProject\Data\";

        public void Load_Port_List()
        {
            List<int> temp_port = new List<int>();
            string[] data_port_list = File.ReadAllLines(DataPath + "PortList.txt");
            foreach (string line in data_port_list)
            {
                temp_port.Add(int.Parse(line));
            }
            list_port = temp_port;

            return;
        }
        public void Save_Port_List()
        {
            string[] data_port_list = new string[list_port.Count];
            for (int i = 0; i < list_port.Count; i++)
            {
                data_port_list[i] = list_port[i].ToString();
            }
            File.WriteAllLines(DataPath + "PortList.txt", data_port_list);
        }
        public void Load_TKMK_List()
        {
            Dictionary<string, (string, string)> temp_TK_MK = new Dictionary<string, (string, string)>();
            foreach (int port_ in list_port)
            {
                Account_Class temp_acc = new Account_Class();
                temp_acc.LoadAccountPort(port_.ToString("0000"));
                temp_TK_MK.Add(temp_acc.TK, (temp_acc.MK, temp_acc.port));
            }
            map_TK_MK = temp_TK_MK;

            return;
        }

        public void CreateAccount(string Ten, string TK, string MK)
        {
            Account_Class new_acc = new Account_Class();
            new_acc.Ten = Ten;
            new_acc.TK = TK;
            new_acc.MK = MK;

            int new_port = list_port.Count() == 0 ? 1000 : list_port.Max() + 1;
            new_acc.port = new_port.ToString("0000");

            list_port.Add(new_port);
            Save_Port_List();

            map_TK_MK.Add(TK, (MK, new_acc.port));

            new_acc.CreateAccountJSON();
        }
        public string GetPort_TK(string TK)
        {
            return map_TK_MK[TK].Item2;
        }


    }
}