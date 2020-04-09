using System;
using System;
using System.IO.Ports;
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
        private readonly object balanceLock = new object();
        private readonly object balanceLock2 = new object();
        public void connect(string ip, int port)
        {
            try
            {
                client = new TcpClient(ip, port);
                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();
                stream = client.GetStream();

                // FINISHED CONNECTION}
            }
            catch (Exception)
            {
                Console.WriteLine("could not connect to server.");
                this.client = null;
            }
        }
        public bool checkIfClientIsNull()
        {
            return this.client == null;
        }
        public void disconnect()
        {
            try
            {
                // Close everything.
                stream.Close();
                client.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("could not disconnect from server.");
            }

        }

        public void write(string command)
        {

            if (client != null)
            {
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(command);


                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);
            }
            else
            {
                Console.WriteLine("not connected to tcp");
            }

        }

        public string read()
        {

            if (client != null)
            {

                Byte[] data = new Byte[256];
                // String to store the response ASCII representation.
                String responseData = String.Empty;
                // Read the first batch of the TcpServer response bytes
                try
                {
                    // Set the COM1 serial port to speed = 4800 baud, parity = odd, 
                    // data bits = 8, stop bits = 1.
                    SerialPort sp = new SerialPort("COM1",
                                    4800, Parity.Odd, 8, StopBits.One);
                    // Timeout after 10 seconds.
                    sp.ReadTimeout = 10000;
                    sp.Open();
                    // Read until either the default newline termination string 
                    // is detected or the read operation times out.
                    int bytes = stream.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    sp.Close();

                    //if the time to read the data from simulator took less then 10 sec - send data.
                    return responseData;
                }
                // Only catch timeout exceptions.
                catch (TimeoutException e)
                {
                    if (checkIfClientIsNull())
                    {
                        Console.WriteLine(e);
                        // diconnect
                        throw e;
                    } 
                    else
                    {
                        return "";
                    }
                }
                // other exceptions
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return "";

                }
            }
            else
            {
                Console.WriteLine("not connected to tcp.");
                return "";
            }
        }


    }
}