using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EchoClient
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 5000;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");

            var client = new TcpClient();

            client.Connect(localAddr, port);

            var strm = client.GetStream();
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            var request = "Hello from a client" + " date er: " + unixTimestamp;
            Console.WriteLine($"Request: {request}");
            var data = Encoding.UTF8.GetBytes(request);
            strm.Write(data, 0, data.Length);
            byte[] buffer = new byte[client.ReceiveBufferSize];
            var bytesRead = strm.Read(buffer, 0, buffer.Length);

            var response = Encoding.UTF8.GetString(buffer);

            Console.WriteLine($"Response: {response.Trim('\0')}");

            strm.Close();
            client.Dispose();

            Console.Read();


        }
    }
}

