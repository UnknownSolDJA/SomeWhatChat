using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
namespace serverChat
{
    public static class Server
    {
        public static List<Client> Clients = new List<Client>();
        public static void NewUser(Socket handle)
        {
            try
            {
                Client newUser = new Client(handle);
                Clients.Add(newUser);
                Console.WriteLine("New client connected: {0}", handle.RemoteEndPoint);
                Client LastClient = Clients.Last();
                int IndexOfLast = Clients.IndexOf(LastClient);
                Clients[IndexOfLast].Broadcast("#mcwmsg&#welcomemsg&Welcome to the One and Half chat \n");
            }
            catch (Exception exp) { Console.WriteLine("Error with addNewClient: {0}",exp.Message); }
        }
        public static void EndConnection(Client client)
        {
            try
            {
                string name = client.UserName;
                client.End();
                Clients.Remove(client);
                Console.WriteLine("User {0} has been disconnected", client.UserName);
                if (Clients.Count() != 0)
                {
                    Client LastClient = Clients.Last();
                    int IndexOfLast = Clients.IndexOf(LastClient);
                    //List<string> allUsers = new List<string>();
                    //for (int i = 0; i <= IndexOfLast; i++)
                    //    allUsers.Add(Clients[i].UserName);
                    for (int i = 0; i <= IndexOfLast; i++)
                        Clients[i].Broadcast("#removeuser&" + name);
                }
            }
            catch (Exception exp) { Console.WriteLine("Error with ending User connection: {0}.",exp.Message); }
        }
        public static void UpdateAllChats()
        {
            try
            {
                int countUsers = Clients.Count;
                for (int i = 0; i < countUsers; i++)
                {
                    Clients[i].UpdateChat();
                }
            }
            catch (Exception exp) { Console.WriteLine("Error with updating all elements: {0}.",exp.Message); }
        }

        public static void GetError(string errMsg)
        {
            try
            {
                for (int i = 0; i < Server.Clients.Count; i++)
                {
                    Server.Clients[i].Broadcast("#messageadd&!err" + "~" + errMsg);
                }
            }
            catch (Exception ex) { Console.WriteLine("Error accured: {0}.", ex.Message); }
        }
        
    }
}
