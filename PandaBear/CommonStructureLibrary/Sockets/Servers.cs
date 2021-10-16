using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSL.Sockets
{
    public record ServerInfo(string target, string port, string? data = null, string? name = null);


    public class Servers
    {
        public static void TCPServer(ServerInfo args)
        {
            IPAddress ip = IPAddress.Parse(args.target);

            try
            {
                TcpListener Listener = new TcpListener(ip, Convert.ToInt32(args.port));

                Listener.Start();

                Console.WriteLine($"({args.target}) | Listening on {args.port}");
                Console.WriteLine($"Local endpoint is {Listener.LocalEndpoint}");

                Console.WriteLine("Waiting for connection...");

                Socket socket = Listener.AcceptSocket();
                Console.WriteLine($"New connection ({socket.RemoteEndPoint})");

                byte[] x = new byte[100];
                int y = socket.Receive(x);
                Console.WriteLine("Incoming data...");

                for (int m = 0; m < y; m++)
                {
                    Console.Write(Convert.ToChar(x[m]));
                }
                Console.WriteLine("");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to bind to port " + args.port + ". Maybe it is being used by another process?");
                Console.WriteLine("EXCEPTION: " + ex.Message);
            }
        }

        public static async Task sender(Socket socket, string output)
        {
            ASCIIEncoding toSend = new ASCIIEncoding();
            try
            {
                socket.Send(toSend.GetBytes(output));
            } catch(Exception ex)
            {
                Console.WriteLine("Failed to send back to client");
                socket.Send(toSend.GetBytes("Failed to send output to client"));
            }
        }

        public static async Task shellServer(ServerInfo args)
        {
            IPAddress ip = IPAddress.Parse(args.target);

            try
            {
                TcpListener Listener = new TcpListener(ip, Convert.ToInt32(args.port));

                Listener.Start();

                Console.WriteLine($"({args.target}) | Listening on {args.port}");
                Console.WriteLine($"Local endpoint is {Listener.LocalEndpoint}");

                Console.WriteLine("Waiting for connection...");

                Socket socket = Listener.AcceptSocket();
                Console.WriteLine($"New connection ({socket.RemoteEndPoint})");


                while (true)
                {
                    byte[] x = new byte[100];
                    int y = socket.Receive(x);
                    Console.WriteLine("Incoming data...");

                    List<String> output = new List<string>();

                    string endout = "";

                    for (int m = 0; m < y; m++)
                    {
                        Console.Write(Convert.ToChar(x[m]));
                        output.Add(Convert.ToString(x[m]));

                        endout = endout + Convert.ToChar(x[m]).ToString();
                    }

                    Console.WriteLine("");

                    try
                    {
                        string sendBack = process(endout);
                        //ThreadStart th = new ThreadStart(() => process(endout));
                        //Thread t = new Thread(th);
                        //t.Start();

                        await sender(socket, sendBack);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Server Error: " + ex.Message);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to bind to port " + args.port + ". Maybe it is being used by another process?");
                Console.WriteLine("EXCEPTION: " + ex.Message);
            }
        }

        public static string process(string args)
        {
            //Process.Start("CMD.exe", "/c" + args);

            try
            {
                ProcessStartInfo exec = new ProcessStartInfo();
                exec.FileName = $@"cmd.exe";
                exec.Arguments = $@"/c " + args;
                exec.UseShellExecute = false;
                exec.RedirectStandardOutput = true;
                using (Process process = Process.Start(exec))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string result = reader.ReadToEnd();
                        Console.Write(result);
                        return result;
                    }
                }
            } catch(Exception ex)
            {
                Console.WriteLine("Failed to execute command");
                return "Failed to execute command";
            }
        }
    }
}
