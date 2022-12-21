using System;
using System.Net;
using System.Net.Sockets;

class Program
{
    static void Main(string[] args)
    {
        // Set up the server
        int port = 8080;
        TcpListener server = new TcpListener(IPAddress.Loopback, port);

        // Start listening for client requests
        server.Start();

        TcpClient client;
        // Enter the listening loop
        while ((client = server.AcceptTcpClient()) != null)
        {
            // Perform a blocking call to accept requests
            Console.WriteLine("Connected!");
            new ClientConnection(client.GetStream());
        }
    }
}