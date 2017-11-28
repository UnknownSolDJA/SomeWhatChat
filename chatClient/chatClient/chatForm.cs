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
using System.IO;
namespace chatClient
{
    public partial class chatForm : Form
    {
        private delegate void printer(string data);
        private delegate void UserPrinter(string data);
        private delegate void cleaner();
        private delegate void needThread();
        UserPrinter UPrint;
        printer Printer;
        cleaner Cleaner;
        needThread nT;
        BackgroundWorker bw = new BackgroundWorker();
        Random Rand = new Random();
        private Socket ServerSocket;
        private Thread UserThread;
        private const string Host = "localhost";
        private const int Port = 8888;
        private string username;
        private const string commandLineStart = "#!Microsoft Windows [version 6.1.7061] \n <c> Microsoft Corporation, 2009. All rights reserved \n \n C:\\Windows\\system32\n\n";
        private const string welcomemsg = "Welcome to the One and Half Chat \n";
        public chatForm()
        {
            InitializeComponent();
            Printer = new printer(print);
            Cleaner = new cleaner(clearChat);
            UPrint = new UserPrinter(UpdateOnlineList);
            bw.DoWork += new DoWorkEventHandler(bw_setName);

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
                if (data.Contains("#removeuser&") || data.Contains("#adduser&") || data.Contains("#userslist&"))
                {
                    UpdateOnlineList(data);
                    continue;
                }
                if (data.Contains("#welcomemsg&") && !data.Contains("#mcwmsg&"))
                {
                    string msg = data.Split('&')[1];
                    addOnChat(msg);
                    continue;
                }
                if (data.Contains("#welcomemsg&") && data.Contains("#mcwmsg&"))
                {
                    string msg = data.Split('&')[2];
                    addOnChat(commandLineStart);
                    addOnChat(msg);
                    continue;
                }
                if (data.Contains("!!!chaturgentclean&"))
                {
                    clearChat();
                    addOnChat(commandLineStart);
                    continue;
                }
                if (data.Contains("#messageadd&!err"))
                {
                    string msg = data.Split('&')[1];
                    addOnChat(msg);
                    continue;
                }
                if (data.Contains("#messageadd&"))
                {
                    string msg = data.Split('&')[1];
                    addOnChat(msg);
                    continue;
                }
                if (data.Contains("#mcwmsg&"))
                {
                    addOnChat(commandLineStart); 
                    continue;
                }
                if (data.Contains("#crrctpsswd&"))
                {
                    bw.RunWorkerAsync(setName);
                    continue;
                }
                if (data.Contains("#incrrctpsswd&"))
                {
                    addOnChat("Incorrect password, try again");
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
            }
            catch { print("Server is unavailable \n"); }
        }
        void bw_setName(object sender, DoWorkEventArgs e)
        {
            nT = new needThread(setNameFunction);
            userPassword.Invoke(nT);
            enterChat.Invoke(nT);
            setName.Invoke(nT);
            userPassword.Visible = false;
            userPassword.Enabled = false;
            enterChat.Visible = false;
            enterChat.Enabled = false;
            setName.Visible = true;
            setName.Enabled = true;
            username = setName.Text;
            //добавить проверку на корректность введенных символов, например кириллица, спец символы и тд
            
        }
        private void addOnChat(string msg)
        {
            if(msg.Contains("#!Microsoft"))
            {
                print(String.Format("{0}", msg.Split('!')[1]));
                return;
            }
            if (!msg.Contains('~'))
            {
                print(msg);
            }
            else print(String.Format("[{0}]:{1}", msg.Split('~')[0], msg.Split('~')[1]) + "\n");

        }
        private void clearChat()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(Cleaner);
                return;
            }
            chatBox.Clear();
            //addOnChat(commandLineStart);
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
                    print(String.Format("[{0}]:{1}", messages[i].Split('~')[0], messages[i].Split('~')[1]));
                }
                catch { continue; }
            }
        }
        private void UpdateOnlineList(string data)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(UPrint, data);
                return;
            }
            string userName = "";
            if (data.Contains('&'))
            {
                userName = data.Split('&')[1];
            }
            switch(data.Split('&')[0])
            {
                case "#removeuser":
                    if (this.InvokeRequired)
                    {
                        this.Invoke(UPrint, userName);
                        return;
                    }
                    string[] allusers = usersOnline.Text.Split('\n');
                    usersOnline.Clear();
                    for (int i = 1; i < allusers.Length; i++)
                    {
                        if (allusers[i] == userName)
                            continue;
                        else
                            usersOnline.AppendText(allusers[i]);
                    }
                    break;
                case "#adduser":
                    usersOnline.AppendText("\n" + userName);
                    break;
                case "#userslist":
                    usersOnline.Clear();
                    string clearlist = data.Split('&')[1];
                    string[] userslist = clearlist.Split('|');
                    for (int i = 0; i < userslist.Count(); i++)
                        usersOnline.AppendText("\n" + userslist[i]);
                    break;
                default:
                    break;
            }
        }
        private void send(string data)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                int bytesSent = ServerSocket.Send(buffer);
            }
            catch 
            { 
                print("Connection lost \n");
                connect();
            }
        }
        private void print(string msg)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(Printer, msg);
                return;
            }
            if(msg.Contains(':'))
            {
                chatBox.AppendText(String.Format("{0}:{1}", msg.Split(':')[0], msg.Split(':')[1]));
            }
            else chatBox.AppendText(msg);
            chatBox.ScrollToCaret();
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
        private void userPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                string password = userPassword.Text;
                PasswordCheck(password);
                FileStream authorizedUsernames = new FileStream("E:\\Пинянский\\Task\\client\\chatClient\\authorizedUsernames.txt",
            FileMode.Open, FileAccess.ReadWrite);
                StreamReader reader = new StreamReader(authorizedUsernames);
                string buffer = reader.ReadToEnd();
                string[] AllAUN = buffer.Split('~');
                for (int i = 0; i < AllAUN.Length; i++)
                {
                    setName.Items.Add(AllAUN[i]);
                }
                reader.Close();
                authorizedUsernames.Close();
            }
        }
        private void setName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && setName.Text != null)
            {
                username = setName.Text;
                FileStream authorizedUsernames = new FileStream("E:\\Пинянский\\Task\\client\\chatClient\\authorizedUsernames.txt",
                FileMode.Open, FileAccess.ReadWrite);
                StreamReader reader = new StreamReader(authorizedUsernames);
                string buffer = reader.ReadToEnd();
                reader.Close();
                string[] AllAUN = buffer.Split('~');
                bool newEntry = false;
                for (int i = 0; i < AllAUN.Length; i++)
                {
                    if (username != AllAUN[i] && username.Length > 1)
                        newEntry = true;
                    else
                        newEntry = false;
                }
                if (newEntry == true)
                {
                    StreamWriter writer = new StreamWriter(authorizedUsernames);
                    writer.Write(username + "~");
                    writer.Close();
                }

                authorizedUsernames.Close();
                send("#setname&" + username);
                chat_msg.Enabled = true;
                chat_msg.Focus();
                setName.Visible = false;
            }
        }
        private void chat_msg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.Control && e.KeyCode == Keys.Z)
            {
                send("!!!urgentchatclean&");
                //    clearChat();
            }
            else if (e.Alt && e.Control && e.KeyCode == Keys.X)
                clearChat();
            else if (e.KeyData == Keys.Enter)
                sendMessage();

        }
        private void PasswordCheck (string pass)
        {
            send("#ntrdpsswd&" + pass);
        }
        private void setNameFunction ()
        {
            userPassword.Visible = false;
            userPassword.Enabled = false;
            enterChat.Visible = false;
            enterChat.Enabled = false;
            setName.Visible = true;
            setName.Enabled = true;
            setName.Focus();
            username = setName.Text;
            //добавить проверку на корректность введенных символов, например кириллица, спец символы и тд
            FileStream authorizedUsernames = new FileStream("E:\\Пинянский\\Task\\client\\chatClient\\authorizedUsernames.txt",
                FileMode.Open, FileAccess.ReadWrite);
            StreamReader reader = new StreamReader(authorizedUsernames);
            string buffer = reader.ReadToEnd();
            string[] AllAUN = buffer.Split('~');
            for (int i = 0; i < AllAUN.Length; i++)
            {
                if (username != AllAUN[i])
                {
                    StreamWriter writer = new StreamWriter(authorizedUsernames);
                    writer.Write(username + "~");
                    writer.Close();
                    break;
                }
            }
            reader.Close();
            authorizedUsernames.Close();
            send("#setname&" + username);
        }
        private void enterChat_Click(object sender, EventArgs e)
        {
            string password = userPassword.Text;
            PasswordCheck(password);
        }
    }
}
