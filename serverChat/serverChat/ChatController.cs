using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace serverChat
{
    public static class ChatController
    {
        private const int _maxMessage = 50;
        public static List<message> Chat = new List<message>();
        public struct message
        {
            public string userName;
            public string data;
            public message(string name, string msg)
            {
                userName = name;
                data = msg;
            }
        }
        public static void SendMessage (string userName, string msg)
        {
            try
            {
                for( int i = 0; i < Server.Clients.Count; i++)
                {
                    Server.Clients[i].Broadcast("#messageadd&" + userName + "~" + msg);
                }
            }
            catch (Exception ex) { Console.WriteLine("Error with sending: {0}.", ex.Message); }
        }
        public static void updateOnlineList (string username)
        {
            try
            {
                string usersList = "";
                for (int i = 0; i < Server.Clients.Count; i++)
                    usersList += Server.Clients[i].UserName + "|";
                for (int i = 0; i < Server.Clients.Count; i++)
                    Server.Clients[i].Broadcast("#userslist&" + usersList);

            }
            catch (Exception ex) { Console.WriteLine("Error with updating clientlist: {0}.", ex.Message); }
        }
        public static void ClearChat()
        {
            Chat.Clear();
            Server.UpdateAllChats();
        }
        public static void ClearAllChats()
        {
            try
            {
                for (int i = 0; i < Server.Clients.Count; i++)
                    Server.Clients[i].Broadcast("!!!chaturgentclean&");
            }
            catch (Exception ex) { Console.WriteLine("Error with sending: {0}.", ex.Message); }
        }
        public static bool CheckingPassword(string currentPassword)
        {
            FileStream passbase = new FileStream("E:\\Пинянский\\Task\\server\\serverChat\\passbase.txt",
                FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(passbase);
            string buffer = reader.ReadToEnd();
            string[] allpswds = buffer.Split('~');
            for( int i = 0; i < allpswds.Length; i++)
            {
                if (currentPassword == allpswds[i])
                    return true;
            }
            return false;
        }
        public static string GetChat()
        {
            try
            {
                string data = "#updatechat&";
                int countMessages = Chat.Count;
                if (countMessages <= 0) return string.Empty;
                for (int i = 0; i < countMessages; i++)
                {
                    data += String.Format("{0}~{1}|", Chat[i].userName, Chat[i].data);
                }
                return data;
            }
            catch (Exception exp) { Console.WriteLine("Error with getting Chat information: {0}", exp.Message); return string.Empty; }
        }
    }
}
