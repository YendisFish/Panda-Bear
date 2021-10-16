using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CSL.Sockets
{
    public class Clients
    {
        public static void TCPClient(ServerInfo args)
        {
            TcpClient client = new TcpClient();

            Console.WriteLine($"Connecting to {args.target}:{args.port}");

            try
            {
                client.Connect(args.target, Convert.ToInt32(args.port));

                Console.WriteLine("Connected!");

                if (args.data != null)
                {
                    Console.WriteLine("data is: " + args.data);

                    Stream stream = client.GetStream();

                    ASCIIEncoding encoding = new ASCIIEncoding();
                    byte[] x = encoding.GetBytes(args.data);

                    Console.WriteLine("Sending data...");

                    stream.Write(x, 0, x.Length);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to connect to {args.target}:{args.port}");
            }
        }
        
        //async function to wait for server, quite buggy still
        public static async Task waitForServer(Stream stream)
        {
            byte[] x = new byte[1000];
            int y = stream.Read(x, 0, 1000);

            for(int m = 0; m < y; m++)
            {
                Console.Write(Convert.ToChar(x[m]));
            }

            Console.WriteLine("");
        }

        public static async Task shellClient(ServerInfo args)
        {
            TcpClient client = new TcpClient();

            Console.WriteLine($"Connecting to {args.target} : {args.port}");

            try
            {
                client.Connect(args.target, Convert.ToInt32(args.port));

                Console.WriteLine("Connected!");


                while(true)
                {
                    Console.Write("|:>");
                    string data = Console.ReadLine();

                    Console.WriteLine("");

                    if (data != null || data != "")
                    {
                        Console.WriteLine("data is: " + data);

                        Stream stream = client.GetStream();

                        ASCIIEncoding encoding = new ASCIIEncoding();
                        byte[] x = encoding.GetBytes(data);

                        Console.WriteLine("Sending data...");

                        stream.Write(x, 0, x.Length);

                        await waitForServer(stream);

                    }

                    Console.WriteLine("");

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to connect to {args.target}:{args.port}");
            }
        }
    }
}
