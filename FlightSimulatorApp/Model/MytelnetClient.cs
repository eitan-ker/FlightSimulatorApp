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
            }
        }
        public void disconnect()
        {
            try
            {
                // Close everything.
                stream.Close();
                client.Close();
            } catch (Exception)
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

                    var watch = System.Diagnostics.Stopwatch.StartNew();
                
                    // Read the first batch of the TcpServer response bytes.
                    int bytes = stream.Read(data, 0, data.Length);
                
                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds;
               

                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

                    //if the time to read the data from simulator took less then 10 sec - send data.
                    if (elapsedMs <= 10000)
                {
                    return responseData;
                } 
                    else // if time read took more then 10 sec - drop information.
                {
                    Console.WriteLine("reading data from simulator took more then 10 seconds.");
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