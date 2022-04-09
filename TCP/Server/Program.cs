using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;
using Newtonsoft.Json;

namespace Server
{
    class Program
    {
        public static TcpListener listen;
        public static Dictionary<int, TcpClient> Clients = new Dictionary<int, TcpClient>();
        static void Main(string[] args)
        {
            listen = new TcpListener(IPAddress.Parse("127.0.0.1"), 3543);
            listen.Start();
            Thread RunThread = new Thread(() => { RunServer(); });
            RunThread.Start();
            while (true)
            {
                string s = Console.ReadLine();
                if (s.Equals("exit"))
                {
                    listen.Stop();
                    Environment.Exit(0);
                }
                else if (s.Equals("test"))
                {
                    Console.WriteLine("Das ist ein Test!");
                }
            }
        }

        static void RunServer()
        {
            while (true)
            {
                TcpClient client = listen.AcceptTcpClient();
                Clients.Add(Convert.ToInt32(client.Client.RemoteEndPoint.ToString().Split(':')[1]), client);
                Console.WriteLine("Incomming connection: {0}", client.Client.RemoteEndPoint.ToString());
                Thread T_ClientHandler = new Thread(() => { ClientHandler(Convert.ToInt32(client.Client.RemoteEndPoint.ToString().Split(':')[1])); });
                T_ClientHandler.Start();
            }
        }

        static void ClientHandler(int clientID)
        {
            TcpClient client = Clients[clientID];
            StreamReader reader = new StreamReader(client.GetStream());
            while (true)
            {
                try
                {
                    if (client.Connected)
                    {
                        string GetRead = reader.ReadLine();
                        HandelCommand(clientID, GetRead);
                    }
                    else
                    {
                        Console.WriteLine("[ClientHandler IF] Connection Lost: {0}", clientID);
                        Clients.Remove(clientID);
                        break;
                    }
                }
                catch
                {
                    Console.WriteLine("[ClientHandler TRY] Connection Lost: {0}", clientID);
                    Clients.Remove(clientID);
                    break;
                }
            }
        }

        static void HandelCommand(int clientID, string Command)
        {
            TcpClient client = Clients[clientID];
            try
            {
                JsonClasses.CMD cmd_command = JsonConvert.DeserializeObject<JsonClasses.CMD>(Command);
                if (cmd_command.TypeName.Equals("connect"))
                {
                    Thread T_Command = new Thread(() => { Command_Connected(clientID, cmd_command.Command); });
                    T_Command.Start();
                }
                if (cmd_command.TypeName.Equals("clientmessage"))
                {
                    Thread T_Command = new Thread(() =>
                    {
                        JsonClasses.CMD_SendMessage message = JsonConvert.DeserializeObject<JsonClasses.CMD_SendMessage>(cmd_command.Command);
                        ServerCommand_SendMessageToAllOther(clientID,message.Message);
                    });
                    T_Command.Start();
                }
            }
            catch { }
        }

        static void Command_Connected(int clientID, string Command)
        {
            JsonClasses.CMD_Connected cmd_command = JsonConvert.DeserializeObject<JsonClasses.CMD_Connected>(Command);
            Console.WriteLine("{0} is Conneced", cmd_command.Name);
            ServerCommand_SendMessageToAll(String.Format("{0} ist den Server beigtreten!", cmd_command.Name));

        }


        static void ServerCommand_SendMessageToAll(string Message)
        {
            Console.WriteLine(Message);
            for(int i = 0;i<Clients.Keys.ToArray<int>().Length; i++)
            {
                int connecid = Clients.Keys.ToArray<int>()[i];
                if (Clients[connecid].Connected)
                {
                    StreamWriter writer = new StreamWriter(Clients[connecid].GetStream());
                    JsonClasses.CMD_SendMessage message = new JsonClasses.CMD_SendMessage();
                    message.Message = Message;

                    JsonClasses.CMD cmd = new JsonClasses.CMD();
                    cmd.TypeName = "servermessage";
                    cmd.Command = JsonConvert.SerializeObject(message);

                    writer.WriteLine(JsonConvert.SerializeObject(cmd));
                    writer.Flush();
                }
                else
                {
                    Console.WriteLine("[ServerCommand_SendMessageToAll IF] Connection Lost: {0}", connecid);
                    Clients.Remove(connecid);
                }
            }

        }

        static void ServerCommand_SendMessageToAllOther(int clientID,string Message)
        {
            Console.WriteLine(Message);
            for (int i = 0; i < Clients.Keys.ToArray<int>().Length; i++)
            {
                int connecid = Clients.Keys.ToArray<int>()[i];
                if (!(connecid == clientID))
                {
                    if (Clients[connecid].Connected)
                    {
                        StreamWriter writer = new StreamWriter(Clients[connecid].GetStream());
                        JsonClasses.CMD_SendMessage message = new JsonClasses.CMD_SendMessage();
                        message.Message = Message;

                        JsonClasses.CMD cmd = new JsonClasses.CMD();
                        cmd.TypeName = "servermessage";
                        cmd.Command = JsonConvert.SerializeObject(message);

                        writer.WriteLine(JsonConvert.SerializeObject(cmd));
                        writer.Flush();
                    }
                    else
                    {
                        Console.WriteLine("[ServerCommand_SendMessageToAll IF] Connection Lost: {0}", connecid);
                        Clients.Remove(connecid);
                    }
                }
            }

        }

    }
}
