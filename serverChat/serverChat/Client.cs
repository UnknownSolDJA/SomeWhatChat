using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
namespace serverChat
{
    public class Client
    {
        private string _userName;
        private Socket Hand;
        private Thread Thread;
        public Client(Socket socket)
        {
            Hand = socket;
            Thread = new Thread(listner);
            Thread.IsBackground = true;
            Thread.Start();
        }
        public string UserName
        {
            get { return _userName; }
        }
        private void listner()
        {
            while (true)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    int bytesRec = Hand.Receive(buffer);
                    string data = Encoding.UTF8.GetString(buffer, 0, bytesRec);
                    handleCommand(data);
                }
                catch { Server.EndConnection(this); return; }
            }
        }
        public void End()
        {
            try
            {
                Hand.Close();
                try
                {
                    Thread.Abort();
                }
                catch { }
            }
            catch (Exception exp) { Console.WriteLine("Error with end: {0}.",exp.Message); }
        }
        private void handleCommand(string data)
        {
            if (data.Contains("#setname"))
            {
                _userName = data.Split('&')[1];
                UpdateChat();
                return;
            }
            if (data.Contains("#newmsg"))
            {
                string message = data.Split('&')[1];
                ChatController.AddMessage(_userName,message);
                return;
            }
        }
        public void UpdateChat()
        {
            Send(ChatController.GetChat(), Server.Clients);
        }
        public void Send(string command, List<Client> Clients)
        {
            try
            {
                int bytesSent = Hand.Send(Encoding.UTF8.GetBytes(command));
                if (bytesSent > 0) Console.WriteLine("Success");
            }
            catch (Exception exp) { Console.WriteLine("Error with send command: {0}.", exp.Message); Server.EndConnection(this); }
        }
    }
}
