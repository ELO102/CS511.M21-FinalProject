using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace CS511.M21_FinalProject
{
    internal class Account_Class
    {
        public Account_Class()
        {
            Ten = "";
            TK = "";
            MK = "";
            port = "1000";
        }

        public string Ten;
        public string TK;
        public string MK;
        public string port;

        public void CreateAccountJSON()
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            string Path_save = @"E:\Study\CS511.M21\CS511.M21-FinalProject\Data\ClientJSON\" + this.port + ".json";

            if (!File.Exists(Path_save)) File.Create(Path_save).Dispose();

            using (StreamWriter sw = new StreamWriter(Path_save))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, this);
            }

            return;
        }

        public void LoadAccountJSON(string path)
        {
            Account_Class new_acc = new Account_Class();
            string JsonString = File.ReadAllText(path);

            new_acc = JsonConvert.DeserializeObject<Account_Class>(JsonString);
            this.Ten = new_acc.Ten;
            this.TK = new_acc.TK;
            this.MK = new_acc.MK;
            this.port = new_acc.port;

            return;
        }

        public void LoadAccountPort(string port_)
        {
            string Path_load = @"E:\Study\CS511.M21\CS511.M21-FinalProject\Data\ClientJSON\" + port_ + ".json";
            LoadAccountJSON(Path_load);

            return;
        }
    }
}
