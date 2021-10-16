using System;
using CSL.Sockets;

namespace PandaBear_Client
{
    class Program
    {
        private static string[] argumentParser(string[] args)
        {
            if(args.ToString().Contains("target") && args.ToString().Contains("port") && args.ToString().Contains("data") && args.ToString().Contains("name"))
            {
                foreach (string val in args)
                {
                    if (val.Contains("target:"))
                    {
                        args[0] = val;
                    }
                    if (val.Contains("port:"))
                    {
                        args[1] = val;
                    }
                    if (val.Contains("data:"))
                    {
                        args[2] = val;
                    }
                    if (val.Contains("name:"))
                    {
                        args[3] = val;
                    }
                }

                return args;
            }
            if (args.ToString().Contains("target") && args.ToString().Contains("port") && args.ToString().Contains("data"))
            {
                foreach (string val in args)
                {
                    if (val.Contains("target:"))
                    {
                        args[0] = val;
                    }
                    if (val.Contains("port:"))
                    {
                        args[1] = val;
                    }
                    if (val.Contains("data:"))
                    {
                        args[2] = val;
                    }
                }
                return args;
            }
            if (args.ToString().Contains("target") && args.ToString().Contains("port"))
            {
                foreach (string val in args)
                {
                    if (val.Contains("target:"))
                    {
                        args[0] = val;
                    }
                    if (val.Contains("port:"))
                    {
                        args[1] = val;
                    }

                }
                return args;
            }
            if (args.ToString().Contains("target"))
            {
                foreach (string val in args)
                {
                    if (val.Contains("target:"))
                    {
                        args[0] = val;
                    }
                }
                return args;
            }
            return null;
        }

        static void Main(string[] args)
        {
            string[] toset = argumentParser(args);

            try
            {
                ServerInfo info = new ServerInfo(toset[0], toset[1], toset[2], toset[3]);

                Clients.shellClient(info);
            } catch(Exception ex)
            {
                Console.WriteLine("Connection failed!");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
