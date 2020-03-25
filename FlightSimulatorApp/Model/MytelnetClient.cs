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
        bool Connectenabled;
        public bool ConnectEnabled
        {
            get
            {
                return Connectenabled;
            }
            set
            {
                Connectenabled = value;
            }
        }

        public void connect(string ip, int port)
        {
          
             client = new TcpClient(ip, port);

            string message = "get /position/latitude-deg\n";
                 // Translate the passed message into ASCII and store it as a Byte array.
                 Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

            // Get a client stream for reading and writing.
            //  Stream stream = client.GetStream();

            stream = client.GetStream();

            // FINISHED CONNECTION


            // Send the message to the connected TcpServer. 
            stream.Write(data, 0, data.Length);

            Console.WriteLine("Sent: {0}", message);

            // Receive the TcpServer.response.

            // Buffer to store the response bytes.
            data = new Byte[256];

            // String to store the response ASCII representation.
            String responseData = String.Empty;

            // Read the first batch of the TcpServer response bytes.
            int bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            Console.WriteLine("Received: {0}", responseData);

        }

        public void disconnect()
        {
            // Close everything.
            stream.Close();
            client.Close();
        }

        public string read()
        {

            throw new NotImplementedException();
        }

        public void write(string command)
        {

            throw new NotImplementedException();
        }
    }
}