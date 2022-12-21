using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using System;
using System.Net;
using System.Text;
public class ClientConnection
{

    NetworkStream stream;

    public ClientConnection(NetworkStream stream)
    {
        this.stream = stream;
        Thread thread = new Thread(run);
        thread.Start();
    }

    public void run()
    {
        byte[] buffer = new byte[16];
        String data = "";
        int i;
        while (true)
        {
            i = stream.Read(buffer, 0, buffer.Length);
            // Translate data bytes to a ASCII string
            data += Encoding.ASCII.GetString(buffer, 0, i);
            Console.WriteLine("Read: " + Encoding.ASCII.GetString(buffer, 0, i));

            // Process the data sent by the client
            if (data.Contains(';'))
            {
                String command = data.Substring(0, data.IndexOf(';'));
                Console.WriteLine("Command detected: " + command);
                data = data.Substring(command.Length + 1);

                process(command);
            }
        }
    }

    private void process(String command)
    {
        Dictionary<string, string> response = new Dictionary<string, string>
        {
            {"PING","PONG;"},
            {"PONG","PING;"},
        };
        byte[] msg;
        try
        {
            msg = Encoding.ASCII.GetBytes(response[command]);

        }
        catch (KeyNotFoundException e)
        {
            msg = Encoding.ASCII.GetBytes(e.ToString() + ";");
        }
        // Send back a response
        stream.Write(msg, 0, msg.Length);
    }
}