﻿using System;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace SeaWars
{
    class Server
    {
        TcpListener listener;
        public Server()
        {
            TcpListener listener = new TcpListener(System.Net.IPAddress.Any, 1302);
            this.listener = listener;
            this.listener.Start();
        }
        public void Start() { 
            while (true)
            {
                Console.WriteLine("Waiting for a connection.");
                TcpClient client = this.listener.AcceptTcpClient();
                Console.WriteLine("Client accepted.");
                NetworkStream stream = client.GetStream();
                StreamReader sr = new StreamReader(client.GetStream());
                StreamWriter sw = new StreamWriter(client.GetStream());
                try
                {
                    byte[] buffer = new byte[1024];
                    stream.Read(buffer, 0, buffer.Length);
                    int recv = 0;
                    foreach (byte b in buffer)
                    {
                        if (b != 0)
                        {
                            recv++;
                        }
                    }
                    string request = Encoding.UTF8.GetString(buffer, 0, recv);
                    Console.WriteLine("request received: " + request);
                    sw.WriteLine("You rock!");
                    sw.Flush();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Something went wrong.");
                    sw.WriteLine(e.ToString());
                }
            }
        }
    }
}