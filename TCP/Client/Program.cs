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

namespace Client
{
    class Program
    {
        public static string Username;
        static void Main(string[] args)
        {
            Console.Write("Bitte geben sie eine Username an: ");
            Username = Console.ReadLine();
            TcpClient client = new TcpClient();
            client.Connect(IPAddress.Parse("127.0.0.1"), 3543);
            if(client.Connected)
            {
                Thread Reader = new Thread(() => { ReadServerCommands(client); });
                Reader.Start();

                StreamWriter writer = new StreamWriter(client.GetStream());
                SendConnectMessage(writer);

                while (true)
                {
                    if (client.Connected)
                    {
                        string eingabe = Console.ReadLine();
                        if (eingabe.Equals("exit"))
                        {
                            client.Close();
                            break;
                        }
                        else
                        {
                            string messasge = String.Format("{0}: {1}", Username, eingabe);
                            JsonClasses.CMD cmd = new JsonClasses.CMD();
                            cmd.TypeName = "clientmessage";
                            JsonClasses.CMD_SendMessage jmessage = new JsonClasses.CMD_SendMessage();
                            jmessage.Message = messasge;
                            cmd.Command = JsonConvert.SerializeObject(jmessage);
                            string SendString = JsonConvert.SerializeObject(cmd);

                            writer.WriteLine(SendString);
                            writer.Flush();

                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Der Server konnte nicht erreicht werden!");

            }
            Console.Read();
        }

        static void ReadServerCommands(TcpClient client)
        {
            StreamReader reader = new StreamReader(client.GetStream());
            while (true)
            {
                try
                {
                    if (client.Connected)
                    {
                        string GetRead = reader.ReadLine();
                        HandelCommand(client, GetRead);
                    }
                    else
                    {
                        Console.WriteLine("Connection Lost: {0}", client.Client.RemoteEndPoint.ToString());
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString()) ;
                }
            }
        }

        static void HandelCommand(TcpClient client, string Command)
        {
            try
            {
                JsonClasses.CMD cmd_command = JsonConvert.DeserializeObject<JsonClasses.CMD>(Command);
                if (cmd_command.TypeName.Equals("servermessage"))
                {
                    JsonClasses.CMD_SendMessage cmd = JsonConvert.DeserializeObject<JsonClasses.CMD_SendMessage>(cmd_command.Command);
                    Console.WriteLine(cmd.Message);
                }
            }
            catch { }
        }

        static void SendConnectMessage(StreamWriter writer)
        {
            JsonClasses.CMD cmd = new JsonClasses.CMD();
            cmd.TypeName = "connect";
            JsonClasses.CMD_Connected jmessage = new JsonClasses.CMD_Connected();
            jmessage.Name = Username;
            cmd.Command = JsonConvert.SerializeObject(jmessage);
            string SendString = JsonConvert.SerializeObject(cmd);
            writer.WriteLine(SendString);
            writer.Flush();
        }
    }
}
