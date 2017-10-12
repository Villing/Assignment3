using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;


namespace EchoClient
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 5000;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");

            var client1 = new TcpClient();

            client1.Connect(localAddr, port);

            var strm = client1.GetStream();
            var request = "Hello from a client1";
            Console.WriteLine($"Request: {request}");
            var data = Encoding.UTF8.GetBytes(request);
            strm.Write(data, 0, data.Length);
            byte[] buffer = new byte[client1.ReceiveBufferSize];
            var bytesRead = strm.Read(buffer, 0, buffer.Length);

            var response = Encoding.UTF8.GetString(buffer);

            Console.WriteLine($"Response: {response.Trim('\0')}");

            strm.Close();
            client1.Dispose();

            Console.Read();


        }
    }
}

