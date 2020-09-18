using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CarFactory;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CarJsonServer
{
    class Server
    {

        public static void Start()
        {
            try
            {
                TcpListener server = null;

                IPAddress localAddress = null;
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        Console.WriteLine(ip.ToString());

                        localAddress = IPAddress.Parse(ip.ToString());

                    }
                }

                int port = 10001;


                server = new TcpListener(IPAddress.Loopback, port);

                server.Start();

                Console.WriteLine("Waiting for a connection........");




                while (true)
                {


                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    Task.Run(() =>
                    {
                        TcpClient tempSocket = client;
                        DoClient(tempSocket);
                    });


                }

                server.Stop();
                Console.WriteLine("server stopped");




            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static void DoClient(TcpClient socket)
        {
            Stream ns = socket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;

            sw.WriteLine("You are connected!!!");



            while (true)
            {


                string message = sr.ReadLine();
                Console.WriteLine(message);
                if (message.ToLower().Contains("luk"))
                {
                    break;
                }


                //Car c = new Car()
                //{
                //    Color = message.Split(" ")[1],
                //    Model = message.Split(" ")[0],
                //    RegNr = message.Split(" ")[2]
                //};
                Car c = JsonSerializer.Deserialize<Car>(message);

                Console.WriteLine(c);
            }
            sw.WriteLine("Luk");

            ns.Close();

            Console.WriteLine("net stream closed");

            socket.Close();
            Console.WriteLine("client closed");
        }
    }
}
