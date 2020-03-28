using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model
{
    class MytelnetClient : ItelnetClient
    {
        TcpClient client;
        NetworkStream stream;

        public void connect(string ip, int port)
        {
          
             client = new TcpClient(ip, port);
            // Get a client stream for reading and writing.
            //  Stream stream = client.GetStream();
            stream = client.GetStream();
            // FINISHED CONNECTION
        }

        public void disconnect()
        {
            // Close everything.
            stream.Close();
            client.Close();
        }

        public void write(string command)
        {
            if (client != null)
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(command);


                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent: {0}", command);
            }
            else
            {
                Console.WriteLine("not connected to tcp")
            }

        }

        public string read()
        {
            if (client != null)
            {

                Byte[] data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                int bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

                Console.WriteLine("Received: {0}", responseData);

                return responseData;
            }
            else
            {
                Console.WriteLine("not connected to tcp");
                    return "";
            }
        }

       
    }
}