using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EchoServer
{
    class Program
    {
       
        static void Main(string[] args)
        {
            var category = new List<string>();
            category.Add("name: Beverages");
            category.Add("name: Condiments");
            category.Add("name: Confections");
           
            var cid = 1; 
            foreach (var element in category)
                Console.WriteLine("cid,"+ cid++ + "." + element);
                Console.WriteLine("");

            int port = 5000;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");

            var server = new TcpListener(localAddr, port);

            server.Start();

            Console.WriteLine(" Server is Started");

            while (true)
            {
                Console.WriteLine("Server is listening on " + server.LocalEndpoint);
                Console.WriteLine("Waiting for a new connection... ");
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("a client now is connected");
                NetworkStream data = client.GetStream();
                var thread = new Thread(HandleClient);    
                thread.Start(client);
    
            }
        }
            

        static void HandleClient(object clientObj)
        {
            var client = (TcpClient)clientObj;
            if (client == null) return;
            var strm = client.GetStream();
            byte[] buffer = new byte[client.ReceiveBufferSize];
            var bytesRead = strm.Read(buffer, 0, buffer.Length);
            var request = Encoding.UTF8.GetString(buffer);
            Console.WriteLine(request.Trim('\0'));
            var requestVar = request.Trim('\0');
            //Console.WriteLine(" requestVar til read" + requestVar);
            if (requestVar != "Hello from a client1")
            {
                //var request1 = "der er date med i request";
                Console.WriteLine(" der er date med i request");
            }
            else
            {
                
                Console.WriteLine(" der er ikke date med i request");
                //request = "der er ikke date med i request";
            }

            var response = Encoding.UTF8.GetBytes(request.ToUpper());
                strm.Write(response, 0, bytesRead);

            strm.Close();

            client.Dispose();
        }
        
    }

    /*public class Category
    {
        [JsonProperty("cid")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
    */
}


