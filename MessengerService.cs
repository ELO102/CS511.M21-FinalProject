using System.Net.Sockets;
using System.Net;
using System.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CS511.M21_FinalProject
{
    public partial class MessengerService
    {
        public MessengerService(string port)
        {
            IP_client = new IPEndPoint(IPAddress.Any, int.Parse(port));
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            client.Bind(IP_client);

            Thread Connect = AcceptConnectThread();
            Connect.IsBackground = true;
            Connect.Start();
        }

        private IPEndPoint IP_client;
        private Socket client;
        private List<Socket> ListTarget = new List<Socket>();
        private Dictionary<string, List<string>> MessageOfTarget = new Dictionary<string, List<string>>();

        public Thread AcceptConnectThread()
        {
            return new Thread(() =>
            {
                client.Listen(1000);
                Socket target = client.Accept();
                string target_port = GetPort_Socket(target);

                ListTarget.Add(target);
                MessageOfTarget.Add(target_port, new List<string>());

                Thread receiveThread = new Thread(Receive);
                receiveThread.IsBackground = true;
                receiveThread.Start(target);
            });
        }

        public void Send(string text, string port)
        {
            if (!string.IsNullOrEmpty(text))
            {
                IPEndPoint IP_target = new IPEndPoint(IPAddress.Parse("127.0.0.1"), int.Parse(port));
                try
                {
                    client.Connect(IP_target);

                    client.Send(Serialize(text));
                    MessageOfTarget[port].Add(text);

                    client.Disconnect(true);
                }
                catch { }
            }
        }
        public void Receive(object obj)
        {
            Socket target = obj as Socket;
            while (true)
            {
                byte[] data = new byte[1024 * 5000];
                target.Receive(data);

                string message = (string)Deserialize(data);

                string target_port = GetPort_Socket(target);
                MessageOfTarget[target_port].Add(message);
            }
        }

        private byte[] Serialize(string text)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(ms, text);

            return ms.ToArray();
        }
        private object Deserialize(byte[] data)
        {
            MemoryStream ms = new MemoryStream(data);
            BinaryFormatter bf = new BinaryFormatter();

            return bf.Deserialize(ms);
        }

        private string GetPort_Socket(Socket socket)
        {
            string port = socket.LocalEndPoint.ToString().Split(':')[1];
            return int.Parse(port).ToString("0000");
        }
    }
}