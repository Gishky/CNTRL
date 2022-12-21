using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class Client
{
    public static void Main()
    {
        // Create a client and connect to the server
        TcpClient client;
        try
        {
            client = new TcpClient();
            client.Connect(IPAddress.Loopback, 8080);
        }
        catch (Exception e)
        {
            Console.WriteLine("Could not connect to Server.");
            Thread.Sleep(5000);
            return;
        }

        // Get the stream to send and receive data
        NetworkStream stream = client.GetStream();

        while (true)
        {
            String? input = Console.ReadLine();
            byte[] msg = Encoding.ASCII.GetBytes(input + ";");
            stream.Write(msg, 0, msg.Length);

            byte[] buffer = new byte[2];
            String data = "";
            int i;
            bool done = false;
            while (!done)
            {

                i = stream.Read(buffer, 0, buffer.Length);
                // Translate data bytes to a ASCII string
                data += Encoding.ASCII.GetString(buffer, 0, i);

                // Process the data sent by the server
                if (data.Contains(';'))
                {
                    String command = data.Substring(0, data.IndexOf(';'));
                    data = data.Substring(command.Length);

                    Console.WriteLine(command);
                    done = true;
                }
            }
        }
    }
}