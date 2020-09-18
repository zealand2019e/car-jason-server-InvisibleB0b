using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using CarFactory;

namespace CarJsonClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string localAddress = null;
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    Console.WriteLine(ip.ToString());

                    localAddress = ip.ToString();

                }
            }

            Console.ReadKey();

            TcpClient client = new TcpClient("localhost", 10001);

            Stream ns = client.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;

            while (true)
            {


                string message = sr.ReadLine();

                if (message.ToLower().Contains("luk"))
                {
                    break;
                }
                else
                {
                    Car c = new Car() { Color = "Red", Model = "Tesla", RegNr = "1122456Q" };
                    Console.WriteLine(message);
                    string messag = c.ToString();
                    sw.WriteLine(messag);
                }


            }
        }
    }
}
