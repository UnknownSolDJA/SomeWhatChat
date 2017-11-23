using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace serverChat
{
    class Program
    {
        private const string Host = "localhost";
        private const int Port = 8888;
        private static Thread ServerThread;
        static void Main(string[] args)
        {
            ServerThread = new Thread(startServer);
            ServerThread.IsBackground = true;
            ServerThread.Start();
            while (true)
                handlerCommands(Console.ReadLine());
        }
        private static void handlerCommands(string cmd)
        {
            cmd = cmd.ToLower();
            string advCmd = "";
            if(cmd.Contains(' '))
            {
                advCmd = cmd.Split(' ')[1];
                cmd = cmd.Split(' ')[0];
            }
            switch(cmd)
            { 
                case "/getusers":
                    int countUsers = Server.Clients.Count;
                    for (int i = 0; i < countUsers; i++)
                    {
                        Console.WriteLine("[{0}]: {1}",i,Server.Clients[i].UserName);
                    }
                    break;
                case "/restart":
                    restartServer();
                    break;
                case "/closeall":
                    for (int i = 0; i < Server.Clients.Count; i++ )
                    { 
                        Server.EndConnection(Server.Clients[i]);
                    }
                    break;
                case "/closeuser":
                    Server.EndConnection(Server.Clients[Convert.ToInt32(advCmd)]);
                    break;
                case "/clearall":
                    ChatController.ClearChat();
                    break;
                default:
                    break;
            }
        }
        private static void startServer()
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Host);
            IPAddress ipAddress = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, Port);
            Socket socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ipEndPoint);
            socket.Listen(1000);
            Console.WriteLine("Server has been started on IP: {0}.",ipEndPoint);
            while(true)
            {
                try
                {
                    Socket user = socket.Accept();
                    Server.NewUser(user);
                }
                catch (Exception exp) { Console.WriteLine("Error: {0}",exp.Message); }
            }
        }
        private static void restartServer()
        {
            ChatController.ClearChat();
        }
    }
}
