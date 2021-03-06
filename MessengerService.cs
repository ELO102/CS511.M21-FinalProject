using System.Net.Sockets;
using System.Net;
using System.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CS511.M21_FinalProject
{
    public partial class MessengerService
    {
        public MessengerService(string port)
        {
            IP_server = new IPEndPoint(IPAddress.Any, int.Parse(port));
            server_port=port;
            InitServer();
            threadServer = new Thread(AcceptConnect_and_ReceiveMsg);
            threadServer.IsBackground = true;
            threadServer.Start();
        }

        private IPEndPoint IP_server;
        private TcpClient client;
        private TcpListener server;
        private Thread threadServer;
        private string server_port;

        public Dictionary<string, List<string>> MessageOfTarget = new Dictionary<string, List<string>>();

        private void InitServer()
        {
            server = new TcpListener(IP_server);
            server.Start();
        }
        private void AcceptConnect_and_ReceiveMsg()
        {
            try
            {
                while (true)
                {
                    TcpClient client_come = server.AcceptTcpClient();
                    NetworkStream ns = client_come.GetStream();

                    byte[] msg = new byte[client_come.ReceiveBufferSize];
                    int bytesRead = ns.Read(msg, 0, client_come.ReceiveBufferSize);

                    string message = Encoding.UTF8.GetString(msg, 0 , bytesRead);

                    if (message != "")
                    {
                        // Get port 
                        string port_come = message.Split('|')[3];
                        // Handler unknown Client connect
                        if (!MessageOfTarget.ContainsKey(port_come))
                            MessageOfTarget.Add(port_come, new List<string>());

                        MessageOfTarget[port_come].Add(message);
                    }
                }
            }
            catch
            {
                InitServer();
            }
        }

        public void Send(string msg, string port, string Ten, string FontName, string FontSize, Color color)
        {
            if (string.IsNullOrEmpty(msg)) return;

            // FontName - FontSize - color - port - Ten - time > msg
            msg = $"{FontName}|{FontSize}|{color.ToArgb()}|{server_port}|User:{Ten}|{DateTime.Now.ToString("HH:mm:ss")} > " + msg;

            try
            {
                client = new TcpClient();
                IPEndPoint target_ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"), int.Parse(port));
                client.Connect(target_ip);
                NetworkStream ns = client.GetStream();
                
                byte[] buffer = Encoding.UTF8.GetBytes(msg);
                ns.Write(buffer, 0, buffer.Length);

                ns.Close();
                client.Close();

                if (!MessageOfTarget.ContainsKey(port))
                    MessageOfTarget.Add(port, new List<string>());
                MessageOfTarget[port].Add(msg);
            }
            catch
            {
                if (!MessageOfTarget.ContainsKey(port))
                    MessageOfTarget.Add(port, new List<string>());
                MessageOfTarget[port].Add("Offline");
            }
           
        }
        public Tuple<string, Font, Color, string> MsgSplitter(string data)
        {
            // FontName - FontSize - color - title - msg
            string[] splited_msg = data.Split('|');

            Font font_msg = new Font(splited_msg[0], float.Parse(splited_msg[1]));
            Color font_color = Color.FromArgb(int.Parse(splited_msg[2]));

            string title = "";
            for (int i = 3; i < 5; i++)
            {
                title += splited_msg[i] + "|";
            }
            title += splited_msg[5].Split('>')[0] + ">";

            string msg = splited_msg[5].Split('>')[1];

            return new Tuple<string, Font, Color, string> (title, font_msg, font_color, msg);
        }
        public void ServerClose()
        {
            server.Stop();
            threadServer.Abort();
        }

        //private byte[] Serialize(string text)
        //{
        //    MemoryStream ms = new MemoryStream();
        //    BinaryFormatter bf = new BinaryFormatter();
        //
        //    bf.Serialize(ms, text);
        //
        //    return ms.ToArray();
        //}

        //private object Deserialize(byte[] data)
        //{
        //    MemoryStream ms = new MemoryStream(data);
        //    BinaryFormatter bf = new BinaryFormatter();
        //
        //    return bf.Deserialize(ms);
        //}
    }
}