using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
namespace chatClient
{
    public partial class chatForm : Form
    {
        private delegate void printer(string data);
        private delegate void cleaner();
        printer Printer;
        cleaner Cleaner;
        Random Rand = new Random();
        private Socket ServerSocket;
        private Thread UserThread;
        private const string Host = "localhost";
        private const int Port = 8888;
        private PasswordInsert PassForm;
        private string username;
        public chatForm()
        {
            InitializeComponent();
            Printer = new printer(print);
            Cleaner = new cleaner(clearChat);
            connect();
            UserThread = new Thread(listner);
            UserThread.IsBackground = true;
            UserThread.Start();
        }
        private void listner()
        {
            while (ServerSocket.Connected)
            {
                byte[] buffer = new byte[8196];
                int bytesRec = ServerSocket.Receive(buffer);
                string data = Encoding.UTF8.GetString(buffer, 0, bytesRec);
                if (data.Contains("#updatechat"))
                {
                    UpdateChat(data);
                    continue;
                }
            }
        }
        private void connect()
        {
            try
            {
                IPHostEntry ipHost = Dns.GetHostEntry(Host);
                IPAddress ipAddress = ipHost.AddressList[0];
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, Port);
                ServerSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                ServerSocket.Connect(ipEndPoint);
                username = PassForm.textBox1.Text;
            }
            catch { print("Server is unavailable"); }
        }
        private void clearChat()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(Cleaner);
                return;
            }
            chatBox.Clear();
        }
        private void UpdateChat(string data)
        {
            //#updatechat&userName~data|username~data
            clearChat();
            string[] messages = data.Split('&')[1].Split('|');
            int countMessages = messages.Length;
            if (countMessages <= 0) return;
            for (int i = 0; i < countMessages; i++)
            {
                try
                {
                    if (string.IsNullOrEmpty(messages[i])) continue;
                    print(String.Format("[{0}]:{1}.", messages[i].Split('~')[0], messages[i].Split('~')[1]));
                    
                }
                catch { continue; }
            }
        }
        private void send(string data)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                int bytesSent = ServerSocket.Send(buffer);
            }
            catch { print("Connection lost");}
        }
        private void print(string msg)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(Printer, msg);
                return;
            }
            string CurrentUser = "";
            bool Append = false;
            if (chatBox.Text.Length == 0)
            {
                chatBox.AppendText(msg);
                string[] Usernames = richTextBox1.Text.Split('\n');
                if (Usernames.Length == 1 && msg.Contains(':'))
                {
                    CurrentUser = msg.Split(':')[0];
                    CurrentUser = CurrentUser.Split('[')[1];
                    CurrentUser = CurrentUser.Split(']')[0];
                    richTextBox1.AppendText("\n" + CurrentUser);
                }
            }
            else
            {
                chatBox.AppendText(Environment.NewLine + msg);
                if (msg != "Server is unavailable")
                {
                    string[] Usernames = richTextBox1.Text.Split('\n');
                    CurrentUser = msg.Split(':')[0];
                    CurrentUser = CurrentUser.Split('[')[1];
                    CurrentUser = CurrentUser.Split(']')[0];
                    for (int i = 1; i < Usernames.Length; i++)
                    {
                        if (CurrentUser != Usernames[i].Split(']')[0])
                        {
                            Append = true;
                        }
                        if (CurrentUser == Usernames[i].Split(']')[0])
                        {
                            Append = false;
                            break;
                        }

                    }
                    if (Append == true)
                    {
                        richTextBox1.AppendText("\n" + CurrentUser);
                        Append = false;
                    }
                }
            }
        }
        private void chat_send_Click(object sender, EventArgs e)
        {
            sendMessage();
        }
        private void sendMessage()
        {
            try
            {
                string data = chat_msg.Text;
                if (string.IsNullOrEmpty(data)) return;
                send("#newmsg&" + data);
                chat_msg.Text = string.Empty;
            }
            catch { MessageBox.Show("Sending failure"); }
        }
        private void chatBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                sendMessage();
        }
        private void chat_msg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                sendMessage();
        }

        private void enterChat_Click(object sender, EventArgs e)
        {
            if(userPassword.Text == "1")
            {
                int randUser = Rand.Next(0, 100);
                string UserName = "unknown" + randUser;
                if (string.IsNullOrEmpty(UserName)) return;
                send("#setname&" + UserName);
                chatBox.Enabled = true;
                chat_msg.Enabled = true;
                chat_send.Enabled = true;
                enterChat.Enabled = false;
                enterChat.Visible = false;
                userPassword.Enabled = false;
                userPassword.Visible = false;
            }
        }
    }
}
