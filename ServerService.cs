using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace CS511.M21_FinalProject
{
    public partial class ServerService
    {
        public ServerService()
        {

        }

        IPEndPoint IP;
        Socket client;
        Account_Class acc;
        void Connect()
        {
            IP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), Int32.Parse(acc.port));
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            client.Connect(IP);
        }
    }
}